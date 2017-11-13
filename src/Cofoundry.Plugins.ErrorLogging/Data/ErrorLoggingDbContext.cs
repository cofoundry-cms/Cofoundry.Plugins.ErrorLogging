using Cofoundry.Core;
using Cofoundry.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Cofoundry.Plugins.ErrorLogging.Data
{
    public partial class ErrorLoggingDbContext : DbContext
    {
        #region constructor

        private readonly ILoggerFactory _loggerFactory;
        private readonly DatabaseSettings _databaseSettings;

        public ErrorLoggingDbContext(
            ILoggerFactory loggerFactory,
            DatabaseSettings databaseSettings
            )
        {
            _loggerFactory = loggerFactory;
            _databaseSettings = databaseSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.UseSqlServer(_databaseSettings.ConnectionString);
        }

        #endregion

        #region properties

        public DbSet<Error> Errors { get; set; }

        #endregion
        
        #region mapping

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseDefaultConfig(DbConstants.CofoundryPluginSchema)
                .ApplyConfiguration(new ErrorMap());
        }

        #endregion
    }
}
