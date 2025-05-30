using System.Security.Cryptography;
using Ezima.API.Model.Config;

namespace Ezima.API.Authentication;

public class SecurityKeyHelper
{
    private readonly JwtOptions _options;

    public SecurityKeyHelper(JwtOptions options)
    {
        _options = options;
    }

    public RSA PrivateRSA => LoadRsaKey(_options.RsaPrivateKeyLocation);
    public RSA PublicRSA => LoadRsaKey(_options.RsaPublicKeyLocation);


    private static RSA LoadRsaKey(string rsaKeyPath)
    {
        var rsa = RSA.Create();
        if (!File.Exists(rsaKeyPath))
        {
            throw new FileNotFoundException("RSA key file not found", rsaKeyPath);
        }
        var pemContents = File.ReadAllText(rsaKeyPath); 
        rsa.ImportFromPem(pemContents.ToCharArray());

        return rsa;
    }
}