using Progetto_19._07.Models;
using Progetto_19._07.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAnagraficaService, AnagraficaService>();
builder.Services.AddScoped<ITipoViolazioneService, TipoViolazioneService>();
builder.Services.AddScoped<IVerbaleService, VerbaleService>();
builder.Services.AddScoped<ITotalePunti, TotalePuntiService>();
builder.Services.AddScoped<IVerbaleSuperiorePunti, VerbaliSuperiorePuntiService>();
builder.Services.AddScoped<IVerbaliSuperiore400EuroService, VerbaliSuperiore400EuroService>();


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

