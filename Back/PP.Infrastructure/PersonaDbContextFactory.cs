using Microsoft.EntityFrameworkCore.Design;
using PP.Common.Configuration;

namespace PP.Infrastructure;

public class PersonaDbContextFactory : IDesignTimeDbContextFactory<PersonaDbContext>
{
    public PersonaDbContext CreateDbContext(string[] args)
    {
        return CreateDbContext();
    }

    public PersonaDbContext CreateDbContext()
    {
        var config = new PersonaConfigurationBuilder()
            .Build();

        return new PersonaDbContext(config);
    }
}