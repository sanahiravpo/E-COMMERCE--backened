
using E_COMMERCE_WEBSITE.Context;
using E_COMMERCE_WEBSITE.Mapper;
using E_COMMERCE_WEBSITE.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using E_COMMERCE_WEBSITE.Repositories.categories;
using E_COMMERCE_WEBSITE.Repositories.ProductService;
using E_COMMERCE_WEBSITE.Repositories.CartServices;
using E_COMMERCE_WEBSITE.Repositories.WishlistRepository;
using E_COMMERCE_WEBSITE.Repositories.OrderRepository;
using E_COMMERCE_WEBSITE.JwtServise;



namespace E_COMMERCE_WEBSITE
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add services to the container.
            builder.Services.AddScoped<UserDBContext>();
            builder.Services.AddScoped<IUser, UserRepositories>();
            builder.Services.AddScoped<ICategory, CategoryRepository>();
            builder.Services.AddScoped<IProduct, ProductRepository>();
            builder.Services.AddScoped<ICart, CartRepository>();
            builder.Services.AddScoped<IWishlist, WishListRepository>();
            builder.Services.AddScoped<IOrder, OrderRepository>();
            builder.Services.AddScoped<IJwtToken,JwtRepository> ();
            builder.Services.AddAutoMapper(typeof(UserProfiler));


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            builder.Services.AddAuthorization();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("ReactPolicy");
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.MapControllers();

            app.Run();
        }
    }
}
