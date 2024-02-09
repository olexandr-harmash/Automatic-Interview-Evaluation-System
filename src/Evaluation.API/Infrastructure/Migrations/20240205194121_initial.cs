using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Evaluation.API.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PositionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationPosition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationCandidate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CandidateId = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCandidate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationCandidate_EvaluationPosition_PositionId",
                        column: x => x.PositionId,
                        principalTable: "EvaluationPosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationUnitOfWork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CandidateId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationUnitOfWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationUnitOfWork_EvaluationCandidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "EvaluationCandidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationRequirement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BasicProgramingSkills = table.Column<int>(type: "integer", nullable: false),
                    SoftwarePatternsKnowledge = table.Column<int>(type: "integer", nullable: false),
                    UnitOfWorkId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationRequirement_EvaluationUnitOfWork_UnitOfWorkId",
                        column: x => x.UnitOfWorkId,
                        principalTable: "EvaluationUnitOfWork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCandidate_PositionId",
                table: "EvaluationCandidate",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationRequirement_UnitOfWorkId",
                table: "EvaluationRequirement",
                column: "UnitOfWorkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationUnitOfWork_CandidateId",
                table: "EvaluationUnitOfWork",
                column: "CandidateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationRequirement");

            migrationBuilder.DropTable(
                name: "EvaluationUnitOfWork");

            migrationBuilder.DropTable(
                name: "EvaluationCandidate");

            migrationBuilder.DropTable(
                name: "EvaluationPosition");
        }
    }
}
