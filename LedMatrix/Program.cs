using LedMatrix.Components;
using Library.Collection;
using Library.Util;
using Microsoft.AspNetCore.HttpOverrides;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
  options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddSingleton<PixelList>();
builder.Services.AddSingleton<TaskGoList>();
builder.Services.AddSingleton<RadioCanada>();
builder.Services.AddSingleton<OpenWeather>();
//builder.Services.AddSingleton<KodiWebService>();
builder.Services.AddSingleton(CouleurList.Load());

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  //app.UseForwardedHeaders();
  app.UseHsts();
}
else
{
  app.UseDeveloperExceptionPage();
  //app.UseForwardedHeaders();
}

app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();


// /etc/systemd/system/kestrel-BlazorAppMatrix.service
// sudo systemctl enable kestrel-BlazorAppMatrix.service
// sudo systemctl start kestrel-BlazorAppMatrix.service
// sudo systemctl stop kestrel-BlazorAppMatrix.service
// sudo systemctl status kestrel-BlazorAppMatrix.service
// /home/benoit/.dotnet/dotnet