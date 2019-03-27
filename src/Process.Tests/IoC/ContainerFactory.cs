namespace Process.Tests.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Aspects.Audit;
    using Aspects.Notifications;
    using Doubles;
    using FluentValidation;
    using log4net.Core;
    using MediatR;
    using MediatR.Pipeline;
    using Pipeline;
    using Ports;
    using SimpleInjector;
    using SimpleInjector.Lifestyles;

    public static class ContainerFactory
    {
        static readonly Lazy<Container> Value;

        public static Container Instance => Value.Value;

        static ContainerFactory()
        {
            Value = new Lazy<Container>(BuildContainer);
        }

        static Container BuildContainer()
        {
            Container container = new Container();
            container.Options.DefaultScopedLifestyle =
                new AsyncScopedLifestyle();

            // mediator
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(
                () => new ServiceFactory(container.GetInstance),
                Lifestyle.Singleton);

            // pipeline
            container.Collection.Register(typeof(IPipelineBehavior<,>), new []
            {
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>),
                typeof(PipelineBehavior<,>)
            });

            Assembly[] assemblies = { typeof(IProcessLivesHere).Assembly };
            
            // pre-processors
            container.Collection.Register(typeof(IRequestPreProcessor<>), assemblies);

            // handlers
            container.Register(typeof(IRequestHandler<,>), assemblies);

            // post-processors
            container.Collection.Register(
                typeof(IRequestPostProcessor<,>),
                new[] {
                    typeof(NotificationsSender<,>),
                    typeof(RequestAuditor<,>) } );

            // validators
            container.Collection.Register(typeof(IValidator<>), assemblies);

            // notification handlers
            IEnumerable<Type> notificationHandlerTypes = container.GetTypesToRegister(
                typeof(INotificationHandler<>),
                new[] { Assembly.GetExecutingAssembly() },
                new TypesToRegisterOptions
                {
                    IncludeGenericTypeDefinitions = true
                });

            container.Collection.Register(typeof(INotificationHandler<>), notificationHandlerTypes);

            container.Register<InMemoryNotificationStore>(Lifestyle.Scoped);

            // document storage
            container.Register<IDocumentStore, InMemoryDocumentStore>(Lifestyle.Scoped);

            // logging
            container.Register<ILogger, NopLogger>();

            container.Verify();

            return container;
        }
    }
}
