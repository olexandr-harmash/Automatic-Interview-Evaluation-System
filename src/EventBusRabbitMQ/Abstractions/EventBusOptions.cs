﻿namespace AutomaticInterviewEvaluationSystem.EventBusRabbitMQ.Abstractions
{
    public class EventBusOptions
    {
        public string SubscriptionClientName { get; set; }
        public int RetryCount { get; set; } = 10;
    }
}
