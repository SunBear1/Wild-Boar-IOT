using System;
using System.Threading.Tasks;
using MassTransit;
using Sklep;


namespace Magazyn
{
    class Magazyn : IConsumer<IPytanieoWolne>, IConsumer<IAkceptacjaZamowienia>, IConsumer<IOdrzucenieZamowienia>
    {
        public int WOLNE { get; set; } = 0;
        public int ZAREZERWOWANE { get; set; } = 0;
        public Task Consume(ConsumeContext<IPytanieoWolne> context)
        {
            if (WOLNE >= context.Message.Ilosc)
            {
                WOLNE -= context.Message.Ilosc;
                ZAREZERWOWANE += context.Message.Ilosc;
                Console.WriteLine("\n-----------------------------------");
                Console.Out.WriteLineAsync($"Magazyn jest w stanie zrealizowac zamowienie o id: {context.Message.CorrelationId}\nPozadana ilosc produktow: {context.Message.Ilosc}");
                return context.RespondAsync(new OdpowiedzWolne() { CorrelationId = context.Message.CorrelationId });
            }
            else
            {
                ZAREZERWOWANE += context.Message.Ilosc;
                WOLNE -= context.Message.Ilosc;
                Console.WriteLine("\n-----------------------------------");
                Console.Out.WriteLineAsync($"Magazyn nie jest w stanie zrealizowac zamowienie o id: {context.Message.CorrelationId}\nPozadana ilosc produktow: {context.Message.Ilosc}");
                return context.RespondAsync(new OdpowiedzWolneNegatywna() { CorrelationId = context.Message.CorrelationId });
            }
        }

        public Task Consume(ConsumeContext<IAkceptacjaZamowienia> context)
        {
            ZAREZERWOWANE -= context.Message.Ilosc;
            return Console.Out.WriteLineAsync($"Zamowienie zostalo zrealizowane: {context.Message.CorrelationId}\nIlosc wysylanych " +
                $"produktow: {context.Message.Ilosc}\nProdukty zostaly wyslane do klienta-----------------------------------" );
        }

        public Task Consume(ConsumeContext<IOdrzucenieZamowienia> context)
        {
            ZAREZERWOWANE -= context.Message.Ilosc;
            WOLNE += context.Message.Ilosc;
            return Console.Out.WriteLineAsync($"Zamowienie nie zostalo zrealizowane: {context.Message.CorrelationId}\nIlosc produktow wracajacych" +
                $" do sklepu: {context.Message.Ilosc}\nProdukty zarezerwowany wrocily do sprzedazy -----------------------------------");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var magazyn = new Magazyn();
            var bus = Bus.Factory.CreateUsingRabbitMq(
            sbc =>
            {
                sbc.Host(new Uri("amqps://orivhjpp:UuQmibEpV9-1617BlCm6-3O3tItX9Oj8@rattlesnake.rmq.cloudamqp.com/orivhjpp"),
                 h => { h.Username("orivhjpp"); h.Password("UuQmibEpV9-1617BlCm6-3O3tItX9Oj8"); });
                sbc.ReceiveEndpoint("magazyn",
                    ep => ep.Instance(magazyn));
            });
            Console.WriteLine("Magazyn");
            bus.Start();

            int liczbaDostarczonychProduktow = 0;

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.D)
                {
                    Console.WriteLine("\nIle produktow zostalo dostarczonych do magazynu : ");
                    try
                    {
                        liczbaDostarczonychProduktow = Convert.ToInt32(Console.ReadLine());
                        magazyn.WOLNE += liczbaDostarczonychProduktow;
                        Console.WriteLine($"\nRozklad produktow w Magazynie \nDODANE: {liczbaDostarczonychProduktow} \nWOLNE: {magazyn.WOLNE} \nZAREZERWOWANE: {magazyn.ZAREZERWOWANE}");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Nie podano ile produktow zostalo dostarczonych do magazynu. Anulowanie dostawy !!!");
                        Console.WriteLine("-----------------------------------");
                    }
                }
            }
            //infinite loop nie zatrzyma bus :(
            bus.Stop();
        }
    }
}

