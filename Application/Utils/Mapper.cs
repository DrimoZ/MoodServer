using Application.Dtos.User;
using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Application.UseCases.Utils;

public class Mapper: Profile
{
    public Mapper()
    {
        //Users
        CreateMap<DbUser, DtoOutputUser>();
    }
}