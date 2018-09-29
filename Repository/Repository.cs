using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private bool disposed = false;
        private readonly IUnitOfWork _unitOfWork;
        public DbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(IUnitOfWork unitOfWork)
        {
            //   if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
            this.dbSet = _unitOfWork.Db.Set<TEntity>();
            this.context = _unitOfWork.Db;
            //context.Configuration.ProxyCreationEnabled = false;
        }

        public virtual IEnumerable<TEntity> GetList()
        {
            return dbSet.AsNoTracking();
        }


        public virtual IQueryable<TEntity> GetIQuerable()
        {
            return dbSet;
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
           IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        public virtual IEnumerable<TEntity> GetGridData(out int dataCount, int page, int count, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }
                dataCount = query.Count();
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).Skip((page - 1) * count).Take(count).ToList();
                }
                else
                {
                    return query.Skip((page - 1) * count).Take(count).ToList();
                }
            }
            catch
            {
                dataCount = 0;
                return null;
            }
        }
        public virtual TEntity GetById(object id)
        {
            try
            {
                return dbSet.Find(id);
            }
            catch
            {

                return null;
            }
        }

        public virtual TEntity GetById(params object[] ids)
        {
            try
            {

                return dbSet.Find(ids);
            }
            catch
            {

                return null;
            }
        }

        public virtual void Insert(TEntity entity)
        {
            try
            {
                if (entity != null)
                    dbSet.Add(entity);
            }
            catch { }
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            //try
            //{
            if (entityToUpdate != null)
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            //}
            //catch(Exception ex) { }
        }

        //public virtual void Save(TEntity entityToSave)
        //{
        //    ObjectContext objContext = ((IObjectContextAdapter)context).ObjectContext;
        //    ObjectSet<TEntity> objSet = objContext.CreateObjectSet<TEntity>();
        //    EntityKey entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entityToSave);
        //    object foundEntity;
        //    bool exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

        //    if (exists)
        //    {
        //        /*You must check if an entity with the same key is already tracked by the context 
        //         * and modify that entity instead of attaching the current one:*/
        //        //var entry = context.Entry<TEntity>(entityToSave);
        //        //if (entry.State == EntityState.Detached)
        //        //{
        //        //    var set = context.Set<TEntity>();


        //        //    if (foundEntity != null)
        //        //    {
        //        //        var attachedEntry = context.Entry(foundEntity);
        //        //        attachedEntry.CurrentValues.SetValues(entityToSave);
        //        //    }
        //        //    else
        //        //    {
        //        //        entry.State = EntityState.Modified; // This should attach entity
        //        //    }
        //        //}

        //        context.Entry(foundEntity).State = EntityState.Detached;
        //        this.Update(entityToSave);
        //    }
        //    else
        //    {
        //        this.Insert(entityToSave);
        //    }
        //}

        public virtual void Delete(object id)
        {
            try
            {
                TEntity entityToDelete = dbSet.Find(id);
                if (entityToDelete != null)
                {
                    Delete(entityToDelete);
                }
            }
            catch
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            try
            {
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            }
            catch
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        //public virtual List<TEntity> CallStoredProcedure(string SPname, object[] listParams)
        //{
        //    try
        //    {
        //        string SPSignature = "";
        //        if (listParams != null)
        //        {
        //            SPSignature = PrepareSPNameWithParameter(SPname, listParams);
        //        }
        //        ObjectContext currContext = ((IObjectContextAdapter)context).ObjectContext;
        //        ObjectResult<TEntity> result = currContext.ExecuteStoreQuery<TEntity>(SPSignature, listParams);
        //        return result.ToList<TEntity>();
        //    }
        //    catch { return new List<TEntity>(); }
        //}

        //public virtual List<TEntity> CallStoredProcedure(string SPname)
        //{
        //    try
        //    {
        //        ObjectContext currContext = ((IObjectContextAdapter)context).ObjectContext;
        //        ObjectResult<TEntity> result = currContext.ExecuteStoreQuery<TEntity>(SPname);
        //        return result.ToList();
        //    }
        //    catch { return new List<TEntity>(); }
        //}

        public string PrepareSPNameWithParameter(string SPname, object[] listParams)
        {
            try
            {
                string ParameterSignature = SPname;
                if (listParams != null)
                {
                    for (int i = 0; i < listParams.Length; i++)
                    {
                        if (i > 0)
                        {
                            ParameterSignature += ",";
                        }
                        ParameterSignature += " " + listParams[i].ToString();
                    }
                }
                return ParameterSignature;
            }
            catch { return ""; }
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        context.Dispose();
                    }
                }
                this.disposed = true;
            }
            catch { }
        }

        public void Dispose()
        {
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch { }
        }

    }
}
