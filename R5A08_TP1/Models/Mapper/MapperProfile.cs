using AutoMapper;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.DTO;
using R5A08_TP1.Models.EntityFramework;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace R5A08_TP1.Models.Mapper
{
    public class MapperProfile : Profile
    {
        private readonly AppDbContext _context;

        public MapperProfile(AppDbContext context)
        {
            _context = context;

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
            CreateMap<Product, CreateProductDTO>()
                .ForMember(dest => dest.NameBrand, opt => opt.MapFrom(src => src.BrandNavigation != null ? src.BrandNavigation.NameBrand : null))
                .ForMember(dest => dest.NameProductType, opt => opt.MapFrom(src => src.ProductTypeNavigation != null ? src.ProductTypeNavigation.NameProductType : null));
            CreateMap<CreateProductDTO, Product>()
                .AfterMap((dto, product) =>
                {
                    SetBrandAndProductType(product, dto.NameBrand, dto.NameProductType);
                });

            // PUT.
            CreateMap<Product, UpdateProductDTO>();
            CreateMap<UpdateProductDTO, Product>()
                .AfterMap((dto, product) =>
                {
                    SetBrandAndProductType(product, dto.NameBrand, dto.NameProductType);
                });
        }

        public void SetBrandAndProductType(Product product, string nameBrand, string nameProductType)
        {
            Brand? brand = _context.Brands.FirstOrDefault(b => b.NameBrand == nameBrand);
            if (brand == null) 
            {
                brand = new Brand { NameBrand = nameBrand };
                _context.Brands.Add(brand);
                _context.SaveChanges();
            }
            product.IdBrand = brand?.IdBrand;

            ProductType? productType = _context.ProductTypes.FirstOrDefault(pt => pt.NameProductType == nameProductType);
            if (productType == null)
            {
                productType = new ProductType { NameProductType = nameProductType };
                _context.ProductTypes.Add(productType);
                _context.SaveChanges();
            }
            product.IdProductType = productType?.IdProductType;
        }
    }
}
