using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Transactions;

namespace Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private TransactionScope _transaction;
        private readonly DbContext _db;

        public UnitOfWork(DbContext context)
        {
            _db = context;
        }

        public DbContext Db
        {
            get { return _db; }
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void StartTransaction()
        {
            _transaction = new TransactionScope();
        }

        public int Submit()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
                return 0;
            }
        }

        public int Submit(out string exceptionMessage)
        {
            exceptionMessage = null;
            try
            {
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break(); // open vs on debugging mode
                exceptionMessage = ex.InnerException == null ? ex.Message : ex.InnerException.StackTrace;
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                        exceptionMessage = ex.InnerException.InnerException.Message;
                    else
                        exceptionMessage = ex.InnerException.Message;
                }
                else
                {
                    exceptionMessage = ex.Message;
                }
                return 0;
            }
        }
    }
}
