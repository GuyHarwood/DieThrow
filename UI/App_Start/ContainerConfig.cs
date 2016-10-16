using System.Reflection;
using System.Web.Mvc;
using GuyHarwood.DieThrow.Domain;
using GuyHarwood.DieThrow.Domain.Core;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace UI.App_Start
{
    public class ContainerConfig
    {
        public static void RegisterBindings()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            DomainBindings.Configure(container);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }

    public class DomainBindings
    {
        public static void Configure(Container container)
        {
            container.Register<IHandler<ThrowDice, HighestStreak>>(() =>
                new CommandValidator<ThrowDice, HighestStreak>(
                    new ThrowDiceHandler(new Dice(), new WinningStreakIdentifier()), container));
        }
    }
}