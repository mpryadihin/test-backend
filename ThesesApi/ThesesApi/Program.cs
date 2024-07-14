using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ThesesApi;
using ThesesApi.Data;
using ThesesApi.Profiles;
using ThesesApi.Repositories;
using ThesesApi.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<ThesisContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
        builder.Services.AddScoped<IThesisRepository, ThesisRepository>();
        builder.Services.AddScoped<ThesisService>();

        builder.Services.AddControllersWithViews();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.migrateDb();
        app.Run();
    }
}
