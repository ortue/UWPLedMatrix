using LedMatrix.Components;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
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