﻿//////////////////////////////////////////////////////////////
// <auto-generated>This code was generated by LLBLGen Pro 5.8.</auto-generated>
//////////////////////////////////////////////////////////////
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
//////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Authorize.Dal.DatabaseSpecific
{
	/// <summary>Class which contains the static logic to execute action stored procedures in the database.</summary>
	public static partial class ActionProcedures
	{
		/// <summary>Delegate definition for stored procedure 'get_menu_by_user' to be used in combination of a UnitOfWork2 object.</summary>
		public delegate int GetMenuByUserCallBack(System.String a, System.Int64 pAppid, IDataAccessCore dataAccessProvider);

		/// <summary>Calls stored procedure 'get_menu_by_user'.<br/><br/></summary>
		/// <param name="a">Input parameter. </param>
		/// <param name="pAppid">Input parameter. </param>
		/// <returns>Number of rows affected, if the database / routine doesn't suppress rowcounting.</returns>
		public static int GetMenuByUser(System.String a, System.Int64 pAppid)
		{
			using(var dataAccessProvider = new DataAccessAdapter())
			{
				return GetMenuByUser(a, pAppid, dataAccessProvider);
			}
		}

		/// <summary>Calls stored procedure 'get_menu_by_user'.<br/><br/></summary>
		/// <param name="a">Input parameter. </param>
		/// <param name="pAppid">Input parameter. </param>
		/// <param name="dataAccessProvider">The data access provider.</param>
		/// <returns>Number of rows affected, if the database / routine doesn't suppress rowcounting.</returns>
		public static int GetMenuByUser(System.String a, System.Int64 pAppid, IDataAccessCore dataAccessProvider)
		{
			using(var call = CreateGetMenuByUserCall(dataAccessProvider, a, pAppid))
			{
				int toReturn = call.Call();
				return toReturn;
			}
		}

		/// <summary>Calls stored procedure 'get_menu_by_user'. Async variant<br/><br/></summary>
		/// <param name="a">Input parameter. </param>
		/// <param name="pAppid">Input parameter. </param>
		/// <param name="cancellationToken">The cancellationtoken to be used</param>
		/// <returns>Number of rows affected, if the database / routine doesn't suppress rowcounting.</returns>
		public static async Task<int> GetMenuByUserAsync(System.String a, System.Int64 pAppid, CancellationToken cancellationToken)
		{
			using(var dataAccessProvider = new DataAccessAdapter())
			{
				var toReturn = await GetMenuByUserAsync(a, pAppid, dataAccessProvider, cancellationToken).ConfigureAwait(false);
				return toReturn;
			}
		}

		/// <summary>Calls stored procedure 'get_menu_by_user'. Async variant<br/><br/></summary>
		/// <param name="a">Input parameter. </param>
		/// <param name="pAppid">Input parameter. </param>
		/// <param name="dataAccessProvider">The data access provider.</param>
		/// <param name="cancellationToken">The cancellationtoken to be used</param>
		/// <returns>Number of rows affected, if the database / routine doesn't suppress rowcounting.</returns>
		public static async Task<int> GetMenuByUserAsync(System.String a, System.Int64 pAppid, IDataAccessCore dataAccessProvider, CancellationToken cancellationToken)
		{
			using(var call = CreateGetMenuByUserCall(dataAccessProvider, a, pAppid))
			{
				int _procReturnValue = await call.CallAsync(cancellationToken).ConfigureAwait(false);
				return _procReturnValue;
			}
		}

		/// <summary>Creates the call object for the call 'GetMenuByUser' to stored procedure 'get_menu_by_user'.</summary>
		/// <param name="dataAccessProvider">The data access provider.</param>
		/// <param name="a">Input parameter</param>
		/// <param name="pAppid">Input parameter</param>
		/// <returns>Ready to use StoredProcedureCall object</returns>
		private static StoredProcedureCall CreateGetMenuByUserCall(IDataAccessCore dataAccessProvider, System.String a, System.Int64 pAppid)
		{
			return new StoredProcedureCall(dataAccessProvider, "\"DATAWAREHOUSEBG\".\"appqtht\".\"get_menu_by_user\"", "GetMenuByUser")
							.AddParameter("a", "Varchar", 10485760, ParameterDirection.Input, true, 0, 0, a)
							.AddParameter("b", "Bigint", 0, ParameterDirection.Input, true, 0, 0, pAppid);
		}


	}
}