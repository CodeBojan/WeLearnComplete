﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models;

public abstract class BaseEntity : DatedEntity
{
    public Guid Id { get; set; }
}
