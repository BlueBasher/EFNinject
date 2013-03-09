using EFNinjectApplication.CrossCutting.Models;

namespace EFNinjectApplication.CrossCutting.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Customer> Customers { get; }

        void Save();
    }
}
