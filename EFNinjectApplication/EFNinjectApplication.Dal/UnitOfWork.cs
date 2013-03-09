using System;
using EFNinjectApplication.CrossCutting.Interfaces;
using EFNinjectApplication.CrossCutting.Models;

namespace EFNinjectApplication.Dal
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Fields and Properties

        #region Fields
        private readonly AppDbContext _context;
        private IRepository<Customer> _customers;
        #endregion

        #region Properties

        public IRepository<Customer> Customers 
        {
            get { return GetOrCreateRepository(ref _customers); }
        }
        #endregion

        #endregion

        #region Methods

        #region Ctor
        public UnitOfWork()
        {
            _context = new AppDbContext();
        }
        public void Dispose()
        {
            Dispose(true);
// ReSharper disable GCSuppressFinalizeForTypeWithoutDestructor
            GC.SuppressFinalize(this);
// ReSharper restore GCSuppressFinalizeForTypeWithoutDestructor
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            
            if (_context != null)
                _context.Dispose();
        }

        #endregion

        #region Private Methods
        private IRepository<T> GetOrCreateRepository<T>(ref IRepository<T> repo) where T : class
        {
            return repo ?? (repo = new BaseRepository<T>(_context));
        }
        #endregion

        #region Public Methods
        public void Save()
        {
            _context.SaveChanges();
        }
        #endregion

        #endregion
    }
}
