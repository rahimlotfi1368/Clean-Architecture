using Bogus.DataSets;
using GlobalTicketManagement.Api.Utility.Filters;
using GlobalTicketManagement.Persistence;
using Microsoft.OpenApi.Models;

namespace GlobalTicketManagement.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddMyCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Open",
                    builder => builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
             
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "GloboTicket Ticket Management API"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                           {
                             new OpenApiSecurityScheme
                                 {
                                   Reference = new OpenApiReference
                                       {
                                         Type = ReferenceType.SecurityScheme,
                                         Id = "Bearer"
                                       },
                                      Scheme = "oauth2",
                                      Name = "Bearer",
                                      In = ParameterLocation.Header,
                                  },
                              new string[] { }
                           }
                });

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "GloboTicket Ticket Management API",

                });

                options.OperationFilter<FileResultContentTypeOperationFilter>();
            });
        }

        public static  void ConfigSwagger(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GloboTicket Ticket Management API");
                });
            }
        }
    }
}
