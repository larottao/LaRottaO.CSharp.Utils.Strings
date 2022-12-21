using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace LaRottaO.CSharp.StringUtilities
{
    public static class UtilsStrings
    {
        /// <summary>
        ///
        /// A collection of useful methods for String manipulation taken from the Internet
        ///
        /// 2021 06 14 - Initial commit
        /// 2021 10 08 - Improved repeated whitespace removal by https://stackoverflow.com/users/13913/patrick-desjardins
        /// 2021 10 09 - Added MakeValidFileName by https://stackoverflow.com/users/104672/andre
        ///
        /// other code by Felipe La Rotta
        ///
        /// </summary>
        ///

        public static string makeValidFileName(string argInvalidName)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(argInvalidName, invalidRegStr, "_");
        }

        public static String getStringBetweenStrings(string argOpeningString, string argClosingString, string argBaseText)
        {
            if (argBaseText == null)
            {
                return null;
            }

            if (argBaseText.IndexOf(argOpeningString) == -1)
            {
                return null;
            }
            if (argBaseText.IndexOf(argClosingString) == -1)
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

        public static List<string> getStringsBetweenStrings(string argOpeningString, string argClosingString, string argBaseText, Boolean trimResult = false)
        {
            try
            {
                if (argBaseText == null)
                {
                    return null;
                }

                List<string> foundElementsList = new List<string>();

                int startingIndex = 0;
                int endingIndex = 0;

                bool pendingToCheck = false;

                while (!pendingToCheck)
                {
                    startingIndex = argBaseText.ToLower().IndexOf(argOpeningString.ToLower());

                    if (startingIndex != -1)
                    {
                        endingIndex = startingIndex + argBaseText.ToLower().Substring(startingIndex).IndexOf(argClosingString.ToLower());

                        String foundElement = argBaseText.Substring(startingIndex + argOpeningString.Length, endingIndex - startingIndex - argOpeningString.Length);

                        if (trimResult)
                        {
                            foundElementsList.Add(foundElement.Trim());
                        }
                        else
                        {
                            foundElementsList.Add(foundElement);
                        }

                        argBaseText = argBaseText.Substring(endingIndex + argClosingString.Length);
                    }
                    else
                    {
                        pendingToCheck = true;
                    }
                }

                return foundElementsList;
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

        public static String removeRepeatedCharacter(String orgOiginalText, String argCharacterToReplace)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[" + argCharacterToReplace + "]{2,}", options);
            orgOiginalText = regex.Replace(orgOiginalText, argCharacterToReplace);
            return orgOiginalText;
        }

        public static List<String> removeStringFromList(List<String> argOriginalList, String argStringToRemove)
        {
            argOriginalList.RemoveAll(u => u.Contains(argStringToRemove));

            return argOriginalList;
        }

        public static Boolean checkIfLineContainsConsecutiveWords(String argLine, String[] argWordsArray)
        {
            int lastPos = 0;

            foreach (String texto in argWordsArray)
            {
                int searchResult = argLine.ToLower().IndexOf(texto.ToLower(), lastPos);

                if (searchResult > -1)
                {
                    lastPos = searchResult;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public static String getNumericValueAfterWord(String argLine, String argWord)
        {
            //EXAMPLE:
            //Clg:5625484734
            // Result: 5625484734

            if (argLine.IndexOf(argWord) == -1)
            {
                return null;
            }

            int cursorPosition = argLine.IndexOf(argWord) + argWord.Length;

            StringBuilder sbNumericResult = new StringBuilder();
            Boolean noMoreNumbersFound = false;

            while (!noMoreNumbersFound)
            {
                String candidate = argLine.Substring(cursorPosition, 1);

                int dummy = 0;

                if (int.TryParse(candidate, out dummy) || candidate.Equals(" "))
                {
                    sbNumericResult.Append(candidate);
                }
                else
                {
                    noMoreNumbersFound = true;
                }

                if (cursorPosition == argLine.Length)
                {
                    noMoreNumbersFound = true;
                }

                cursorPosition = cursorPosition + 1;
            }

            return sbNumericResult.ToString().Trim();
        }
    }
}