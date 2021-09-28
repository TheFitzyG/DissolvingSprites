//Copyright: Sean FitzGerald (TheFitzyGames) CC BY-SA 4.0
//https://creativecommons.org/licenses/by-sa/4.0/
//modifications, and commercial use are allowed with appropriate credit given

namespace TFG_SP
{
    /// <summary>
    /// Static class for TFG_SP string based extension methods.  
    /// </summary>
    public static class TfgString 
    { 
        /// <summary>
        /// Returns a string where every non space character is replaced by the replacement character
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string ToHangman(this string origin, char replacement = '_')
        {
            var newString = "";
            foreach (var c in origin)
            {
                if (c == ' ')
                {
                    newString += ' ';
                    continue;
                }

                newString += replacement;
            }

            return newString;
        }
    }
}