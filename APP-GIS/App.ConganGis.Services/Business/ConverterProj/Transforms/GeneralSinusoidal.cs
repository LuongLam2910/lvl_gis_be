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
// The original content was ported from the C language from the 4.6 version of Proj4 libraries.
// Frank Warmerdam has released the full content of that version under the MIT license which is
// recognized as being approximately equivalent to public domain.  The original work was done
// mostly by Gerald Evenden.  The latest versions of the C libraries can be obtained here:
// http://trac.osgeo.org/proj/
//
// The Initial Developer of this Original Code is Ted Dunsford. Created 8/13/2009 11:57:48 AM
//
// Contributor(s): (Open source contributors should list themselves and their modifications here).
//        Name         |    Date    |        Comment
// --------------------|------------|------------------------------------------------------------
// Ted Dunsford        |   5/3/2010 |  Updated project to eKMap.Projection and license to LGPL
// ********************************************************************************************************

namespace App.CongAnGis.Services.Business.ConverterProj.Transforms
{
    /// <summary>
    /// GeneralSinusoidal
    /// </summary>
    public class GeneralSinusoidal : Sinusoidal
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of GeneralSinusoidal
        /// </summary>
        public GeneralSinusoidal()
        {
            Proj4Name = "gn_sinu";
            Name = "General_Sinusoidal";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the transform using the parameters from the specified coordinate system information
        /// </summary>
        /// <param name="projInfo">A ProjectionInfo class contains all the standard and custom parameters needed to initialize this transform</param>
        protected override void OnInit(ProjectionInfo projInfo)
        {
            if (projInfo.n.HasValue && projInfo.mGeneral.HasValue)
            {
                N = projInfo.n.Value;
                M = projInfo.mGeneral.Value;
            }
            else
            {
                throw new ProjectionException(99);
            }
        }

        #endregion
    }
}