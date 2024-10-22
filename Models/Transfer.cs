namespace DesafioPicPay.Models;
public class Transfer
{
    public Guid Id {get;set;} = Guid.NewGuid();
    public User? Payer {get;set;}
    public int PayerId {get;set;}
    public User? Payee {get;set;}
    public int PayeeId {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
    public decimal Value {get;set;}
}
