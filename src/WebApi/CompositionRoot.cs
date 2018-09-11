using Autofac;
using MassTransit;
using System;
using System.Reflection;

namespace WebApi
{
    public static class CompositionRoot
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<OcrController>().InstancePerRequest();

            builder.Register(context =>
            {
                var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                });

                return busControl;
            })
               .SingleInstance()
               .As<IBusControl>()
               .As<IBus>();

            return builder.Build();
        }
    }
}