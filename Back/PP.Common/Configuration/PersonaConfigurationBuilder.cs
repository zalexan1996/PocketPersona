using Microsoft.Extensions.Configuration;

namespace PP.Common.Configuration;

public class PersonaConfigurationBuilder
{
    public IConfigurationRoot Build(IConfigurationBuilder ?builder = null)
    {
        if (builder is null)
        {
            builder = new ConfigurationBuilder();
        }

        builder.AddUserSecrets<PersonaConfigurationBuilder>();
        builder.AddJsonFile("appsettings.json", true);
        
        return builder.Build();
    }
}