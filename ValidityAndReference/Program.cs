using System;
using System.Text;

namespace ValidityAndReference
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int a = 5;
            Console.WriteLine(TestValidity(a));
            int b = 20;
            TestReference1(ref a, ref b);
            Console.WriteLine(a + " " + b);
            TestReference2(out a, out b);
            Console.WriteLine(a + " " + b);
        }

        /// <summary>
        /// Không thay đổi giá trị sau khi kết thúc hàm
        /// </summary>
        /// <param name="temp1"></param>
        /// <returns></returns>
        private static int TestValidity(int temp1)
        {
            temp1 = 4;
            return temp1;
        }

        /// <summary>
        /// Có thay đổi giá trị sau khi kết thúc hàm
        /// </summary>
        /// <param name="temp1"></param>
        /// <param name="temp2"></param>
        /// <returns></returns>
        private static void TestReference1(ref int temp1, ref int temp2)
        {
            temp1 = 10;
            var temp = temp1;
            temp1 = temp2;
            temp2 = temp;
        }

        /// <summary>
        /// Muốn sử dụng out phải có giá trị trước khi sử dụng
        /// </summary>
        /// <param name="temp1"></param>
        /// <param name="temp2"></param>
        private static void TestReference2(out int temp1, out int temp2)
        {
            temp2 = 10;
            temp1 = temp2;
        }
    }
}