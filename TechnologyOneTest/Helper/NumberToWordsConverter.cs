namespace TechnologyOneTest.Helper
{
    public class NumberToWordsConverter
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

        private readonly ApplicationDbContext _context;
        private readonly Dictionary<string, string> _wordCache;


        public NumberToWordsConverter(ApplicationDbContext context)
        {
            _context = context;
            _wordCache = _context.NumberWords
                .ToDictionary(w => $"{w.Word}_{w.Category}", w => w.Word);
        }

        public static string ConvertToWordsLocal(decimal number)
        {
            if (number == 0) return "ZERO DOLLARS";

            var dollars = (long)number;
            var cents = (long)((number - dollars) * 100);

            string dollarWords = dollars > 0 ? $"{ConvertWholeNumberToWordsLocal(dollars)} DOLLARS" : "";
            string centWords = cents > 0 ? $"{ConvertWholeNumberToWordsLocal(cents)} CENTS" : "";

            return string.IsNullOrWhiteSpace(centWords) ? dollarWords : $"{dollarWords} AND {centWords}";
        }

        private static string ConvertWholeNumberToWordsLocal(long number)
        {
            if (number == 0) return Units[0];

            var words  = "";
            var place = 0;

            do
            {
                var n = number % 1000;
                if (n > 0)
                {
                    var groupWords = ConvertHundredsToWordsLocal(n);
                    words = $"{groupWords} {Thousands[place]} {words}".Trim();
                }
                place++;
                number /= 1000;
            } while (number > 0);

            return words.Trim();
        }
         
        private static string ConvertHundredsToWordsLocal(long number)
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



        private string GetWordDB(string fallbackWord, string category)
        {
            var key = $"{fallbackWord}_{category}";
            if (_wordCache.TryGetValue(key, out var cachedWord))
            {
                return cachedWord;
            }
            return fallbackWord;
        }

        public string ConvertToWordsDB(decimal number)
        {
            if (number == 0) return "ZERO DOLLARS";

            var dollars = (long)number;
            var cents = (long)((number - dollars) * 100);

            string dollarWords = dollars > 0 ? $"{ConvertWholeNumberToWordsDB(dollars)} DOLLARS" : "";
            string centWords = cents > 0 ? $"{ConvertWholeNumberToWordsDB(cents)} CENTS" : "";

            return string.IsNullOrWhiteSpace(centWords) ? dollarWords : $"{dollarWords} AND {centWords}";
        }

        private string ConvertWholeNumberToWordsDB(long number)
        {
            if (number == 0) return "ZERO";

            var words = "";
            var place = 0;

            do
            {
                var n = number % 1000;
                if (n > 0)
                {
                    var groupWords = ConvertHundredsToWordsDB(n);
                    words = $"{groupWords} {Thousands[place]} {words}".Trim();
                }
                place++;
                number /= 1000;
            } while (number > 0);

            return words.Trim();
        }

        private string ConvertHundredsToWordsDB(long number)
        {
            var words = "";

            if (number > 99)
            {
                words += $"{GetWordDB(Units[number / 100], "Units")} HUNDRED AND ";
                number %= 100;
            }

            if (number > 19)
            {
                words += $"{GetWordDB(Tens[number / 10], "Tens")} ";
                number %= 10;
            }
            else if (number > 9)
            {
                words += $"{GetWordDB(Teens[number - 10], "Teens")} ";
                return words.Trim();
            }

            if (number > 0)
            {
                words += GetWordDB(Units[number], "Units");
            }

            return words.Trim();
        }

    }

}
