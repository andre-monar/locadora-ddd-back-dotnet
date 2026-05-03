using Domain.Interfaces.Generics;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Generics
{
    public class GenericRepository<T> : IGeneric<T> where T : class
    {
        private readonly ContextBase _context;

        public GenericRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task Add(T objeto)
        {
            await _context.Set<T>().AddAsync(objeto);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T objeto)
        {
            _context.Set<T>().Update(objeto);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T objeto)
        {
            _context.Set<T>().Remove(objeto);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetEntityById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> List()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

    }
}
