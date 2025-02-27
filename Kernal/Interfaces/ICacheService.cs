namespace Kernal.Interfaces;


public interface ICacheService
{

    Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null);
    Task<T?> GetRecordAsync<T>(string controllerName,string propertyName, string propertyValue, string baseUrl);
    Task<T?> GetFromOtherAPIs<T>(string controllerName, string propertyName, object propertyValue, string cacheKey, string baseUrl);
}