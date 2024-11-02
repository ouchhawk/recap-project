using _Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(CarImage carImage, Microsoft.AspNetCore.Http.IFormFile imageFile);
        IResult Delete(CarImage carImage);
        IResult DeleteByFilename(string fileName);
        IResult Update(Microsoft.AspNetCore.Http.IFormFile imageFile, CarImage carImage);
        IDataResult<CarImage> GetById(int id);
        IDataResult<byte[]> GetByFilename(string filename);
        IDataResult<List<CarImage>> GetAllByCarId(int carId);
        IDataResult<List<CarImage>> GetAll();
    }
}
