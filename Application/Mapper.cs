using System.ComponentModel.Design;
using Application.Dtos.Account;
using Application.Dtos.Publication;
using Application.Dtos.User;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Microsoft.Identity.Client;
using WebApi.Services;

namespace Application;

public class Mapper: Profile
{
    private readonly IdService _idService = new IdService();
    public Mapper()
    {
        //Users
        CreateMap<DbUser, DtoOutputUser>();
        CreateMap<DtoInputUser, DbUser>();
        
        //Account
        CreateMap<DbAccount, DtoOutputAccount>();
        CreateMap<DtoInputAccount, DbAccount>()
            .BeforeMap((s, d) => d.Id = _idService.GenerateRandomId(32) );
        
        //Publication
        CreateMap<DbPublication, DtoOutputPublication>();

    }
}