using Civit.ProdDictionary.Host.Business;
using Civit.ProdDictionary.Host.Model;
using Civit.ProdDictionary.Host.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Civit.ProdDictionary.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdDictionaryDbController : ControllerBase
    {
        private readonly IProdDictionaryDbAFL _prodDictionaryDbAFL;
        private readonly IProdDictionaryRepo _dictionaryRepo;

        public ProdDictionaryDbController(IProdDictionaryRepo prodEntity, IProdDictionaryDbAFL prodAFL)
        {
            _prodDictionaryDbAFL = prodAFL;
            _dictionaryRepo = prodEntity;
        }

        //Add ProdDictionary
        [HttpPost("AddProdDictionary")]
        public async Task AddProdDictionary(PdObjectCatalogueDto entity)
        {
            await _prodDictionaryDbAFL.AddProdDictionary(entity);
        }

        //Delete ProdDictionary
        [HttpDelete("DeleteProdDictionary")]
        public async Task DeleteProdDictionary(int id)
        {
            await _prodDictionaryDbAFL.DeleteProdDictionaryAsync(id);            
        }

        //Update ProdDictionary
        [HttpPut("UpdateProdDictionary")]
        public async Task UpdateProdDictionary(PdObjectCatalogueDto entity)
        {
            await _prodDictionaryDbAFL.EditProdDictionary(entity);
        }

        //Get ProdDictionary By Id
        [HttpGet("GetProdDictionaryList/{orgId}")]
        public IEnumerable<PdObjectCatalogueDto> GetProdDictionaryList(int orgId)
        {
            return _prodDictionaryDbAFL.GetList(orgId);
        }

        //Get GetProdDictionaryById
        [HttpGet("GetProdDictionary")]
        public async Task<PdObjectCatalogueDto> GetProdDictionary(int id)
        {
            return await _prodDictionaryDbAFL.GetProdDictionaryById(id);
        }
    }
}
