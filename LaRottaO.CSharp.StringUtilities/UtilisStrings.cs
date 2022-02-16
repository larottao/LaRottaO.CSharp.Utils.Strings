using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// 2021 10 08 - Improved repeated whitespace removal by https://stackoverflow.com/users/13913/patrick-desjardins
        /// 2021 10 09 - Added MakeValidFileName by https://stackoverflow.com/users/104672/andre
        ///
        /// other code by Felipe La Rotta
        ///
        /// </summary>
        ///

        public UtilsStrings()
        {
            //Not implemented
        }

        public string MakeValidFileName(string name)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }

        public String getStringBetweenStrings(string argOpeningString, string argClosingString, string argBaseText)
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

        public List<string> getStringsBetweenStrings(string argOpeningString, string argClosingString, string argBaseText)
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

        public String removeAllWhitespaces(String argString)
        {
            return string.Join("", argString.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        public String removeRepeatedWhitespaces(String argString)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            return regex.Replace(argString, " ");
        }

        public string removeDigitsFromString(string argString)
        {
            return Regex.Replace(argString, @"\d", "");
        }

        public String removeRepeatedCharacter(String originalText, String characterToReplace)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[" + characterToReplace + "]{2,}", options);
            originalText = regex.Replace(originalText, characterToReplace);
            return originalText;
        }

        public List<String> removeStringFromList(List<String> originalList, String stringToRemove)
        {
            originalList.RemoveAll(u => u.Contains(stringToRemove));

            return originalList;
        }

        public Boolean lineContainsConsecutiveWords(String line, String[] words)
        {
            int lastPos = 0;

            foreach (String texto in words)
            {
                int searchResult = line.ToLower().IndexOf(texto.ToLower(), lastPos);

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

        public String getNumericValueAfterWord(String line, String word)
        {
            if (line.IndexOf(word) == -1)
            {
                return null;
            }

            int cursorPosition = line.IndexOf(word) + word.Length;

            ;

            //[W]|2022-02-02 16:41:23.822587|140710980351744|[CALL-SCF]-->[BNR]Sending Authentication Req|Cld:5577504994|Clg:5625484734|LocNum:5294100005980|Srvky:123, SessionId:3557010210291752, <UserId: 22325>|CapCallSl.cpp|buildAuthenticationReq|2844

            ;

            StringBuilder sbNumericResult = new StringBuilder();
            Boolean noMoreNumbersFound = false;

            ;

            while (!noMoreNumbersFound)
            {
                String candidate = line.Substring(cursorPosition, 1);

                int dummy = 0;

                if (int.TryParse(candidate, out dummy) || candidate.Equals(" "))
                {
                    sbNumericResult.Append(candidate);
                }
                else
                {
                    noMoreNumbersFound = true;
                }

                if (cursorPosition == line.Length)
                {
                    noMoreNumbersFound = true;
                }

                cursorPosition = cursorPosition + 1;
            }

            ;

            return sbNumericResult.ToString().Trim();
        }
    }
}