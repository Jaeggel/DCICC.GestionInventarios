using DCICC.GestionInventarios.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DCICC.GestionInventarios
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new MenuActionFilter(), 0);
            GlobalFilters.Filters.Add(new UsuarioActionFilter(), 0);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.AppDomain.CurrentDomain.RelativeSearchPath ?? "") + "\\DCICC.WebApp.Logs-" + DateTime.Now.Date.ToString("ddMMyyyy") + ".txt";
            //"C:\\Test\\DCICC.WebApp.Logs" + DateTime.Now.Date.ToString("ddMMyyyy") + ".txt";
            log4net.GlobalContext.Properties["LogFileName"] = "C:\\DCICC.WebApp.Logs\\DCICC.WebApp.Logs." + DateTime.Now.Date.ToString("ddMMyyyy") + ".txt";
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
