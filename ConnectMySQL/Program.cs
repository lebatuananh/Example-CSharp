using System;

namespace ConnectMySQL
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var context=new AppDbContext())
            {
                //DbInitializer dbInitializer=new DbInitializer(context);
                //dbInitializer.Seed().Wait();
            }
        }
    }
}