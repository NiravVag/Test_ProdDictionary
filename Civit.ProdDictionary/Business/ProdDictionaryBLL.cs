using Civit.ProdDictionary.Business.Dto;
using Civit.ProdDictionary.Host.Business;
//using Civit.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Civit.ProdDictionary.Business
{
    public class ProdDictionaryBLL : IProdDictionaryAFL
    {
        private string JsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Json", "ProdDictionary.json");
        private JObject prodDictionary;
        public ProdDictionaryBLL()
        {
            prodDictionary = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(JsonPath));
        }

        public List<ProdDictionaryDto> GetChildNodes(string version, string node, string parentNode, string parentId)
        {
            var nodeValue = Validate(version, node);
            List<ChildDictionaryDto> nodes = JsonConvert.DeserializeObject<List<ChildDictionaryDto>>(nodeValue.ToString());
            if (!nodes.Where(x => x.ParentNode == parentNode).Any())
            {
                throw new Exception(ProdDictionaryDomainErrorCodes.ParentNodeWasNotExist);
            }
            return nodes.Where(x => x.ParentNode == parentNode && x.ParentID.Contains(parentId)).Select(y => new ProdDictionaryDto()
            {
                Code = y.Code,
                Value = y.Value
            }).ToList();
        }

        public List<ProdDictionaryDto> GetProdDictionary(string version, string node)
        {

            var nodeValue = Validate(version, node);
            return JsonConvert.DeserializeObject<List<ProdDictionaryDto>>(nodeValue.ToString());
        }

        private JToken Validate(string version, string node)
        {
            if (!prodDictionary.TryGetValue(version, out JToken versionToken))
                throw new Exception(ProdDictionaryDomainErrorCodes.VersionWasNotExist);

            var versionValue = versionToken.ToObject<JObject>();
            if (!versionValue.TryGetValue(node, out JToken nodeValue))
                throw new Exception(ProdDictionaryDomainErrorCodes.NodeWasNotExist);

            return nodeValue;
        }
        private JObject ValidateVersion(string version)
        {
            if (!prodDictionary.TryGetValue(version, out JToken versionToken))
                throw new Exception(ProdDictionaryDomainErrorCodes.VersionWasNotExist);

            var versionValue = versionToken.ToObject<JObject>();
            return versionValue;
        }

        public ProdDictionaryDto GetMyValue(string version, string stringNode, string code)
        {
            var nodeValue = Validate(version, stringNode);
            var nodes = JsonConvert.DeserializeObject<List<ProdDictionaryDto>>(nodeValue.ToString());
            var node = nodes.Where(x => x.Code == code).FirstOrDefault();
            if (node == null)
                throw new Exception(ProdDictionaryDomainErrorCodes.CodeWasNotExist);

            return node;
        }

        public CompareValueResponse CompareMyValues(string version, string node, List<ProdDictionaryDto> input)
        {
            var nodeValues = Validate(version, node);
            var nodeData = JsonConvert.DeserializeObject<List<ProdDictionaryDto>>(nodeValues.ToString());

            if (nodeData.OrderBy(e => e.Code).ThenBy(e => e.Value).SequenceEqual(input.OrderBy(e => e.Code).ThenBy(e => e.Value), new ProdDictionaryComparer()))
                return new CompareValueResponse() { IsNew = false };
            else
                return new CompareValueResponse() { IsNew = true, Data = nodeData };
        }

        public IDictionary<string, List<ProdDictionaryDto>> GetValues(string version, List<string> nodes)
        {
            var versionValue = ValidateVersion(version);
            var undefineNodes=nodes.Where(x => !versionValue.Properties().Select(y => y.Name).Contains(x)).ToList();
            if (undefineNodes.Any())
                throw new Exception(string.Join(", ",undefineNodes)+" Keys are not exist");
            var result = new Dictionary<string, List<ProdDictionaryDto>>();
            foreach (var node in nodes)
            {
                var prodDictionaries = GetProdDictionary(version, node);
                result.Add(node, prodDictionaries);
            }
            return result;
        }
    }
}
