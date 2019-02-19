using Microsoft.Extensions.Configuration;

namespace bend2
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