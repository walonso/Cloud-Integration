using Services.Integration.Application.Interfaces;
using Services.Integration.Application.Config;

namespace Services.Integration.Application.Services;

public class CacheDatabaseService : ICacheDatabaseService
{
    private ICacheDatabase _cacheDatabase;
    public CacheDatabaseService(ICacheDatabase cacheDatabase) {
        this._cacheDatabase=cacheDatabase;
    }
    
    public async Task<string> GetValueAsync(string key){
        string value= await this._cacheDatabase.GetValueAsync(key);

        return value;
    }

    public async Task<bool> SetValueAsync(string key, string value) {
        bool response= await this._cacheDatabase.SetValueAsync(key, value);

        return response;
    }
}
