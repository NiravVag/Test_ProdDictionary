using Civit.ProdDictionary.Host.Context;
using Civit.ProdDictionary.Host.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Civit.ProdDictionary.Host.Repository
{
    public class ProdDictionaryRepo : IProdDictionaryRepo
    {
        private readonly ProdDictionaryDbContext _context;

        public ProdDictionaryRepo(ProdDictionaryDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            PdObjectCatalogue pdObjectCatalogue = await _context.PdObjectCatalogue.FirstOrDefaultAsync(x => x.Id == id);

            if (pdObjectCatalogue != null)
            {
                _context.PdObjectCatalogue.Remove(pdObjectCatalogue);
                await _context.SaveChangesAsync();
            }            
        }

        public async Task<PdObjectCatalogue> GetById(int id)
        {
            return await _context.PdObjectCatalogue.FirstOrDefaultAsync(x => x.Id == id);
        }
        public IQueryable<PdObjectCatalogue> GetQueryable(int orgId)
        {
            return _context.PdObjectCatalogue.Where(x => x.OrgId == orgId && x.IsDeleted != true).AsNoTracking();
        }

        public async Task<PdObjectCatalogue> Insert(PdObjectCatalogue pdObjectCatalogue)
        {
            var data = await _context.PdObjectCatalogue.AddAsync(pdObjectCatalogue);
            await _context.SaveChangesAsync();
            return data.Entity;
        }

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}

        public async Task<PdObjectCatalogue> Update(PdObjectCatalogue pdObjectCatalogue)
        {
            var data = await _context.PdObjectCatalogue.FirstOrDefaultAsync(x => x.Id == pdObjectCatalogue.Id);

            if (data != null)
            {
                data.Code = pdObjectCatalogue.Code;
                data.AppName = pdObjectCatalogue.AppName;
                data.Name = pdObjectCatalogue.Name;
                //data.IsActive = pdObjectCatalogue.IsActive;
                data.UpdatedOn = DateTime.Now;
                //data.UpdatedById = Guid.NewGuid(); //currentloginuserId
                //data.UpdatedByPostId = 201;//willchangePostId
                //data.IsDeleted = false;

                await _context.SaveChangesAsync();

                return data;
            }
            else
            {
                throw new Exception ("User not found");
            }

            return null;
        }
    }
}
