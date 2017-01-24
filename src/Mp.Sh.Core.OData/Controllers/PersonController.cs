/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mp.Sh.Core.OData.Controllers
{
    /// <summary>
    /// Returns information for the resource "Person" 
    /// </summary>
    [Route("persons")]
    [Authorize]
    public class PersonController : ControllerBase
    {
        #region Public Methods

        /// <summary>
        /// Get a collection of Persons 
        /// </summary>
        /// <returns> It can intercept query filters of type OData v 4.0 </returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new JsonResult(new
            {
                Title = "Person",
                Data = "some values"
            }));
        }

        #endregion Public Methods
    }
}