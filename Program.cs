using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


/* SERVICE WITH THE CONNECTION WITH DATABASE */
var builder = WebApplication.CreateBuilder(args);

/* cors */
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/* BUILDER FOR USERCONTEXT */
var configuration = builder.Configuration;
builder.Services.AddDbContext<UserContext> (options => 
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
/* npgsql is for postgress  */


/* cors issue solved */
   /*  builder.Services.AddCors(
        Options.AddPolicy(name: MyAllowSpecificOrigins,
        PolicyServiceCollectionExtensions => {
            policy.WithOrigins()
        })
    ) */

/*  */
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
