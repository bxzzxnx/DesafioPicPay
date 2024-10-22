using System.Net;
using System.Text;
using DesafioPicPay.Exception;
using DesafioPicPay.Resources;

namespace DesafioPicPay.Services;
public class NotificationService
{
    public static async Task Notification()
    {
        var content = new StringContent("", Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var response = await client.PostAsync("https://util.devi.tools/api/v1/notify", content);
        if (response.StatusCode != HttpStatusCode.NoContent)
        {
            throw new BaseException(ErrorMessages.NotificationError);
        }
    }
}