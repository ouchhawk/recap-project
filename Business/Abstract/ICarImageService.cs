using _Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(CarImage carImage, IFormFile imageFile);
        IResult Delete(CarImage carImage);
        IResult DeleteByFilename(string fileName);
        IResult Update(IFormFile imageFile, CarImage carImage);
        IDataResult<CarImage> GetById(int id);
        IDataResult<byte[]> GetByFilename(string filename);
        IDataResult<List<CarImage>> GetAllByCarId(int carId);
        IDataResult<List<CarImage>> GetAll();
    }
}
