using fluita_API.@interface;
using fluita_API.Services;
using fluita_API.utils.Queries;


var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<fb_APIContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("fb_APIContext") ?? 
//	throw new InvalidOperationException("Connection string 'fb_APIContext' not found.")));

builder.Services.AddTransient<IEmailSender, EmailService>();
builder.Services.AddTransient<IDbQuery, UserDbQuery>();

// Add services to the container.

builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
