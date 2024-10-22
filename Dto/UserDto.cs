using System.ComponentModel.DataAnnotations;
using DesafioPicPay.Models;

namespace DesafioPicPay.Dto;
public class UserDto
{
    public string Name {get;set;} = string.Empty; 
    public required string Password {get;set;}
    
    [EmailAddress]
    public required string Email {get;set;}
    public required string Document {get;set;}
    public required decimal Balance {get;set;}
    public WalletType WalletType {get;set;}
}
