using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LaRottaO.CSharp.StringUtilities
{
    public class UtilsStrings
    {
        /// <summary>
        ///
        /// A collection of useful methods for String manipulation taken from the Internet
        ///
        /// 2021 06 14 - Initial commit
        /// 2021 10 08 - Improved repeated whitespace removal
        ///
        /// by Felipe La Rotta
        ///
        /// </summary>
        ///

        private UtilsStrings()
        {
            //Not implemented
        }

        public static String getStringBetweenStrings(string argOpeningString, string argClosingString, string argBaseText)
        {
            if (argBaseText == null)
            {
                return null;
            }

            if (!argBaseText.Contains(argOpeningString))
            {
                return null;
            }
            if (!argBaseText.Contains(argClosingString))
            {
                return null;
            }

            int posicionIni = argBaseText.IndexOf(argOpeningString) + argOpeningString.Length;
            int posicionFin = argBaseText.IndexOf(argClosingString, posicionIni);

            if (posicionFin == -1)
            {
                return null;
            }
            return argBaseText.Substring(posicionIni, posicionFin - posicionIni);
        }

        public static List<string> getStringsBetweenStrings(string argOpeningString, string argClosingString, string argBaseText)
        {
            try
            {
                if (argBaseText == null)
                {
                    return null;
                }

                List<string> listElementsFound = new List<string>();

                int startingIndex = 0;
                int endingIndex = 0;

                bool pendingToCheck = false;

                while (!pendingToCheck)
                {
                    startingIndex = argBaseText.ToLower().IndexOf(argOpeningString.ToLower());

                    if (startingIndex != -1)
                    {
                        endingIndex = startingIndex + argBaseText.ToLower().Substring(startingIndex).IndexOf(argClosingString.ToLower());

                        listElementsFound.Add(argBaseText.Substring(startingIndex + argOpeningString.Length, endingIndex - startingIndex - argOpeningString.Length));

                        argBaseText = argBaseText.Substring(endingIndex + argClosingString.Length);
                    }
                    else
                    {
                        pendingToCheck = true;
                    }
                }

                return listElementsFound;
            }
            catch
            {
                return null;
            }
        }

        public static String removeAllWhitespaces(String argString)
        {
            return string.Join("", argString.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        public static String removeRepeatedWhitespaces(String argString)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            return regex.Replace(argString, " ");
        }

        public static string removeDigitsFromString(string argString)
        {
            return Regex.Replace(argString, @"\d", "");
        }
    }
}