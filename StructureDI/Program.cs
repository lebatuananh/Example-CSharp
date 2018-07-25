namespace StructureDI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var client=new Client(new Service());
            client.Start();
            IService service=new Service();
            service.Serve();
        }
    }
}