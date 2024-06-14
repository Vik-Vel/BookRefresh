using BookRefresh.Extensions;


var builder = WebApplication.CreateBuilder(args);

//Adds and configures the database context to the DI container.
builder.Services.AddApplicationDbContext(builder.Configuration);

//Adds and configures identity services to the DI container.
builder.Services.AddApplicationIdentity(builder.Configuration);

//Adds services to support MVC controllers and views to the DI container.
builder.Services.AddControllersWithViews();

//Adds other services specific to your application to the DI container.
builder.Services.AddApplicationServices();

//Creates and configures a WebApplication instance using builder.
var app = builder.Build();

if (app.Environment.IsDevelopment()) //In Development mode, UseMigrationsEndPoint is used to automatically apply migrations at startup.
{
    app.UseMigrationsEndPoint();
}
else
{
    //In Production mode, UseExceptionHandler is used to display an error page and UseHsts to add an HTTP Strict Transport Security (HSTS) header.
    app.UseExceptionHandler("/Home/Error");
  
    app.UseHsts();
}

app.UseHttpsRedirection(); //UseHttpsRedirection redirects HTTP requests to HTTPS.
app.UseStaticFiles(); //UseStaticFiles enables the serving of static files (such as CSS, JavaScript, and images).

app.UseRouting(); //Enables routing, which defines how HTTP requests are mapped to endpoints.

app.UseAuthentication(); //UseAuthentication adds request authentication.
app.UseAuthorization(); //UseAuthorization adds a check of resource access rights.

app.MapDefaultControllerRoute(); //MapDefaultControllerRoute configures routing for view controllers using the default routing pattern {controller=Home}/{action=Index}/{id?}.
app.MapRazorPages(); //MapRazorPages конфигурира маршрутизация за Razor Pages.

await app.RunAsync(); //Starts the web application and starts listening for incoming HTTP requests.
