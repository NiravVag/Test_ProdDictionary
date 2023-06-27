using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Civit.ProdDictionary.Host.Business
{
    public static class ProdDictionaryDomainErrorCodes
    {
        public const string VersionWasNotExist = "Requested Key does not exist in database";
        public const string NodeWasNotExist= "Requested Key does not exist in database";
        public const string ParentNodeWasNotExist = "Requested Key does not exist in database";
        public const string CodeWasNotExist = "Requested Code does not exist in database";
    }
}
