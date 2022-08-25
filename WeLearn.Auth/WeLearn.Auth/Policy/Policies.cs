using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Auth.Policy;

public static class Policies
{
    public const string IsAdmin = nameof(IsAdmin);
    public const string IsUser = nameof(IsUser);
    public const string IsResourceAdmin = nameof(IsResourceAdmin);
}
