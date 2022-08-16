using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Auth.Authorization.Mvc.Filters;

public class IpWhitelistFilter<TOptions> : IPageFilter
    where TOptions : IpWhitelistSettings
{
    private TOptions settings;

    private readonly ILogger _logger;

    public IpWhitelistFilter(ILogger<IpWhitelistFilter<TOptions>> logger, IOptionsSnapshot<TOptions> optionsSnapshot)
    {
        _logger = logger;
        settings = optionsSnapshot.Value;
    }

    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        var allowed = false;
        var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
        _logger.LogInformation("Remote IP: {@RemoteIp}", remoteIp.ToString());

        var whitelistedIps = settings.WhitelistedIps;

        if (remoteIp.IsIPv4MappedToIPv6)
            remoteIp = remoteIp.MapToIPv4();

        if (whitelistedIps?.Any() ?? false)
            

        foreach (var whitelistedStr in whitelistedIps)
        {
            var whitelisted = IPAddress.Parse(whitelistedStr);

            if (whitelisted.Equals(remoteIp))
            {
                allowed = true;
                break;
            }
        }

        if (!allowed)
        {
            Forbid(context, remoteIp);
            return;
        }
    }

    private void Forbid(PageHandlerExecutingContext context, IPAddress remoteIp)
    {
        _logger.LogWarning("Forbidden Request from Remote IP: {@RemoteIp}", remoteIp.ToString());
        context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
    }
}
