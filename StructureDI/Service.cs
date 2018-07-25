using System;

namespace StructureDI
{
    public interface IService
    {
        void Serve();
    }
    public class Service:IService
    {
        public void Serve()
        {
            Console.WriteLine("Servive Called");
            Console.WriteLine("Hello Word");
        }
    }
}