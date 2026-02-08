using dealership_api.Data;
using dealership_api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt"); // Busca en el appsettings.json la sección "Jwt" 

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // si se intenta acceder a un sitio protegido, se va a autenticar usando JWT
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Si intennta ingresar a un sitio protegido sin estar autenticado, se le va a pedir a autenticarse
})

.AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {

         // Revisa diferentes aspectos del token para asegurarse de que es válido

         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,

         //Compara
         ValidIssuer = jwtSettings["Issuer"],
         ValidAudience = jwtSettings["Audience"],
         IssuerSigningKey = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(jwtSettings["Key"])// Usa mi key para verificar la firma 
         )
     };
 });

//IMPORTACIONES DE SERVICOS
builder.Services.AddScoped<VehiculoService>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<VentaService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddDbContext<DealershipDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
        )
    );

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
