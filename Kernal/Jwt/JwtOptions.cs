using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernal.Jwt;

public class JwtOptions
{
    public string Secret { get; set; } = "super-duper-secret-key-that-should-also-be-fairly-long";
    public string Issuer { get; set; } = "CRM";
    public string Audience { get; set; } = "account";
    public int ExpirationMinutes { get; set; } = 30;
}