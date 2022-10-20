using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Saga;
using System.Threading;

namespace Sklep
{
    public class RejestracjaZamowienie : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public string Login { get; set; }
        public int Ilosc { get; set; }
        public Guid? TimeoutId { get; set; }
    }

    public class RejestracjaSklep : MassTransitStateMachine<RejestracjaZamowienie>
    {
        // stany sagi poza początkowym
        public State Niepotwierdzone { get; private set; }
        public State PotwierdzoneKlient { get; private set; }
        public State PotwierdzoneMagazyn { get; private set; }

        // zdarzenia
        public Event<StartZamowienia> StartZamowienia { get; private set; }
        public Event<Potwierdzenie> Potwierdzenie { get; private set; }
        public Event<BrakPotwierdzenia> BrakPotwierdzenia { get; private set; }
        public Event<OdpowiedzWolne> OdpowiedzWolne { get; private set; }
        public Event<OdpowiedzWolneNegatywna> OdpowiedzWolneNegatywna { get; set; }
        public Event<Timeout> TimeoutEvent { get; private set; }
        public Schedule<RejestracjaZamowienie, Timeout> TO { get; set; }

        public RejestracjaSklep()
        {
            InstanceState(x => x.CurrentState);

            Event(() => StartZamowienia,
                x => x.CorrelateBy(
                        s => s.Login,
                        ctx => ctx.Message.Login
                    ).SelectId(context => Guid.NewGuid())
                );

            Schedule(() => TO,
                    x => x.TimeoutId,
                    x => { x.Delay = TimeSpan.FromSeconds(10); }
                );

            Initially(

                When(StartZamowienia)
                .Schedule(TO, ctx => new Timeout() { CorrelationId = ctx.Saga.CorrelationId })
                .Then(ctx => ctx.Saga.Login = ctx.Message.Login)
                .Then(ctx => ctx.Saga.Ilosc = ctx.Message.Ilosc)
                .ThenAsync(context => { return Console.Out.WriteLineAsync($"Klient {context.Message.Login}\nZamowil produktow w ilosci: {context.Message.Ilosc}"); })
                .Respond(ctx => { return new PytanieoPotwierdzenie() { CorrelationId = ctx.Saga.CorrelationId, Login = ctx.Saga.Login }; })
                .Respond(ctx => { return new PytanieoWolne() { CorrelationId = ctx.Saga.CorrelationId, Ilosc = ctx.Saga.Ilosc }; })
                .TransitionTo(Niepotwierdzone)
                );

            During(Niepotwierdzone,

                When(TimeoutEvent)
                .ThenAsync(context => { return Console.Out.WriteLineAsync($"TIMEOUT -> klient : {context.Saga.Login} Przekroczyl limit czasu zamowienia o id: {context.Message.CorrelationId}"); })
                .Respond(ctx => { return new OdrzucenieZamowienia() { CorrelationId = ctx.Saga.CorrelationId, Login = ctx.Saga.Login, Ilosc = ctx.Saga.Ilosc }; })
                .Finalize(),

                When(Potwierdzenie)
                .ThenAsync(context => { return Console.Out.WriteLineAsync($"klient : {context.Saga.Login} potwierdzil zamowienie o id: {context.Message.CorrelationId}"); })
                .Unschedule(TO)
                .TransitionTo(PotwierdzoneKlient),

                When(BrakPotwierdzenia)
                .ThenAsync(context => { return Console.Out.WriteLineAsync($"klient : {context.Saga.Login} nie potwierdzil zamowienie o id: {context.Message.CorrelationId}"); })
                .Respond(ctx => { return new OdrzucenieZamowienia() { CorrelationId = ctx.Saga.CorrelationId, Login = ctx.Saga.Login, Ilosc = ctx.Saga.Ilosc }; })
                .Finalize(),

                When(OdpowiedzWolne)
                .ThenAsync(context => { return Console.Out.WriteLineAsync($"Magazyn jest w stanie zrealizowac zamowienie o id: {context.Message.CorrelationId}"); })
                .TransitionTo(PotwierdzoneMagazyn),

                When(OdpowiedzWolneNegatywna)
                .ThenAsync(context => { return Console.Out.WriteLineAsync($"Magazyn nie jest w stanie zrealizowac zamowienia o id: {context.Message.CorrelationId}"); })
                .Respond(ctx => { return new OdrzucenieZamowienia() { CorrelationId = ctx.Saga.CorrelationId, Login = ctx.Saga.Login, Ilosc = ctx.Saga.Ilosc }; })
                .Finalize()
                );

            During(PotwierdzoneKlient,

                When(OdpowiedzWolne)
                .ThenAsync(context => { return Console.Out.WriteLineAsync($"Magazyn jest w stanie zrealizowac zamowienie o id: {context.Message.CorrelationId}"); })
                .Respond(ctx => { return new AkceptacjaZamowienia() { CorrelationId = ctx.Saga.CorrelationId, Login = ctx.Saga.Login, Ilosc = ctx.Saga.Ilosc }; })
                .Finalize(),

                When(OdpowiedzWolneNegatywna)
                .ThenAsync(context => { return Console.Out.WriteLineAsync($"Magazyn nie jest w stanie zrealizowac zamowienia o id: {context.Message.CorrelationId}"); })
                .Respond(ctx => { return new OdrzucenieZamowienia() { CorrelationId = ctx.Saga.CorrelationId, Login = ctx.Saga.Login, Ilosc = ctx.Saga.Ilosc }; })
                .Finalize()
                );

            During(PotwierdzoneMagazyn,

                When(TimeoutEvent)
                .ThenAsync(context => { return Console.Out.WriteLineAsync($"TIMEOUT -> klient: {context.Saga.Login} Przekroczyl limit czasu zamowienia o id: {context.Message.CorrelationId}"); })
                .Respond(ctx => { return new OdrzucenieZamowienia() { CorrelationId = ctx.Saga.CorrelationId, Login = ctx.Saga.Login, Ilosc = ctx.Saga.Ilosc }; })
                .Finalize(),

                When(Potwierdzenie)
                .ThenAsync(context => { return Console.Out.WriteLineAsync($"klient: {context.Saga.Login} potwierdzil zamowienie o id: {context.Message.CorrelationId}"); })
                .Respond(ctx => { return new AkceptacjaZamowienia() { CorrelationId = ctx.Saga.CorrelationId, Login = ctx.Saga.Login, Ilosc = ctx.Saga.Ilosc }; })
                .Unschedule(TO)
                .Finalize(),

                When(BrakPotwierdzenia)
                .ThenAsync(context => { return Console.Out.WriteLineAsync($"klient: {context.Saga.Login} nie potwierdzil zamowienia o id: {context.Message.CorrelationId}"); })
                .Respond(ctx => { return new OdrzucenieZamowienia() { CorrelationId = ctx.Saga.CorrelationId, Login = ctx.Saga.Login, Ilosc = ctx.Saga.Ilosc }; })
                .Finalize()
                );

            SetCompletedWhenFinalized();
        }

    }

    class Sklep
    {
        static void Main(string[] args)
        {
            var repo = new InMemorySagaRepository<RejestracjaZamowienie>();
            var sklep = new RejestracjaSklep();
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("amqps://orivhjpp:UuQmibEpV9-1617BlCm6-3O3tItX9Oj8@rattlesnake.rmq.cloudamqp.com/orivhjpp"),
                h => { h.Username("orivhjpp"); h.Password("UuQmibEpV9-1617BlCm6-3O3tItX9Oj8"); });
                sbc.ReceiveEndpoint("sklep", ep => {
                    ep.StateMachineSaga(sklep, repo);
                });
                sbc.UseInMemoryScheduler();
            });

            bus.Start();
            Console.WriteLine("Otworzono sklep !!!");
            Console.ReadKey();
            bus.Stop();
            Console.WriteLine("Zamknieto sklep !!!");
            Console.ReadKey();
        }
    }
}

