using Application.Dtos.Account;
using Application.UseCases.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("api/v1/account")]
public class AccountController : ControllerBase
{
    private readonly UseCaseCreateAnAccount _useCaseCreateAn;
    private readonly UseCaseGetAccountById _useCaseGetAccountById;

    public AccountController(UseCaseCreateAnAccount useCaseCreateAn, UseCaseGetAccountById useCaseGetAccountById)
    {
        _useCaseCreateAn = useCaseCreateAn;
        _useCaseGetAccountById = useCaseGetAccountById;
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputAccount> GetAccountById(string id)
    {
        return _useCaseGetAccountById.Execute(id);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputAccount> Create(DtoInputAccount dto)
    {
        
        var accountCreated = _useCaseCreateAn.Execute(dto);
            
        return StatusCode(201, accountCreated);
    }
}