using LinkDev.IKEA.DAL.persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.PL
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			#region Configure Services
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
			{
				optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			}); 
			#endregion
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			#region Configure Kestrel MiddleWares
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
			#endregion

			app.Run();
		}
	}
}
