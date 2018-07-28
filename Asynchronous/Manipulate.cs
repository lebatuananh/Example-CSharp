using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Asynchronous
{
    public class Manipulate
    {
        public async Task<int> AccessWebAsync()
        {
            HttpClient client=new HttpClient();
            Task<string> getStringTask = client.GetStringAsync("https://www.youtube.com/watch?v=0KxgLe2mpb0");
            string content = await getStringTask;
            Display2();
            Display();
            return content.Length;
        }

        private static void Display()
        {
            Console.WriteLine("Do working...");
        }

        private static void Display2()
        {
            for (int i = 0; i < 10000; i++)
            {
                Console.WriteLine(i);
            }
        }
        
    }
}