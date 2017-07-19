using System;

namespace FromRomanNumerals
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Please enter a roman number (<quit> to exit) [MMXVII]:");
            string line;
            while ((line = Console.ReadLine()) != "quit")
            {
                try
                {
                    var roman = line;
                    if (roman == "")
                    {
                        roman = "MMXVII";
                    }
                    var latin = RomanNumber.ToDecimal(roman?.ToUpper());
                    Console.WriteLine($"{roman} is {latin}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
