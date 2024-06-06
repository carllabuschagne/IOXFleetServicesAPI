using IOXFleetServicesAPI;
using IOXFleetServicesAPI.Shared.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace IOXFleetServicesAPI
{
    public static class DbInitializer
    {
        public static void Initialize(LocalDatabaseContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}