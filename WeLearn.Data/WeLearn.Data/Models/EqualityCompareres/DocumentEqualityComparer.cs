using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content;

namespace WeLearn.Data.Models.EqualityCompareres;

// TODO compare by other props as well
public class DocumentComparer : IEqualityComparer<Document>
{
    public bool Equals(Document? x, Document? y)
    {
        if (x is null && y is null)
        {
            return true;
        }
        else if (x is null || y is null)
        {
            return false;
        }
        else
        {
            return x.ExternalId == y.ExternalId &&
                   x.ExternalSystemId == y.ExternalSystemId &&
                   x.Hash == y.Hash &&
                   x.HashAlgorithm == y.HashAlgorithm;
        }
    }

    public int GetHashCode([DisallowNull] Document obj)
    {
        return HashCode.Combine(
            obj.ExternalId,
            obj.ExternalSystemId,
            obj.Hash,
            obj.HashAlgorithm);
    }
}
