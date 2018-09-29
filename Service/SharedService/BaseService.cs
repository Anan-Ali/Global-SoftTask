using Context;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SharedService
{
    public class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BaseService()
        {
            var ctx = new TaskDbEntities();
            //ctx.Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["QVControlPanelEntities"].ConnectionString;
            _unitOfWork = new UnitOfWork(ctx);
        }
    }
}
