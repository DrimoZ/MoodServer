using Application.Dtos.Images;
using Application.Services.Utils;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Images;

public class UseCaseCreateImage: IUseCaseWriter<DbImage, DtoInputImage>
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public UseCaseCreateImage(IImageRepository imageRepository, IMapper mapper)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
    }

    public DbImage Execute(DtoInputImage input)
    {
        input.Path = IdService.Generate32CharId(EClassType.Image);
        return _imageRepository.Create(_mapper.Map<DbImage>(input));
    }
}