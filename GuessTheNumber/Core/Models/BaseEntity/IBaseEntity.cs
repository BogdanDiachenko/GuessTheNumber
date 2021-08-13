namespace Core.Models.BaseEntity
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
    }
}