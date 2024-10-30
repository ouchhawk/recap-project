using _Core.Utilities.Results;
using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer customer)
        {
            var isSuccess = _customerDal.Add(customer);
            if (isSuccess)
            {
                return new Result(true, Messages.CustomerAdded);
            }
            return new Result(false, Messages.Failed);
        }

        public IResult Delete(Customer customer)
        {
            var isSuccess = _customerDal.Delete(customer);
            if (isSuccess)
            {
                return new Result(true, Messages.CustomerDeleted);
            }
            return new Result(false, Messages.Failed);
        }
        public IResult Update(Customer customer)
        {
            var isSuccess = _customerDal.Update(customer);
            if (isSuccess)
            {
                return new Result(true, Messages.CustomerUpdated);
            }
            return new Result(false, Messages.Failed);

        }

        public IDataResult<List<Customer>> GetAll()
        {
            var customers = _customerDal.GetAll();
            if (customers == null)
            {
                return new ErrorDataResult<List<Customer>>(Messages.Failed);
            }
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.CustomersListed);
        }

        public IDataResult<Customer> GetById(int id)
        {
            var customer = _customerDal.Get(c => c.Id == id);
            if (customer == null)
            {
                return new ErrorDataResult<Customer>(Messages.Failed);
            }
            return new SuccessDataResult<Customer>(customer, Messages.CustomerFound);
        }

    }
}
