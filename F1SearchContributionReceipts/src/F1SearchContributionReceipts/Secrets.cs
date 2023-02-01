using System;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;
using Amazon;

namespace F1SearchContributionReceipts
{
  public  class Secret
    {
        public string? ConsumerKey { get; set; }
        public string? ConsumerSecret { get; set; }
        public string? ChurchCode { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }
    }

    public class Secrets
	{
        public static Secret? GetSecretValue(string secretName, string region)
        {
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest()
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT"
            };

            GetSecretValueResponse response = client.GetSecretValueAsync(request).GetAwaiter().GetResult(); // there is an async way to do this, I will write another short post on that
            return JsonConvert.DeserializeObject<Secret>(response.SecretString);

        }
    }
}

