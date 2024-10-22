namespace DesafioPicPay.Resources;
public static class ErrorMessages
{
    public static string MinimumValue = "O valor mínimo deve ser de 1R$";
    public static string InsuficientValue = "Você não possui este valor";
    public static string IsMerchant = "Mercadores não podem fazer transferência";
    public static string AuthorizationError = "Ocorreu um erro no serviço de autorização";
    public static string NotificationError = "Ocorreu um erro no serviço de notificação";
    public static string NotPermitted = "Esse tipo de operação não é permitida";
    public static string PayeeError = "O campo Payee deve ser preenchido corretamente";
    public static string PayerError = "O campo Payer deve ser preenchido corretamente";
    public static string ValueError = "O campo Value deve ser preenchido corretamente";
    public static string WalletType = "O Campo WalletType deve ser USER ou MERCHANT";
}
