using DesafioPicPay.Dto;
using DesafioPicPay.Exception;
using DesafioPicPay.Models;
using DesafioPicPay.Resources;
using DesafioPicPay.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioPicPay.Controllers;

[ApiController]
[Route("/transfer")]
public class TransferController : ControllerBase
{
    private readonly DatabaseContext _context;
    public TransferController(DatabaseContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> Transfer([FromBody] TransferDto request)
    {
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                var payee = await _context.User.FindAsync(request.Payee);
                var payer = await _context.User.FindAsync(request.Payer);

                var validator = new TransferValidator(_context).Validate(request);
                if(!validator.IsValid)
                {
                    return BadRequest(new {
                        error = validator.ToDictionary()
                    });
                }

                if(request.Value > payer!.Balance)
                {
                    throw new BaseException(ErrorMessages.InsuficientValue);
                }
                

                payer!.Balance -= request.Value;
                await NotificationService.Notification(); 
                payee!.Balance += request.Value;

                var transferEntity = new Transfer
                {
                    Payee = payee,
                    Payer = payer,
                    Value = request.Value
                };
                
                await _context.Transfer.AddAsync(transferEntity);
                await _context.SaveChangesAsync();


                await AuthorizationService.Authorization();
                await transaction.CommitAsync();

                return Created(string.Empty, new {
                    transferId = transferEntity.Id
                });
            }
            catch (BaseException ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new {
                    error = ex.Message
                });
            }

            catch
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }
    }
}