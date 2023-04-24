using Microsoft.Extensions.Configuration;

namespace Service;

public interface IConfigurationService
{
    IConfiguration Configuration { get; }
}

public class ConfigurationService : IConfigurationService
{
    private int count = 0;
    public IConfiguration Configuration { get; }

    public ConfigurationService(IConfiguration configuration)
    {
        Configuration = configuration;
    }
}