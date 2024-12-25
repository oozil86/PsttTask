using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PsttTask.Domain.Entities;

namespace PsttTask.Infrastucture.EntitiesConfigurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable(nameof(Branch)).HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();

            builder.HasIndex(c => c.Reference).IsUnique();
            builder.Property(c => c.Reference).HasDefaultValueSql("newid()").ValueGeneratedOnAdd();

            builder.HasOne(c => c.Company).WithOne(c => c.Branch);

        }
    }
}
