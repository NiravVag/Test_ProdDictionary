using Civit.ProdDictionary.Host.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Civit.ProdDictionary.Host.Business
{
    public interface IProdDictionaryDbAFL
    {
        IEnumerable<PdObjectCatalogueDto> GetList(int orgId);
        Task <PdObjectCatalogueDto> GetProdDictionaryById(int id);
        Task<PdObjectCatalogueDto> AddProdDictionary(PdObjectCatalogueDto prodDto);
        Task EditProdDictionary(PdObjectCatalogueDto prodDto);
        Task DeleteProdDictionaryAsync(int id);
    }
}
