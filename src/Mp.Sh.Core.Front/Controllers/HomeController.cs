/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Mp.Sh.Core.Front.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Public Methods

        /// <summary>
        /// Index View 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var claims = User.Claims.ToList();
            return View();
        }

        /// <summary>
        /// Logout from the Browser and also from Identity Server 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies"); // remove the cookie
            return Redirect("http://localhost:83/Account/Logout");
        }

        #endregion Public Methods
    }
}