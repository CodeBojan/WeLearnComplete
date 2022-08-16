using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models;
using WeLearn.Shared.Dtos.Credentials;

namespace WeLearn.Shared.Extensions.Models;

public static class CredentialsExtensions
{
    public static GetCredentialsDto MapToGetDto(this Credentials c)
    {
        return new GetCredentialsDto
        {
            Id = c.Id,
            CreatorId = c.CreatorId,
            ExternalSystemId = c.ExternalSystemId,
            Username = c.Username,
            CreatedDate = c.CreatedDate
        };
    }
}
