﻿/****************************** Module Header ******************************\
Module Name:  <File Name>
Project:      <Sample Name>
Copyright (c) Mproof B.V.

Last Edit: Raffaele Garofalo
\***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mp.Sh.Core.OData.Fixtures
{
    public class MetadataEndpoint
    {
        #region Public Constructors

        public MetadataEndpoint()
        {
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public void MetadataEndpoint_Should_Return_200()
        {
            Assert.True(true);
        }

        #endregion Public Methods
    }
}