/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        #endregion Public Methods
    }
}