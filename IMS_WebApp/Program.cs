using IMS.Plugins.EFCore;
using IMS.UseCases;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Reports;
using IMS_WebApp.Components;
using IMS_WebApp.Components.Account;
using IMS_WebApp.Components.Account.Pages.Manage;
using IMS_WebApp.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace IMS_WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();


            builder.Services.AddDbContext<IMSContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryManagement_new"));
            });

            //DI repositories
            builder.Services.AddTransient<IInventoryRepository, InventoryRepository>();
            builder.Services.AddTransient<IAssemblyRepository, AssemblyRepository>();
            builder.Services.AddTransient<IInventoryTransactionRepository, InventoryTransactionRepository>();
            builder.Services.AddTransient<IAssemblyTransactionRepository, AssemblyTransactionRepository>();

            //DI use cases
            builder.Services.AddTransient<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>();
            builder.Services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
            builder.Services.AddTransient<IEditInventoryUseCase, EditInventoryUseCase>();
            builder.Services.AddTransient<IViewInventoryByIdUseCase, ViewInventoryByIdUseCase>();

            builder.Services.AddTransient<IViewAssembliesByNameUseCase, ViewAssembliesByNameUseCase>();
            builder.Services.AddTransient<IAddAssemblyUseCase, AddAssemblyUseCase>();
            builder.Services.AddTransient<IViewAssemblyByIdUseCase, ViewAssemblyByIdUseCase>();
            builder.Services.AddTransient<IEditAssemblyUseCase, EditAssemblyUseCase>();
            builder.Services.AddTransient<IDeleteAssemblyUseCase, DeleteAssemblyUseCase>();
            builder.Services.AddTransient<IPurchaseInventoryUseCase, PurchaseInventoryUseCase>();
            builder.Services.AddTransient<IValidateEnoughInventoriesForProducingUseCase, ValidateEnoughInventoriesForProducingUseCase>();
            builder.Services.AddTransient<IProduceAssemblyUseCase, ProduceAssemblyUseCase>();
            builder.Services.AddTransient<ISellAssemblyUseCase, SellAssemblyUseCase>();
            builder.Services.AddTransient<ISearchInventoryTransactionsUseCase, SearchInventoryTransactionsUseCase>();
            builder.Services.AddTransient<ISearchAssemblyTransactionsUseCase, SearchAssemblyTransactionsUseCase>();


            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var imsContext = scope.ServiceProvider.GetRequiredService<IMSContext>();
            //imsContext.Database.EnsureDeleted();
            //imsContext.Database.EnsureCreated();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
