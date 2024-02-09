using Evaluation.API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Infrastructure
{
    public class EvaluationContext: DbContext
    {
        public EvaluationContext(DbContextOptions<EvaluationContext> options, IConfiguration configuration) : base(options)
        {
        }

        public DbSet<EvaluationPosition> EvaluationPositions { get; set; }
        public DbSet<EvaluationCandidate> EvaluationCandidates { get; set; }
        public DbSet<EvaluationUnitOfWork> EvaluationUnitOfWorks { get; set; }
        public DbSet<EvaluationRequirement> EvaluationRequirements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EvaluationCandidateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EvaluationPositionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EvaluationUnitOfWorkEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EvaluationRequirementTypeConfiguration());
        }
    }
}
