/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using Mp.Sh.Core.License.Models;

namespace Mp.Sh.Core.License.Fixtures.Mocks
{
    public class ModelMockFactory
    {
        #region Public Methods

        public static Company BuildCompany()
        {
            return new Company(name: "name", description: "description");
        }

        #endregion Public Methods
    }
}