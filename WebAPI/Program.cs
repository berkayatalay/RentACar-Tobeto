using Business.DependencyResolvers;
using Core.CrossCuttingConcerns.Exceptions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//ADDSINGLETON KISIMLARINI BUS�NESSDA DEPENDECYRESOLVERS ��ER�S�NDE CLASS A�ARAK EKLED�K. 
// Add services to the container.
//ibrandservice istendi�inde, brandmanager ver
//builder.Services.AddSingleton<IBrandService, BrandManager>();
//Singleton: tek bir nesne olu�turur ve herkese onu verir.
//ek �dev di�er y�ntemleri ara�t�r�n�z.

//builder.Services.AddSingleton<IBrandDal, InMemoryBrandDal>();//Ibranddal isteyen heryerde, inmemorybranddal referans�n� verecek. 1 kere newliyor ald��� referans� uygulama hayat� boyunca kullanacak. 
//public static readonly IBrandDal BrandDal = new InMemoryBrandDal(); singleton kar��l��� olarak bu. 
//builder.Services.AddSingleton<BrandBusinessRules>();
//addscoped
//builder.Services.AddScoped<Random>();new Random();
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


//reflection y�ntemiyle profile class'�n� kal�t�m alan t�m classlar� bulur ve Automappera ekler
//ManageNuget Automapper microsoft dependencyinjection indirdik//Serviceregistration sildi�im i�in bu mapperler� businesse (klas�r) ekledik. 
//addTransient

builder.Services.AddBusinessServices(builder.Configuration);



//builder.Services.AddSingleton<IFuelService, FuelManager>();
//builder.Services.AddSingleton<IFuelDal, InMemoryFuelDal>();
//builder.Services.AddSingleton<BrandBusinessRules>();

//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
if (app.Environment.IsProduction())
    app.UseGlobalExceptionHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseMiddleware<ExceptionMiddleware>();

app.Run();
