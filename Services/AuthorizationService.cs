using System.Net;
using DesafioPicPay.Exception;
using DesafioPicPay.Resources;

namespace DesafioPicPay.Services;
public class AuthorizationService
{
    public static async Task Authorization()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("https://util.devi.tools/api/v2/authorize");
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new BaseException(ErrorMessages.AuthorizationError);
        }
    }
}