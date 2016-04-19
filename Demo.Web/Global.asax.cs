using Autofac;
using Autofac.Integration.Mvc;
using Data.Seedwork;
using Demo.Data;
using Demo.Domain.Customers;
using Demo.Dto;
using Demo.Service;
using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Demo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var timer = System.Diagnostics.Stopwatch.StartNew();
            SetDependencyResolver();
            timer.Stop();
            System.Diagnostics.Debug.WriteLine(string.Format("Execute SetDependencyResolver() elapsed：{0} ms !", timer.ElapsedMilliseconds));

            timer.Restart();
            //初始化数据库
            new YmtCS_DbInitializer().InitializeDatabase(new MainUnitOfWork());

              timer.Stop();
            System.Diagnostics.Debug.WriteLine(string.Format("InitializeDatabase() elapsed：{0} ms !", timer.ElapsedMilliseconds));
        }


        /// <summary>
        /// 依赖注入处理
        /// </summary>
        private void SetDependencyResolver()
        {
            var containerBuilder = new ContainerBuilder();

            var assembly1 = System.Reflection.Assembly.GetAssembly(typeof(IRepository<>)); //Domain.Seedwork
            var assembly2 = System.Reflection.Assembly.GetAssembly(typeof(IQueryableUnitOfWork)); //Data.Seedwork
            var assembly3 = System.Reflection.Assembly.GetAssembly(typeof(IService));   //Service
            var assembly4 = System.Reflection.Assembly.GetAssembly(typeof(ICustomerRepository)); //Domain
            var assembly5 = System.Reflection.Assembly.GetAssembly(typeof(CustomerRepository)); //Data


            containerBuilder.RegisterAssemblyTypes(new System.Reflection.Assembly[] { assembly1, assembly2, assembly3, assembly4, assembly5 }).AsImplementedInterfaces();

            containerBuilder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = containerBuilder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            var customerRepo = container.Resolve<ICustomerRepository>();
            var customerSvc = container.Resolve<ICustomerService>();

            var tt = string.Empty;
        } 
    }
}
