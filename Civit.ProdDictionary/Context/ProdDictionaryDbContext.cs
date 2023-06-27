using Civit.ProdDictionary.Host.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Civit.ProdDictionary.Host.Context
{
    public class ProdDictionaryDbContext : DbContext
    {
        public ProdDictionaryDbContext(DbContextOptions<ProdDictionaryDbContext> options)
            : base(options)
        {

        }

        public DbSet<PdObjectCatalogue> PdObjectCatalogue { get; set; }
    }
}
