using System.Text.Json;
using Kernal.Caching;
using Kernal.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Kernal.Chaching;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly HttpClient _httpClient;

    public CacheService(IDistributedCache cache, HttpClient httpClient)
    {
        _cache = cache;
        _httpClient = httpClient;
    }

    // Method to set a record in the cache
    public async Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(300),
            SlidingExpiration = unusedExpireTime
        };

        var jsonData = JsonSerializer.Serialize(data);
        await _cache.SetStringAsync(recordId, jsonData, options);
    }

    // Generic method to get a record from cache, or from DB if not found
    public async Task<T> GetRecordAsync<T>(string controllerName,string propertyName, string propertyValue, string baseUrl)
    {
        // string controllerName = typeof(T).Name.Replace("Dto", "").ToLower(); // Example: BranchDto -> branch
        string cacheKey = $"{controllerName}:{propertyName}:{propertyValue}";

        var jsonData = await _cache.GetStringAsync(cacheKey);
        var optionsDes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        if (jsonData is not null)
            return JsonSerializer.Deserialize<T>(jsonData, optionsDes);
    
        return await GetFromOtherAPIs<T>(controllerName, propertyName, propertyValue, cacheKey, baseUrl);
    }

    public async Task<T> GetFromOtherAPIs<T>(string controllerName, string propertyName, object propertyValue, string cacheKey, string baseUrl)
    {
        try
        {
            var url = new Uri($"{baseUrl}/api/{controllerName}/GetFromDB?{propertyName}={propertyValue}");
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();

            var optionsDes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<T>(responseContent, optionsDes);

            await SetRecordAsync(cacheKey, result, TimeSpan.FromMinutes(10));
            return result;
        }
        catch (HttpRequestException)
        {
            return default;
        }
    }

}
