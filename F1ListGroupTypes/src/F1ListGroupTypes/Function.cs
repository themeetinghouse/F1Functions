using System.Xml;
using Amazon.Lambda.Core;
using FellowshipOne.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Amazon.Lambda.Serialization;
using Newtonsoft.Json.Converters;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace F1ListGroupTypes;
public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>


    public object? FunctionHandler(ILambdaContext context)
    {
        string secretName = "F1Secrets";
        string region = "us-east-1"; // You may be using a different region
        Secret? secret =Secrets.GetSecretValue(secretName, region);
    
        F1OAuthTicket f1OAuthTicket = new F1OAuthTicket()
        {
             ConsumerKey = secret?.ConsumerKey,
             ConsumerSecret = secret?.ConsumerSecret,
             ChurchCode = secret?.ChurchCode
        };
        string? str = secret?.userName;
        string? str1 = secret?.password; 
        string jsonResponse = (new RestClient(RestClient.Authorize(f1OAuthTicket, str, str1, LoginType.PortalUser, false), false, false)).GroupRealm.GroupTypes.List().JsonResponse;
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(jsonResponse);
        object? ret=JsonConvert.DeserializeObject<object>(JsonConvert.SerializeXmlNode(xmlDocument).Replace("@id", "id"));

        return ret;

    }
}

