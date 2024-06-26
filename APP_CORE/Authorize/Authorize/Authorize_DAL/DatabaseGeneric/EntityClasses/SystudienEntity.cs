﻿//////////////////////////////////////////////////////////////
// <auto-generated>This code was generated by LLBLGen Pro 5.8.</auto-generated>
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
using Authorize.Dal.HelperClasses;
using Authorize.Dal.FactoryClasses;
using Authorize.Dal.RelationClasses;

using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Authorize.Dal.EntityClasses
{
	// __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
	// __LLBLGENPRO_USER_CODE_REGION_END
	/// <summary>Entity class which represents the entity 'Systudien'.<br/><br/></summary>
	[Serializable]
	public partial class SystudienEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static SystudienEntityStaticMetaData _staticMetaData = new SystudienEntityStaticMetaData();
		private static SystudienRelations _relationsFactory = new SystudienRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SystudienEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SystudienEntityStaticMetaData()
			{
				SetEntityCoreInfo("SystudienEntity", InheritanceHierarchyType.None, false, (int)Authorize.Dal.EntityType.SystudienEntity, typeof(SystudienEntity), typeof(SystudienEntityFactory), false);
			}
		}

		/// <summary>Static ctor</summary>
		static SystudienEntity()
		{
		}

		/// <summary> CTor</summary>
		public SystudienEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SystudienEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SystudienEntity</param>
		public SystudienEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Systudien which data should be fetched into this Systudien object</param>
		public SystudienEntity(System.Int64 id) : this(id, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Systudien which data should be fetched into this Systudien object</param>
		/// <param name="validator">The custom validator object for this SystudienEntity</param>
		public SystudienEntity(System.Int64 id, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SystudienEntity(SerializationInfo info, StreamingContext context) : base(info, context)
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
		/// <param name="validator">The validator object for this SystudienEntity</param>
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
		public static SystudienRelations Relations { get { return _relationsFactory; } }

		/// <summary>The Cap property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."cap".<br/>Table field type characteristics (type, precision, scale, length): Bigint, 20, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int64> Cap
		{
			get { return (Nullable<System.Int64>)GetValue((int)SystudienFieldIndex.Cap, false); }
			set	{ SetValue((int)SystudienFieldIndex.Cap, value); }
		}

		/// <summary>The Cqbanhanh property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."cqbanhanh".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 250.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Cqbanhanh
		{
			get { return (System.String)GetValue((int)SystudienFieldIndex.Cqbanhanh, true); }
			set	{ SetValue((int)SystudienFieldIndex.Cqbanhanh, value); }
		}

		/// <summary>The Fieldname property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."fieldname".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 20.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Fieldname
		{
			get { return (System.String)GetValue((int)SystudienFieldIndex.Fieldname, true); }
			set	{ SetValue((int)SystudienFieldIndex.Fieldname, value); }
		}

		/// <summary>The Ghichu property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."ghichu".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 500.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Ghichu
		{
			get { return (System.String)GetValue((int)SystudienFieldIndex.Ghichu, true); }
			set	{ SetValue((int)SystudienFieldIndex.Ghichu, value); }
		}

		/// <summary>The Id property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."id".<br/>Table field type characteristics (type, precision, scale, length): Bigint, 20, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int64 Id
		{
			get { return (System.Int64)GetValue((int)SystudienFieldIndex.Id, true); }
			set { SetValue((int)SystudienFieldIndex.Id, value); }		}

		/// <summary>The MaDinhdanhtinh property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."ma_dinhdanhtinh".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 20.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String MaDinhdanhtinh
		{
			get { return (System.String)GetValue((int)SystudienFieldIndex.MaDinhdanhtinh, true); }
			set	{ SetValue((int)SystudienFieldIndex.MaDinhdanhtinh, value); }
		}

		/// <summary>The MaTudien property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."ma_tudien".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String MaTudien
		{
			get { return (System.String)GetValue((int)SystudienFieldIndex.MaTudien, true); }
			set	{ SetValue((int)SystudienFieldIndex.MaTudien, value); }
		}

		/// <summary>The Ngaybanhanh property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."ngaybanhanh".<br/>Table field type characteristics (type, precision, scale, length): Timestamp, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> Ngaybanhanh
		{
			get { return (Nullable<System.DateTime>)GetValue((int)SystudienFieldIndex.Ngaybanhanh, false); }
			set	{ SetValue((int)SystudienFieldIndex.Ngaybanhanh, value); }
		}

		/// <summary>The Qdbanhanh property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."qdbanhanh".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 250.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Qdbanhanh
		{
			get { return (System.String)GetValue((int)SystudienFieldIndex.Qdbanhanh, true); }
			set	{ SetValue((int)SystudienFieldIndex.Qdbanhanh, value); }
		}

		/// <summary>The TenEng property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."ten_eng".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 250.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String TenEng
		{
			get { return (System.String)GetValue((int)SystudienFieldIndex.TenEng, true); }
			set	{ SetValue((int)SystudienFieldIndex.TenEng, value); }
		}

		/// <summary>The TenFieldname property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."ten_fieldname".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 250.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String TenFieldname
		{
			get { return (System.String)GetValue((int)SystudienFieldIndex.TenFieldname, true); }
			set	{ SetValue((int)SystudienFieldIndex.TenFieldname, value); }
		}

		/// <summary>The TenTudien property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."ten_tudien".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 150.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String TenTudien
		{
			get { return (System.String)GetValue((int)SystudienFieldIndex.TenTudien, true); }
			set	{ SetValue((int)SystudienFieldIndex.TenTudien, value); }
		}

		/// <summary>The TenVie property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."ten_vie".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 250.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String TenVie
		{
			get { return (System.String)GetValue((int)SystudienFieldIndex.TenVie, true); }
			set	{ SetValue((int)SystudienFieldIndex.TenVie, value); }
		}

		/// <summary>The TrangThai property of the Entity Systudien<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systudien"."trang_thai".<br/>Table field type characteristics (type, precision, scale, length): Bigint, 20, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int64> TrangThai
		{
			get { return (Nullable<System.Int64>)GetValue((int)SystudienFieldIndex.TrangThai, false); }
			set	{ SetValue((int)SystudienFieldIndex.TrangThai, value); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace Authorize.Dal
{
	public enum SystudienFieldIndex
	{
		///<summary>Cap. </summary>
		Cap,
		///<summary>Cqbanhanh. </summary>
		Cqbanhanh,
		///<summary>Fieldname. </summary>
		Fieldname,
		///<summary>Ghichu. </summary>
		Ghichu,
		///<summary>Id. </summary>
		Id,
		///<summary>MaDinhdanhtinh. </summary>
		MaDinhdanhtinh,
		///<summary>MaTudien. </summary>
		MaTudien,
		///<summary>Ngaybanhanh. </summary>
		Ngaybanhanh,
		///<summary>Qdbanhanh. </summary>
		Qdbanhanh,
		///<summary>TenEng. </summary>
		TenEng,
		///<summary>TenFieldname. </summary>
		TenFieldname,
		///<summary>TenTudien. </summary>
		TenTudien,
		///<summary>TenVie. </summary>
		TenVie,
		///<summary>TrangThai. </summary>
		TrangThai,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace Authorize.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Systudien. </summary>
	public partial class SystudienRelations: RelationFactory
	{

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSystudienRelations
	{

		/// <summary>CTor</summary>
		static StaticSystudienRelations() { }
	}
}
