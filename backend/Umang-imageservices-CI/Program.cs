using Consul;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*// Register Consul client
builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
{
    consulConfig.Address = new Uri(builder.Configuration.GetSection("ConsulConf:ConsulUri").Value);
}));
builder.Services.AddSingleton<IHostedService, ConsulHostedService>();
builder.Services.Configure<ConsulConfig>(builder.Configuration.GetSection("Consul"));*/

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//static/images/<file-name.ext>
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "static")),
    RequestPath = "/static"
});

app.UseCors("AllowAny");

app.UseAuthorization();

app.MapControllers();

app.Run();
