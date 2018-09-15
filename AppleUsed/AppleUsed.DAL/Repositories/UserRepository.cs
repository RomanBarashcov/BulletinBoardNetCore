using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppleUsed.DAL.Repositories
{
    public class UserRepository : IUserRepository , IDisposable
    {
        private AppDbContext _db { get; set; }

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<ApplicationUser> GetUsers()
        {
            var users = _db.Users;
            return users;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db = null;
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
