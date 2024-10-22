using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioPicPay.Models;
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get;set;}
    public string Name {get; set;} = string.Empty;
    public string Document {get; set;} = string.Empty;
    public string Email  {get; set;} = string.Empty;    
    public string Password {get;set;} = string.Empty;    
    public decimal Balance {get; set;}
    public WalletType WalletType {get;set;}
}