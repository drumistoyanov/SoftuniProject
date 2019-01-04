using System;
using GroceryStore.Data;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Tests.Mocks
{
    public static class MockDbContext
    {
        public static GroceryStoreDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<GroceryStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new GroceryStoreDbContext(options);
        }
    }
}
