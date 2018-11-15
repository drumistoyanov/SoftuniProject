using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using GroceryStore.Data;

namespace GroceryStore.Services
{
    public abstract class BaseEFService
    {
        protected BaseEFService(
            GroceryStoreDbContext dbContext,
            IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        protected GroceryStoreDbContext DbContext { get; private set; }

        protected IMapper Mapper { get; private set; }
    }
}
