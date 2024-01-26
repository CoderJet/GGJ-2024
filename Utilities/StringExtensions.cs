// System includes.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities;

public static class StringExtensions
{
    public static bool ContainsAny(this string input, IEnumerable<string> containsKeywords)
    {
        return containsKeywords.Any(keyword => input.Contains(keyword, StringComparison.Ordinal));
    }
}