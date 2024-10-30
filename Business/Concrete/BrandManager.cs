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
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Add(Brand brand)
        {
            var isSuccess = _brandDal.Add(brand);
            if (isSuccess)
            {
                return new Result(true, Messages.BrandAdded);
            }
            return new Result(false, Messages.Failed);
        }

        public IResult Delete(Brand brand)
        {
            var isSuccess = _brandDal.Delete(brand);
            if (isSuccess)
            {
                return new Result(true, Messages.BrandDeleted);
            }
            return new Result(false, Messages.Failed);
        }
        public IResult Update(Brand brand)
        {
            var isSuccess = _brandDal.Update(brand);
            if (isSuccess)
            {
                return new Result(true, Messages.BrandUpdated);
            }
            return new Result(false, Messages.Failed);

        }

        public IDataResult<List<Brand>> GetAll()
        {
            var brands = _brandDal.GetAll();
            if(brands == null)
            {
                return new ErrorDataResult<List<Brand>>(Messages.Failed);
            }
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.BrandsListed);
        }

        public IDataResult<Brand> GetById(int id)
        {
            var brand = _brandDal.Get(c => c.Id == id);
            if (brand == null)
            {
                return new ErrorDataResult<Brand>(Messages.Failed);
            }
            return new SuccessDataResult<Brand>(brand, Messages.BrandFound);
        }
    }
}
