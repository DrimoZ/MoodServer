using Application.UseCases.Accounts;
using Microsoft.AspNetCore.Authorization;
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
    
    [HttpGet("id={id:int")]
    public IActionResult getAccountById(int id)
    {
        return OkResult(_useCaseGetAccountById.Execute(id));
    }
}