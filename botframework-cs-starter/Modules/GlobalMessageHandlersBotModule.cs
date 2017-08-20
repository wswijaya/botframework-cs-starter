namespace StarterBot.Modules
{
    using Autofac;
    using StarterBot.Scorables;
    using Microsoft.Bot.Builder.Dialogs.Internals;
    using Microsoft.Bot.Builder.Scorables;
    using Microsoft.Bot.Connector;
    using System.Text.RegularExpressions;

    public class GlobalMessageHandlersBotModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c => new CancelScorable(c.Resolve<IDialogTask>()))
                .As<IScorable<IActivity, double>>()
                .InstancePerLifetimeScope();

            //var scorable = Actions
            //.Bind(async (IBotToUser botToUser, IMessageActivity message) =>
            //{
            //    await botToUser.PostAsync("polo");
            //})
            //.When(new Regex("marco"))
            //.Normalize();
            //builder.RegisterInstance(scorable).AsImplementedInterfaces().SingleInstance();
        }
    }
}