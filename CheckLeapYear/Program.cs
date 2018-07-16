using System;
using System.Text;

namespace CheckLeapYear
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
<<<<<<< HEAD
            bool flag = true;
=======
            var flag = true;
>>>>>>> Check Leap Year Example
            do
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("Kiểm tra năm nhuận");
                Console.WriteLine("Nhập năm bạn muốn kiếm tra");
                // ReSharper disable once AssignNullToNotNullAttribute
<<<<<<< HEAD
                var year = Int32.Parse(s: Console.ReadLine());
=======
                var year = int.Parse(s: Console.ReadLine());
>>>>>>> Check Leap Year Example
                Console.WriteLine(CheckLeapYear(year) ? "{0} là năm nhuận nhé" : "{0} không phải năm nhuận nhé", year);
                Console.WriteLine("Bạn có muốn tiếp tục không, nhấn c/k");
                var s = Console.ReadLine();
                switch (s)
                {
                    case "c":
                        flag = true;
                        break;
                    case "k":
                        flag = false;
                        break;
                        
                }
            } while (flag);
        }

        private static bool CheckLeapYear(int year)
        {
<<<<<<< HEAD
            if ((year % 400) == 0 || ((year % 4) == 0 && (year % 100) != 0)) return true;
            return false;
=======
            return (year % 400) == 0 || ((year % 4) == 0 && (year % 100) != 0);
>>>>>>> Check Leap Year Example
        }
    }
}