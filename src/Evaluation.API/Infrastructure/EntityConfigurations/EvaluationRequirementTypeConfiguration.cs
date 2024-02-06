using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evaluation.API.Infrastructure.EntityConfigurations
{
    public class EvaluationRequirementTypeConfiguration : IEntityTypeConfiguration<EvaluationRequirement>
    {
        public void Configure(EntityTypeBuilder<EvaluationRequirement> builder)
        {
            builder.ToTable(nameof(EvaluationRequirement));

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
