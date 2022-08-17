using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Identity;

public class DbPersistedGrant : PersistedGrant
{
    public DbPersistedGrant() : base()
    {
    }

    public DbPersistedGrant(PersistedGrant grant)
    {
        Key = grant.Key;
        ClientId = grant.ClientId;
        ConsumedTime = grant.ConsumedTime;
        CreationTime = grant.CreationTime;
        Data = grant.Data;
        Description = grant.Description;
        Expiration = grant.Expiration;
        SessionId = grant.SessionId;
        SubjectId = grant.SubjectId;
        Type = grant.Type;
    }

    public void Update(PersistedGrant grant)
    {
        ClientId = grant.ClientId;
        ConsumedTime = grant.ConsumedTime;
        CreationTime = grant.CreationTime;
        Data = grant.Data;
        Description = grant.Description;
        Expiration = grant.Expiration;
        SessionId = grant.SessionId;
        SubjectId = grant.SubjectId;
        Type = grant.Type;
    }
}
