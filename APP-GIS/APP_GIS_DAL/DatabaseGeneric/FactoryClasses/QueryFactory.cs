﻿//////////////////////////////////////////////////////////////
// <auto-generated>This code was generated by LLBLGen Pro 5.10.</auto-generated>
//////////////////////////////////////////////////////////////
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
////////////////////////////////////////////////////////////// 
using System;
using System.Linq;
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec.AdapterSpecific;
using SD.LLBLGen.Pro.QuerySpec;

namespace App.CongAnGis.Dal.FactoryClasses
{
	/// <summary>Factory class to produce DynamicQuery instances and EntityQuery instances</summary>
	public partial class QueryFactory : QueryFactoryBase2
	{
		/// <summary>Creates and returns a new EntityQuery for the Sysbaochay entity</summary>
		public EntityQuery<SysbaochayEntity> Sysbaochay { get { return Create<SysbaochayEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysbaotainan entity</summary>
		public EntityQuery<SysbaotainanEntity> Sysbaotainan { get { return Create<SysbaotainanEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Syscauhinh entity</summary>
		public EntityQuery<SyscauhinhEntity> Syscauhinh { get { return Create<SyscauhinhEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Syscsgt entity</summary>
		public EntityQuery<SyscsgtEntity> Syscsgt { get { return Create<SyscsgtEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysdataset entity</summary>
		public EntityQuery<SysdatasetEntity> Sysdataset { get { return Create<SysdatasetEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the SysdatasetUser entity</summary>
		public EntityQuery<SysdatasetUserEntity> SysdatasetUser { get { return Create<SysdatasetUserEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysdmtrangthietbipccc entity</summary>
		public EntityQuery<SysdmtrangthietbipcccEntity> Sysdmtrangthietbipccc { get { return Create<SysdmtrangthietbipcccEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysfeatureclass entity</summary>
		public EntityQuery<SysfeatureclassEntity> Sysfeatureclass { get { return Create<SysfeatureclassEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the SysfeatureclassUser entity</summary>
		public EntityQuery<SysfeatureclassUserEntity> SysfeatureclassUser { get { return Create<SysfeatureclassUserEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysfield entity</summary>
		public EntityQuery<SysfieldEntity> Sysfield { get { return Create<SysfieldEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysfile entity</summary>
		public EntityQuery<SysfileEntity> Sysfile { get { return Create<SysfileEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysfilemanager entity</summary>
		public EntityQuery<SysfilemanagerEntity> Sysfilemanager { get { return Create<SysfilemanagerEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysfolder entity</summary>
		public EntityQuery<SysfolderEntity> Sysfolder { get { return Create<SysfolderEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Syshistoryuserline entity</summary>
		public EntityQuery<SyshistoryuserlineEntity> Syshistoryuserline { get { return Create<SyshistoryuserlineEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the SysimportLog entity</summary>
		public EntityQuery<SysimportLogEntity> SysimportLog { get { return Create<SysimportLogEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Syslinhsucanhbao entity</summary>
		public EntityQuery<SyslinhsucanhbaoEntity> Syslinhsucanhbao { get { return Create<SyslinhsucanhbaoEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Syslogactiongis entity</summary>
		public EntityQuery<SyslogactiongisEntity> Syslogactiongis { get { return Create<SyslogactiongisEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysmap entity</summary>
		public EntityQuery<SysmapEntity> Sysmap { get { return Create<SysmapEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the SysmapUser entity</summary>
		public EntityQuery<SysmapUserEntity> SysmapUser { get { return Create<SysmapUserEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysphieuchienthuat entity</summary>
		public EntityQuery<SysphieuchienthuatEntity> Sysphieuchienthuat { get { return Create<SysphieuchienthuatEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysphuongana entity</summary>
		public EntityQuery<SysphuonganaEntity> Sysphuongana { get { return Create<SysphuonganaEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysphuonganb entity</summary>
		public EntityQuery<SysphuonganbEntity> Sysphuonganb { get { return Create<SysphuonganbEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysphuonganc entity</summary>
		public EntityQuery<SysphuongancEntity> Sysphuonganc { get { return Create<SysphuongancEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysphuongand entity</summary>
		public EntityQuery<SysphuongandEntity> Sysphuongand { get { return Create<SysphuongandEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Syssetting entity</summary>
		public EntityQuery<SyssettingEntity> Syssetting { get { return Create<SyssettingEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Systtphieuchienthuat entity</summary>
		public EntityQuery<SysttphieuchienthuatEntity> Systtphieuchienthuat { get { return Create<SysttphieuchienthuatEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Systrangthietbidoituong entity</summary>
		public EntityQuery<SystrangthietbidoituongEntity> Systrangthietbidoituong { get { return Create<SystrangthietbidoituongEntity>(); } }

		/// <summary>Creates and returns a new EntityQuery for the Sysuserhistory entity</summary>
		public EntityQuery<SysuserhistoryEntity> Sysuserhistory { get { return Create<SysuserhistoryEntity>(); } }

		/// <inheritdoc/>
		protected override IElementCreatorCore CreateElementCreator() { return new ElementCreator(); }
 
	}
}