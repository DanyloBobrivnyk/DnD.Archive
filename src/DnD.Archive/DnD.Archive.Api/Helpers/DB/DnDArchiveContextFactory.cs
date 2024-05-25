using DnD.Archive.Api.Models;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace DnD.Archive.Api.Helpers.DB
{
    public class DnDArchiveContextFactory : IDesignTimeDbContextFactory<DnDArchiveContext>
    {
        public DnDArchiveContext CreateDbContext(string[] args)
        {
            const string ENV_VARIABLE_NAME = "SQLCONNSTR_DnDArchive";
            var connectionString = Environment.GetEnvironmentVariable(ENV_VARIABLE_NAME);
            var connectionStr = "Data Source=(localdb)\\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            /*if (connectionString == null)
            {
                throw new ArgumentNullException(ENV_VARIABLE_NAME);
            }*/

            var optionsBuilder = new DbContextOptionsBuilder<DnDArchiveContext>();
            optionsBuilder.UseSqlServer(connectionStr);

            return new DnDArchiveContext(optionsBuilder.Options);
        }
    }

}
