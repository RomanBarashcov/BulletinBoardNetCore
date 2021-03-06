﻿using AppleUsed.DAL.Identity;
using AppleUsed.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Repositories
{
    public class UserRepository : IUserRepository
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

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            var user = await _db.Users.FindAsync(userId);
            return user;
        }

        public async Task<ApplicationUser> FindUserByUserName(string userName)
        {
            var user = await _db.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            return user;
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
