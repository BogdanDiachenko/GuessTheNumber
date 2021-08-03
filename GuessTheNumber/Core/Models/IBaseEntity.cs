using System;

namespace Core.Models
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}