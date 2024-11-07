using AutoMapper;
using BadmintonBlast.DataAccess.Adstraction;
using BadmintonBlast.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BadmintonBlast.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;
        public ICustomerRepository CustomerRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IBillsRepository BillRepository { get; set; }
        public IInvoiceRepository InvoiceRepository { get; set; }
        public IRepository<Image> ImageRepository { get; set; }

        public IRepository<Brand> BrandRepository { get; set; }

        public IRepository<Preview> PreviewRepository { get; set; }

        public IRepository<Racketspecification> RacketspecificationRepository { get; set; }

        public IRepository<Productstock> ProductStockRepository { get; set; }

        public IRepository<Coupon> CouponRepository { get; set; }

        public IRepository<Cart> CartRepository { get; set; }

        public IRepository<Order> OrderRepository { get; set; }

        public IRepository<Otp> OtpRepository { get; set; }

        public IRepository<Reservation> ReservationRepository { get; set; }

        public IRepository<Kindproduct> KindproductRepository { get; set; }
        public UnitOfWork(DbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            CustomerRepository = new CustomerRepository(dbContext, mapper);
            ProductRepository = new ProductRepository(dbContext, mapper, httpContextAccessor);
            ImageRepository = new Repository<Image>(dbContext, mapper);
            BrandRepository = new Repository<Brand>(dbContext, mapper);
            PreviewRepository = new Repository<Preview>(dbContext, mapper);
            RacketspecificationRepository = new Repository<Racketspecification>(dbContext, mapper);
            ProductStockRepository = new Repository<Productstock>(dbContext, mapper);
            CouponRepository = new Repository<Coupon>(dbContext, mapper);
            CartRepository = new Repository<Cart>(dbContext, mapper);
            OrderRepository=new Repository<Order>(dbContext, mapper);
            OtpRepository=new Repository<Otp>(dbContext,mapper);
            ReservationRepository=  new Repository<Reservation>(dbContext, mapper);
            KindproductRepository = new Repository<Kindproduct>(dbContext, mapper);
            BillRepository =new BillRepository(dbContext, mapper);
            InvoiceRepository = new InvoiceRepository(dbContext, mapper);

        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
