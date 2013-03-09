using System;
using System.Linq;
using System.Data.Entity;
using System.Data;
using EFNinjectApplication.CrossCutting.Interfaces;

namespace EFNinjectApplication.Dal
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields and Properties
        #region Fields
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        #endregion
        #region Protected Properties
        protected DbContext Context
        {
            get
            {
                return _context;
            }
        }

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return _dbSet;
            }
        }
        #endregion
        #endregion

        #region Methods

        #region Ctor
        internal BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        #endregion

        #region Public Methods
        public virtual void Add(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Added;
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public TEntity FetchById(object id)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            return _dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> Fetch(string includeProperties)
        {
            _context.Configuration.ProxyCreationEnabled = false;

            IQueryable<TEntity> query = _dbSet;

            query = includeProperties
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query;
        }
        #endregion

        #endregion
    }
}
