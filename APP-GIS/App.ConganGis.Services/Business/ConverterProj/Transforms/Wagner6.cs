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
// The Initial Developer of this Original Code is Ted Dunsford. Created 8/12/2009 4:43:14 PM
//
// Contributor(s): (Open source contributors should list themselves and their modifications here).
//
// ********************************************************************************************************

namespace App.CongAnGis.Services.Business.ConverterProj.Transforms
{
    /// <summary>
    /// Wagner6
    /// </summary>
    public class Wagner6 : Transform
    {
        #region Private Variables

        private const double CX = 0.94745;
        private const double CY = 0.94745;
        private const double CA = 0.0;
        private const double CB = 0.30396355092701331433;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of Wagner6
        /// </summary>
        public Wagner6()
        {
            Proj4Name = "wag6";
            Name = "Wagner_VI";
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
                xy[y] = CY * lp[phi];
                xy[x] = CX * lp[lam] * (CA + Proj.Asqrt(1 - CB * lp[phi] * lp[phi]));
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
                lp[phi] = xy[y] / CY;
                lp[lam] = xy[x] / (CX * (CA + Proj.Asqrt(1 - CB * lp[phi] * lp[phi])));
            }
        }

        #endregion

        #region Properties

        #endregion
    }
}