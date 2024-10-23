using DesafioPicPay.Dto;
using DesafioPicPay.Models;
using DesafioPicPay.Resources;
using DesafioPicPay.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioPicPay.Controllers;

[ApiController]
[Route("/user")]
public class UserController : ControllerBase
{
    private readonly DatabaseContext _context;
    public UserController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> RegistryUser([FromBody] UserDto request)
    {
        if (request.WalletType.GetHashCode() != 0 && request.WalletType.GetHashCode() != 1)
        {
            return BadRequest(ErrorMessages.WalletType);
        };
        

        var validator = new UserValidator(_context).Validate(request);
        if(!validator.IsValid)
        {
            return BadRequest(new {
                errors = validator.ToDictionary()
            });
        }
        
        var entity = new User
        {
            Document = request.Document,
            Name = request.Name,
            Email = request.Email,
            WalletType = request.WalletType,
            Password = request.Password,
            Balance = request.Balance,
        };
        await _context.User.AddAsync(entity);
        await _context.SaveChangesAsync();
        return Created(string.Empty, new {
            userId = entity.Id
        });
    }
}


