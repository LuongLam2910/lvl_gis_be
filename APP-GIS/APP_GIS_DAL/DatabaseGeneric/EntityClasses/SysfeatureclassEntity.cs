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
	/// <summary>Entity class which represents the entity 'Sysfeatureclass'.<br/><br/></summary>
	[Serializable]
	public partial class SysfeatureclassEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static SysfeatureclassEntityStaticMetaData _staticMetaData = new SysfeatureclassEntityStaticMetaData();
		private static SysfeatureclassRelations _relationsFactory = new SysfeatureclassRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SysfeatureclassEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SysfeatureclassEntityStaticMetaData()
			{
				SetEntityCoreInfo("SysfeatureclassEntity", InheritanceHierarchyType.None, false, (int)App.CongAnGis.Dal.EntityType.SysfeatureclassEntity, typeof(SysfeatureclassEntity), typeof(SysfeatureclassEntityFactory), false);
			}
		}

		/// <summary>Static ctor</summary>
		static SysfeatureclassEntity()
		{
		}

		/// <summary> CTor</summary>
		public SysfeatureclassEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SysfeatureclassEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SysfeatureclassEntity</param>
		public SysfeatureclassEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Sysfeatureclass which data should be fetched into this Sysfeatureclass object</param>
		public SysfeatureclassEntity(System.Int32 id) : this(id, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Sysfeatureclass which data should be fetched into this Sysfeatureclass object</param>
		/// <param name="validator">The custom validator object for this SysfeatureclassEntity</param>
		public SysfeatureclassEntity(System.Int32 id, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SysfeatureclassEntity(SerializationInfo info, StreamingContext context) : base(info, context)
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
		/// <param name="validator">The validator object for this SysfeatureclassEntity</param>
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
		public static SysfeatureclassRelations Relations { get { return _relationsFactory; } }

		/// <summary>The Config property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."config".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Config
		{
			get { return (System.String)GetValue((int)SysfeatureclassFieldIndex.Config, true); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Config, value); }
		}

		/// <summary>The Datacreated property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."datacreated".<br/>Table field type characteristics (type, precision, scale, length): Date, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> Datacreated
		{
			get { return (Nullable<System.DateTime>)GetValue((int)SysfeatureclassFieldIndex.Datacreated, false); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Datacreated, value); }
		}

		/// <summary>The Datasetid property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."datasetid".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Datasetid
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysfeatureclassFieldIndex.Datasetid, false); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Datasetid, value); }
		}

		/// <summary>The Datemodified property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."datemodified".<br/>Table field type characteristics (type, precision, scale, length): Date, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> Datemodified
		{
			get { return (Nullable<System.DateTime>)GetValue((int)SysfeatureclassFieldIndex.Datemodified, false); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Datemodified, value); }
		}

		/// <summary>The Description property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."description".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Description
		{
			get { return (System.String)GetValue((int)SysfeatureclassFieldIndex.Description, true); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Description, value); }
		}

		/// <summary>The Geotype property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."geotype".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Geotype
		{
			get { return (System.String)GetValue((int)SysfeatureclassFieldIndex.Geotype, true); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Geotype, value); }
		}

		/// <summary>The Id property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."id".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)SysfeatureclassFieldIndex.Id, true); }
			set { SetValue((int)SysfeatureclassFieldIndex.Id, value); }		}

		/// <summary>The Name property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."name".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)SysfeatureclassFieldIndex.Name, true); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Name, value); }
		}

		/// <summary>The Prj property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."prj".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Prj
		{
			get { return (System.String)GetValue((int)SysfeatureclassFieldIndex.Prj, true); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Prj, value); }
		}

		/// <summary>The Status property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."status".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Status
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysfeatureclassFieldIndex.Status, false); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Status, value); }
		}

		/// <summary>The Tablename property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."tablename".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Tablename
		{
			get { return (System.String)GetValue((int)SysfeatureclassFieldIndex.Tablename, true); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Tablename, value); }
		}

		/// <summary>The Type property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."type".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Type
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysfeatureclassFieldIndex.Type, false); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Type, value); }
		}

		/// <summary>The Unitcode property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."unitcode".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Unitcode
		{
			get { return (System.String)GetValue((int)SysfeatureclassFieldIndex.Unitcode, true); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Unitcode, value); }
		}

		/// <summary>The Usercreated property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."usercreated".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Usercreated
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysfeatureclassFieldIndex.Usercreated, false); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Usercreated, value); }
		}

		/// <summary>The Usermodified property of the Entity Sysfeatureclass<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfeatureclass"."usermodified".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Usermodified
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysfeatureclassFieldIndex.Usermodified, false); }
			set	{ SetValue((int)SysfeatureclassFieldIndex.Usermodified, value); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace App.CongAnGis.Dal
{
	public enum SysfeatureclassFieldIndex
	{
		///<summary>Config. </summary>
		Config,
		///<summary>Datacreated. </summary>
		Datacreated,
		///<summary>Datasetid. </summary>
		Datasetid,
		///<summary>Datemodified. </summary>
		Datemodified,
		///<summary>Description. </summary>
		Description,
		///<summary>Geotype. </summary>
		Geotype,
		///<summary>Id. </summary>
		Id,
		///<summary>Name. </summary>
		Name,
		///<summary>Prj. </summary>
		Prj,
		///<summary>Status. </summary>
		Status,
		///<summary>Tablename. </summary>
		Tablename,
		///<summary>Type. </summary>
		Type,
		///<summary>Unitcode. </summary>
		Unitcode,
		///<summary>Usercreated. </summary>
		Usercreated,
		///<summary>Usermodified. </summary>
		Usermodified,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace App.CongAnGis.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Sysfeatureclass. </summary>
	public partial class SysfeatureclassRelations: RelationFactory
	{

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSysfeatureclassRelations
	{

		/// <summary>CTor</summary>
		static StaticSysfeatureclassRelations() { }
	}
}
