using Azure.Core;
using Azure.Identity;

namespace Services.Integration.Infrastructure.Azure.Authentication
{
    public interface IDefaultAzureCredentialAuthentication
    {
        public DefaultAzureCredential GetAuthentication();
    }
}
