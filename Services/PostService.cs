
using AnniesTech.Infrastructure.Interfaces;
using AutoMapper;

namespace AnniesTech.Services
{
    public class PostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}