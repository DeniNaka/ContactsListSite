
using ContactsListSite.Api.Extensions;

namespace ContactsListSite.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplicationServices();

            var app = builder.Build();

            app.UseCustomExceptionHandler();

            app.ConfigureMiddleware();

            app.Run();
        }
    }
}
