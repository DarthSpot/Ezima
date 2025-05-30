using System.Security.Cryptography;
using Ezima.API.Model.Config;
using Microsoft.Extensions.Options;

namespace Ezima.API.Authentication;

public class SecurityKeyHelper(IOptions<JwtOptions> options)
{
    private JwtOptions _options = options.Value;
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