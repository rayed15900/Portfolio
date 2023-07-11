using AutoMapper;
using Portfolio.BusinessLogic.DependencyExtension;
using Portfolio.BusinessLogic.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region All Dependency Injection from BusinessLogic layer

builder.Services.AddDependencies(builder.Configuration);

#endregion

#region Auto Mapper

var profiles = ProfileHelper.GetProfiles();

var configuration = new MapperConfiguration(opt =>
{
    opt.AddProfiles(profiles);
});

var mapper = configuration.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

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

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
