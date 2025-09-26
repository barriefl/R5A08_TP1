using BlazorApp.Components;
using BlazorApp.Models;
using BlazorApp.Services;
using BlazorApp.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped<IService<Product>, WebService>(_ => new WebService());
            builder.Services.AddScoped<ProductsViewModel>();
            builder.Services.AddScoped<CreateProductViewModel>();
            builder.Services.AddScoped<UpdateProductViewModel>();

            var app = builder.Build();

            builder.Services.AddBlazorBootstrap();

            await builder.Build().RunAsync();
        }
    }
}
