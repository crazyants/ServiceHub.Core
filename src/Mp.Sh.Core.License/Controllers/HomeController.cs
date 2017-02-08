/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mp.Sh.Core.License.Models;
using Mp.Sh.Core.License.Services;
using System.Threading.Tasks;

namespace Mp.Sh.Core.License.Controllers
{
    [SecurityHeaders]
    public class HomeController : Controller
    {
        #region Private Fields

        private readonly IIdentityServerInteractionService _interaction;

        #endregion Private Fields

        #region Public Constructors

        public HomeController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Shows the error page 
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        #endregion Public Methods
    }
}