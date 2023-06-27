using Civit.ProdDictionary.Host.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Civit.ProdDictionary.Host.Repository
{
    public interface IProdDictionaryRepo
    {
        IQueryable<PdObjectCatalogue> GetQueryable(int orgId);
        Task <PdObjectCatalogue> GetById(int id);
        Task<PdObjectCatalogue> Insert(PdObjectCatalogue pdObjectCatalogue);
        Task<PdObjectCatalogue> Update(PdObjectCatalogue pdObjectCatalogue);
        Task Delete(int id);
        //void Save();
    }
}
