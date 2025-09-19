namespace R5A08_TP1.Models.Mapper
{
    public interface IMapper<Entity, DTO>
    {
        Entity? FromDTO(DTO dto);
        DTO? FromEntity(Entity entity);

        IEnumerable<DTO> ToDTO(IEnumerable<Entity> entities)
        {
            return entities.Select(e => FromEntity(e));
        }

        IEnumerable<DTO> QuelquechoseDTO(IEnumerable<Entity> entities)
        {
            return entities.Select(e => FromEntity(e));
        }
    }
}
