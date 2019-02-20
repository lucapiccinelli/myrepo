using Microsoft.Extensions.Configuration;

namespace bend1.Controllers
{
    public class MyConfigs
    {
        public IConfiguration Configuration {get; set;}

        public MyConfigs(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}