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
	/// <summary>Entity class which represents the entity 'Systrangthietbidoituong'.<br/><br/></summary>
	[Serializable]
	public partial class SystrangthietbidoituongEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static SystrangthietbidoituongEntityStaticMetaData _staticMetaData = new SystrangthietbidoituongEntityStaticMetaData();
		private static SystrangthietbidoituongRelations _relationsFactory = new SystrangthietbidoituongRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SystrangthietbidoituongEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SystrangthietbidoituongEntityStaticMetaData()
			{
				SetEntityCoreInfo("SystrangthietbidoituongEntity", InheritanceHierarchyType.None, false, (int)App.CongAnGis.Dal.EntityType.SystrangthietbidoituongEntity, typeof(SystrangthietbidoituongEntity), typeof(SystrangthietbidoituongEntityFactory), false);
			}
		}

		/// <summary>Static ctor</summary>
		static SystrangthietbidoituongEntity()
		{
		}

		/// <summary> CTor</summary>
		public SystrangthietbidoituongEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SystrangthietbidoituongEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SystrangthietbidoituongEntity</param>
		public SystrangthietbidoituongEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Systrangthietbidoituong which data should be fetched into this Systrangthietbidoituong object</param>
		public SystrangthietbidoituongEntity(System.Int32 id) : this(id, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Systrangthietbidoituong which data should be fetched into this Systrangthietbidoituong object</param>
		/// <param name="validator">The custom validator object for this SystrangthietbidoituongEntity</param>
		public SystrangthietbidoituongEntity(System.Int32 id, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SystrangthietbidoituongEntity(SerializationInfo info, StreamingContext context) : base(info, context)
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
		/// <param name="validator">The validator object for this SystrangthietbidoituongEntity</param>
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
		public static SystrangthietbidoituongRelations Relations { get { return _relationsFactory; } }

		/// <summary>The Id property of the Entity Systrangthietbidoituong<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systrangthietbidoituong"."id".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)SystrangthietbidoituongFieldIndex.Id, true); }
			set { SetValue((int)SystrangthietbidoituongFieldIndex.Id, value); }		}

		/// <summary>The Iddoituong property of the Entity Systrangthietbidoituong<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systrangthietbidoituong"."iddoituong".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 Iddoituong
		{
			get { return (System.Int32)GetValue((int)SystrangthietbidoituongFieldIndex.Iddoituong, true); }
			set	{ SetValue((int)SystrangthietbidoituongFieldIndex.Iddoituong, value); }
		}

		/// <summary>The Idthietbi property of the Entity Systrangthietbidoituong<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systrangthietbidoituong"."idthietbi".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Idthietbi
		{
			get { return (Nullable<System.Int32>)GetValue((int)SystrangthietbidoituongFieldIndex.Idthietbi, false); }
			set	{ SetValue((int)SystrangthietbidoituongFieldIndex.Idthietbi, value); }
		}

		/// <summary>The Ngaycapnhat property of the Entity Systrangthietbidoituong<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systrangthietbidoituong"."ngaycapnhat".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Ngaycapnhat
		{
			get { return (Nullable<System.Int32>)GetValue((int)SystrangthietbidoituongFieldIndex.Ngaycapnhat, false); }
			set	{ SetValue((int)SystrangthietbidoituongFieldIndex.Ngaycapnhat, value); }
		}

		/// <summary>The Ngaytao property of the Entity Systrangthietbidoituong<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systrangthietbidoituong"."ngaytao".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Ngaytao
		{
			get { return (System.String)GetValue((int)SystrangthietbidoituongFieldIndex.Ngaytao, true); }
			set	{ SetValue((int)SystrangthietbidoituongFieldIndex.Ngaytao, value); }
		}

		/// <summary>The Soluong property of the Entity Systrangthietbidoituong<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systrangthietbidoituong"."soluong".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Soluong
		{
			get { return (Nullable<System.Int32>)GetValue((int)SystrangthietbidoituongFieldIndex.Soluong, false); }
			set	{ SetValue((int)SystrangthietbidoituongFieldIndex.Soluong, value); }
		}

		/// <summary>The Tablename property of the Entity Systrangthietbidoituong<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systrangthietbidoituong"."tablename".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Tablename
		{
			get { return (System.String)GetValue((int)SystrangthietbidoituongFieldIndex.Tablename, true); }
			set	{ SetValue((int)SystrangthietbidoituongFieldIndex.Tablename, value); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace App.CongAnGis.Dal
{
	public enum SystrangthietbidoituongFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Iddoituong. </summary>
		Iddoituong,
		///<summary>Idthietbi. </summary>
		Idthietbi,
		///<summary>Ngaycapnhat. </summary>
		Ngaycapnhat,
		///<summary>Ngaytao. </summary>
		Ngaytao,
		///<summary>Soluong. </summary>
		Soluong,
		///<summary>Tablename. </summary>
		Tablename,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace App.CongAnGis.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Systrangthietbidoituong. </summary>
	public partial class SystrangthietbidoituongRelations: RelationFactory
	{

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSystrangthietbidoituongRelations
	{

		/// <summary>CTor</summary>
		static StaticSystrangthietbidoituongRelations() { }
	}
}