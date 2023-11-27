using Application.Dtos.Publication;
using Application.Dtos.User;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;

namespace Application;

public class Mapper: Profile
{
    public Mapper()
    {
        //Users
        CreateMap<DbUser, DtoOutputUser>();
        CreateMap<DtoInputUser, DbUser>();
        
        //Account
        
        
        //Publication
        CreateMap<DbPublication, DtoOutputPublication>();

    }
}