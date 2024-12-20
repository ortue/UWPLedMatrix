using BlazorAppMatrix.Class;
using BlazorAppMatrix.Components;
using Library.Collection;
using Library.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddSingleton<PixelList>();
builder.Services.AddSingleton<TaskGoList>();
builder.Services.AddSingleton<RadioCanada>();
builder.Services.AddSingleton<OpenWeather>();
builder.Services.AddSingleton<KodiWebService>();
builder.Services.AddSingleton(CouleurList.Load());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseHttpsRedirection();

  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();


// /etc/systemd/system/kestrel-BlazorAppMatrix.service
// sudo systemctl enable kestrel-BlazorAppMatrix.service
// sudo systemctl start kestrel-BlazorAppMatrix.service
// sudo systemctl stop kestrel-BlazorAppMatrix.service
// sudo systemctl status kestrel-BlazorAppMatrix.service
// /home/benoit/.dotnet/dotnet
