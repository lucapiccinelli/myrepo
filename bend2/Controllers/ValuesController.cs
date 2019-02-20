using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace bend2.Controllers
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
            string connectionStr = _configs.Configuration["ConnectionStrings:DefaultConnection"];
            Console.WriteLine(connectionStr);
            using (MySqlConnection connection = new MySqlConnection(connectionStr))
            {
                connection.Open();
                var cmd = connection.CreateCommand() as MySqlCommand;
                cmd.CommandText = "select * from test_tb";
                MySqlDataReader result = cmd.ExecuteReader();
                int count = 0;
                List<int> values = new List<int>();
                while(result.Read())
                {
                    int value = (int)result.GetValue(count++);
                    values.Add(value);
                }
                connection.Close();
                return values.Select(v => v.ToString()).ToArray();
            }
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
