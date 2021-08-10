// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Core.Models;
// using DAL.Abstraction.Interfaces;
// using Microsoft.EntityFrameworkCore;
//
// namespace DAL.Services
// {
//     public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
//     {
//         private readonly ApplicationDbContext context;
//
//         public UserRepository(ApplicationDbContext context)
//             : base(context)
//         {
//             this.context = context;
//         }
//     }
// }