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

        public static List<String> getStringsBetweenStrings(String argOpeningString1, String argClosingString1, String argBaseText1, Boolean trimResult = false)
        {
            try
            {
                if (argBaseText1 == null ||
                    argBaseText1.Length < 3 ||
                    argOpeningString1.Length == 0 ||
                    argClosingString1.Length == 0)
                {
                    return new List<String>();
                }

                String baseText = argBaseText1.ToLower();
                String openingString = argOpeningString1.ToLower();
                String closingString = argClosingString1.ToLower();

                Debug.WriteLine("opening string:" + argOpeningString1);
                Debug.WriteLine("closing string:" + argClosingString1);

                if (!baseText.Contains(openingString))
                {
                    Debug.WriteLine("getStringsBetweenStrings: not even 1 opening string found");
                    return new List<string>();
                }

                if (!baseText.Contains(closingString))
                {
                    Debug.WriteLine("getStringsBetweenStrings: not even 1 closing string found");
                    return new List<string>();
                }

                List<String> foundElementsList = new List<String>();

                //Let's find the first opening and closing ocurrences of all

                int posicionCursor = 0;

                Debug.WriteLine(baseText);

                while (true)
                {
                    int startingIndex = baseText.IndexOf(openingString, posicionCursor);

                    if (startingIndex < 0) {

                        break;
                    }

                    int endingIndex = baseText.IndexOf(closingString, startingIndex);
                    int selectionLenght = endingIndex - startingIndex;

                    Debug.WriteLine("baseText:" + baseText.Length);
                    Debug.WriteLine("startingIndex:" + startingIndex);
                    Debug.WriteLine("endingIndex:" + endingIndex);
                    Debug.WriteLine("selectionLenght:" + selectionLenght);

                    if (startingIndex == -1 || endingIndex == -1)
                    {
                        break;
                    }

                    String result = argBaseText1.Substring(startingIndex, selectionLenght);

                    posicionCursor = startingIndex + 1;                  

                    Debug.WriteLine(result);

                    if (trimResult)
                    {
                        foundElementsList.Add(result.Trim());
                    }
                    else
                    {
                        foundElementsList.Add(result);
                    }
                }

                return foundElementsList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("getStringsBetweenStrings exception: " + ex);
                return new List<string>();
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