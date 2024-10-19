using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Product added";
        public static string ProductNameInvalid = "Name invalid";
        public static string MaintenanceTime = "Under maintenance";
        public static string ProductsListed = "Products listed";
        public static string ProductDeleted = "Product deleted";
        public static string ProductFetched = "Product fetched";
        public static string BrandAdded = "BrandAdded";
        public static string BrandDeleted = "BrandDeleted";
        public static string BrandsListed = "BrandsListed";
        public static string CarAdded = "CarAdded";
        public static string CarDeleted = "CarDeleted";
        public static string CarsListed = "CarsListed";
        public static string ColorAdded = "ColorAdded";
        public static string ColorDeleted = "ColorDeleted";
        public static string ColorsListed = "ColorsListed";
        public static string RentalAdded = "RentalAdded";
        public static string RentalDeleted = "RentalDeleted";
        public static string RentalsListed = "RentalsListed";
        public static string UserAdded = "UserAdded";
        public static string UserDeleted = "UserDeleted";
        public static string UsersListed = "UsersListed";
        public static string CarDetailsListed = "CarDetailsListed";

        public static string CarFound = "CarFound";

        public static string BrandFound { get; internal set; }
        public static string ColorFound { get; internal set; }
        public static string RentalFound { get; internal set; }
        public static string CustomerAdded { get; internal set; }
        public static string CustomerDeleted { get; internal set; }
        public static string CustomersListed { get; internal set; }
        public static string CustomerFound { get; internal set; }
    }
}
