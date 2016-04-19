using Demo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Dto;

namespace Demo.Web.Controllers
{
    public class TestController : Controller
    {
        private ICustomerService customerSvc;

        public TestController(ICustomerService customerSvc)
        {
            this.customerSvc = customerSvc;
        }

        //
        // GET: /Test/
        public ActionResult Index()
        {
            var result=customerSvc.GetEnableCustomer(1, 10);

            var vdata = result.Data.ProjectedAsCollection<CustomerDto>();

            return View(vdata);
        }
	}
}