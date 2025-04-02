using AVS.DBLib.Class;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = AVS.DBLib.Class.Type;

public interface ITypeProvider
{
    Task<List<AVS.DBLib.Class.Type>> GetAllTypesAsync();
    Task<Type> GetTypeByIdAsync(int id);
    Task CreateTypeAsync(Type type);
    Task UpdateTypeAsync(Type type);
    Task DeleteTypeAsync(int id);
}

namespace AVS.DBLib.Providers
{
    class TypeProvider : ITypeProvider
    {
        private readonly AvsContext _context;

        public TypeProvider(AvsContext context)
        {
            _context = context;
        }

        public async Task<List<Type>> GetAllTypesAsync()
        {
            return await _context.Types.ToListAsync();
        }

        public async Task<Type> GetTypeByIdAsync(int id)
        {
            return await _context.Types.FindAsync(id);
        }

        public async Task CreateTypeAsync(Type type)
        {
            _context.Types.Add(type);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTypeAsync(Type type)
        {
            _context.Entry(type).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTypeAsync(int id)
        {
            var type = await _context.Types.FindAsync(id);
            if (type != null)
            {
                _context.Types.Remove(type);
                await _context.SaveChangesAsync();
            }
        }
    }
}
