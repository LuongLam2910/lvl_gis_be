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
	/// <summary>Entity class which represents the entity 'Sysfolder'.<br/><br/></summary>
	[Serializable]
	public partial class SysfolderEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static SysfolderEntityStaticMetaData _staticMetaData = new SysfolderEntityStaticMetaData();
		private static SysfolderRelations _relationsFactory = new SysfolderRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SysfolderEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SysfolderEntityStaticMetaData()
			{
				SetEntityCoreInfo("SysfolderEntity", InheritanceHierarchyType.None, false, (int)App.CongAnGis.Dal.EntityType.SysfolderEntity, typeof(SysfolderEntity), typeof(SysfolderEntityFactory), false);
			}
		}

		/// <summary>Static ctor</summary>
		static SysfolderEntity()
		{
		}

		/// <summary> CTor</summary>
		public SysfolderEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SysfolderEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SysfolderEntity</param>
		public SysfolderEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Sysfolder which data should be fetched into this Sysfolder object</param>
		public SysfolderEntity(System.Int32 id) : this(id, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Sysfolder which data should be fetched into this Sysfolder object</param>
		/// <param name="validator">The custom validator object for this SysfolderEntity</param>
		public SysfolderEntity(System.Int32 id, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SysfolderEntity(SerializationInfo info, StreamingContext context) : base(info, context)
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
		/// <param name="validator">The validator object for this SysfolderEntity</param>
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
		public static SysfolderRelations Relations { get { return _relationsFactory; } }

		/// <summary>The Createby property of the Entity Sysfolder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfolder"."createby".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Createby
		{
			get { return (System.String)GetValue((int)SysfolderFieldIndex.Createby, true); }
			set	{ SetValue((int)SysfolderFieldIndex.Createby, value); }
		}

		/// <summary>The Createdate property of the Entity Sysfolder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfolder"."createdate".<br/>Table field type characteristics (type, precision, scale, length): Date, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> Createdate
		{
			get { return (Nullable<System.DateTime>)GetValue((int)SysfolderFieldIndex.Createdate, false); }
			set	{ SetValue((int)SysfolderFieldIndex.Createdate, value); }
		}

		/// <summary>The Id property of the Entity Sysfolder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfolder"."id".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)SysfolderFieldIndex.Id, true); }
			set { SetValue((int)SysfolderFieldIndex.Id, value); }		}

		/// <summary>The Name property of the Entity Sysfolder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfolder"."name".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)SysfolderFieldIndex.Name, true); }
			set	{ SetValue((int)SysfolderFieldIndex.Name, value); }
		}

		/// <summary>The Parentid property of the Entity Sysfolder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfolder"."parentid".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Parentid
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysfolderFieldIndex.Parentid, false); }
			set	{ SetValue((int)SysfolderFieldIndex.Parentid, value); }
		}

		/// <summary>The Path property of the Entity Sysfolder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfolder"."path".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Path
		{
			get { return (System.String)GetValue((int)SysfolderFieldIndex.Path, true); }
			set	{ SetValue((int)SysfolderFieldIndex.Path, value); }
		}

		/// <summary>The Unitcode property of the Entity Sysfolder<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysfolder"."unitcode".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Unitcode
		{
			get { return (System.String)GetValue((int)SysfolderFieldIndex.Unitcode, true); }
			set	{ SetValue((int)SysfolderFieldIndex.Unitcode, value); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace App.CongAnGis.Dal
{
	public enum SysfolderFieldIndex
	{
		///<summary>Createby. </summary>
		Createby,
		///<summary>Createdate. </summary>
		Createdate,
		///<summary>Id. </summary>
		Id,
		///<summary>Name. </summary>
		Name,
		///<summary>Parentid. </summary>
		Parentid,
		///<summary>Path. </summary>
		Path,
		///<summary>Unitcode. </summary>
		Unitcode,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace App.CongAnGis.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Sysfolder. </summary>
	public partial class SysfolderRelations: RelationFactory
	{

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSysfolderRelations
	{

		/// <summary>CTor</summary>
		static StaticSysfolderRelations() { }
	}
}