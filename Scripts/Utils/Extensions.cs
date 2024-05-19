using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static string UnderscoreToSpace(this string input)
    {
        return input.Replace("_", " ");
    }
}
