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
// The Initial Developer of this Original Code is Ted Dunsford. Created 8/12/2009 4:19:46 PM
//
// Contributor(s): (Open source contributors should list themselves and their modifications here).
//        Name         |    Date    |        Comment
// --------------------|------------|------------------------------------------------------------
// Ted Dunsford        |   5/3/2010 |  Updated project to eKMap.Projection and license to LGPL
// ********************************************************************************************************

using System;

namespace App.CongAnGis.Services.Business.ConverterProj.Transforms
{
    /// <summary>
    /// Eckert1
    /// </summary>
    public class Eckert1 : Transform
    {
        #region Private Variables

        private const double FC = .92131773192356127802;
        private const double RP = .31830988618379067154;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of Eckert1
        /// </summary>
        public Eckert1()
        {
            Proj4Name = "eck1";
            Name = "Eckert_I";
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void OnForward(double[] lp, double[] xy, int startIndex, int numPoints)
        {
            for (int i = startIndex; i < startIndex + numPoints; i++)
            {
                int phi = i * 2 + PHI;
                int lam = i * 2 + LAMBDA;
                int x = i * 2 + X;
                int y = i * 2 + Y;
                xy[x] = FC * lp[lam] * (1 - RP * Math.Abs(lp[phi]));
                xy[y] = FC * lp[phi];
            }
        }

        /// <inheritdoc />
        protected override void OnInverse(double[] xy, double[] lp, int startIndex, int numPoints)
        {
            for (int i = startIndex; i < startIndex + numPoints; i++)
            {
                int phi = i * 2 + PHI;
                int lam = i * 2 + LAMBDA;
                int x = i * 2 + X;
                int y = i * 2 + Y;
                lp[phi] = xy[y] / FC;
                lp[lam] = xy[x] / (FC * (1 - RP * Math.Abs(lp[phi])));
            }
        }

        #endregion
    }
}