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
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            var isSuccess = _userDal.Add(user);
            if (isSuccess)
            {
                return new Result(true, Messages.UserAdded);
            }
            return new Result(false, Messages.Failed);
        }

        public IResult Delete(User user)
        {
            var isSuccess = _userDal.Delete(user);
            if (isSuccess)
            {
                return new Result(true, Messages.UserDeleted);
            }
            return new Result(false, Messages.Failed);
        }
        public IResult Update(User user)
        {
            var isSuccess = _userDal.Update(user);
            if (isSuccess)
            {
                return new Result(true, Messages.UserUpdated);
            }
            return new Result(false, Messages.Failed);

        }

        public IDataResult<List<User>> GetAll()
        {
            var users = _userDal.GetAll();
            if(users == null)
            {
                return new ErrorDataResult<List<User>>(Messages.Failed);
            }
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UsersListed);
        }

        public IDataResult<User> GetById(int id)
        {
            var user = _userDal.Get(c => c.Id == id);
            if (user == null)
            {
                return new ErrorDataResult<User>(Messages.Failed);
            }
            return new SuccessDataResult<User>(user, Messages.UserFound);
        }
    }
}
