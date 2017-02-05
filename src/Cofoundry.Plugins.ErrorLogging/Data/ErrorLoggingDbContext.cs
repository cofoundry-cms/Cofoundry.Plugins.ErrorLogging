using Cofoundry.Core;
using Cofoundry.Domain.Data;
using System;
using System.Data.Entity;

namespace Cofoundry.Plugins.ErrorLogging.Data
{
    public partial class ErrorLoggingDbContext : DbContext
    {
        #region constructor

        static ErrorLoggingDbContext()
        {
            Database.SetInitializer<ErrorLoggingDbContext>(null);
        }

        public ErrorLoggingDbContext()
            : base(DbConstants.ConnectionStringName)
        {
            DbContextConfigurationHelper.SetDefaults(this);
        }

        #endregion

        #region properties

        public DbSet<Error> Errors { get; set; }

        #endregion
        
        #region mapping

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .UseDefaultConfig(DbConstants.CofoundryPluginSchema)
                .Map(new ErrorMap());
        }

        #endregion
    }
}
