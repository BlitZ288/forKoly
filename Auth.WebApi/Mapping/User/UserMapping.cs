using Auth.Application.Models.User;
using Auth.Models.User;
using Mapster;

namespace Auth.Mapping.User
{
    public class UserMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, UserModel>()
                .Map(dest => dest.Name, src => src.UserName);

            config.NewConfig<UserModel, Auth.Domain.Entities.User>();
        }
    }
}
