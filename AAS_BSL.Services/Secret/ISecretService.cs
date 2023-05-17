using AAS_BSL.Domain.Dtos;

namespace AAS_BSL.Services.Secret;

public interface ISecretService
{
    Task<int> AddOrGet(SecretUpdateRequestDto request);
}