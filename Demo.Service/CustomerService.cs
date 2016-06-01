using Demo.Domain;
using Demo.Domain.Customers; 
using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository cusomterRepo;
        public CustomerService(ICustomerRepository cusomterRepo)
        {
            this.cusomterRepo = cusomterRepo;
        }

        public void Dispose()
        {
            cusomterRepo.Dispose();
        }

        public PagedResult<Customer> GetEnableCustomer(int pageIndex, int pageSize)
        {
            return cusomterRepo.GetEnabled(pageIndex, pageSize);
        }

        public ResultInfo DiableCustomer(int id)
        {
            ResultInfo result = new ResultInfo();
            var model = cusomterRepo.Get(id);
            if (model!=null)
            {
                model.IsDel = false;
            }
            return result;
        }


        public Customer GetCustomer(int id)
        {
            return cusomterRepo.Get<int>(id);
        }
    }
}
