using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wschodnioslowianskie_nazwy
{
    class Translator
    {
        private char[] consonants = { 'B', 'W', 'H', 'G', 'D', 'Ż', 'Z', 'K', 'Ł', 'M', 'N', 'P', 'R', 'S', 'T', 'F', 'C' };
        private Dictionary<char, string> ukrainianAlph = new Dictionary<char, string>()
        {
            {'А', "A" },
            {'Б', "B" },
            {'В', "W" },
            {'Г', "H" },
            {'Ґ', "G" },
            {'Д', "D" },
            {'Е', "E" },
            {'Є', "JE" },
            {'Ж', "Ż" },
            {'З', "Z" },
            {'И', "Y" },
            {'І', "I" },
            {'Ї', "JI" },
            {'Й', "J" },
            {'К', "K" },
            {'Л', "Ł" },
            {'М', "M" },
            {'Н', "N" },
            {'О', "O" },
            {'П', "P" },
            {'Р', "R" },
            {'С', "S" },
            {'Т', "T" },
            {'У', "U" },
            {'Ф', "F" },
            {'Х', "CH" },
            {'Ц', "C" },
            {'Ч', "CZ" },
            {'Ш', "SZ" },
            {'Щ', "SZCZ" },
            {'Ю', "JU" },
            {'Я', "JA" },
        };
        private Dictionary<char, string> belarusianAlph = new Dictionary<char, string>()
        {
            {'А', "A" },
            {'Б', "B" },
            {'В', "W" },
            {'Г', "H" },
            {'Ґ', "G" },
            {'Д', "D" },
            {'Э', "E" },
            {'Е', "JE" },
            {'Ё', "JO" },
            {'Ж', "Ż" },
            {'З', "Z" },
            {'Ы', "Y" },
            {'І', "I" },
            {'Й', "J" },
            {'К', "K" },
            {'Л', "Ł" },
            {'М', "M" },
            {'Н', "N" },
            {'О', "O" },
            {'П', "P" },
            {'Р', "R" },
            {'С', "S" },
            {'Т', "T" },
            {'У', "U" },
            {'Ў', "U" },
            {'Ф', "F" },
            {'Х', "CH" },
            {'Ц', "C" },
            {'Ч', "CZ" },
            {'Ш', "SZ" },
            {'Ю', "JU" },
            {'Я', "JA" },
        };
        private Dictionary<char, string> russianAlph = new Dictionary<char, string>()
        {
            {'А', "A" },
            {'Б', "B" },
            {'В', "W" },
            {'Г', "G" },
            {'Д', "D" },
            {'Е', "JE" },
            {'Ё', "JO" },
            {'Ж', "Ż" },
            {'З', "Z" },
            {'И', "I" },
            {'Й', "J" },
            {'К', "K" },
            {'Л', "Ł" },
            {'М', "M" },
            {'Н', "N" },
            {'О', "O" },
            {'П', "P" },
            {'Р', "R" },
            {'С', "S" },
            {'Т', "T" },
            {'У', "U" },
            {'Ф', "F" },
            {'Х', "CH" },
            {'Ц', "C" },
            {'Ч', "CZ" },
            {'Ш', "SZ" },
            {'Щ', "SZCZ" },
            {'Ы', "Y" },
            {'Э', "E" },
            {'Ю', "JU" },
            {'Я', "JA" },
        };
        private Dictionary<string, string> ukrBelReplacements = new Dictionary<string, string>()
        {
            {"ŁIA", "LA"},
            {"ŁIE", "LE"},
            {"ŁIU", "LU"},
            {"ŁIO", "LO"},
            {"ŁI", "LI"},
            {"ŁЬ", "L"},
            {"ŁL", "LL" },
            {"NЬ", "Ń"},
            {"ZЬ", "Ź"},
            {"SЬ", "Ś"},
            {"CЬ", "Ć"}
        };
        private Dictionary<string, string> rusReplacements = new Dictionary<string, string>()
        {
            {"ŻIE", "ŻE"},
            {"CIE", "CE"},
            {"CZIE", "CZE"},
            {"SZIE", "SZE"},
            {"ŁЬ", "LЬ"},
            {"ЪJO", "JO" },
            {"ЬJO", "JO" },
            {"ЪJE", "JE" },
            {"ЬJE", "JE" },
            {"ЪJU", "JU" },
            {"ЬJU", "JU" },
            {"ЪJA", "JA" },
            {"ЬJA", "JA" },
            {"ŻIO", "ŻO" },
            {"CZIO", "CZO" },
            {"SZIO", "SZO" },
            {"ŻЬ", "Ż"},
            {"ЬI", "JI" },
            {"ŻI", "ŻY" },
            {"CI", "CY" },
            {"SZI", "SZY" },
            {"CZЬ", "CZ"},
            {"SZЬ", "SZ"},
            {"ŁIE", "LE"},
            {"ŁIA", "LA"},
            {"ŁIU", "LU"},
            {"ŁIO", "LO" },
            {"ŁI", "LI"},
            {"NЬ", "Ń"},
            {"ZЬ", "Ź"},
            {"SЬ", "Ś"},
            {"CЬ", "Ć"},
            {"LЬ", "L" }
        };

        public Translator()
        {
            //initializes Tranlsator class
        }

        public string translateUkrainian(string text, bool polszczenie)
        {
            StringBuilder newString = new StringBuilder(text.ToUpper());
            newString.Replace("ЬО", "IO");
            foreach(char key in ukrainianAlph.Keys)
            {
                newString.Replace(key.ToString(), ukrainianAlph[key]);
            }
            adjustUkrBelWriting(newString);
            string returnString = newString.ToString();
            if (polszczenie)
            {
                returnString = Regex.Replace(returnString, @"(ŚKYJ\b)", "SKI");
                returnString = Regex.Replace(returnString, @"(ĆKYJ\b)", "CKI");
                returnString = Regex.Replace(returnString, @"(ŚKA\b)", "SKA");
                returnString = Regex.Replace(returnString, @"(ĆKA\b)", "CKA");
                returnString = Regex.Replace(returnString, @"(YJ\b)", "Y");
            }
            return returnString;
        }
        public string translateBelarusian(string text, bool polszczenie)
        {
            StringBuilder newString = new StringBuilder(text.ToUpper());
            foreach (char key in belarusianAlph.Keys)
            {
                newString.Replace(key.ToString(), belarusianAlph[key]);
            }
            adjustUkrBelWriting(newString);
            string returnString = newString.ToString();
            if (polszczenie)
            {
                returnString = Regex.Replace(returnString, @"(SKAJA\b)", "SKA");
                returnString = Regex.Replace(returnString, @"(CKAJA\b)", "CKA");
            }
            return returnString;
        }
        public string translateRussian(string text, bool polszczenie)
        {
            StringBuilder newString = new StringBuilder(text.ToUpper());
            foreach (char key in russianAlph.Keys)
            {
                newString.Replace(key.ToString(), russianAlph[key]);
            }
            replaceConsonantsBeforeYot(newString);
            foreach (string key in rusReplacements.Keys)
            {
                newString.Replace(key, rusReplacements[key]);
            }
            newString = newString.Replace("Ь", "´");
            newString = newString.Replace("Ъ", "");
            string returnString = newString.ToString();
            if (polszczenie)
            {
                returnString = Regex.Replace(returnString, @"(SKIJ\b)", "SKI");
                returnString = Regex.Replace(returnString, @"(CKIJ\b)", "CKI");
                returnString = Regex.Replace(returnString, @"(SKAJA\b)", "SKA");
                returnString = Regex.Replace(returnString, @"(CKAJA\b)", "CKA");
                returnString = Regex.Replace(returnString, @"(IJ\b)", "I");
                returnString = Regex.Replace(returnString, @"(YJ\b)", "Y");
                returnString = Regex.Replace(returnString, @"([BWHGDŻZKŁLMNPRSTFC])(J)", "$1I");
            }
            return returnString;
        }
        private void adjustUkrBelWriting(StringBuilder text)
        {
            replaceConsonantsBeforeYot(text);
            foreach (string key in ukrBelReplacements.Keys)
            {
                text.Replace(key, ukrBelReplacements[key]);
            }
            removeApostrophe(text);
            text.Replace("Ь", "´");
        }
        private void removeApostrophe(StringBuilder text)
        {
            List<char> charsToRemove = new List<char>() { (char)39, '`', '´', '’', '‘', 'ʻ', 'ʽ', 'ʹ', '΄', '՚', '‛', '′' };
            foreach (char c in charsToRemove)
            {
                text.Replace(c.ToString(), String.Empty);
            }
        }
        private void replaceConsonantsBeforeYot(StringBuilder text)
        {
            text.Append(' ');
            for(int i = 0; i < text.Length - 1; i++)
            {
                if (consonants.Contains(text[i]) && text[i + 1] == 'J')
                {
                    text[i + 1] = 'I';
                    i++;
                }
            }
        }
    }
}
