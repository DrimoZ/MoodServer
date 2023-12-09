using Application.Dtos.Images;
using Application.UseCases.Images;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Images;

[ApiController]
[Route("api/v1/image")]
public class ImageController:ControllerBase
{

    private readonly UseCaseCreateImage _useCaseCreateImage;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;

    public ImageController(UseCaseCreateImage caseCreateImage, IMapper mapper, IWebHostEnvironment env)
    {
        _useCaseCreateImage = caseCreateImage;
        _mapper = mapper;
        _env = env;
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
        if (image != null)
        {
            var fileName = Path.GetFileName(image.Name);
            var dtoInput = new DtoInputImage
            {
                Path = fileName
            };
            
            var img = Create(dtoInput);

            fileName = img.Path + ".png";
            
            Console.WriteLine(_env.WebRootPath);
            
            var filePath = Path.Combine("img",fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
}