using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content;

public class Notice : ContentBase
{
    public DateTime? RelevantUntil { get; set; }
}
