using Microsoft.Extensions.Configuration;

namespace bend1.Controllers
{
    public class MyConfigs
    {
        private IConfiguration Configuration {get; set;}

        public MyConfigs(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}