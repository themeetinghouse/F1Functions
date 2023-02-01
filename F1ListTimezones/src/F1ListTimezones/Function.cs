using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using System.Xml;
using Amazon.Lambda.Core;
using FellowshipOne.Api;
using Newtonsoft.Json;
using FellowshipOne.API.Events.Model;
using FellowshipOne.Api.Model;
using FellowshipOne.API.Realms;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace F1ListTimezones;

public class InputTest
{
    public Arguments? arguments { get; set; }
}
public class Arguments
{
    public string[] itemId { get; set; }
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
        //var variable = null;

        F1OAuthTicket f1OAuthTicket = new F1OAuthTicket()
        {
            ConsumerKey = secret?.ConsumerKey,
            ConsumerSecret = secret?.ConsumerSecret,
            ChurchCode = secret?.ChurchCode
        };
        string? str = secret?.userName;
        string? str1 = secret?.password;
        f1OAuthTicket = RestClient.Authorize(f1OAuthTicket, "george.bell", "kivqew-0huCku-rinrom", LoginType.PortalUser, false);
        RestClient restClient = new RestClient(f1OAuthTicket, false, false);
        TimezoneRealm timezoneRealm = new TimezoneRealm(f1OAuthTicket, "https://meetinghouse.fellowshiponeapi.com/", restClient.ContentType);
        ConcurrentBag<string> concurrentBag = new ConcurrentBag<string>();
        Parallel.ForEach<string>(input.arguments.itemId, new Action<string>( (string str) => {
            
            string jsonResponse = timezoneRealm.Timezone.Get(str).JsonResponse;
            concurrentBag.Add(string.Concat(new string[] { "{\"id\":\"", str, "\",\"info\":", jsonResponse, "}" }));
        }));
        string strA = "[";
        foreach (string str2 in concurrentBag)
        {
            strA = string.Concat(strA, str2, ",");
        }
        strA = string.Concat(strA.Remove(strA.Length - 1), "]");
        strA = strA.Replace("@id", "id");
        return strA;
        //return input.Key1.ToUpper();
    }
}

