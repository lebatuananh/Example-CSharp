using System;
using System.Threading.Tasks;

namespace Asynchronous
{
    public static class Program
    {
        public static void Main(string[] args)
        {
//            Manipulate manipulate=new Manipulate();
//            Task<int> result = manipulate.AccessWebAsync();
//            Console.WriteLine(result.Result);
            Manipulate1 manipulate1=new Manipulate1();
            manipulate1.Method1();
            manipulate1.Method2();
            Console.ReadKey();
        }
    }
}