using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Repository
{
    public interface IUnitOfWork:IDisposable
    {
        /// <summary>
        /// Call this to commit the unit of work
        /// </summary>
        int Submit();
        int Submit(out string exceptionMessage);


        /// <summary>
        /// Return the database reference for this UOW
        /// </summary>
        DbContext Db { get; }

        /// <summary>
        /// Starts a transaction on this unit of work
        /// </summary>
        void StartTransaction();
    }
}
