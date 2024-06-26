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
// The Initial Developer of this Original Code is Ted Dunsford. Created 6/2/2009 3:34:43 PM
//
// Contributor(s): (Open source contributors should list themselves and their modifications here).
//        Name         |    Date    |        Comment
// --------------------|------------|------------------------------------------------------------
// Ted Dunsford        |   5/3/2010 |  Updated project to eKMap.Projection and license to LGPL
// ********************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.CongAnGis.Services.Business.ConverterProj.Reflection
{
    /// <summary>
    /// FieldInfo
    /// </summary>
    public static class ProjFieldInfoEM
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="self"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FieldInfo GetFirst(this IEnumerable<FieldInfo> self, string name)
        {
            Func<FieldInfo, bool> criteria = current => (current.Name == name);
            return self.First(criteria);
        }

        /// <summary>
        /// Determines whether there is a member with the specified name
        /// </summary>
        /// <param name="self"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Contains(this IEnumerable<FieldInfo> self, string name)
        {
            foreach (FieldInfo info in self)
            {
                if (info.Name == name) return true;
            }
            return false;
        }
    }
}