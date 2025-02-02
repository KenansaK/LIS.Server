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

    public async Task<T> GetRecordAsync<T>(string key, string apiUrl)
    {
        CacheQueryOption options = new CacheQueryOption { Name = key };
        var jsonData = await _cache.GetStringAsync(options.Name);
        var optionsDes = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        if (jsonData is not null)

            return JsonSerializer.Deserialize<T>(jsonData, optionsDes);
        else
        {

            return await GetFromOtherAPIs<T>(key, apiUrl);


        }


    }

    public async Task<T> GetFromOtherAPIs<T>(string cachekey, string apiUrl)
    {
        // Define the API URL
        //apiUrl = apiUrl + "/" + cachekey;
        try
        {
            // Send a GET request to the API
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Read the response content
            string responseContent = await response.Content.ReadAsStringAsync();

            // Optionally, deserialize the JSON response to a C# object
            var optionsDes = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var result = JsonSerializer.Deserialize<T>(responseContent, optionsDes);

            // Cache the fetched data
            await SetRecordAsync(cachekey, result, TimeSpan.FromMinutes(10));

            // Return the data to the caller
            return result;
        }
        catch (HttpRequestException ex)
        {
            // Handle errors
            return default;
        }
    }


}
