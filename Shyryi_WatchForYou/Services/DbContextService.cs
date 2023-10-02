using Microsoft.EntityFrameworkCore;
using Shyryi_WatchForYou.Data;

namespace Shyryi_WatchForYou.Services
{
    public static class DbContextService
    {
        private static WatchForYouContext _dbContext = new WatchForYouContext();

        public static WatchForYouContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                }
                return _dbContext;
            }
            set
            {
                _dbContext = value;
            }
        }

        public static void RefreshDataFromDatabase()
        {
            foreach (var entry in DbContext.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    entry.Reload();
                }
            }
        }
    }
}
