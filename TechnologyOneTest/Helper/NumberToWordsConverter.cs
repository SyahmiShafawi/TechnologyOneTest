namespace TechnologyOneTest.Helper
{
    public static class NumberToWordsConverter
    {
        private static readonly string[] Units =
        { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE" };

        private static readonly string[] Teens =
        { "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };

        private static readonly string[] Tens =
        { "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

        private static readonly string[] Thousands =
        { "", "THOUSAND", "MILLION", "BILLION", "TRILLION", "QUADRILLION",
          "QUINTILLION", "SEXTILLION", "SEPTILLION", "OCTILLION", "NONILLION", "DECILLION" };

        public static string ConvertToWords(decimal number)
        {
            if (number == 0) return "ZERO DOLLARS";

            var dollars = (long)number;
            var cents = (long)((number - dollars) * 100);

            string dollarWords = dollars > 0 ? $"{ConvertWholeNumberToWords(dollars)} DOLLARS" : "";
            string centWords = cents > 0 ? $"{ConvertWholeNumberToWords(cents)} CENTS" : "";

            return string.IsNullOrWhiteSpace(centWords) ? dollarWords : $"{dollarWords} AND {centWords}";
        }

        private static string ConvertWholeNumberToWords(long number)
        {
            if (number == 0) return Units[0];

            var words  = "";
            var place = 0;

            do
            {
                var n = number % 1000;
                if (n > 0)
                {
                    var groupWords = ConvertHundredsToWords(n);
                    words = $"{groupWords} {Thousands[place]} {words}".Trim();
                }
                place++;
                number /= 1000;
            } while (number > 0);

            return words.Trim();
        }
         
        private static string ConvertHundredsToWords(long number)
        {
            var words = "";

            if (number > 99)
            {
                words += $"{Units[number / 100]} HUNDRED AND ";
                number %= 100;
            }

            if (number > 19)
            {
                words += $"{Tens[number / 10]} ";
                number %= 10;
            }
            else if (number > 9)
            {
                words += $"{Teens[number - 10]} ";
                return words.Trim();
            }

            if (number > 0)
            {
                words += Units[number];
            }

            return words.Trim();
        }
    }

}
