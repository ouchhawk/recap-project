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
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICarDal _carDal;

        public RentalManager(IRentalDal rentalDal, ICarDal carDal)
        {
            _rentalDal = rentalDal;
            _carDal = carDal;
        }

        public IResult Add(Rental rental)
        {
            var carInfo = _carDal.Get(c => c.Id == rental.CarId);
            if (carInfo == null)
            {
                return new ErrorResult(Messages.CarNotFound);
            }

            var rentalInfo = _rentalDal.GetAll(r => r.CarId == rental.CarId).OrderByDescending(r => r.Id).FirstOrDefault();
            if ( rentalInfo != null && rentalInfo.ReturnDate == null)
            {
                return new ErrorResult(Messages.RentalAlreadyExists);
            }
            else
            {
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.RentalAdded);

            }
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
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

    }
}
