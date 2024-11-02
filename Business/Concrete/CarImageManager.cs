using _Core.Utilities.Business;
using _Core.Utilities.Results;
using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        ICarService _carService;
        private string folderPath = Path.Combine("..", "Assets", "Images", "Uploads");
        private string placeholderPath = Path.Combine("..", "Assets", "Images", "Placeholders", "car-image-placeholder.jpg");


        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }

        public IResult Add(CarImage carImage, Microsoft.AspNetCore.Http.IFormFile imageFile)
        {
            List<IResult> errors = BusinessRules.Run(IsImageLimitReached(carImage.CarId));
            if (errors.Count > 0)
            {
                return errors[0];
            }

            carImage.ImagePath = CreateFile(imageFile, folderPath).Data;
            carImage.Date = DateTime.Now;

            var isSuccess = _carImageDal.Add(carImage);
            if (isSuccess)
            {
                return new Result(true, Messages.CarImageAdded);
            }
            return new Result(false, Messages.Failed);
        }

        public IResult Delete(CarImage carImage)
        {
            bool isSuccess = DeleteFile(carImage.ImagePath).Success;
            if (isSuccess) 
            { 
                return new Result(false, Messages.FileDeletionFailed);
            }

            isSuccess = _carImageDal.Delete(carImage);
            if (!isSuccess)
            {
                return new Result(false, Messages.ImageDataDeletionFailed);
            }
            return new Result(true, Messages.CarImageDeleted);
        }

        public IResult DeleteByFilename(string fileName)
        {
            var filePath = Path.Combine(folderPath, fileName);
            CarImage carImage = _carImageDal.GetAll(c => c.ImagePath == filePath).FirstOrDefault();
            return Delete(carImage);
        }

        public IResult Update(Microsoft.AspNetCore.Http.IFormFile imageFile, CarImage carImage)
        {
            bool isSuccess = DeleteFile(carImage.ImagePath).Success;
            if (!isSuccess) {
                return new ErrorResult(Messages.FileDeletionFailed);
            }

            carImage.ImagePath = CreateFile(imageFile, folderPath).Data;
            isSuccess = _carImageDal.Update(carImage);
            if (isSuccess)
            {
                return new Result(true, Messages.CarImageUpdated);
            }
            return new Result(false, Messages.ImageDataUpdateFailed);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            var carImages = _carImageDal.GetAll();
            if (carImages == null)
            {
                return new ErrorDataResult<List<CarImage>>(Messages.Failed);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.CarImagesListed);
        }

        public IDataResult<CarImage> GetById(int id)
        {
            var carImage = _carImageDal.Get(c => c.Id == id);
            if (carImage == null)
            {
                return new ErrorDataResult<CarImage>(Messages.CarImageNotFound);
            }
            return new SuccessDataResult<CarImage>(carImage, Messages.CarImageFound);
        }

        public IDataResult<List<CarImage>> GetAllByCarId(int carId)
        {
            bool isSuccess = _carService.GetById(carId).Success;
            if (!isSuccess)
            {
                return new ErrorDataResult<List<CarImage>>(Messages.CarNotFound);
            }

            var carImages = _carImageDal.GetAll(c => c.CarId == carId);
            if (carImages.Count == 0)
            {
                CarImage placeholder = new CarImage { Id = 0, CarId = carId, ImagePath = this.placeholderPath, Date = DateTime.Now };
                return new ErrorDataResult<List<CarImage>>(new List<CarImage> { placeholder }, Messages.CarImageNotFound);
            }
            return new SuccessDataResult<List<CarImage>>(carImages, Messages.CarImagesListed);
        }

        public IDataResult<byte[]> GetByFilename(string filename)
        {
            var filePath = Path.Combine(this.folderPath, filename);

            if (!System.IO.File.Exists(filePath))
            {
                return new ErrorDataResult<byte[]>(null, "File not found");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return new SuccessDataResult<byte[]>(fileBytes, "File retrieved successfully");
        }


        private IResult IsImageLimitReached(int id)
        {
            int imageLimit = 5;
            var carInfo = _carImageDal.GetAll(c => c.CarId == id);
            if (carInfo.Count >= imageLimit)
            {
                return new ErrorResult(Messages.ImageLimitReached);
            }
            return new SuccessResult();
        }

        private IDataResult<string> CreateFile(Microsoft.AspNetCore.Http.IFormFile imageFile, string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string guid = $"{Guid.NewGuid()}";
            string filePath = Path.Combine(folderPath, guid);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            return new SuccessDataResult<string>(filePath, Messages.FileCreated);
        }

        private IResult DeleteFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return new SuccessResult();
                }
            }
            return new ErrorResult();
        }
    }
}
