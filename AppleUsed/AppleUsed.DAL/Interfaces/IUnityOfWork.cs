using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Interfaces
{
    public interface IUnityOfWork
    {
       IUserRepository UserRepository { get; set; }
    }
}
