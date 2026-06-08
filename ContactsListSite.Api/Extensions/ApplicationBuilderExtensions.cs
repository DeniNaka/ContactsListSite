namespace ContactsListSite.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
