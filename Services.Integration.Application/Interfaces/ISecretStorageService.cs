using Services.Integration.Application.Interfaces;

namespace Services.Integration.Application.Interfaces;

public interface ISecretStorageService {
    Task<string> GetSecretAsync(string nameSecret);
}
