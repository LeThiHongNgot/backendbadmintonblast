using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.DataAccess;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.Services;
using BadmintonBlast.Services.Adstraction; // Thêm namespace chứa ICustomerService
using Microsoft.EntityFrameworkCore;
using BadmintonBlast.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbContext, BadmintonblastContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BADMINTIONBLAST")));

builder.Services.AddAutoMapper(conf =>
{
    conf.AddProfile<AutoMapperProfile>();
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Đăng ký dịch vụ 
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IPreviewService, PreviewService>();
builder.Services.AddScoped<IRacketspecificationService, RacketspecificationService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IKindProductService, KindProductService>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IOTPRepository, OTPRepository>();
builder.Services.AddScoped<IProductStockService, ProductStockService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyCors");
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
