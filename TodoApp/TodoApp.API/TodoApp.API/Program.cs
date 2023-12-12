var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddAutoMapper(typeof(Program).Assembly);
services.AddSqlDbContext(builder.Configuration);
services.AddDependencies();
services.AddJwtAuthentication(builder.Configuration);
services.AddSwagger();
services.AddIdentity();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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