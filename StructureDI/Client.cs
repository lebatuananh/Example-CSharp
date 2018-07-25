using System;

namespace StructureDI
{
    public class Client
    {
        private readonly IService _service;

        public Client(IService service)
        {
            _service = service;
        }

        public void Start()
        {
            Console.WriteLine("Service Start");
            _service.Serve();
        }
    }
}