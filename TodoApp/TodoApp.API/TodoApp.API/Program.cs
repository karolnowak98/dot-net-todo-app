var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddAutoMapper(typeof(Program).Assembly);
services.ConfigureSqlDbContext(builder.Configuration);
services.AddDependencies();
services.ConfigureJwtAuthentication(builder.Configuration);
services.ConfigureSwagger();
services.ConfigureIdentity();
services.ConfigureMediatR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    //TODO Middleware
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program;