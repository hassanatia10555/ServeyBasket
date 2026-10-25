using Core.Contracts.Auth.Register;
using Core.Contracts.Poll;

namespace Core.Mapping
{
    public class MappingConfigurations  : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Poll

            config.NewConfig<PollRequest, Poll>()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Summary, src => src.Summary)
                .Map(dest => dest.StartsAt, src => src.StartsAt)
                .Map(dest => dest.EndsAt, src => src.EndsAt)
                .Map(dest => dest.IsPublished, src => src.IsPublished ?? false)
                .TwoWays();

            config.NewConfig<Poll, PollResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Summary, src => src.Summary)
                .Map(dest => dest.StartsAt, src => src.StartsAt)
                .Map(dest => dest.EndsAt, src => src.EndsAt)
                .Map(dest => dest.IsPublished, src => src.IsPublished);

            // Application User

            config.NewConfig<RegisterRequest, ApplicationUser>()
                .Map(dest=>dest.UserName , src => src.Email)
                .TwoWays();                
        }
    }
}
