using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static Texture2D Blur(this Texture2D texture, int radius)
    {
        int w = texture.width, h = texture.height;
        int r = radius;
        int rs = Mathf.CeilToInt(radius * 2.57f);     // significant radius

        Texture2D result = new Texture2D(w, h);

        for (var i = 0; i < h; i++)
        for (var j = 0; j < w; j++)
        {
            Color val = new Color();
            float wsum = 0;
            for (var iy = i - rs; iy < i + rs + 1; iy++)
            for (var ix = j - rs; ix < j + rs + 1; ix++)
            {
                var x = Mathf.Min(w - 1, Mathf.Max(0, ix));
                var y = Mathf.Min(h - 1, Mathf.Max(0, iy));
                var dsq = (ix - j) * (ix - j) + (iy - i) * (iy - i);
                var wght = Mathf.Exp(-dsq / (2 * r * r)) / (Mathf.PI * 2 * r * r);
                val += texture.GetPixel(x, y) * wght;
                wsum += wght;
            }
            result.SetPixel(j, i, val / wsum);
        }

        return result;
    }
}
