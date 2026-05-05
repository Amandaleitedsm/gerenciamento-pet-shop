using Serilog;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Gerenciamento_PetShop;
using Gerenciamento_PetShop.Application.Interfaces;
using Gerenciamento_PetShop.Application.Services;
using Gerenciamento_PetShop.Application.Validations;
using Gerenciamento_PetShop.Domain.Interfaces;
using Gerenciamento_PetShop.Infraestrutura;
using Gerenciamento_PetShop.Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using FluentValidation;
using Gerenciamento_PetShop.Presentation.Middlewares;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information() // Só vai registrar de Information para cima (ignora os rascunhos)
        .WriteTo.Console() // Continua mostrando no terminal
        .WriteTo.File("Logs/petshop-log-.txt", rollingInterval: RollingInterval.Day) // Cria um arquivo por dia!
        .CreateLogger();

            // Troca o motor de log padrão pelo Serilog
            builder.Host.UseSerilog();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddApiVersioning(v =>
        {
            v.DefaultApiVersion = new ApiVersion(1, 0); 
            v.AssumeDefaultVersionWhenUnspecified = true;
        }).AddApiExplorer(options => 
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        builder.Services.AddSwaggerGen(options =>
        {
            var provider = builder.Services.BuildServiceProvider()
                          .GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"PetShop API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString()
                });

            }
            var arquivoSwagger = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var caminhoXml = Path.Combine(AppContext.BaseDirectory, arquivoSwagger);
            options.IncludeXmlComments(caminhoXml);

            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Insira o token JWT desta maneira: Bearer {seu_token}"
            });

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();

        builder.Services.AddValidatorsFromAssemblyContaining<ClientesCreateValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<PetsCreateValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<PetsUpdateValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<ClientesUpdateValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UsuariosCreateValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UsuariosUpdateValidator>();

        builder.Services.AddScoped<IClientesRepository, ClientesRepository>();
        builder.Services.AddScoped<IPetsRepository, PetsRepository>();
        builder.Services.AddScoped<IClientesService, ClientesService>();
        builder.Services.AddScoped<IPetsService, PetsService>();
        builder.Services.AddScoped<IFileStorageService, FileStorageService>();
        builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
        builder.Services.AddScoped<IUsuariosServices, UsuariosServices>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

        var key = Encoding.ASCII.GetBytes(Key.Secret);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        builder.Services.AddDbContext<GerenciamentoPetShopContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            sqlServerOptions => sqlServerOptions.EnableRetryOnFailure() // <-- A mágica está aqui!
        ));


        builder.Services.AddCors(options => {
            options.AddPolicy("AngularPolicy", policy => {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        var app = builder.Build();
        
        app.UseCors("AngularPolicy");

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<GerenciamentoPetShopContext>();
            db.Database.Migrate(); // Cria as tabelas se elas não existirem
        }
        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }

}