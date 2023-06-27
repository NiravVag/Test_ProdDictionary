using Civit.ProdDictionary.Business;
using Civit.ProdDictionary.Business.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Civit.ProdDictionary.Controllers
{
    /// <summary>
    /// Prod Dictionary Controller
    /// </summary>
    [Route("api/[controller]/{version:apiVersion}")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Prod Dictironary API")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProdDictionaryController : ControllerBase
    {
        private IProdDictionaryAFL _prodDictionaryAFL;

        public ProdDictionaryController(IProdDictionaryAFL prodDictionaryAFL)
        {
            _prodDictionaryAFL = prodDictionaryAFL;
            //LocalizationResource = typeof(ProdDictionaryResource);
        }
        /// <summary>
        /// Get Me Values By Node
        /// </summary>
        /// <param name="version"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        [HttpGet]        
        [Route("getMeValues/{node}")]
        public List<ProdDictionaryDto> GetMeValues(string version,string node)
        {            
            return _prodDictionaryAFL.GetProdDictionary(version,node);
        }

        /// <summary>
        /// Get Child values by parent id and parent node
        /// </summary>
        /// <param name="version"></param>
        /// <param name="node"></param>
        /// <param name="parentNode"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getChildValues/{node}")]
        public List<ProdDictionaryDto> GetChildValues(string version,string node,[Required]string parentNode, [Required] string parentID)
        {
            return _prodDictionaryAFL.GetChildNodes(version,node, parentNode, parentID);
        }

        /// <summary>
        /// get node value by code
        /// </summary>
        /// <param name="version"></param>
        /// <param name="node"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getMyValue/{node}")]
        public ProdDictionaryDto GetMyValue(string version, [Required] string node, [Required] string code)
        {
            return _prodDictionaryAFL.GetMyValue(version, node,code);
        }

        /// <summary>
        /// get node value by code
        /// </summary>
        /// <param name="version"></param>
        /// <param name="node"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("compareMyValues/{node}")]
        public CompareValueResponse CompareMyValues(string version, [Required] string node,List<ProdDictionaryDto> prodDictionaries)
        {
            return _prodDictionaryAFL.CompareMyValues(version, node, prodDictionaries);
        }

        /// <summary>
        /// Get Me Values By Node
        /// </summary>
        /// <param name="version"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getMeValues")]
        public IDictionary<string,List<ProdDictionaryDto>> GetValues(string version, List<string> nodes)
        {
            return _prodDictionaryAFL.GetValues(version, nodes);
        }
    }
}
