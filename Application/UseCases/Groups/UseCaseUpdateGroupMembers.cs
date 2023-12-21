using Application.Dtos.UserGroup;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;
using Infrastructure.EntityFramework.UnitOfWork;

namespace Application.UseCases.Groups;

public class UseCaseUpdateGroupMembers: IUseCaseWriter<IEnumerable<DtoOutputUserGroup>, IEnumerable<DtoInputUserGroup>>
{
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UseCaseUpdateGroupMembers(IUserGroupRepository userGroupRepository, IGroupRepository groupRepository, IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userGroupRepository = userGroupRepository;
        _groupRepository = groupRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<DtoOutputUserGroup> Execute(IEnumerable<DtoInputUserGroup> input)
    {
        _unitOfWork.BeginTransaction();
        IEnumerable<DtoOutputUserGroup> output = new List<DtoOutputUserGroup>();
        try
        {
            foreach (var dtoInputUserGroup in input)
            {
                _groupRepository.FetchById(dtoInputUserGroup.GroupId);
                _userRepository.FetchById(dtoInputUserGroup.UserId);
                var entity = _userGroupRepository.Create(_mapper.Map<DbUserGroup>(dtoInputUserGroup));
                output = output.Append(_mapper.Map<DtoOutputUserGroup>(entity));
            }
        }
        catch (Exception e)
        {
            _unitOfWork.Rollback();
            Console.WriteLine(e);
            throw;
        }
        _unitOfWork.Commit();
        return output;
    }
}