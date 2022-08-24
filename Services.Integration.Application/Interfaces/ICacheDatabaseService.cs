namespace Services.Integration.Application.Interfaces;

public interface ICacheDatabaseService {
    Task<string> GetValueAsync(string key);

    Task<bool> SetValueAsync(string key, string value);
}
