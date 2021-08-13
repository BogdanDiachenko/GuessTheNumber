using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;
using DAL.Abstraction.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly ApplicationDbContext context;

        public HistoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<List<UserGame>> ToListAsync()
        {
            return this.context.UserGames
                .Include(ug => ug.Game)
                .ThenInclude(g => g.Steps)
                .Include(ug => ug.User)
                .ToListAsync();
        }

        public Task<List<UserGame>> ToListAsync(Expression<Func<UserGame, bool>> predicate)
        {
            return this.context.UserGames
                .Include(ug => ug.Game)
                .ThenInclude(g => g.Steps)
                .Include(ug => ug.User)
                .Where(predicate).ToListAsync();
        }
    }
    }