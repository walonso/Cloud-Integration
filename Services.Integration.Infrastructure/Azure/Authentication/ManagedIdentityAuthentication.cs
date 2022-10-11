using Azure.Identity;

namespace Services.Integration.Infrastructure.Azure.Authentication
{
    public class ManagedIdentityAuthentication : IDefaultAzureCredentialAuthentication
    {
        private string _managedIdentityId;

        public ManagedIdentityAuthentication(string managedIdentity)
        {
            _managedIdentityId = managedIdentity;
        }
        
        public DefaultAzureCredential GetAuthentication()
        {
            var optionCredential = new DefaultAzureCredentialOptions()
            {
                ManagedIdentityClientId = _managedIdentityId
            };
            DefaultAzureCredential credential = new DefaultAzureCredential(optionCredential);
            return credential;
        }
    }
}
