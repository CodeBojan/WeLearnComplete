using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Auth.Authorization.Mvc.Filters;

public class IpWhitelistSettings
{
    public string[] WhitelistedIps { get; set; }
}
