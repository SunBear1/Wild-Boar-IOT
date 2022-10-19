
using System;
using System.Threading.Tasks;
using MassTransit;
using Sklep;

namespace KlientB
{
    static class DaneKlienta
    {
        public const string Login = "KlientB";
    }
    class HandlerKlient : IConsumer<IPytanieoPotwierdzenie>, IConsumer<IAkceptacjaZamowienia>, IConsumer<IOdrzucenieZamowienia>
    {
        public Task Consume(ConsumeContext<IPytanieoPotwierdzenie> context)
        {
            if (context.Message.Login == DaneKlienta.Login)
            {
                Console.WriteLine("\n-----------------------------------");
                Console.WriteLine($"Czy potwierdzasz zamowienie {context.Message.CorrelationId}: ");
                bool OPTION;
                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    OPTION = true;
                }
                else
                {
                    OPTION = false;
                }

                Console.WriteLine();
                if (OPTION)
                {
                    return context.RespondAsync(new Potwierdzenie() { CorrelationId = context.Message.CorrelationId });
                }
                else
                {
                    return context.RespondAsync(new BrakPotwierdzenia() { CorrelationId = context.Message.CorrelationId });
                }
            }
            else
            {
                return Console.Out.WriteLineAsync();
            }
        }

        public Task Consume(ConsumeContext<IAkceptacjaZamowienia> context)
        {
            if (context.Message.Login == DaneKlienta.Login)
            {
                return Console.Out.WriteLineAsync($"Zamowienie zostalo zaakceptowane !!!\nId zamowienia :" +
                    $" {context.Message.CorrelationId}\nzamowiona ilosc :{context.Message.Ilosc} \n-----------------------------------");
            }
            else
            {
                return Console.Out.WriteLineAsync();
            }
        }

        public Task Consume(ConsumeContext<IOdrzucenieZamowienia> context)
        {
            if (context.Message.Login == DaneKlienta.Login)
            {
                return Console.Out.WriteLineAsync($"Zamowienie zostalo odrzucone !!!\nId zamowienia :" +
    $" {context.Message.CorrelationId}\nzamowiona ilosc :{context.Message.Ilosc}\n-----------------------------------");
            }
            else
            {
                return Console.Out.WriteLineAsync();
            }
        }
    }

    class KlientA
    {
        static void Main(string[] args)
        {
            var klient = new HandlerKlient();
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("amqps://orivhjpp:UuQmibEpV9-1617BlCm6-3O3tItX9Oj8@rattlesnake.rmq.cloudamqp.com/orivhjpp"),
                h => { h.Username("orivhjpp"); h.Password("UuQmibEpV9-1617BlCm6-3O3tItX9Oj8"); });
                sbc.ReceiveEndpoint("klientB", ep =>
                {
                    ep.Instance(klient);
                });
            });

            bus.Start();

            Console.WriteLine(DaneKlienta.Login);
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.K)
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Podaj Ilosc produktow do kupienia: ");

                    int ilosc = 0;
                    try
                    {
                        ilosc = Convert.ToInt32(Console.ReadLine());
                        bus.Publish(new StartZamowienia() { Login = DaneKlienta.Login, Ilosc = ilosc });
                        Console.WriteLine();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Liczba produktow nie zostala podana!\n Transakcja zostala anulowana !!!");
                        Console.WriteLine("-----------------------------------");
                    }
                }
            }
            //infinite loop nie zatrzyma bus :(
            bus.Stop();
        }
    }
}

