using Civit.ProdDictionary.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Civit.ProdDictionary.Business
{
    public interface IProdDictionaryAFL
    {
        List<ProdDictionaryDto> GetProdDictionary(string version, string node);

        List<ProdDictionaryDto> GetChildNodes(string version, string node, string parentNode, string parentId);
        ProdDictionaryDto GetMyValue(string version, string stringNode, string code);
        CompareValueResponse CompareMyValues(string version, string node, List<ProdDictionaryDto> input);
        IDictionary<string, List<ProdDictionaryDto>> GetValues(string version, List<string> nodes);
    }
}
