using Autofac.Extensions.DependencyInjection;
using Autofac;
using Lesson_3_App.Db;
using Lesson_3_App.Mapper;
using Lesson_3_App.Query;
using Lesson_3_App.Services;
using Lesson_3_App.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Lesson_3_App.Mutations;

namespace Lesson_3_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);             

            var connectionString = builder.Configuration.GetConnectionString("db");

            //builder.Services.AddPooledDbContextFactory<AppDbContext>(o => o.UseNpgsql(connectionString));
            //builder.Services.AddDbContext<AppDbContext>(conf => conf.UseNpgsql(connectionString));

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(container =>
            {
                container.Register(c => new AppDbContext(connectionString)).InstancePerDependency();
            });

            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddMemoryCache();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IStorageService, StorageService>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            
            
            

            builder.Services
                            .AddGraphQLServer()
                            .AddQueryType<MySimpleQuery>()
                            .AddMutationType<MySimpleMutation>();

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.MapGraphQL();
            app.Run();
        }
    }
}
