using System;
using System.Threading.Tasks;

namespace Asynchronous
{
    public class Manipulate1
    {
        public async Task Method1()
        {
            await Task.Run(
                () =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        Console.WriteLine("Method1 {0}",i);
                    }
                }
            );
        }

        public void Method2()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Method2 {0}",i);
            }
        }
    }
}