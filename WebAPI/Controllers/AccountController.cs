using Application.Dtos.Account;
using Application.UseCases.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("api/v1/account")]
public class AccountController
{
    private readonly UseCaseCreateAnAccount _useCaseCreateAn;
    private readonly UseCaseGetAccountById _useCaseGetAccountById;

    public AccountController(UseCaseCreateAnAccount useCaseCreateAn, UseCaseGetAccountById useCaseGetAccountById)
    {
        _useCaseCreateAn = useCaseCreateAn;
        _useCaseGetAccountById = useCaseGetAccountById;
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputAccount> GetAccountById(int id)
    {
        return _useCaseGetAccountById.Execute(id);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<DtoInputAccount> CreateAnAccount
    {
        
    }
}