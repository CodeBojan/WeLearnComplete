using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Dtos.Credentials;

public class GetCredentialsDto
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public Guid ExternalSystemId { get; set; }
    public string Username { get; set; }
    public DateTime CreatedDate { get; set; }
}
