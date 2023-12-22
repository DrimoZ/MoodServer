using Application.Dtos.Publication;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories.Publications;

namespace Application.UseCases.Publications;

public class UseCaseLikePublication: IUseCaseParameterizedWriter<bool, string, DtoInputLikePublication>
{
    private readonly ILikeRepository _likeRepository;
    private readonly IMapper _mapper;

    public UseCaseLikePublication(ILikeRepository likeRepository, IMapper mapper)
    {
        _likeRepository = likeRepository;
        _mapper = mapper;
    }

    public bool Execute(string connectedUserId, DtoInputLikePublication dto)
    {
        var dbLike = _likeRepository.FetchLikeByUserAndPublication(connectedUserId, dto.PublicationId);

        if (dto.IsLiked)
        {
            if (dbLike == null)
            {
                var newDbLike = _mapper.Map<DbLike>(dto);
                newDbLike.UserId = connectedUserId;
                
                _likeRepository.Create(newDbLike);
            }
            else
            {
                _likeRepository.UpdateDate(dbLike.LikeId);
            }

             
        }
        else
        {
            if (dbLike != null)
            {
                _likeRepository.Delete(dbLike.LikeId);
            }
        }


        return true;
    }
}