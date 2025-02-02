namespace Kernal.Interfaces;


public interface ICacheService
{

    Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null);
    Task<T> GetRecordAsync<T>(string cachekey, string apiUrl);
    Task<T> GetFromOtherAPIs<T>(string cachekey, string apiUrl);
}