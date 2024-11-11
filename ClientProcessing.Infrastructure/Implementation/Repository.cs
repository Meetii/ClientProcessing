using ClientProcessing.Domain.Models;
using ClientProcessing.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProcessing.Infrastructure.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public async Task<int> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var addedEntity =  await _context.AddAsync(entity);
            
            await _context.SaveChangesAsync();
            return addedEntity.Entity.Id;
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

}
