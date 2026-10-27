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
                .TwoWays();

            config.NewConfig<Poll, PollResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Summary, src => src.Summary)
                .Map(dest => dest.StartsAt, src => src.StartsAt)
                .Map(dest => dest.EndsAt, src => src.EndsAt)
                .Map(dest => dest.IsPublished, src => src.IsPublished);
                //.Map(dest => dest.CreatedBy, src => src.CreatedById)
                //.Map(dest => dest.UpdatedBy, src => src.UpdatedById)
                //.Map(dest => dest.CreatedOn, src => src.CreatedOn)
                //.Map(dest => dest.UpdatedOn, src => src.UpdatedOn);

            // Application User

            config.NewConfig<RegisterRequest, ApplicationUser>()
                .Map(dest=>dest.UserName , src => src.Email)
                .TwoWays();                
        }
    }
}
