using Microsoft.EntityFrameworkCore;

namespace ThesesApi.Data
{
    public static class DataExtensions
    {
        public static void migrateDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ThesisContext>();
            dbContext.Database.Migrate();

        }
    }
}
