using System;

namespace Authentication.Entity.EF
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}