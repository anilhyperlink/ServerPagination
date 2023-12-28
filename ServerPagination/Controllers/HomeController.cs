using Microsoft.AspNetCore.Mvc;
using ServerPagination.DataAccess.DatabaseContext;
using ServerPagination.Models;
using ServerPagination.Models.Comman;
using ServerPagination.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServerPagination.Controllers
{
    public class HomeController : Controller
    {
        private readonly IManageService<SetPagination, PaginationModel> _userListManageService;
        private readonly IManageService<UserModel, string> _addUserManageService;
        private readonly IManageService<(string, int), string> _deleteUserManageService;
        private readonly IManageService<(string, int), string> _activeManageService;
        private readonly IManageService<(string, int), EditUserModel> _getUserManageService;
        private readonly IManageService<EditUserModel, string> _editUserManageService;

        public HomeController
            (
                IManageService<SetPagination, PaginationModel> userListManageService,
                IManageService<UserModel, string> addUserManageService,
                IManageService<(string, int), string> deleteUserManageService,
                IManageService<(string, int), string> activeManageService,
                IManageService<(string, int), EditUserModel> getUserManageService,
                IManageService<EditUserModel, string> editUserManageService
            )
        {
            _userListManageService = userListManageService;
            _addUserManageService = addUserManageService;
            _deleteUserManageService = deleteUserManageService;
            _activeManageService = activeManageService;
            _getUserManageService = getUserManageService;
            _editUserManageService = editUserManageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Users()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadUserList(int pageNumber, string searchQuery)
        {
            try
            {
                SetPagination setPagination = new SetPagination();
                setPagination.PageNumber = pageNumber;
                if (searchQuery != null)
                {
                    setPagination.SearchQuery = searchQuery;
                }
                var response = await _userListManageService.PostAsync(UrlConstants.GetUserListUrl, setPagination);
                return PartialView("_UserList", response);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        
        [HttpGet]
        public IActionResult AddUser()
        {
            return PartialView("_AddUser");
        }
        
        [HttpPost]
        public IActionResult AddUser(UserModel user)
        {
            try
            {
                TryValidateModel(user);
                if (ModelState.IsValid)
                {
                    var response = _addUserManageService.PostAsync(UrlConstants.AddUserUrl, user);
                    if (response == null)
                    {
                        return BadRequest();
                    }
                    return Ok(true);
                }
                return View(user);  
                
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetUser(int UserId)
        {
            var response = _getUserManageService.GetAsync(UrlConstants.GetUserManageUrl, UserId);
            if (response == null)
            {
                return BadRequest();
            }
            return PartialView("_EditUser", response.Result);
        }

        [HttpPut]
        public IActionResult EditUser(EditUserModel user)
        {
            TryValidateModel(user);
            if (ModelState.IsValid)
            {
                var response = _editUserManageService.PutAsync(UrlConstants.EditUserUrl, user);
                if (response == null)
                {
                    return BadRequest();
                }
                return Ok(true);
            }
            return PartialView("_EditUser", user);
        }

        [HttpGet]
        public IActionResult ActiveManage(int UserId)
        {
            var response = _activeManageService.GetAsync(UrlConstants.ActiveManageUrl, UserId);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(true);
        }

        [HttpDelete]
        public IActionResult DeleteUser(int UserId)
        {
            var response = _deleteUserManageService.DeleteAsync(UrlConstants.DeleteUserUrl, UserId);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(true);
        }

        [HttpGet]
        public IActionResult ViewUser(int UserId)
        {
            var response = _getUserManageService.GetAsync(UrlConstants.GetUserManageUrl, UserId);
            if (response == null)
            {
                return BadRequest();
            }
            return PartialView("_ViewUser", response.Result);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


