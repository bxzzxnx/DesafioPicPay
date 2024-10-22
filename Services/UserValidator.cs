using FluentValidation;
using DesafioPicPay.Dto;
using DesafioPicPay.Models;

namespace DesafioPicPay.Services;

public class UserValidator : AbstractValidator<UserDto>
{
    private readonly DatabaseContext _context;
    public UserValidator(DatabaseContext context)
    {
        _context = context;

        
        RuleFor(u => u.Email)
            .EmailAddress()
            .NotNull()
            .Must(UniqueEmail).WithMessage("Esse email ja existe");

        RuleFor(u => u.Document)
            .NotNull()
            .MaximumLength(14).WithMessage("Máximo 14 dígitos")
            .Must(UniqueDocument).WithMessage("CPF/CNPJ inválido");

        RuleFor(u => u.WalletType).NotNull().WithMessage("Precisa ser ou USER ou MERCHANT");
    }


    public bool UniqueEmail(string email)
    { 
        if (_context.User.FirstOrDefault(u => u.Email == email) != null)
        {
            return false;
        }
        return true;
    }
    public bool UniqueDocument(string document)
    {
        if(_context.User.FirstOrDefault(u => u.Document == document) != null)
        {
            return false;
        }

        return true;
    }
}
