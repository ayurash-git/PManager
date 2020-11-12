using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PManager.Domain.Models.Base;
using PManager.EF.Context;
using PManager.Interfaces;

namespace PManager.EF
{
    internal class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly PManagerDB _db;
        private readonly DbSet<T> _set;
        public virtual IQueryable<T> Items => _set;
        public bool AutoSaveChanges { get; set; } = true;
        
        #region Constructor

        /// <summary> Constructor </summary>
        /// <param name="db"></param>
        public DbRepository(PManagerDB db)
        {
            _db = db;
            _set = db.Set<T>();
        }

        #endregion


        #region Get()

        public T Get(int id) => Items.SingleOrDefault(item => item.Id == id);

        public async Task<T> GetAsync(int id, CancellationToken cancel = default) => await Items
            .SingleOrDefaultAsync(item => item.Id == id, cancel)
            .ConfigureAwait(false);

        #endregion
        
        #region Add()

        public T Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges) _db.SaveChanges();
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges) await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
            return item;
        }

        #endregion

        #region Update()

        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges) _db.SaveChanges();
        }

        public async Task UpdateAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges) await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        }

        #endregion

        #region Remove()

        public void Remove(int id)
        {
            _db.Remove(new T { Id = id });
            if (AutoSaveChanges) _db.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken cancel = default)
        {
            _db.Remove(new T { Id = id });
            if (AutoSaveChanges) await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        }

        #endregion
    }
}
