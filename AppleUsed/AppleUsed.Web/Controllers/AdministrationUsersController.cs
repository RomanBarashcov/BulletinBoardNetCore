using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.Web.Models.ViewModels.AdViewModels;
using AppleUsed.Web.Models.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppleUsed.Web.Controllers
{
    public class AdministrationUsersController : Controller
    {
        private IUserService _userService;

        public AdministrationUsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5;
            UserIndexViewModel model = new UserIndexViewModel() { UserList = new List<UserDTO>() };
            model.UserList = await _userService.GetUsers().ToListAsync();

            int count = model.UserList.Count();
            model.PageViewModel = new PageViewModel(count, page, pageSize);
            model.UserList = model.UserList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(model);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _userService = null;
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