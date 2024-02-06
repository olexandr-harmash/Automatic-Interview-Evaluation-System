using AutomaticInterviewEvaluationSystem.Evaluation.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutomaticInterviewEvaluationSystem.Evaluation.API.Apis
{
    public static class EvaluationApi
    {
        public static IEndpointRouteBuilder MapEvaluationApi(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPost("position/", CreateEvaluationPosition);
            endpointRouteBuilder.MapPost("position/{positionId:int}/candidate/", CreateEvaluationCandidate);
            endpointRouteBuilder.MapPost("position/candidate/{candidateId:int}/work/", CreateEvaluationUnitOfWork);

            endpointRouteBuilder.MapGet("position/{positionId:int}/", GetEntiresByPositionId);

            return endpointRouteBuilder;
        }

        private static async Task<Results<NotFound, Ok<EvaluationPositionEntiresResult>>> GetEntiresByPositionId(
            int positionId,
            EvaluationContext context)
        {
            var evaluationUnitOfWorks = await context.EvaluationUnitOfWorks
                .Include(u => u.Requirements)
                .Where(u => u.EvaluationCandidate.PositionId == positionId)
                .ToListAsync();

            if (evaluationUnitOfWorks is null)
            {
                return TypedResults.NotFound();
            }
            
            return TypedResults.Ok(new EvaluationPositionEntiresResult(positionId, evaluationUnitOfWorks));
        }

        public static async Task<Ok<int>> CreateEvaluationPosition(
        EvaluationContext context,
        EvaluationPosition positionToAdd)
        {
            var evaluationPosition = new EvaluationPosition
            {
               PositionId = positionToAdd.PositionId,
            };

            context.EvaluationPositions.Add(evaluationPosition);
            await context.SaveChangesAsync();

            return TypedResults.Ok(evaluationPosition.Id);
        }

        public static async Task<Results<Ok<int>, BadRequest>> CreateEvaluationCandidate(
        int positionId,
        EvaluationContext context,
        EvaluationCandidate candidateToAdd)
        {
            var evaluationPosition = await context.EvaluationPositions.FindAsync(positionId);

            if (evaluationPosition is null)
            {
                return TypedResults.BadRequest();
            };

            var evaluationCandidate = new EvaluationCandidate
            {
                PositionId = positionId,
                CandidateId = candidateToAdd.CandidateId,
            };

            context.EvaluationCandidates.Add(evaluationCandidate);
            await context.SaveChangesAsync();

            return TypedResults.Ok(evaluationCandidate.Id);
        }

        public static async Task<Results<Ok<int>, BadRequest>> CreateEvaluationUnitOfWork(
        int candidateId,
        [AsParameters] EvaluationServices services)
        {
            var evaluationCandidate = await services.Context.EvaluationCandidates.FindAsync(candidateId);

            if (evaluationCandidate is null)
            {
                return TypedResults.BadRequest();
            };

            var evaluationUnitOfWork = new EvaluationUnitOfWork
            {
                CandidateId = candidateId,
                Requirements = new (),
            };

            services.Context.EvaluationUnitOfWorks.Add(evaluationUnitOfWork);
            await services.Context.SaveChangesAsync();

            var workCreatedEvent = new CreatedUnitOfWorkIntegrationEvent(evaluationUnitOfWork.Id);

            await services.EventService.PublishThroughEventBusAsync(workCreatedEvent);

            return TypedResults.Ok(evaluationUnitOfWork.Id);
        }
    }
}
