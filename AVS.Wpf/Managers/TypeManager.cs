using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = AVS.DBLib.Class.Type;

namespace AVS.Wpf.Managers
{
    public interface ITypeManager
    {
        Task<List<Type>> GetAllTypesAsync();
        Task<Type> GetTypeByIdAsync(int id);
        Task<bool> CreateTypeAsync(Type type);
        Task<bool> UpdateTypeAsync(Type type);
        Task<bool> DeleteTypeAsync(int id);
    }

    public class TypeManager : ITypeManager
    {
        private readonly ITypeProvider TypeProvider;

        public TypeManager(ITypeProvider provider)
        {
            TypeProvider = provider;
        }

        public async Task<List<Type>> GetAllTypesAsync()
        {
            return await TypeProvider.GetAllTypesAsync();
        }

        public async Task<Type> GetTypeByIdAsync(int id)
        {
            return await TypeProvider.GetTypeByIdAsync(id);
        }

        public async Task<bool> CreateTypeAsync(Type type)
        {
            // Validation
            if (string.IsNullOrEmpty(type.Nom))
                return false;

            await TypeProvider.CreateTypeAsync(type);
            return true;
        }

        public async Task<bool> UpdateTypeAsync(Type type)
        {
            // Validation
            if (string.IsNullOrEmpty(type.Nom))
                return false;

            await TypeProvider.UpdateTypeAsync(type);
            return true;
        }

        public async Task<bool> DeleteTypeAsync(int id)
        {
            try
            {
                await TypeProvider.DeleteTypeAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
