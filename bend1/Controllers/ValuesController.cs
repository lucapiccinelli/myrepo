using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace bend1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly MyConfigs _configs;

        public ValuesController(MyConfigs configs)
        {
            _configs = configs;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            string baseUrl = $@"{(_configs.Configuration["hosts:default"])}/api/";
            Console.WriteLine(baseUrl);
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest("values");
            request.AddHeader("Access-Control-Allow-Origin", "*");
            var bend2Values = client.Execute<List<string>>(request, Method.GET);
            return new string[] { "bend1", $"bend2-{bend2Values?.Data?.Last()}"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
