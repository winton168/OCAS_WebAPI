using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OCAS.Core.IRepository;
using OCAS.DataAccess;

namespace OCAS.Core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OCASContext _context;
        private IGenericRepository<User> _users;
        private IGenericRepository<Activity> _activies;

        public UnitOfWork(OCASContext context)
        {
            _context = context;
        }


        public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);


        public IGenericRepository<Activity> Activities => _activies ??= new GenericRepository<Activity>(_context);


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }

}
