using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Services.Integration.Application.Interfaces;
using Services.Integration.Application.Config;

namespace Services.Integration.Infrastructure.Azure;
public class KeyVaultIntegration : ISecretStorage
{ 
    private SecretStorageConfig _secretStorageConfig;

    public void SetConfig(SecretStorageConfig storageConfig){
        this._secretStorageConfig=storageConfig;
    }
    
    public async Task<string> GetSecretAsync(string secretName) {        
        string keyVaultUrl = $"https://{this._secretStorageConfig.NameStorageSecret}.vault.azure.net";

        // Use userAssignedClientId setting application when keyvault access is given to an user assigned managed or leave it empty when A system assigned managed identity is used.
        var credentials = new DefaultAzureCredential();// new DefaultAzureCredentialOptions() { ExcludeManagedIdentityCredential=true });
         /* var credentials = String.IsNullOrWhiteSpace(this._secretStorageConfig.AuthenticationConnectionString) ? 
             new DefaultAzureCredential() : 
             new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = this._secretStorageConfig.AuthenticationConnectionString }); */

         SecretClient client = new SecretClient(new Uri(keyVaultUrl), credentials);
        var secret = await client.GetSecretAsync(secretName);
        
        return secret.Value.Value;    
    }
}