using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Civit.ProdDictionary.Host.Model
{
    public class PdObjectCatalogue
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public string Code { get; set; }
        public string AppName { get; set; }
        public string Name { get; set; }
        public byte IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedById { get; set; }
        public int CreatedByPostId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Guid UpdatedById { get; set; }
        public int UpdatedByPostId { get; set; }
        public Boolean IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public Guid DeletedById { get; set; }
        public int DeletedByPostId { get; set; }

    }

    public class PdObjectCatalogueDto
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public string Code { get; set; }
        public string AppName { get; set; }
        public string Name { get; set; }
    }
}
