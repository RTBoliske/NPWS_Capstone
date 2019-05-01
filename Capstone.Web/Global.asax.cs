using Capstone.Web.DAL;
using Capstone.Web.DAL.Interfaces;
using Ninject;
using Ninject.Web.Common.WebHost;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace Capstone.Web
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            string connectionString = ConfigurationManager.ConnectionStrings["dbNPGeek"].ConnectionString;
            kernel.Bind<IParkDAL>().To<ParkSqlDAL>().WithConstructorArgument("connectionString", connectionString);

            return kernel;
        }
    }
}
