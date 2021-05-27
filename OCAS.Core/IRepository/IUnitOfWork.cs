using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OCAS.DataAccess;

namespace OCAS.Core.IRepository
{
    public interface IUnitOfWork: IDisposable 
    {
        IGenericRepository<User> Users { get; }

        IGenericRepository<Activity> Activities { get; }

        Task Save();

    }

}
