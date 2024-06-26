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
	/// <summary>Entity class which represents the entity 'Systaoma'.<br/><br/></summary>
	[Serializable]
	public partial class SystaomaEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static SystaomaEntityStaticMetaData _staticMetaData = new SystaomaEntityStaticMetaData();
		private static SystaomaRelations _relationsFactory = new SystaomaRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SystaomaEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SystaomaEntityStaticMetaData()
			{
				SetEntityCoreInfo("SystaomaEntity", InheritanceHierarchyType.None, false, (int)App.QTHTGis.Dal.EntityType.SystaomaEntity, typeof(SystaomaEntity), typeof(SystaomaEntityFactory), false);
			}
		}

		/// <summary>Static ctor</summary>
		static SystaomaEntity()
		{
		}

		/// <summary> CTor</summary>
		public SystaomaEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SystaomaEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SystaomaEntity</param>
		public SystaomaEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Systaoma which data should be fetched into this Systaoma object</param>
		public SystaomaEntity(System.Int64 id) : this(id, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Systaoma which data should be fetched into this Systaoma object</param>
		/// <param name="validator">The custom validator object for this SystaomaEntity</param>
		public SystaomaEntity(System.Int64 id, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SystaomaEntity(SerializationInfo info, StreamingContext context) : base(info, context)
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
		/// <param name="validator">The validator object for this SystaomaEntity</param>
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
		public static SystaomaRelations Relations { get { return _relationsFactory; } }

		/// <summary>The Dodai property of the Entity Systaoma<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systaoma"."dodai".<br/>Table field type characteristics (type, precision, scale, length): Smallint, 5, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int16> Dodai
		{
			get { return (Nullable<System.Int16>)GetValue((int)SystaomaFieldIndex.Dodai, false); }
			set	{ SetValue((int)SystaomaFieldIndex.Dodai, value); }
		}

		/// <summary>The Giatricap property of the Entity Systaoma<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systaoma"."giatricap".<br/>Table field type characteristics (type, precision, scale, length): Bigint, 20, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int64> Giatricap
		{
			get { return (Nullable<System.Int64>)GetValue((int)SystaomaFieldIndex.Giatricap, false); }
			set	{ SetValue((int)SystaomaFieldIndex.Giatricap, value); }
		}

		/// <summary>The Id property of the Entity Systaoma<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systaoma"."id".<br/>Table field type characteristics (type, precision, scale, length): Bigint, 20, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int64 Id
		{
			get { return (System.Int64)GetValue((int)SystaomaFieldIndex.Id, true); }
			set { SetValue((int)SystaomaFieldIndex.Id, value); }		}

		/// <summary>The Kyhieubatdau property of the Entity Systaoma<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systaoma"."kyhieubatdau".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Kyhieubatdau
		{
			get { return (System.String)GetValue((int)SystaomaFieldIndex.Kyhieubatdau, true); }
			set	{ SetValue((int)SystaomaFieldIndex.Kyhieubatdau, value); }
		}

		/// <summary>The State property of the Entity Systaoma<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systaoma"."state".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String State
		{
			get { return (System.String)GetValue((int)SystaomaFieldIndex.State, true); }
			set	{ SetValue((int)SystaomaFieldIndex.State, value); }
		}

		/// <summary>The Unitcode property of the Entity Systaoma<br/><br/></summary>
		/// <remarks>Mapped on  table field: "systaoma"."unitcode".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Unitcode
		{
			get { return (System.String)GetValue((int)SystaomaFieldIndex.Unitcode, true); }
			set	{ SetValue((int)SystaomaFieldIndex.Unitcode, value); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace App.QTHTGis.Dal
{
	public enum SystaomaFieldIndex
	{
		///<summary>Dodai. </summary>
		Dodai,
		///<summary>Giatricap. </summary>
		Giatricap,
		///<summary>Id. </summary>
		Id,
		///<summary>Kyhieubatdau. </summary>
		Kyhieubatdau,
		///<summary>State. </summary>
		State,
		///<summary>Unitcode. </summary>
		Unitcode,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace App.QTHTGis.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Systaoma. </summary>
	public partial class SystaomaRelations: RelationFactory
	{

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSystaomaRelations
	{

		/// <summary>CTor</summary>
		static StaticSystaomaRelations() { }
	}
}
