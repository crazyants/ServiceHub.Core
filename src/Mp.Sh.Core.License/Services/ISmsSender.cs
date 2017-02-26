/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mp.Sh.Core.License.Services
{
    public interface ISmsSender
    {
        #region Public Methods

        Task SendSmsAsync(string number, string message);

        #endregion Public Methods
    }
}