using api.Data; 
using Microsoft.EntityFrameworkCore; // para UseMySql()


var builder = WebApplication.CreateBuilder(args);

// vamos a corsear

builder.Services.AddCors(options => {
    options.AddPolicy("AllowVueApp", policy =>{
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});




// Add services to the container.
builder.Services.AddControllers();


// conectar ef con MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 4, 3)) // version di mii mysql
    )
);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// uisar cors

app.UseCors("AllowVueApp");

app.MapControllers();

app.Run();
