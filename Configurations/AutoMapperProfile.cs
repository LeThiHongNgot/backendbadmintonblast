using AutoMapper;
using BadmintonBlast.Models.Dtos;
using BadmintonBlast.Models.Entities;
using BadmintonBlast.RequestModels;

namespace BadmintonBlast.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMapsEntitiesToDto();
            CreateMapsEntitiesToRequests();
        }
        private void CreateMapsEntitiesToDto()
        {
            // Mapping entities to DTOs and vice versa
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Product, ProductsDTO>().ReverseMap();
            CreateMap<Image, ImageDTO>().ReverseMap()
    .ForMember(dest => dest.Idproduct, opt => opt.MapFrom(src => src.Idproduct))
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id));
            ;
            CreateMap<Brand, BrandDTO>().ReverseMap();
            CreateMap<Racketspecification, RacketspecificationDTO>().ReverseMap();
            CreateMap<Preview, PreviewDTO>().ReverseMap();
            CreateMap<Productstock, ProductStockDTO>().ReverseMap();
            CreateMap<Coupon, CouponDTO>().ReverseMap();
            CreateMap<Cart, CartDTO>().ReverseMap();
            CreateMap<Cart, Cart>().ReverseMap();
            CreateMap<Bill, BillDTO>().ReverseMap();
            CreateMap<Bill, Bill>().ReverseMap();
            CreateMap<Productstock, Productstock>().ReverseMap();
            CreateMap<Kindproduct, KindProductDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Invoice, InvoiceDTO>().ReverseMap();
            CreateMap<Reservation, ReservationDTO>();
            CreateMap<Image, Image>();
            CreateMap<Order, Order>().ReverseMap();
            CreateMap<Brand, BrandDTO>()
            .ForMember(dest => dest.Image, opt => opt.Ignore()); // Nếu cần tùy chỉnh việc ánh xạ cho trường Image

            CreateMap<BrandDTO, Brand>();
            // Ngược lại nếu cần
            CreateMap<ReservationDTO, Reservation>();
        }

        private void CreateMapsEntitiesToRequests()
        {
            CreateMap<Customer, UpdateCustomerRequest>()
            //bởi vì HinhAnh của cusstomer là string, HinhAnh của UpdateCustomerRequest
            // là IFormFile, không cùng kiểu dữ liệu nên phải loại trừ (ignore)
                .ForMember(dest => dest.ImageCustomer, config => config.Ignore())
                .ReverseMap()
                .ForMember(customer => customer.ImageCustomer, config => config.MapFrom(updateCustomerRequest => updateCustomerRequest.ImageCustomer.FileName));

            CreateMap<Image, UpdateImageRequest>()
               .ForMember(dest => dest.Image4, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.Image4, opt => opt.MapFrom(src => src.Image4));

            CreateMap<Kindproduct, KindProductDTO>()
            //bởi vì HinhAnh của cusstomer là string, HinhAnh của UpdateCustomerRequest
            // là IFormFile, không cùng kiểu dữ liệu nên phải loại trừ (ignore)
                .ForMember(kind => kind.Image, config => config.Ignore())
                .ReverseMap()
                .ForMember(kind => kind.Image, config => config.MapFrom(updatekind => updatekind.Image.FileName));      
            CreateMap<Brand, BrandDTO>()
              .ForMember(kind => kind.Image, config => config.Ignore())
              .ReverseMap()
              .ForMember(kind => kind.Image, config => config.MapFrom(updatekind => updatekind.Image.FileName));

        }
    }
}
