using DnD.Archive.Api.Models;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace DnD.Archive.Api.Helpers.DB
{
    public class DnDArchiveContextFactory : IDesignTimeDbContextFactory<DnDArchiveContext>
    {
        private string _dbConnectionString = null!;

        public DnDArchiveContextFactory(string connectionString)
        {
            _dbConnectionString = connectionString;
        }

        public DnDArchiveContext CreateDbContext(string[] args)
        {
            if (_dbConnectionString== null)
            {
                throw new ArgumentNullException("Connection string was not provided.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<DnDArchiveContext>();
            optionsBuilder.UseSqlServer(_dbConnectionString);

            return new DnDArchiveContext(optionsBuilder.Options);
        }
    }

}
