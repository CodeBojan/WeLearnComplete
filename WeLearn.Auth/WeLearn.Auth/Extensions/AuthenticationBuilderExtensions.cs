using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Auth.Extensions;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddIdentityServerAuthentication(this AuthenticationBuilder builder, string authority)
    {
        builder.AddJwtBearer(options =>
        {
            options.AddIdentityServerAuthentication(authority);
        });

        return builder;
    }
}
