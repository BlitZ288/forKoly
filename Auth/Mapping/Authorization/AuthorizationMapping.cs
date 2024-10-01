using Auth.Application.Models.Authorization;
using Auth.Models.Authorization;
using Mapster;

namespace Auth.Mapping.Authorization
{
    public class AuthorizationMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<LoginRequest, LoginRequestDto>();
            config.NewConfig<LoginResponseDto, LoginResponse>();
            config.NewConfig<LoginResponse, RegisterRequestDto>();
        }
    }
}