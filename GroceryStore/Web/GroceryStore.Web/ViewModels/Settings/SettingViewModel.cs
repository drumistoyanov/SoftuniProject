namespace GroceryStore.Web.ViewModels.Settings
{
    using GroceryStore.Common.Mapping;
    using GroceryStore.Data.Models;

    public class SettingViewModel : IMapFrom<Setting>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
