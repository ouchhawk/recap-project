using _Core.Aspects.Autofac.Validation;
using _Core.CrossCuttingConcerns.Validation;
using _Core.CrossCuttingConcerns.Validation.FluentValidation;
using _Core.Utilities.Results;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System.Dynamic;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperation("admin")]
        public IResult Add(Car car)
        {
            var isSuccess = _carDal.Add(car);
            if (isSuccess)
            {
                return new Result(true, Messages.CarAdded);
            }
            return new Result(false, Messages.Failed);
        }

        public IResult Delete(Car car)
        {
            var isSuccess = _carDal.Delete(car);
            if (isSuccess)
            {
                return new Result(true, Messages.CarDeleted);
            }
            return new Result(false, Messages.Failed);
        }
        public IResult Update(Car car)
        {
            var isSuccess = _carDal.Update(car);
            if (isSuccess)
            {
                return new Result(true, Messages.CarUpdated);
            }
            return new Result(false, Messages.Failed);
        }

        public IDataResult<List<Car>> GetAll()
        {
            var cars = _carDal.GetAll();
            if (cars == null)
            {
                return new ErrorDataResult<List<Car>>(Messages.Failed);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        public IDataResult<Car> GetById(int id)
        {
            var car = _carDal.Get(c => c.Id == id);
            if (car == null)
            {
                return new ErrorDataResult<Car>(Messages.Failed);
            }
            return new SuccessDataResult<Car>(car, Messages.CarFound);
        }

        public IDataResult<List<CarDetailDTO>> GetCarDetails()
        {
            var details = _carDal.GetCarDetails();
            if (details == null)
            {
                return new ErrorDataResult<List<CarDetailDTO>>(Messages.Failed);
            }
            return new SuccessDataResult<List<CarDetailDTO>>(details, Messages.CarDetailsListed);
        }

    }
}
