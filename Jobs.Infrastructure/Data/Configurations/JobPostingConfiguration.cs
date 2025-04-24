using Jobs.Domain.Entities;
using Jobs.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobs.Infrastructure.Data.Configurations
{
    public class JobPostingConfiguration : IEntityTypeConfiguration<JobPosting>
    {
        public void Configure(EntityTypeBuilder<JobPosting> builder)
        {
            builder.ConfigureEntityId();

            builder.Property(j => j.Title)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(j => j.Description)
                .HasMaxLength(4096);

            builder.Property(j => j.ContactEmail)
                .HasMaxLength(320);

            builder.Property(j => j.JobUrl)
                .HasMaxLength(2048);
        }
    }
}
