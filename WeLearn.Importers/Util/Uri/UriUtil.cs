using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeLearn.Importers.Util.Uri;

public static class UriUtil
{
    private static readonly Regex fileExtensionRegex = new(".*(\\..*)");

    public static string ExtractFileExtensions(string uri)
    {
        return fileExtensionRegex.Match(uri).Groups[1].Value;
    }
}
