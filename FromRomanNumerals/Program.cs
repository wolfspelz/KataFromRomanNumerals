using System;

namespace FromRomanNumerals
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Please enter a roman number (ENTER to exit):");
            string line;
            while ((line = Console.ReadLine()) != "")
            {
                try
                {
                    var roman = line;
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
