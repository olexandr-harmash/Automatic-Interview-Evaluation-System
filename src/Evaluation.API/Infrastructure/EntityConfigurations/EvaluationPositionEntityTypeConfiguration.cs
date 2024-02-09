using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evaluation.API.Infrastructure.EntityConfigurations
{
    public class EvaluationPositionEntityTypeConfiguration : IEntityTypeConfiguration<EvaluationPosition>
    {
        public void Configure(EntityTypeBuilder<EvaluationPosition> builder)
        {
            builder.ToTable(nameof(EvaluationPosition));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.HasMany(p => p.Candidates)
                .WithOne(c => c.EvaluationPosition)
                .HasForeignKey(c => c.PositionId);
        }
    }
}
