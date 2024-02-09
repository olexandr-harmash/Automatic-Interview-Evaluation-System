using AutomaticInterviewEvaluationSystem.Evaluation.API.Extensions;
using AutomaticInterviewEvaluationSystem.Evaluation.API.Apis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/api/v1/evaluation")
    .WithTags("Evaluation API")
    .MapEvaluationApi();

app.Run();