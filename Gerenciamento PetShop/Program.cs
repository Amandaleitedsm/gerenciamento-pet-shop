using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using AutoMapper;
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

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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

        builder.Services.AddTransient<IClientesRepository, ClientesRepository>();
        builder.Services.AddTransient<IPetsRepository, PetsRepository>();
        builder.Services.AddTransient<IClientesService, ClientesService>();
        builder.Services.AddTransient<IPetsService, PetsService>();
        builder.Services.AddTransient<IFileStorageService, FileStorageService>();

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
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();


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