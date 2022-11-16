using System.Net;
using System.Text;
using Newtonsoft.Json;
using WebApi.Model;

namespace WebApi.RestApiCall;

public class RestApiCall
{
    private readonly string urlToPost = "";

    public RestApiCall(string urlToPost)
    {
        this.urlToPost = urlToPost;
    }

    public bool postData(WildBoarIotData wildBoarIotData)
    {
        var webClient = new WebClient();
        byte[] resByte;
        string resString;
        byte[] reqString;

        try
        {
            webClient.Headers["content-type"] = "application/json";
            reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(wildBoarIotData, Formatting.Indented));
            resByte = webClient.UploadData(urlToPost, "post", reqString);
            resString = Encoding.Default.GetString(resByte);
            Console.WriteLine(resString);
            webClient.Dispose();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return false;
    }
}