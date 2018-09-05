using IntegrationContracts;
using MassTransit;
using OCREndpoint.Handlers;
using System;
using System.Reflection;

namespace OCREndpoint
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, Assembly.GetExecutingAssembly().GetName().Name + "_queue", endpoint =>
                  {
                      endpoint.Consumer<DocumentConsumer>();
                  });
            });

            bus.Start();

        }
    }
}
