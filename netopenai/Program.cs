using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(e => e.AllowAnyOrigin());
//    options.AddPolicy("MyMyAllowCredentialsPolicy",
//        policy =>
//        {
//            policy.WithOrigins("http://localhost:44389", "http://localhost:5075").AllowCredentials();
//        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Use(async (context, next) => { context.Response.Headers.Add("Access-Control-Allow-Origin", "*"); await next(); });

app.UseAuthorization();

app.MapControllers();

app.Run();