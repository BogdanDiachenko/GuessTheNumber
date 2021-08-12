using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;
using DAL.Abstraction.Interfaces;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DAL.Services
{
    public class HistoryRepository : GenericRepository<Game>, IHistoryRepository
    {
        private readonly ApplicationDbContext context;

        public HistoryRepository(ApplicationDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public override Task<List<Game>> ToListAsync(Expression<Func<Game, bool>> predicate)
        {
            return this.context.Games
                .Include(g => g.Steps)
                .Include(g => g.Players).Where(predicate)
                .ToListAsync();
        }
    }
    }