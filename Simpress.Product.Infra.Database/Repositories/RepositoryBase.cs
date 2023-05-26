using Microsoft.EntityFrameworkCore;
using Simpress.Product.Domain.Adapters;
using Simpress.Product.Domain.Entities;
using Simpress.Product.Infra.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpress.Product.Infra.Database.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        protected readonly SimpressContext? Context;
        private DbSet<T>? _entities;

        public RepositoryBase(SimpressContext? context)
        {
            Context = context;
            _entities = Context?.Set<T>();
        }

        public async Task Add(T entity) 
        { 
            await _entities.AddAsync(entity); 
        }
        

        public async Task<List<T>> GetAll()
        {
            return await _entities.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public async Task Update(T entity)
        {
            _entities.Update(entity);

            
        }
    }
}
