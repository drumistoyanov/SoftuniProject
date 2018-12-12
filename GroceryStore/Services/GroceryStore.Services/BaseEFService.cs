using AutoMapper;
using GroceryStore.Data;

namespace GroceryStore.Services
{
    public abstract class BaseEFService
    {
        protected BaseEFService(
            GroceryStoreDbContext dbContext,
            IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        protected GroceryStoreDbContext DbContext { get; private set; }

        protected IMapper Mapper { get; private set; }
    }
}
