using System;
using System.Threading.Tasks;
using Core.Models.Identity;
using Core.Models.Responses;
using DAL.Abstraction.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<ApplicationUser> GetById(Guid id)
        {
            return this.context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<Response<ApplicationUser>> GetById(Guid? id)
        {
            if (id == null)
            {
                Response<ApplicationUser>.Failure(null);
            }

            return Response<ApplicationUser>.Success(await this.context.Users.FirstOrDefaultAsync(user => user.Id == id));
        }
    }
}