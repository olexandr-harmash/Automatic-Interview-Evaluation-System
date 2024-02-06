using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evaluation.API.Infrastructure.EntityConfigurations
{
    public class EvaluationUnitOfWorkEntityTypeConfiguration : IEntityTypeConfiguration<EvaluationUnitOfWork>
    {
        public void Configure(EntityTypeBuilder<EvaluationUnitOfWork> builder)
        {
            builder.ToTable(nameof(EvaluationUnitOfWork));

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(u => u.Requirements)
                .WithOne(r => r.EvaluationUnitOfWork)
                .HasForeignKey<EvaluationRequirement>(r => r.UnitOfWorkId);
        }
    }
}
