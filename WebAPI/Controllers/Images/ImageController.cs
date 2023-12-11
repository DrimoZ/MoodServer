using System.Drawing;
using Application.Dtos.Images;
using Application.UseCases.Images;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Utils;

namespace WebAPI.Controllers.Images;

[ApiController]
[Route("api/v1/image")]
public class ImageController:ControllerBase
{

    private readonly UseCaseCreateImage _useCaseCreateImage;
    private readonly UseCaseGetImageById _useCaseGetImageById;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;

    public ImageController(UseCaseCreateImage caseCreateImage, IMapper mapper, IWebHostEnvironment env, UseCaseGetImageById useCaseGetImageById)
    {
        _useCaseCreateImage = caseCreateImage;
        _mapper = mapper;
        _env = env;
        _useCaseGetImageById = useCaseGetImageById;
    }
    
    private DbImage Create(DtoInputImage img)
    {
        var image = _useCaseCreateImage.Execute(img);
        return image;
    }
    
    [HttpPost]
    // [Authorize]
    [ProducesResponseType(400)]
    public  ActionResult Post(IFormFile image)
    {

        var dtoInput = new DtoInputImage();
        using (var memoryStream = new MemoryStream())
        {
            image.CopyTo(memoryStream);
            dtoInput.Data = memoryStream.ToArray();
        }
        
        var img = Create(dtoInput);
        
        return Ok();
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputImage> GetImageById(int id)
    {
        var entity = _useCaseGetImageById.Execute(id);
        
        return Ok(entity);
    }
}