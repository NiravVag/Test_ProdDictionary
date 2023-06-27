using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Civit.ProdDictionary.Business.Dto
{
    public class ProdDictionaryDto
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public IDictionary<string,object> Properties { get; set; }
    }
    public class ChildDictionaryDto  : ProdDictionaryDto
    {
        public string ParentNode { get; set; }
        public List<string> ParentID { get; set; }
    }
    public class CompareValueResponse
    {
        public bool IsNew { get; set; }
        public List<ProdDictionaryDto> Data { get; set; }
    }
    
    public class ProdDictionaryComparer : IEqualityComparer<ProdDictionaryDto>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(ProdDictionaryDto x, ProdDictionaryDto y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.Code == y.Code && x.Value == y.Value;
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.
        public int GetHashCode(ProdDictionaryDto prodDictionary)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(prodDictionary, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProdDictionaryCode = prodDictionary.Code == null ? 0 : prodDictionary.Code.GetHashCode();

            //Get hash code for the Code field.
            int hashProdDictionaryValue = prodDictionary.Value.GetHashCode();

            //Calculate the hash code for the product.
            return hashProdDictionaryCode ^ hashProdDictionaryValue;
        }
    }
}
