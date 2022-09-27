using Services.Integration.Application.Interfaces;
using Services.Integration.Application.Config;

namespace Services.Integration.Application.Services;

public class SecretStorageService : ISecretStorageService {

    private ISecretStorage _secretStorage;
    public SecretStorageService(ISecretStorage secretStorage) {
        this._secretStorage=secretStorage;
    }

    public async Task<string> GetSecretAsync(string nameSecret){
        SecretStorageConfig storageConfig = new SecretStorageConfig();
        storageConfig.NameStorageSecret= "bootcampcloudkv";
        this._secretStorage.SetConfig(storageConfig);

        return await this._secretStorage.GetSecretAsync(nameSecret);
    }
}

