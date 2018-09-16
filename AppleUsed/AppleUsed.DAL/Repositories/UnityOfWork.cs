using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        private IUserRepository _userRepository { get; set; }

        public UnityOfWork(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository; }
            set { _userRepository = value; }
        }
    }
}
