using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cofoundry.Plugins.ErrorLogging.Data
{
    public class ErrorMap : EntityTypeConfiguration<Error>
    {
        public ErrorMap()
        {
            // Properties
            this.Property(t => t.ExceptionType)
                .IsRequired();

            this.Property(t => t.Url)
                .HasMaxLength(255);

            this.Property(t => t.Source)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Target)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.StackTrace)
                .IsRequired();

            this.Property(t => t.QueryString)
                .HasMaxLength(255);

            this.Property(t => t.UserAgent)
                .HasMaxLength(255);
        }
    }
}
