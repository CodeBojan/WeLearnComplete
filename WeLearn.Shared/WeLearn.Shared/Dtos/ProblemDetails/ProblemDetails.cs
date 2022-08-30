using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Dtos.ProblemDetails;

public class WeLearnProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    public WeLearnProblemDetails()
    {
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
    }
}
