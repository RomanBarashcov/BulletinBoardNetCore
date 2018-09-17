using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        private IAdRepository _adRepository { get; set; }
        private IAdUpRepository _adUpRepository { get; set; }
        private IUserRepository _userRepository { get; set; }

        public UnityOfWork(
            IAdRepository adRepository,
            IAdUpRepository adUpRepository,
            IUserRepository userRepository)

        {
            _adRepository = adRepository;
            _adUpRepository = adUpRepository;
            _userRepository = userRepository;
            
        }

        public IAdRepository AdRepository
        {
            get { return _adRepository; }
            set { _adRepository = value; }
        }

        public IAdUpRepository AdUpRepository
        {
            get { return _adUpRepository; }
            set { _adUpRepository = value; }
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository; }
            set { _userRepository = value; }
        }

       
    }
}
