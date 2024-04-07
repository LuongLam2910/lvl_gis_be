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
	/// <summary>Entity class which represents the entity 'Syssetting'.<br/><br/></summary>
	[Serializable]
	public partial class SyssettingEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static SyssettingEntityStaticMetaData _staticMetaData = new SyssettingEntityStaticMetaData();
		private static SyssettingRelations _relationsFactory = new SyssettingRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SyssettingEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SyssettingEntityStaticMetaData()
			{
				SetEntityCoreInfo("SyssettingEntity", InheritanceHierarchyType.None, false, (int)App.CongAnGis.Dal.EntityType.SyssettingEntity, typeof(SyssettingEntity), typeof(SyssettingEntityFactory), false);
			}
		}

		/// <summary>Static ctor</summary>
		static SyssettingEntity()
		{
		}

		/// <summary> CTor</summary>
		public SyssettingEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SyssettingEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SyssettingEntity</param>
		public SyssettingEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Syssetting which data should be fetched into this Syssetting object</param>
		public SyssettingEntity(System.Int32 id) : this(id, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Syssetting which data should be fetched into this Syssetting object</param>
		/// <param name="validator">The custom validator object for this SyssettingEntity</param>
		public SyssettingEntity(System.Int32 id, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SyssettingEntity(SerializationInfo info, StreamingContext context) : base(info, context)
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
		/// <param name="validator">The validator object for this SyssettingEntity</param>
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
		public static SyssettingRelations Relations { get { return _relationsFactory; } }

		/// <summary>The Code property of the Entity Syssetting<br/><br/></summary>
		/// <remarks>Mapped on  table field: "syssettings"."code".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Code
		{
			get { return (System.String)GetValue((int)SyssettingFieldIndex.Code, true); }
			set	{ SetValue((int)SyssettingFieldIndex.Code, value); }
		}

		/// <summary>The Config property of the Entity Syssetting<br/><br/></summary>
		/// <remarks>Mapped on  table field: "syssettings"."config".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Config
		{
			get { return (System.String)GetValue((int)SyssettingFieldIndex.Config, true); }
			set	{ SetValue((int)SyssettingFieldIndex.Config, value); }
		}

		/// <summary>The Datecreated property of the Entity Syssetting<br/><br/></summary>
		/// <remarks>Mapped on  table field: "syssettings"."datecreated".<br/>Table field type characteristics (type, precision, scale, length): Date, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> Datecreated
		{
			get { return (Nullable<System.DateTime>)GetValue((int)SyssettingFieldIndex.Datecreated, false); }
			set	{ SetValue((int)SyssettingFieldIndex.Datecreated, value); }
		}

		/// <summary>The Datemodified property of the Entity Syssetting<br/><br/></summary>
		/// <remarks>Mapped on  table field: "syssettings"."datemodified".<br/>Table field type characteristics (type, precision, scale, length): Date, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> Datemodified
		{
			get { return (Nullable<System.DateTime>)GetValue((int)SyssettingFieldIndex.Datemodified, false); }
			set	{ SetValue((int)SyssettingFieldIndex.Datemodified, value); }
		}

		/// <summary>The Id property of the Entity Syssetting<br/><br/></summary>
		/// <remarks>Mapped on  table field: "syssettings"."id".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)SyssettingFieldIndex.Id, true); }
			set { SetValue((int)SyssettingFieldIndex.Id, value); }		}

		/// <summary>The Name property of the Entity Syssetting<br/><br/></summary>
		/// <remarks>Mapped on  table field: "syssettings"."name".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)SyssettingFieldIndex.Name, true); }
			set	{ SetValue((int)SyssettingFieldIndex.Name, value); }
		}

		/// <summary>The Usercreated property of the Entity Syssetting<br/><br/></summary>
		/// <remarks>Mapped on  table field: "syssettings"."usercreated".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Usercreated
		{
			get { return (Nullable<System.Int32>)GetValue((int)SyssettingFieldIndex.Usercreated, false); }
			set	{ SetValue((int)SyssettingFieldIndex.Usercreated, value); }
		}

		/// <summary>The Usermodified property of the Entity Syssetting<br/><br/></summary>
		/// <remarks>Mapped on  table field: "syssettings"."usermodified".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Usermodified
		{
			get { return (Nullable<System.Int32>)GetValue((int)SyssettingFieldIndex.Usermodified, false); }
			set	{ SetValue((int)SyssettingFieldIndex.Usermodified, value); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace App.CongAnGis.Dal
{
	public enum SyssettingFieldIndex
	{
		///<summary>Code. </summary>
		Code,
		///<summary>Config. </summary>
		Config,
		///<summary>Datecreated. </summary>
		Datecreated,
		///<summary>Datemodified. </summary>
		Datemodified,
		///<summary>Id. </summary>
		Id,
		///<summary>Name. </summary>
		Name,
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
	/// <summary>Implements the relations factory for the entity: Syssetting. </summary>
	public partial class SyssettingRelations: RelationFactory
	{

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSyssettingRelations
	{

		/// <summary>CTor</summary>
		static StaticSyssettingRelations() { }
	}
}