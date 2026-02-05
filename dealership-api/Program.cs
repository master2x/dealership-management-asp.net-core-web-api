using dealership_api.Services;

var builder = WebApplication.CreateBuilder(args);

//IMPORTACIONES DE SERVICOS
builder.Services.AddSingleton<VehiculoService>();
builder.Services.AddSingleton<EmpleadoService>();
builder.Services.AddSingleton<ClienteService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
