using AutoMapper;
using R5A08_TP1.Models.DTO;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Source -> Target.

            // GET.
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.NameBrand, opt => opt.MapFrom(src => src.BrandNavigation != null ? src.BrandNavigation.NameBrand : null))
                .ForMember(dest => dest.NameProductType, opt => opt.MapFrom(src => src.ProductTypeNavigation != null ? src.ProductTypeNavigation.NameProductType : null));
            CreateMap<Product, ProductDetailsDTO>()
                .ForMember(dest => dest.NameBrand, opt => opt.MapFrom(src => src.BrandNavigation != null ? src.BrandNavigation.NameBrand : null))
                .ForMember(dest => dest.NameProductType, opt => opt.MapFrom(src => src.ProductTypeNavigation != null ? src.ProductTypeNavigation.NameProductType : null))
                .ForMember(dest => dest.Restocking, opt => opt.MapFrom(src => src.RealStock < src.MinStock));

            // POST.
            CreateMap<CreateProductDTO, Product>();

            // PUT.
            CreateMap<UpdateProductDTO, Product>()
                .ForMember(dest => dest.BrandNavigation, opt => opt.MapFrom(src => new Brand { NameBrand = src.NameBrand }))
                .ForMember(dest => dest.ProductTypeNavigation, opt => opt.MapFrom(src => new ProductType { NameProductType = src.NameProductType }));
        }
    }
}
