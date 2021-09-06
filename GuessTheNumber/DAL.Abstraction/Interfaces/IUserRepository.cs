using System;
using System.Threading.Tasks;
using Core.Models.Identity;
using Core.Models.Responses;

namespace DAL.Abstraction.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetById(Guid id);

        Task<Response<ApplicationUser>> GetById(Guid? id);
    }
}