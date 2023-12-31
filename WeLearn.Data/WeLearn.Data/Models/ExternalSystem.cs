﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models;

public class ExternalSystem : BaseEntity, IExternalSystem
{
    public ExternalSystem(string name, string? friendlyName)
    {
        Id = Guid.NewGuid();
        Name = name;
        FriendlyName = friendlyName;
    }

    public string Name { get; set; }
    public string? FriendlyName { get; set; }
    public string? Url { get; set; }
    public string? LogoUrl { get; set; }

    Guid IExternalSystem.ExternalSystemId => Id;

    public virtual ICollection<Credentials> Credentials { get; set; }
    public virtual ICollection<Content.Content> Contents { get; set; }
}
