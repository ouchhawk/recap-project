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
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IResult Add(Color color)
        {
            var isSuccess = _colorDal.Add(color);
            if (isSuccess)
            {
                return new Result(true, Messages.ColorAdded);
            }
            return new Result(false, Messages.Failed);
        }

        public IResult Delete(Color color)
        {
            var isSuccess = _colorDal.Delete(color);
            if (isSuccess)
            {
                return new Result(true, Messages.ColorDeleted);
            }
            return new Result(false, Messages.Failed);
        }
        public IResult Update(Color color)
        {
            var isSuccess = _colorDal.Update(color);
            if (isSuccess)
            {
                return new Result(true, Messages.ColorUpdated);
            }
            return new Result(false, Messages.Failed);

        }

        public IDataResult<List<Color>> GetAll()
        {
            var colors = _colorDal.GetAll();
            if (colors == null)
            {
                return new ErrorDataResult<List<Color>>(Messages.Failed);
            }
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.ColorsListed);
        }

        public IDataResult<Color> GetById(int id)
        {
            var color = _colorDal.Get(c => c.Id == id);
            if (color == null)
            {
                return new ErrorDataResult<Color>(Messages.Failed);
            }
            return new SuccessDataResult<Color>(color, Messages.ColorFound);
        }
    }
}
