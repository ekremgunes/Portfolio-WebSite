using AutoMapper;
using Portfolio.Business.Business.Helpers;
using Portfolio.Business.DependencyExtension;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDependencies(builder.Configuration);
        //services.AddTransient<IValidator<UserCreateModel>, UserCreateModelValidator>();

        var profiles = ProfileHelper.GetProfiles();

        var configuration = new MapperConfiguration(opt =>
        {
            opt.AddProfiles(profiles);
        });

        var mapper = configuration.CreateMapper();
        builder.Services.AddSingleton(mapper);



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error/Error");
        }
        //app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseStatusCodePagesWithReExecute("/Error/Error", "?code={0}"); // status page
        app.UseAuthentication();
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "panel",
                pattern: "panel",
                defaults: new { controller = "Dashboard", action = "Index" }
            );

            endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");                  
    
        });

        app.Run();
    }
}