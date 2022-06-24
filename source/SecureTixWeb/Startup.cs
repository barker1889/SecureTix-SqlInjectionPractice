using SecureTixWeb.DataAccess;
using SecureTixWeb.DataAccess.Utils;
using SecureTixWeb.Services;

namespace SecureTixWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            services.AddSingleton<IUserRepo, UserRepo>();
            services.AddSingleton<IEventsRepo, EventsRepo>();
            services.AddSingleton<IBasketRepo, BasketRepo>();
            services.AddSingleton<IOrderRepo, OrderRepo>();
            services.AddSingleton<IMailingListRepo, MailingListRepo>();
            
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton<IBasketService, BasketService>();
            services.AddSingleton<IOrdersService, OrdersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
