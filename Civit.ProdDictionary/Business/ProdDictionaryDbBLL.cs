using Civit.ProdDictionary.Host.Model;
using Civit.ProdDictionary.Host.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Civit.ProdDictionary.Host.Business
{
    public class ProdDictionaryDbBLL : IProdDictionaryDbAFL
    {
        private readonly IProdDictionaryRepo _repo;
        private readonly HttpContext _httpContext;

        public ProdDictionaryDbBLL(IProdDictionaryRepo repo, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public async Task<PdObjectCatalogueDto> AddProdDictionary(PdObjectCatalogueDto prodDictionaryDto)
        {
            string UserId = this._httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            PdObjectCatalogue data = new PdObjectCatalogue()
            {
                Id = prodDictionaryDto.Id,
                OrgId = prodDictionaryDto.OrgId,
                Code = prodDictionaryDto.Code,
                AppName = prodDictionaryDto.AppName,
                Name = prodDictionaryDto.Name,
                CreatedOn = DateTime.Now,
                //CreatedById = Guid.Parse(UserId), //currentloginuserId
                //CreatedByPostId = 0,//willchangePostId
                //IsDeleted = false,
            };

            var prodDictionaryCatalogue = await _repo.Insert(data);

            PdObjectCatalogueDto prodDto = new PdObjectCatalogueDto()
            {
                Id = prodDictionaryCatalogue.Id,
                OrgId = prodDictionaryCatalogue.OrgId,
                Code = prodDictionaryCatalogue.Code,
                AppName = prodDictionaryCatalogue.AppName,
                Name = prodDictionaryCatalogue.Name
            };

            return prodDto;
        }

        public async Task DeleteProdDictionaryAsync(int id)
        {
            string UserId = this._httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var data = await _repo.GetById(id);
            if (data != null)
            {
                data.IsDeleted = true;
                data.DeletedOn = DateTime.Now;
                //data.DeletedById = Guid.Parse(UserId); //currentloginuserId
                //entity.DeletedByPostId = 123; //willchangePostId
            }
            await _repo.Update(data);
        }

        public async Task EditProdDictionary(PdObjectCatalogueDto prodDictionaryDto)
        {
            string UserId = this._httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            PdObjectCatalogue data = new PdObjectCatalogue()
            {
                Id = prodDictionaryDto.Id,
                OrgId = prodDictionaryDto.OrgId,
                Code = prodDictionaryDto.Code,
                AppName = prodDictionaryDto.AppName,
                Name = prodDictionaryDto.Name,
                UpdatedOn = DateTime.Now,
                //UpdatedById = Guid.Parse(UserId), //currentloginuserId
                //UpdatedByPostId = 201,//willchangePostId
                //IsDeleted = false,                
            };

            await _repo.Update(data);
        }

        public async Task<PdObjectCatalogueDto> GetProdDictionaryById(int id)
        {
            var prodDictionary= await _repo.GetById(id);
            return new PdObjectCatalogueDto()
            {
                Id = prodDictionary.Id,
                OrgId = prodDictionary.OrgId,
                Code = prodDictionary.Code,
                AppName = prodDictionary.AppName,
                Name = prodDictionary.Name                            
            };
        }

        public IEnumerable<PdObjectCatalogueDto> GetList(int orgId)
        {
            return _repo.GetQueryable(orgId).Select(y => new PdObjectCatalogueDto()
            {
                Id = y.Id,
                OrgId = y.OrgId,
                Code = y.Code,
                AppName = y.AppName,
                Name = y.Name
            }).ToList();
        }
    }
}
