using AutoMapper;
using QB.Application.AutoMapper.Profiles;

namespace QB.UnitTests.Base
{
    public abstract class BaseSetupTest
    {
        protected readonly IMapper _mapper;

        protected BaseSetupTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new ResponseToDtoProfile()));
            _mapper = new Mapper(configuration);
        }
    }
}
