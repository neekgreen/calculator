namespace CalculatorApp
{
    public static class StringExtensions
    {
        public static string[] ToCommandLineArgs(this string s)
        {
            var parmChars = s.ToCharArray();
            var inQuote = false;

            for (int index = 0; index < parmChars.Length; index++)
            {
                if (parmChars[index] == '"')
                    inQuote = !inQuote;
                if (!inQuote && parmChars[index] == ' ')
                    parmChars[index] = '\n';
            }
            return (new string(parmChars)).Split('\n');
        }
    }
}