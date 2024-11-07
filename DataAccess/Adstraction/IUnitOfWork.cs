using Microsoft.EntityFrameworkCore;
using BadmintonBlast.Models.Entities;

namespace BadmintonBlast.DataAccess.Adstraction
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; set; }

        IProductRepository ProductRepository { get; set; }

        IBillsRepository BillRepository { get; set; }

        IInvoiceRepository InvoiceRepository { get; set; }

        IRepository<Image> ImageRepository { get; set; }

        IRepository<Brand> BrandRepository { get; set; }

        IRepository<Preview> PreviewRepository { get; set; }

        IRepository<Productstock> ProductStockRepository { get; set; }

        IRepository<Racketspecification> RacketspecificationRepository { get; set; }

        IRepository<Coupon> CouponRepository { get; set; }  

        IRepository<Cart> CartRepository { get; set; }

        IRepository<Order> OrderRepository { get; set; }

        IRepository<Otp> OtpRepository { get; set; }

        IRepository<Reservation> ReservationRepository { get; set; }

        IRepository<Kindproduct> KindproductRepository { get; set; }
        Task SaveChangesAsync();
    }
}
