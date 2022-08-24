namespace Services.Integration.Application.Interfaces;

public interface ICacheDatabase {
    Task<string> GetValueAsync(string key);

     Task<bool> SetValueAsync(string key, string value);
}
