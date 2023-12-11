using Application.Dtos.Images;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Images;

public class UseCaseGetImageById:IUseCaseParameterizedQuery<DtoOutputImage, int>
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public UseCaseGetImageById(IImageRepository imageRepository, IMapper mapper)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
    }

    public DtoOutputImage Execute(int id)
    {
        var dbImage = _imageRepository.FetchById(id);
        return _mapper.Map<DtoOutputImage>(dbImage);
    }
}