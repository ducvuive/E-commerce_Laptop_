using AutoMapper;
using BackendAPI.Persistence.Identity;
using BackendAPI.Domain.Entities;
using ShareView.DTO;

namespace BackendAPI.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<ScreenDTO, Screen>().ReverseMap();
            CreateMap<RamDTO, Ram>().ReverseMap();
            CreateMap<Processor, ProcessorDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryAdminDTO>().ReverseMap();
            CreateMap<InvoiceDTO, Invoice>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductAdminDTO>().ReverseMap();
            CreateMap<InvoiceDetailDTO, InvoiceDetail>().ReverseMap();
            CreateMap<UserIdentity, UserIdentityDTO>().ReverseMap();
            //CreateMap<ConnectDTO, Connect>().ReverseMap();
            CreateMap<RatingDTO, Rating>().ReverseMap();
            CreateMap<Rating, RatingDTO>();
        }
    }
}
