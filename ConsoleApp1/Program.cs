
using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System.Drawing;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());

            foreach (var detail in carManager.GetCarDetails().Data)
            {
                Console.WriteLine(detail.CarName + "---" + detail.BrandName + "---" + detail.ColorName + "---");
            }

            /*ColorManager colorManager = new ColorManager(new EfColorDal());
            var color = new Entities.Concrete.Color() {Id = 4, Name = "Velvet" };
            var result = colorManager.Add(color);
            Console.WriteLine(result.Message);*/

        }
    }
}