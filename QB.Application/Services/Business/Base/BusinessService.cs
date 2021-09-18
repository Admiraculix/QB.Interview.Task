using AutoMapper;
using QB.Application.Interfaces.InfrastuctureServices;
using QB.Application.Interfaces.Services.Business.Base;

namespace QB.Application.Services.Business.Base
{
    public abstract class BusinessService : IBusinessService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BusinessService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
