using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Copyright: Sean FitzGerald (TheFitzyGames) CC BY-SA 4.0
//https://creativecommons.org/licenses/by-sa/4.0/
//modifications, and commercial use are allowed with appropriate credit given

namespace TFG_SP
{
    public static class TfgColor
    {
        /// <summary>
        /// Returns a color from a hexadecimal string
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Color FromHex(string hex)
        {
            if (!hex.StartsWith("#"))
            {
                hex = "#" + hex;
            }

            ColorUtility.TryParseHtmlString(hex, out var temp);

            return temp;
        }
    }
}