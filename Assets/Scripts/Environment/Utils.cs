using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Util class to store things
/// </summary>
public class Utils
{

    /// <summary>
    /// Generate a pseudo-gaussien random
    /// </summary>
    /// <param name="min">min value (included)</param>
    /// <param name="max">max value (included)</param>
    /// <returns></returns>
    public static int PseudoGaussRandom(int min, int max)
    {
        int random1 = Random.Range(min,max+1);
        int random2 = Random.Range(min,max+1);
        return min + Mathf.Abs(random1 - random2);
    }
    
}
