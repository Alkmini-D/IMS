using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EFCore
{
    public class AssemblyRepository : IAssemblyRepository
    {
        private readonly IMSContext db;

        public AssemblyRepository(IMSContext db)
        {
            this.db = db;
        }

        public async Task AddAssemblyAsync(Assembly assembly)
        {
            //if (db.Assemblies.Any(x => x.AssemblyName.Equals(assembly.AssemblyName, StringComparison.OrdinalIgnoreCase))) return;
            if (db.Assemblies.Any(x => x.AssemblyName.ToLower() == assembly.AssemblyName.ToLower())) return;

            db.Assemblies.Add(assembly);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAssemblyAsync(int assemblyId)
        {
        
            var assembly = await db.Assemblies.FindAsync(assemblyId);
            if (assembly != null)
            {
                assembly.IsActive = false;
                await db.SaveChangesAsync();
            }
        }

        public async Task<Assembly> GetAssemblyByIdAsync(int assemblyId)
        {
        
            return await db.Assemblies.Include(x => x.AssemblyInventories)
                .ThenInclude(x => x.Inventory)
                .FirstOrDefaultAsync(x => x.AssemblyId == assemblyId);
        }

        public async Task<List<Assembly>> GetAssembliesByNameAsync(string name)
        {
            return await this.db.Assemblies.Where(x => (x.AssemblyName.ToLower().IndexOf(name.ToLower()) >= 0 ||
                                                    string.IsNullOrWhiteSpace(name)) &&
                                                    x.IsActive == true).ToListAsync();
        }

        public async Task UpdateAssemblyAsync(Assembly assembly)
        {
            //prevent same name
            if (db.Assemblies.Any(x => x.AssemblyName.ToLower() == assembly.AssemblyName.ToLower())) return;

            var prod = await db.Assemblies.FindAsync(assembly.AssemblyId);
            if (prod != null)
            {
                prod.AssemblyName = assembly.AssemblyName;
                prod.Price = assembly.Price;
                prod.Quantity = assembly.Quantity;
                prod.AssemblyInventories = assembly.AssemblyInventories;

                await db.SaveChangesAsync();
            }
        }
    }
}

