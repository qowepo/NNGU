using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("Cookie")
    .AddCookie("Cookie", config =>
    {
        config.LoginPath = "/Account/Login";
        config.AccessDeniedPath = "/Account/Login";
    });

builder.Services.AddAuthorization( options =>
{
    
    options.AddPolicy("Administrator", builder =>
    {
        builder.RequireClaim(ClaimTypes.Role, "Administrator");
    });

    options.AddPolicy("User", builder =>
    {
        builder.RequireClaim(ClaimTypes.Role, "User");
    });

    //options.AddPolicy("User", builder =>
    //{
    //    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "User")
    //                                    || x.User.HasClaim(ClaimTypes.Role, "Administrator"));

    //});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

