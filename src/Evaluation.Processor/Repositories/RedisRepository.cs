using System.Text.Json.Serialization;
using System.Text.Json;

namespace Evaluation.Processor.Repositories
{
    public class RedisRepository(
        ILogger<RedisRepository> logger, 
        IConnectionMultiplexer redis) 
        : IRepository
    {

        private readonly IDatabase _database = redis.GetDatabase();

        // implementation:

        // - /basket/{id} "string" per unique basket
        private static RedisKey BasketKeyPrefix = "/prediction/"u8.ToArray();
        // note on UTF8 here: library limitation (to be fixed) - prefixes are more efficient as blobs

        private static RedisKey GetBasketKey(string userId) => BasketKeyPrefix.Append(userId);

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(GetBasketKey(id));
        }

        public async Task<Prediction> GetPredictionAsync(int unitOfWorkId)
        {
            using var data = await _database.StringGetLeaseAsync(GetBasketKey(unitOfWorkId.ToString()));

            if (data is null || data.Length == 0)
            {
                return null;
            }
            return JsonSerializer.Deserialize(data.Span, PredictionSerializationContext.Default.Prediction);
        }

        public async Task<Prediction> SetPredictionAsync(Prediction prediction)
        {
            var json = JsonSerializer.SerializeToUtf8Bytes(prediction, PredictionSerializationContext.Default.Prediction);
            var created = await _database.StringSetAsync(GetBasketKey(prediction.UnitOfWorkId.ToString()), json);

            if (!created)
            {
                logger.LogInformation("Problem occurred persisting the item.");
                return null;
            }

            logger.LogInformation("Prediction item persisted successfully.");
            return await GetPredictionAsync(prediction.UnitOfWorkId);
        }
    }

    [JsonSerializable(typeof(Prediction))]
    [JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
    public partial class PredictionSerializationContext : JsonSerializerContext
    {

    }
}
