using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evaluation.API.Infrastructure.EntityConfigurations
{
    public class EvaluationCandidateEntityTypeConfiguration : IEntityTypeConfiguration<EvaluationCandidate>
    {
        public void Configure(EntityTypeBuilder<EvaluationCandidate> builder)
        {
            builder.ToTable(nameof(EvaluationCandidate));

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.HasMany(c => c.UnitOfWorks)
                .WithOne(u => u.EvaluationCandidate)
                .HasForeignKey(c => c.CandidateId);
        }
    }
}
