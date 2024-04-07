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
	/// <summary>Entity class which represents the entity 'Sysbaotainan'.<br/><br/></summary>
	[Serializable]
	public partial class SysbaotainanEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static SysbaotainanEntityStaticMetaData _staticMetaData = new SysbaotainanEntityStaticMetaData();
		private static SysbaotainanRelations _relationsFactory = new SysbaotainanRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SysbaotainanEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SysbaotainanEntityStaticMetaData()
			{
				SetEntityCoreInfo("SysbaotainanEntity", InheritanceHierarchyType.None, false, (int)App.CongAnGis.Dal.EntityType.SysbaotainanEntity, typeof(SysbaotainanEntity), typeof(SysbaotainanEntityFactory), false);
			}
		}

		/// <summary>Static ctor</summary>
		static SysbaotainanEntity()
		{
		}

		/// <summary> CTor</summary>
		public SysbaotainanEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SysbaotainanEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SysbaotainanEntity</param>
		public SysbaotainanEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Sysbaotainan which data should be fetched into this Sysbaotainan object</param>
		public SysbaotainanEntity(System.Int32 id) : this(id, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Sysbaotainan which data should be fetched into this Sysbaotainan object</param>
		/// <param name="validator">The custom validator object for this SysbaotainanEntity</param>
		public SysbaotainanEntity(System.Int32 id, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SysbaotainanEntity(SerializationInfo info, StreamingContext context) : base(info, context)
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
		/// <param name="validator">The validator object for this SysbaotainanEntity</param>
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
		public static SysbaotainanRelations Relations { get { return _relationsFactory; } }

		/// <summary>The Address property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."address".<br/>Table field type characteristics (type, precision, scale, length): Text, 0, 0, 1073741824.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Address
		{
			get { return (System.String)GetValue((int)SysbaotainanFieldIndex.Address, true); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Address, value); }
		}

		/// <summary>The Createby property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."createby".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Createby
		{
			get { return (System.String)GetValue((int)SysbaotainanFieldIndex.Createby, true); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Createby, value); }
		}

		/// <summary>The Createbyid property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."createbyid".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Createbyid
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysbaotainanFieldIndex.Createbyid, false); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Createbyid, value); }
		}

		/// <summary>The Createdate property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."createdate".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Createdate
		{
			get { return (System.String)GetValue((int)SysbaotainanFieldIndex.Createdate, true); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Createdate, value); }
		}

		/// <summary>The Icon property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."icon".<br/>Table field type characteristics (type, precision, scale, length): Text, 0, 0, 1073741824.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Icon
		{
			get { return (System.String)GetValue((int)SysbaotainanFieldIndex.Icon, true); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Icon, value); }
		}

		/// <summary>The Id property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."id".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)SysbaotainanFieldIndex.Id, true); }
			set { SetValue((int)SysbaotainanFieldIndex.Id, value); }		}

		/// <summary>The Iddata property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."iddata".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Iddata
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysbaotainanFieldIndex.Iddata, false); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Iddata, value); }
		}

		/// <summary>The Mahuyen property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."mahuyen".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Mahuyen
		{
			get { return (System.String)GetValue((int)SysbaotainanFieldIndex.Mahuyen, true); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Mahuyen, value); }
		}

		/// <summary>The Maxa property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."maxa".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 10485760.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Maxa
		{
			get { return (System.String)GetValue((int)SysbaotainanFieldIndex.Maxa, true); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Maxa, value); }
		}

		/// <summary>The Nguoitiepnhan property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."nguoitiepnhan".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Nguoitiepnhan
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysbaotainanFieldIndex.Nguoitiepnhan, false); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Nguoitiepnhan, value); }
		}

		/// <summary>The Phonenumber property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."phonenumber".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Phonenumber
		{
			get { return (System.String)GetValue((int)SysbaotainanFieldIndex.Phonenumber, true); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Phonenumber, value); }
		}

		/// <summary>The Reasonfire property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."reasonfire".<br/>Table field type characteristics (type, precision, scale, length): Text, 0, 0, 1073741824.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Reasonfire
		{
			get { return (System.String)GetValue((int)SysbaotainanFieldIndex.Reasonfire, true); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Reasonfire, value); }
		}

		/// <summary>The Shape property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."shape".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Shape
		{
			get { return (System.String)GetValue((int)SysbaotainanFieldIndex.Shape, true); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Shape, value); }
		}

		/// <summary>The Status property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."status".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> Status
		{
			get { return (Nullable<System.Int32>)GetValue((int)SysbaotainanFieldIndex.Status, false); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Status, value); }
		}

		/// <summary>The Tablename property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."tablename".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Tablename
		{
			get { return (System.String)GetValue((int)SysbaotainanFieldIndex.Tablename, true); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Tablename, value); }
		}

		/// <summary>The Updateat property of the Entity Sysbaotainan<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysbaotainan"."updateat".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Updateat
		{
			get { return (System.String)GetValue((int)SysbaotainanFieldIndex.Updateat, true); }
			set	{ SetValue((int)SysbaotainanFieldIndex.Updateat, value); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace App.CongAnGis.Dal
{
	public enum SysbaotainanFieldIndex
	{
		///<summary>Address. </summary>
		Address,
		///<summary>Createby. </summary>
		Createby,
		///<summary>Createbyid. </summary>
		Createbyid,
		///<summary>Createdate. </summary>
		Createdate,
		///<summary>Icon. </summary>
		Icon,
		///<summary>Id. </summary>
		Id,
		///<summary>Iddata. </summary>
		Iddata,
		///<summary>Mahuyen. </summary>
		Mahuyen,
		///<summary>Maxa. </summary>
		Maxa,
		///<summary>Nguoitiepnhan. </summary>
		Nguoitiepnhan,
		///<summary>Phonenumber. </summary>
		Phonenumber,
		///<summary>Reasonfire. </summary>
		Reasonfire,
		///<summary>Shape. </summary>
		Shape,
		///<summary>Status. </summary>
		Status,
		///<summary>Tablename. </summary>
		Tablename,
		///<summary>Updateat. </summary>
		Updateat,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace App.CongAnGis.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Sysbaotainan. </summary>
	public partial class SysbaotainanRelations: RelationFactory
	{

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSysbaotainanRelations
	{

		/// <summary>CTor</summary>
		static StaticSysbaotainanRelations() { }
	}
}