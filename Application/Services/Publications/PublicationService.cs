﻿using Application.Services.Publications.Util;
using AutoMapper;
using Domain;
using Domain.Exception;
using Infrastructure.EntityFramework.DbComplexEntities;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Publications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.Services.Publications;

public class PublicationService: IPublicationService
{
    private readonly IMapper _mapper;
    
    private readonly IPublicationRepository _publicationRepository;
    private readonly IPublicationElementRepository _elementRepository;
    private readonly ILikeRepository _likeRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IFriendRepository _friendRepository;

    public PublicationService(IMapper mapper, IPublicationRepository publicationRepository, IPublicationElementRepository elementRepository, ICommentRepository commentRepository, ILikeRepository likeRepository, IUserRepository userRepository, IAccountRepository accountRepository, IFriendRepository friendRepository)
    {
        _mapper = mapper;
        _publicationRepository = publicationRepository;
        _elementRepository = elementRepository;
        _commentRepository = commentRepository;
        _likeRepository = likeRepository;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _friendRepository = friendRepository;
    }
    
    public IEnumerable<Publication> FetchPublicationsByUserId(string userId)
    {
        return _publicationRepository
            .FetchUserPublications(userId)
            .Select(p => FetchPublicationById(p.PublicationId, Array.Empty<EPublicationFetchAttribute>()));
    }
    
    public IEnumerable<Publication> FetchPublicationsWithoutUserId(string userId, string searchValue)
    {
        return _publicationRepository
            .FetchPublicationsByFilter(userId)
            .Where(comment => 
            {
                try
                {
                    _userRepository.FetchById(comment.UserId);
                    return true;
                }
                catch
                {
                    return false;
                }
            })
            .Where(publication => _friendRepository.IsFriend(userId, publication.UserId) || 
                                   (_userRepository.FetchById(publication.UserId).IsPublic &&
                                    _userRepository.FetchById(publication.UserId).IsPublicationPublic))
            .Where(publication => _userRepository.FetchById(publication.UserId).UserName.ToLower().Contains(searchValue.ToLower()))
            .Select(p => FetchPublicationById(p.PublicationId, Array.Empty<EPublicationFetchAttribute>()));
    }

    public Publication FetchPublicationById(int id, IEnumerable<EPublicationFetchAttribute> attributesToFetch)
    {
        var dbComplex = FetchComplexByPublicationId(id);
        var publication = _mapper.Map<Publication>(dbComplex);
        var dbComments = _commentRepository.FetchCommentsByPublicationId(id).Where(comment => 
        {
            try
            {
                _userRepository.FetchById(comment.UserId);
                return true;
            }
            catch
            {
                return false;
            }
        }).ToList();

        publication.LikeCount = _likeRepository.FetchLikeCountByPublicationId(id);
        publication.CommentCount = dbComments.Count;
        
        foreach (var attribute in attributesToFetch)
        {
            switch (attribute)
            {
                case EPublicationFetchAttribute.Comments:
                    var comments = dbComments.Select(dbComment => _mapper.Map<Comment>(dbComment)).ToList();

                    foreach (var comment in comments)
                    {
                        var dbUser = _userRepository.FetchById(comment.AuthorId);
                        var dbAccount = _accountRepository.FetchById(dbUser.AccountId);

                        comment.AuthorName = dbUser.UserName;
                        comment.AuthorImageId = dbAccount.ImageId;
                        comment.AuthorRole = dbUser.UserRole;

                        publication.Add(comment);
                    }
                    break;
                
                default:
                    throw new ArgumentException($"Unknown attribute: {attribute}");
            }
        }
        return publication;
    }

    private DbComplexPublication FetchComplexByPublicationId(int pubId)
    {
        var complexPublication = _mapper.Map<DbComplexPublication>(_publicationRepository.FetchById(pubId));
        complexPublication.Elements = _elementRepository.FetchElementsByPublicationId(pubId).ToList();
     
        return complexPublication;
    }
}