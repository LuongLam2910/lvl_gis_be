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
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Dal.FactoryClasses;
using App.QTHTGis.Dal.RelationClasses;

using SD.LLBLGen.Pro.ORMSupportClasses;

namespace App.QTHTGis.Dal.EntityClasses
{
	// __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
	// __LLBLGENPRO_USER_CODE_REGION_END
	/// <summary>Entity class which represents the entity 'Sysapp'.<br/><br/></summary>
	[Serializable]
	public partial class SysappEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static SysappEntityStaticMetaData _staticMetaData = new SysappEntityStaticMetaData();
		private static SysappRelations _relationsFactory = new SysappRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SysappEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SysappEntityStaticMetaData()
			{
				SetEntityCoreInfo("SysappEntity", InheritanceHierarchyType.None, false, (int)App.QTHTGis.Dal.EntityType.SysappEntity, typeof(SysappEntity), typeof(SysappEntityFactory), false);
			}
		}

		/// <summary>Static ctor</summary>
		static SysappEntity()
		{
		}

		/// <summary> CTor</summary>
		public SysappEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SysappEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SysappEntity</param>
		public SysappEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="appid">PK value for Sysapp which data should be fetched into this Sysapp object</param>
		public SysappEntity(System.Int64 appid) : this(appid, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="appid">PK value for Sysapp which data should be fetched into this Sysapp object</param>
		/// <param name="validator">The custom validator object for this SysappEntity</param>
		public SysappEntity(System.Int64 appid, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Appid = appid;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SysappEntity(SerializationInfo info, StreamingContext context) : base(info, context)
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
		/// <param name="validator">The validator object for this SysappEntity</param>
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
		public static SysappRelations Relations { get { return _relationsFactory; } }

		/// <summary>The Appid property of the Entity Sysapp<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysapp"."appid".<br/>Table field type characteristics (type, precision, scale, length): Bigint, 20, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int64 Appid
		{
			get { return (System.Int64)GetValue((int)SysappFieldIndex.Appid, true); }
			set { SetValue((int)SysappFieldIndex.Appid, value); }		}

		/// <summary>The Dinhdanhapp property of the Entity Sysapp<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysapp"."dinhdanhapp".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Dinhdanhapp
		{
			get { return (System.String)GetValue((int)SysappFieldIndex.Dinhdanhapp, true); }
			set	{ SetValue((int)SysappFieldIndex.Dinhdanhapp, value); }
		}

		/// <summary>The Mota property of the Entity Sysapp<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysapp"."mota".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 200.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Mota
		{
			get { return (System.String)GetValue((int)SysappFieldIndex.Mota, true); }
			set	{ SetValue((int)SysappFieldIndex.Mota, value); }
		}

		/// <summary>The State property of the Entity Sysapp<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysapp"."state".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String State
		{
			get { return (System.String)GetValue((int)SysappFieldIndex.State, true); }
			set	{ SetValue((int)SysappFieldIndex.State, value); }
		}

		/// <summary>The Tenapp property of the Entity Sysapp<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysapp"."tenapp".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Tenapp
		{
			get { return (System.String)GetValue((int)SysappFieldIndex.Tenapp, true); }
			set	{ SetValue((int)SysappFieldIndex.Tenapp, value); }
		}

		/// <summary>The Trangthai property of the Entity Sysapp<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysapp"."trangthai".<br/>Table field type characteristics (type, precision, scale, length): Bigint, 20, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int64> Trangthai
		{
			get { return (Nullable<System.Int64>)GetValue((int)SysappFieldIndex.Trangthai, false); }
			set	{ SetValue((int)SysappFieldIndex.Trangthai, value); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace App.QTHTGis.Dal
{
	public enum SysappFieldIndex
	{
		///<summary>Appid. </summary>
		Appid,
		///<summary>Dinhdanhapp. </summary>
		Dinhdanhapp,
		///<summary>Mota. </summary>
		Mota,
		///<summary>State. </summary>
		State,
		///<summary>Tenapp. </summary>
		Tenapp,
		///<summary>Trangthai. </summary>
		Trangthai,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace App.QTHTGis.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Sysapp. </summary>
	public partial class SysappRelations: RelationFactory
	{

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSysappRelations
	{

		/// <summary>CTor</summary>
		static StaticSysappRelations() { }
	}
}
