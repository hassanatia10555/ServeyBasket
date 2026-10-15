using Core.Contracts.Poll;

namespace Core.Mapping
{
    public class MappingConfigurations  : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // poll mapping

            config.NewConfig<Poll, PollRequest>()
                .Map(dest=>dest.Title, src => src.Title)
                .Map(dest=>dest.Description, src => src.Description)
                .TwoWays();
        }
    }
}
