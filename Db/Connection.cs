using Npgsql;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;

namespace dotnet_backend;

public class Connection
{
    public string? username { get; set; }
    public string? password { get; set; }
    public string? engine { get; set; }
    public string? host { get; set; }
    public int? port { get; set; }
    public string? dbCluster { get; set; }

    public Connection()
    {
    }

    public static Connection GetSecret()
    {
        string secretName = "to-do-connection-string";
        string region = "eu-west-2";
        string secret = "";

        MemoryStream memoryStream = new MemoryStream();

        IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

        #if DEBUG
            GetSecretValueRequest request = new GetSecretValueRequest();
        #else
            Console.WriteLine("In else");
            GetSecretValueRequest request = new GetSecretValueRequest(Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID"), Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY"));
        #endif

        request.SecretId = secretName;
        request.VersionStage = "AWSCURRENT"; // VersionStage defaults to AWSCURRENT if unspecified.

        GetSecretValueResponse? response = null;

        // In this sample we only handle the specific exceptions for the 'GetSecretValue' API.
        // See https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
        // We rethrow the exception by default.

        try
        {
            response = client.GetSecretValueAsync(request).Result;
        }
        catch (DecryptionFailureException e)
        {
            // Secrets Manager can't decrypt the protected secret text using the provided KMS key.
            // Deal with the exception here, and/or rethrow at your discretion.
            throw new Exception(e.ToString());
        }
        catch (InternalServiceErrorException e)
        {
            // An error occurred on the server side.
            // Deal with the exception here, and/or rethrow at your discretion.
            throw new Exception(e.ToString());
        }
        catch (InvalidParameterException e)
        {
            // You provided an invalid value for a parameter.
            // Deal with the exception here, and/or rethrow at your discretion
            throw new Exception(e.ToString());
        }
        catch (InvalidRequestException e)
        {
            // You provided a parameter value that is not valid for the current state of the resource.
            // Deal with the exception here, and/or rethrow at your discretion.
            throw new Exception(e.ToString());
        }
        catch (ResourceNotFoundException e)
        {
            // We can't find the resource that you asked for.
            // Deal with the exception here, and/or rethrow at your discretion.
            throw new Exception(e.ToString());
        }
        catch (System.AggregateException ae)
        {
            // More than one of the above exceptions were triggered.
            // Deal with the exception here, and/or rethrow at your discretion.
            throw new Exception(ae.ToString());
        }

        // Decrypts secret using the associated KMS key.
        // Depending on whether the secret is a string or binary, one of these fields will be populated.
        if (response.SecretString != null)
        {
            secret = response.SecretString;
        }
        else
        {
            memoryStream = response.SecretBinary;
            StreamReader reader = new StreamReader(memoryStream);
            string decodedBinarySecret = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
            secret = decodedBinarySecret;
        }
        
        # nullable disable
        return JsonConvert.DeserializeObject<Connection>(secret);
    }

}
