using Services.Integration.Application.Config;

namespace Services.Integration.Application.Interfaces;

public interface ISecretStorage {
    void SetConfig(SecretStorageConfig storageConfig);
    Task<string> GetSecretAsync(string secretName);
}
