// ********************************************************************************************************
// Product Name: eKMap.Projection
// Description:  The basic module for MapWindow version 6.0
// ********************************************************************************************************
// The contents of this file are subject to the MIT License (MIT)
// you may not use this file except in compliance with the License. You may obtain a copy of the License at
// http://dotspatial.codeplex.com/license
//
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF
// ANY KIND, either expressed or implied. See the License for the specific language governing rights and
// limitations under the License.
//
// The Original Code is from MapWindow.dll version 6.0
//
// The Initial Developer of this Original Code is Ted Dunsford. Created 8/17/2009 1:48:45 PM
//
// Contributor(s): (Open source contributors should list themselves and their modifications here).
//        Name         |    Date    |        Comment
// --------------------|------------|------------------------------------------------------------
// Ted Dunsford        |   5/3/2010 |  Updated project to eKMap.Projection and license to LGPL
// ********************************************************************************************************

namespace App.CongAnGis.Services.Business.ConverterProj
{
    /// <summary>
    /// PhiLam has two double values and is used like a coordinate.
    /// </summary>
    public struct PhiLam
    {
        /// <summary>
        /// Geodetic Lambda coordinate (longitude)
        /// </summary>
        public double Lambda;

        /// <summary>
        /// Geodetic Phi coordinate (latitude)
        /// </summary>
        public double Phi;
    }
}