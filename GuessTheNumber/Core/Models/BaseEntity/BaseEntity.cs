namespace Core.Models.BaseEntity
{
    public class BaseEntity<T> : IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}