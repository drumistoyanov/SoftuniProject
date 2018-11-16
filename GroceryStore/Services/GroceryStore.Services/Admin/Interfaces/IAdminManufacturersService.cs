using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryStore.Common.BindingModels.Admin.Manufacturers;
using GroceryStore.Common.ViewModels.Admin.Manufacturers;

namespace GroceryStore.Services.Admin.Interfaces
{
    public interface IAdminManufacturersService
    {
        IEnumerable<ManufacturerIndexViewModel> GetManufacturers();

        Task<ManufacturerBindingModel> GetManufacturer(int id);

        Task<ManufacturerDetailsViewModel> GetDetails(int id);

        Task SaveManufacturer(ManufacturerBindingModel model);

        Task EditManufacturer(int id, ManufacturerBindingModel model);

        Task DeleteManufacturer(int id);
    }
}
