namespace StarterBot.Modules
{
    using System.Web.Http;
    using System.Configuration;
    using System.Text.RegularExpressions;
    using Microsoft.Bot.Builder.Dialogs;
    using Autofac;
    using Microsoft.Bot.Builder.Dialogs.Internals;
    using Microsoft.Bot.Builder.Internals.Fibers;
    using Microsoft.Bot.Builder.Scorables;
    using Microsoft.Bot.Connector;
    using StarterBot.Scorables;
    using StarterBot.Middleware;

    public class GlobalMessageHandlersBotModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<DebugActivityLogger>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterModule(new ReflectionSurrogateModule());
            builder
                .Register(c => new CancelScorable(c.Resolve<IDialogTask>()))
                .As<IScorable<IActivity, double>>()
                .InstancePerLifetimeScope();

            var scorable = Actions
                .Bind(async (IBotToUser botToUser, IMessageActivity message) =>
                {
                    await botToUser.PostAsync("hello back from scorable");
                })
                .When(new Regex(@"^(hi|hello)"))
                .Normalize();

            builder.RegisterInstance(scorable).AsImplementedInterfaces().SingleInstance();
        }
    }
}