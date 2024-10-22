using FluentValidation;
using DesafioPicPay.Dto;
using DesafioPicPay.Models;
using DesafioPicPay.Resources;

namespace DesafioPicPay.Services;

public class TransferValidator : AbstractValidator<TransferDto>
{
    private readonly DatabaseContext _context; 
    public TransferValidator(DatabaseContext context)
    {

        _context = context;
        
        RuleFor(t => t.Payee)
            .Must(UserExists)
            .WithMessage(ErrorMessages.PayeeError);
        
        RuleFor(t => t.Payer)
            .Must(UserExists)
            .WithMessage(ErrorMessages.PayerError);

        RuleFor(t => t.Payer)
            .Must(IsUserMerchant)
            .WithMessage(ErrorMessages.IsMerchant);

        RuleFor(t => t.Payer)
            .NotEqual(t => t.Payee)
            .WithMessage(ErrorMessages.NotPermitted);

        RuleFor(t => t.Value)
            .GreaterThan(1)
            .WithMessage(ErrorMessages.MinimumValue);

        RuleFor(t => t.Value)
            .NotEmpty()
            .NotNull()
            .WithMessage(ErrorMessages.ValueError);
    }

    public bool UserExists(int id)
    {
        if(_context.User.FirstOrDefault(u => u.Id == id) == null)
        {
            return false;
        }
        return true;
    } 

    public bool IsUserMerchant(int id)
    {
        var user = _context.User.FirstOrDefault(u => u.Id == id);
        var merchant  = user?.WalletType.ToString() == "MERCHANT";
        
        if(merchant)
        {
            return false;
        }
        return true;
    }

}