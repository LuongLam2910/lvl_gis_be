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
	/// <summary>Entity class which represents the entity 'Sysdataset'.<br/><br/></summary>
	[Serializable]
	public partial class SysdatasetEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static SysdatasetEntityStaticMetaData _staticMetaData = new SysdatasetEntityStaticMetaData();
		private static SysdatasetRelations _relationsFactory = new SysdatasetRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SysdatasetEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SysdatasetEntityStaticMetaData()
			{
				SetEntityCoreInfo("SysdatasetEntity", InheritanceHierarchyType.None, false, (int)App.CongAnGis.Dal.EntityType.SysdatasetEntity, typeof(SysdatasetEntity), typeof(SysdatasetEntityFactory), false);
			}
		}

		/// <summary>Static ctor</summary>
		static SysdatasetEntity()
		{
		}

		/// <summary> CTor</summary>
		public SysdatasetEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SysdatasetEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SysdatasetEntity</param>
		public SysdatasetEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Sysdataset which data should be fetched into this Sysdataset object</param>
		public SysdatasetEntity(System.Int32 id) : this(id, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Sysdataset which data should be fetched into this Sysdataset object</param>
		/// <param name="validator">The custom validator object for this SysdatasetEntity</param>
		public SysdatasetEntity(System.Int32 id, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SysdatasetEntity(SerializationInfo info, StreamingContext context) : base(info, context)
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
		/// <param name="validator">The validator object for this SysdatasetEntity</param>
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
		public static SysdatasetRelations Relations { get { return _relationsFactory; } }

		/// <summary>The Datecreated property of the Entity Sysdataset<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysdataset"."datecreated".<br/>Table field type characteristics (type, precision, scale, length): Date, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> Datecreated
		{
			get { return (Nullable<System.DateTime>)GetValue((int)SysdatasetFieldIndex.Datecreated, false); }
			set	{ SetValue((int)SysdatasetFieldIndex.Datecreated, value); }
		}

		/// <summary>The Datemodified property of the Entity Sysdataset<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysdataset"."datemodified".<br/>Table field type characteristics (type, precision, scale, length): Date, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> Datemodified
		{
			get { return (Nullable<System.DateTime>)GetValue((int)SysdatasetFieldIndex.Datemodified, false); }
			set	{ SetValue((int)SysdatasetFieldIndex.Datemodified, value); }
		}

		/// <summary>The Description property of the Entity Sysdataset<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysdataset"."description".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Description
		{
			get { return (System.String)GetValue((int)SysdatasetFieldIndex.Description, true); }
			set	{ SetValue((int)SysdatasetFieldIndex.Description, value); }
		}

		/// <summary>The Id property of the Entity Sysdataset<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysdataset"."id".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)SysdatasetFieldIndex.Id, true); }
			set { SetValue((int)SysdatasetFieldIndex.Id, value); }		}

		/// <summary>The Loai property of the Entity Sysdataset<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysdataset"."loai".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Loai
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysdatasetFieldIndex.Loai, false); }
			set	{ SetValue((int)SysdatasetFieldIndex.Loai, value); }
		}

		/// <summary>The Name property of the Entity Sysdataset<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysdataset"."name".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)SysdatasetFieldIndex.Name, true); }
			set	{ SetValue((int)SysdatasetFieldIndex.Name, value); }
		}

		/// <summary>The Parentid property of the Entity Sysdataset<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysdataset"."parentid".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Parentid
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysdatasetFieldIndex.Parentid, false); }
			set	{ SetValue((int)SysdatasetFieldIndex.Parentid, value); }
		}

		/// <summary>The Status property of the Entity Sysdataset<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysdataset"."status".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Status
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysdatasetFieldIndex.Status, false); }
			set	{ SetValue((int)SysdatasetFieldIndex.Status, value); }
		}

		/// <summary>The Unitcode property of the Entity Sysdataset<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysdataset"."unitcode".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Unitcode
		{
			get { return (System.String)GetValue((int)SysdatasetFieldIndex.Unitcode, true); }
			set	{ SetValue((int)SysdatasetFieldIndex.Unitcode, value); }
		}

		/// <summary>The Usercreated property of the Entity Sysdataset<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysdataset"."usercreated".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Usercreated
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysdatasetFieldIndex.Usercreated, false); }
			set	{ SetValue((int)SysdatasetFieldIndex.Usercreated, value); }
		}

		/// <summary>The Usermodified property of the Entity Sysdataset<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysdataset"."usermodified".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Usermodified
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysdatasetFieldIndex.Usermodified, false); }
			set	{ SetValue((int)SysdatasetFieldIndex.Usermodified, value); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace App.CongAnGis.Dal
{
	public enum SysdatasetFieldIndex
	{
		///<summary>Datecreated. </summary>
		Datecreated,
		///<summary>Datemodified. </summary>
		Datemodified,
		///<summary>Description. </summary>
		Description,
		///<summary>Id. </summary>
		Id,
		///<summary>Loai. </summary>
		Loai,
		///<summary>Name. </summary>
		Name,
		///<summary>Parentid. </summary>
		Parentid,
		///<summary>Status. </summary>
		Status,
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
	/// <summary>Implements the relations factory for the entity: Sysdataset. </summary>
	public partial class SysdatasetRelations: RelationFactory
	{

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSysdatasetRelations
	{

		/// <summary>CTor</summary>
		static StaticSysdatasetRelations() { }
	}
}