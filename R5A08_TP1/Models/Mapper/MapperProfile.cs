using AutoMapper;
using R5A08_TP1.Models.DTO;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Source -> Target
            CreateMap<ProductDTO, Product>();
            CreateMap<ProductDetailsDTO, Product>();
        }
    }
}
