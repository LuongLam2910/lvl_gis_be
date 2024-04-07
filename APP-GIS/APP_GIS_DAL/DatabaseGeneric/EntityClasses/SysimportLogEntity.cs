﻿//////////////////////////////////////////////////////////////
// <auto-generated>This code was generated by LLBLGen Pro 5.10.</auto-generated>
//////////////////////////////////////////////////////////////
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
//////////////////////////////////////////////////////////////
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Dal.FactoryClasses;
using App.CongAnGis.Dal.RelationClasses;

using SD.LLBLGen.Pro.ORMSupportClasses;

namespace App.CongAnGis.Dal.EntityClasses
{
	// __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
	// __LLBLGENPRO_USER_CODE_REGION_END
	/// <summary>Entity class which represents the entity 'SysimportLog'.<br/><br/></summary>
	[Serializable]
	public partial class SysimportLogEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static SysimportLogEntityStaticMetaData _staticMetaData = new SysimportLogEntityStaticMetaData();
		private static SysimportLogRelations _relationsFactory = new SysimportLogRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SysimportLogEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SysimportLogEntityStaticMetaData()
			{
				SetEntityCoreInfo("SysimportLogEntity", InheritanceHierarchyType.None, false, (int)App.CongAnGis.Dal.EntityType.SysimportLogEntity, typeof(SysimportLogEntity), typeof(SysimportLogEntityFactory), false);
			}
		}

		/// <summary>Static ctor</summary>
		static SysimportLogEntity()
		{
		}

		/// <summary> CTor</summary>
		public SysimportLogEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SysimportLogEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SysimportLogEntity</param>
		public SysimportLogEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for SysimportLog which data should be fetched into this SysimportLog object</param>
		public SysimportLogEntity(System.Int32 id) : this(id, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for SysimportLog which data should be fetched into this SysimportLog object</param>
		/// <param name="validator">The custom validator object for this SysimportLogEntity</param>
		public SysimportLogEntity(System.Int32 id, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SysimportLogEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}
		
		/// <inheritdoc/>
		protected override EntityStaticMetaDataBase GetEntityStaticMetaData() {	return _staticMetaData; }

		/// <summary>Initializes the class members</summary>
		private void InitClassMembers()
		{
			PerformDependencyInjection();
			// __LLBLGENPRO_USER_CODE_REGION_START InitClassMembers
			// __LLBLGENPRO_USER_CODE_REGION_END
			OnInitClassMembersComplete();
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this SysimportLogEntity</param>
		/// <param name="fields">Fields of this entity</param>
		private void InitClassEmpty(IValidator validator, IEntityFields2 fields)
		{
			OnInitializing();
			this.Fields = fields ?? CreateFields();
			this.Validator = validator;
			InitClassMembers();
			// __LLBLGENPRO_USER_CODE_REGION_START InitClassEmpty
			// __LLBLGENPRO_USER_CODE_REGION_END

			OnInitialized();
		}

		/// <summary>The relations object holding all relations of this entity with other entity classes.</summary>
		public static SysimportLogRelations Relations { get { return _relationsFactory; } }

		/// <summary>The Config property of the Entity SysimportLog<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysimport_logs"."config".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Config
		{
			get { return (System.String)GetValue((int)SysimportLogFieldIndex.Config, true); }
			set	{ SetValue((int)SysimportLogFieldIndex.Config, value); }
		}

		/// <summary>The Count property of the Entity SysimportLog<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysimport_logs"."count".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Count
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysimportLogFieldIndex.Count, false); }
			set	{ SetValue((int)SysimportLogFieldIndex.Count, value); }
		}

		/// <summary>The Endtime property of the Entity SysimportLog<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysimport_logs"."endtime".<br/>Table field type characteristics (type, precision, scale, length): Date, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> Endtime
		{
			get { return (Nullable<System.DateTime>)GetValue((int)SysimportLogFieldIndex.Endtime, false); }
			set	{ SetValue((int)SysimportLogFieldIndex.Endtime, value); }
		}

		/// <summary>The Id property of the Entity SysimportLog<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysimport_logs"."id".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)SysimportLogFieldIndex.Id, true); }
			set { SetValue((int)SysimportLogFieldIndex.Id, value); }		}

		/// <summary>The Message property of the Entity SysimportLog<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysimport_logs"."message".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Message
		{
			get { return (System.String)GetValue((int)SysimportLogFieldIndex.Message, true); }
			set	{ SetValue((int)SysimportLogFieldIndex.Message, value); }
		}

		/// <summary>The Starttime property of the Entity SysimportLog<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysimport_logs"."starttime".<br/>Table field type characteristics (type, precision, scale, length): Date, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> Starttime
		{
			get { return (Nullable<System.DateTime>)GetValue((int)SysimportLogFieldIndex.Starttime, false); }
			set	{ SetValue((int)SysimportLogFieldIndex.Starttime, value); }
		}

		/// <summary>The Status property of the Entity SysimportLog<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysimport_logs"."status".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Status
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysimportLogFieldIndex.Status, false); }
			set	{ SetValue((int)SysimportLogFieldIndex.Status, value); }
		}

		/// <summary>The Tablename property of the Entity SysimportLog<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysimport_logs"."tablename".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Tablename
		{
			get { return (System.String)GetValue((int)SysimportLogFieldIndex.Tablename, true); }
			set	{ SetValue((int)SysimportLogFieldIndex.Tablename, value); }
		}

		/// <summary>The Unitcode property of the Entity SysimportLog<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysimport_logs"."unitcode".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Unitcode
		{
			get { return (System.String)GetValue((int)SysimportLogFieldIndex.Unitcode, true); }
			set	{ SetValue((int)SysimportLogFieldIndex.Unitcode, value); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace App.CongAnGis.Dal
{
	public enum SysimportLogFieldIndex
	{
		///<summary>Config. </summary>
		Config,
		///<summary>Count. </summary>
		Count,
		///<summary>Endtime. </summary>
		Endtime,
		///<summary>Id. </summary>
		Id,
		///<summary>Message. </summary>
		Message,
		///<summary>Starttime. </summary>
		Starttime,
		///<summary>Status. </summary>
		Status,
		///<summary>Tablename. </summary>
		Tablename,
		///<summary>Unitcode. </summary>
		Unitcode,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace App.CongAnGis.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: SysimportLog. </summary>
	public partial class SysimportLogRelations: RelationFactory
	{

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSysimportLogRelations
	{

		/// <summary>CTor</summary>
		static StaticSysimportLogRelations() { }
	}
}
