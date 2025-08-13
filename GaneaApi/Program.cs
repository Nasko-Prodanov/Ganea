var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
//using (var scope = app.Services.CreateScope())
//{
//	var services = scope.ServiceProvider;
//	try
//	{
//		var context = services.GetRequiredService();

//	}
//	catch (Exception ex)
//	{

//		throw;
//	}
//}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
