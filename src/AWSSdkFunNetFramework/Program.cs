using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SecretsManager;

namespace AWSSdkFunNetFramework
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var listSecretsRequest = new Amazon.SecretsManager.Model.ListSecretsRequest
            {
                MaxResults = 100,
            };

            var secretsManager = new AmazonSecretsManagerClient();
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
