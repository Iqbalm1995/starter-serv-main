using System.Globalization;

namespace starter_serv.Helper
{
    public class RupiahHelper
    {
        public static string convertToRupiah(string numberInput)
        {
            decimal amount = 0;

            try
            {
                amount = decimal.Parse(numberInput);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }

            string formattedAmount = amount.ToString("C0", new CultureInfo("id-ID"));

            return formattedAmount; 
        }
    }
}
