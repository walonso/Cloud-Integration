using Azure.Core;
using Azure.Identity;

namespace Services.Integration.Infrastructure.Azure.Authentication
{
    public class LocalAuthentication : IDefaultAzureCredentialAuthentication
    {
        public DefaultAzureCredential GetAuthentication()
        {
            DefaultAzureCredentialOptions options = new DefaultAzureCredentialOptions
            {            
                ExcludeAzureCliCredential = true,
                ExcludeAzurePowerShellCredential = true,
                ExcludeManagedIdentityCredential = true,
                ExcludeEnvironmentCredential = true
            };
            DefaultAzureCredential credential = new DefaultAzureCredential(options);            
            return credential;
        }
    }
}
