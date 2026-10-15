using System;
using System.Collections.Generic;
using System.Text;

namespace SkyDesert.Localization.Extensions
{
    public static class StringExtensions
    {
        public static string ReplacePlaceholders(this string input, PlaceholderResolver resolve, bool ignoreEscapes = false)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (resolve == null) throw new ArgumentNullException(nameof(resolve));

            StringBuilder stringBuilder = new StringBuilder();

            int openBraceIndex = -1;
            int appendedIndex = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char character = input[i];

                if (ignoreEscapes && character == '\\')
                {
                    i++;
                    continue;
                }

                if (character == '{')
                {
                    openBraceIndex = i;
                    stringBuilder.Append(input, appendedIndex, i - appendedIndex);
                    appendedIndex = i;
                }
                else if (character == '}' && openBraceIndex != -1)
                {
                    string placeholderName = input.Substring(openBraceIndex + 1, i - appendedIndex - 1);
                    stringBuilder.Append(resolve(placeholderName));
                    openBraceIndex = -1;
                    appendedIndex = i + 1;
                }
            }

            stringBuilder.Append(input, appendedIndex, input.Length - appendedIndex);

            return stringBuilder.ToString();
        }

        public static string[] Split(this string input, char separator, bool ignoreEscapes)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            if (ignoreEscapes == false) return input.Split(separator);

            int i = 0;

            List<string> output = new List<string>();

            while (i < input.Length)
            {
                int separatorIndex = input.IndexOf(separator, i, true);
                if (separatorIndex == -1)
                {
                    output.Add(input.Substring(i, input.Length - i));
                    break;
                }

                output.Add(input.Substring(i, separatorIndex - i));

                i = separatorIndex + 1;
            }

            return output.ToArray();
        }

        public static int IndexOf(this string input, char value, int startIndex, bool ignoreEscapes)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (startIndex < 0 || startIndex > input.Length) throw new ArgumentOutOfRangeException(nameof(startIndex));

            if (ignoreEscapes == false) return input.IndexOf(value, startIndex);

            for (int i = startIndex; i < input.Length; i++)
            {
                if (input[i] == '\\')
                {
                    if (i + 1 == input.Length)
                    {
                        throw new FormatException("Escape character has no following character.");
                    }
                    i++;
                    continue;
                }
                else if (input[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        public static int IndexOf(this string input, char value, bool ignoreEscapes)
        {
            return input.IndexOf(value, 0, ignoreEscapes);
        }
    }
}
