using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using URLShortener.DAL.Repository;
using URLShortener.Manager.Abstract;
using URLShortener.Manager.Concrete;

namespace URLShortener.UI.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<UrlShortenerManager>().As<IUrlShortenerManager>();
            builder.RegisterType<UrlShortenerRepository>().As<IUrlShortenerRepository>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}