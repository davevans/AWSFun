using Microsoft.Extensions.Configuration;
using Amazon.SecretsManager;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AWSSdkFun
{
    class Program
    { 
        static async Task Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            var config = configurationBuilder.Build();
            var options = config.GetAWSOptions();

            var secretsManager = options.CreateServiceClient<IAmazonSecretsManager>();

            var listSecretsRequest = new Amazon.SecretsManager.Model.ListSecretsRequest 
            {
                MaxResults = 100,
            };

            var secretsResponse = await secretsManager.ListSecretsAsync(listSecretsRequest, CancellationToken.None);

            Console.WriteLine($"SecretsResponse HttpStatus code is {secretsResponse.HttpStatusCode}.");

            foreach (var secret in secretsResponse.SecretList)
            {
                Console.WriteLine($"secret name: {secret.Name}.");

                var secretValueRequest = new Amazon.SecretsManager.Model.GetSecretValueRequest
                {
                    SecretId = secret.ARN
                };

                var secretValueResponse = await secretsManager.GetSecretValueAsync(secretValueRequest, CancellationToken.None);
                
                Console.WriteLine($"SecretValueResponse HttpStatus code is {secretValueResponse.HttpStatusCode}.");
                Console.WriteLine($"Secret value: {secretValueResponse.SecretString}.");
            }

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
