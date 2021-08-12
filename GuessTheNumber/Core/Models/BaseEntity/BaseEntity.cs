namespace Core.Models
{
    public class BaseEntity<T> : IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}