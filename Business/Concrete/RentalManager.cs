using _Core.Utilities.Business;
using _Core.Utilities.Results;
using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICarService _carService;

        public RentalManager(IRentalDal rentalDal, ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
        }

        public IResult Add(Rental rental)
        {
            List<IResult> errors = BusinessRules.Run(DoesCarExist(rental), IsRentalAvailable(rental));
            if (errors.Count > 0)
            {
                return errors[0];
            }
            
            var isSuccess = _rentalDal.Add(rental);
            if (isSuccess)
            {
                return new SuccessResult(Messages.RentalAdded);
            }
            return new ErrorResult(Messages.Failed);
        }

        public IResult Delete(Rental rental)
        {
            var isSuccess = _rentalDal.Delete(rental);
            if (isSuccess)
            {
                return new SuccessResult(Messages.RentalDeleted);
            }
            return new ErrorResult(Messages.Failed);
        }

        public IResult Update(Rental rental)
        {
            var isSuccess = _rentalDal.Update(rental);
            if (isSuccess)
            {
                return new SuccessResult(Messages.RentalUpdated);
            }
            return new ErrorResult(Messages.Failed);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalsListed);
        }

        public IDataResult<Rental> GetByCarId(int carId)
        {
            var car = _rentalDal.GetAll(r => r.CarId == carId).FirstOrDefault();
            if (car == null)
            {
                return new ErrorDataResult<Rental>(Messages.NoRentalsByCarId);
            }
            return new SuccessDataResult<Rental>(car, Messages.RentalsByCarId);
        }

        public IDataResult<Rental> GetById(int id)
        {
            var rental = _rentalDal.Get(c => c.Id == id);
            if (rental == null)
            {
                return new ErrorDataResult<Rental>(Messages.NoRentals);
            }
            return new SuccessDataResult<Rental>(rental, Messages.RentalFound);
        }

        private IResult IsRentalAvailable(Rental rental)
        {
            var rentalInfo = _rentalDal.GetAll(r => r.CarId == rental.CarId).OrderByDescending(r => r.Id).FirstOrDefault();
            if (rentalInfo != null && rentalInfo.ReturnDate == null)
            {
                return new ErrorResult(Messages.RentalAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult DoesCarExist(Rental rental)
        {
            var carInfo = _carService.GetById(rental.CarId);
            if (carInfo == null)
            {
                return new ErrorResult(Messages.CarNotFound);
            }
            return new SuccessResult();
        }

        public IDataResult<List<RentalDetailDTO>> GetRentalDetails()
        {
            var details = _rentalDal.GetRentalDetails();
            if (details == null)
            {
                return new ErrorDataResult<List<RentalDetailDTO>>(Messages.Failed);
            }
            return new SuccessDataResult<List<RentalDetailDTO>>(details, Messages.CarDetailsListed);
        }
    }
}
