using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppleUsed.BLL.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository { get; set; }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IQueryable<UserDTO> GetUsers()
        {
            var users = _userRepository.GetUsers().Select(u =>
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
