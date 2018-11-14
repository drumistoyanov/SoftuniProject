namespace GroceryStore.Data
{
    using System;

    using GroceryStore.Data.Common;

    using Microsoft.EntityFrameworkCore;

    public class DbQueryRunner : IDbQueryRunner
    {
        public DbQueryRunner(GroceryStoreDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GroceryStoreDbContext Context { get; set; }

        public void RunQuery(string query, params object[] parameters)
        {
            this.Context.Database.ExecuteSqlCommand(query, parameters);
        }

        public void Dispose()
        {
            this.Context?.Dispose();
        }
    }
}
