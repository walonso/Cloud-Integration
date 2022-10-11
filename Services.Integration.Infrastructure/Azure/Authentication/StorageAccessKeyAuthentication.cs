using Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Integration.Infrastructure.Azure.Authentication
{
    internal class StorageAccessKeyAuthentication
    {
        public StorageSharedKeyCredential GetAuthentication()
        {
            return new StorageSharedKeyCredential("storage account name", "access key");
        }
    }
}
