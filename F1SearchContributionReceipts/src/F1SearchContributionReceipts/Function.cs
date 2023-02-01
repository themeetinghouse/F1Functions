using System.Text.Json.Serialization;
using System.Xml;
using Amazon.Lambda.Core;
using FellowshipOne.Api;
using FellowshipOne.Api.Giving.Model;
using FellowshipOne.Api.Giving.QueryObject;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace F1SearchContributionReceipts;

public class InputTest
{
    public Arguments? arguments { get; set; }
}
public class Arguments
{
    public string? itemId { get; set; }
}

public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public object? FunctionHandler(InputTest input, ILambdaContext context)
    {
        string secretName = "F1Secrets";
        string region = "us-east-1"; // You may be using a different region
        Secret? secret = Secrets.GetSecretValue(secretName, region);

        F1OAuthTicket f1OAuthTicket = new F1OAuthTicket()
        {
            ConsumerKey = secret?.ConsumerKey,
            ConsumerSecret = secret?.ConsumerSecret,
            ChurchCode = secret?.ChurchCode
        };
        string? str = secret?.userName;
        string? str1 = secret?.password;
        F1OAuthTicket f1OAuthTicket1 = RestClient.Authorize(f1OAuthTicket, str, str1, LoginType.PortalUser, false);
        LambdaLogger.Log("test2");
        RestClient restClient = new RestClient(f1OAuthTicket1, false, false);
        LambdaLogger.Log("test3");
        ContributionReceiptQO contributionReceiptQO = new ContributionReceiptQO();
        LambdaLogger.Log("test6");
        contributionReceiptQO.HouseholdID = input.arguments.itemId;
        string jsonResponse = restClient.GivingRealm.ContributionReceipts.Search<ContributionReceipt>(contributionReceiptQO).JsonResponse;
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(jsonResponse);
        return JsonConvert.DeserializeObject<object>(JsonConvert.SerializeXmlNode(xmlDocument).Replace("@id", "id"));

        //return input.Key1.ToUpper();
    }
}

