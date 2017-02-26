/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System.Threading.Tasks;

namespace Mp.Sh.Core.License.Services
{
    public interface IEmailSender
    {
        #region Public Methods

        Task SendEmailAsync(string email, string subject, string message);

        #endregion Public Methods
    }
}