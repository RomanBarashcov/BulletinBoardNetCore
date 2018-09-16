using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Interfaces;
using AppleUsed.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppleUsed.BLL.Services
{
    public class UserService : IUserService
    {
        private UnityOfWork _unitOfWork { get; set; }

        public UserService(UnityOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<UserDTO> GetUsers()
        {
            var users = _unitOfWork.UserRepository.GetUsers().Select(u =>
            new UserDTO
            {
                Id = u.Id,
                Email = u.Email,
                UserName = u.UserName,
                PhoneNumber = u.PhoneNumber,
                RegistrationDate = u.RegistrationDate
            });

            return users;
        }
    }
}
