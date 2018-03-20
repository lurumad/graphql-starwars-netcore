using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using StarWars.Api.Models;
using System.Threading.Tasks;

namespace StarWars.Api.Controllers
{
    [Route("graphql")]
    public class GraphQLController : Controller
    {
        private readonly IDocumentExecuter executer;
        private readonly ISchema schema;

        public GraphQLController(
            IDocumentExecuter executer,
            ISchema schema)
        {
            this.executer = executer;
            this.schema = schema;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Execute([FromBody] GraphQLQuery query)
        {
            var result = await executer.ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
            }).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
