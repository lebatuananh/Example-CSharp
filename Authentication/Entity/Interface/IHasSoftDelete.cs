namespace Authentication.Entity.Interface
{
    internal interface IHasSoftDelete
    {
        bool IsDelete { get; set; }
    }
}