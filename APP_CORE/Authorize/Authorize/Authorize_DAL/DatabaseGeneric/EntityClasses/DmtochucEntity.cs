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
	/// <summary>Entity class which represents the entity 'Dmtochuc'.<br/><br/></summary>
	[Serializable]
	public partial class DmtochucEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<SysunitEntity> _sysunits;
		private EntityCollection<TochucChucvuEntity> _tochucChucvus;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static DmtochucEntityStaticMetaData _staticMetaData = new DmtochucEntityStaticMetaData();
		private static DmtochucRelations _relationsFactory = new DmtochucRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name Sysunits</summary>
			public static readonly string Sysunits = "Sysunits";
			/// <summary>Member name TochucChucvus</summary>
			public static readonly string TochucChucvus = "TochucChucvus";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class DmtochucEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public DmtochucEntityStaticMetaData()
			{
				SetEntityCoreInfo("DmtochucEntity", InheritanceHierarchyType.None, false, (int)Authorize.Dal.EntityType.DmtochucEntity, typeof(DmtochucEntity), typeof(DmtochucEntityFactory), false);
				AddNavigatorMetaData<DmtochucEntity, EntityCollection<SysunitEntity>>("Sysunits", a => a._sysunits, (a, b) => a._sysunits = b, a => a.Sysunits, () => new DmtochucRelations().SysunitEntityUsingIdTochuc, typeof(SysunitEntity), (int)Authorize.Dal.EntityType.SysunitEntity);
				AddNavigatorMetaData<DmtochucEntity, EntityCollection<TochucChucvuEntity>>("TochucChucvus", a => a._tochucChucvus, (a, b) => a._tochucChucvus = b, a => a.TochucChucvus, () => new DmtochucRelations().TochucChucvuEntityUsingIdTochuc, typeof(TochucChucvuEntity), (int)Authorize.Dal.EntityType.TochucChucvuEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static DmtochucEntity()
		{
		}

		/// <summary> CTor</summary>
		public DmtochucEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public DmtochucEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this DmtochucEntity</param>
		public DmtochucEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Dmtochuc which data should be fetched into this Dmtochuc object</param>
		public DmtochucEntity(System.Int64 id) : this(id, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Dmtochuc which data should be fetched into this Dmtochuc object</param>
		/// <param name="validator">The custom validator object for this DmtochucEntity</param>
		public DmtochucEntity(System.Int64 id, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected DmtochucEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Sysunit' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSysunits() { return CreateRelationInfoForNavigator("Sysunits"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'TochucChucvu' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoTochucChucvus() { return CreateRelationInfoForNavigator("TochucChucvus"); }
		
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
		/// <param name="validator">The validator object for this DmtochucEntity</param>
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
		public static DmtochucRelations Relations { get { return _relationsFactory; } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Sysunit' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSysunits { get { return _staticMetaData.GetPrefetchPathElement("Sysunits", CommonEntityBase.CreateEntityCollection<SysunitEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'TochucChucvu' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathTochucChucvus { get { return _staticMetaData.GetPrefetchPathElement("TochucChucvus", CommonEntityBase.CreateEntityCollection<TochucChucvuEntity>()); } }

		/// <summary>The Id property of the Entity Dmtochuc<br/><br/></summary>
		/// <remarks>Mapped on  table field: "dmtochuc"."id".<br/>Table field type characteristics (type, precision, scale, length): Bigint, 20, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int64 Id
		{
			get { return (System.Int64)GetValue((int)DmtochucFieldIndex.Id, true); }
			set { SetValue((int)DmtochucFieldIndex.Id, value); }		}

		/// <summary>The Ten property of the Entity Dmtochuc<br/><br/></summary>
		/// <remarks>Mapped on  table field: "dmtochuc"."ten".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 200.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Ten
		{
			get { return (System.String)GetValue((int)DmtochucFieldIndex.Ten, true); }
			set	{ SetValue((int)DmtochucFieldIndex.Ten, value); }
		}

		/// <summary>The Trangthai property of the Entity Dmtochuc<br/><br/></summary>
		/// <remarks>Mapped on  table field: "dmtochuc"."trangthai".<br/>Table field type characteristics (type, precision, scale, length): Smallint, 5, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int16 Trangthai
		{
			get { return (System.Int16)GetValue((int)DmtochucFieldIndex.Trangthai, true); }
			set	{ SetValue((int)DmtochucFieldIndex.Trangthai, value); }
		}

		/// <summary>Gets the EntityCollection with the related entities of type 'SysunitEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SysunitEntity))]
		public virtual EntityCollection<SysunitEntity> Sysunits { get { return GetOrCreateEntityCollection<SysunitEntity, SysunitEntityFactory>("Dmtochuc", true, false, ref _sysunits); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'TochucChucvuEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(TochucChucvuEntity))]
		public virtual EntityCollection<TochucChucvuEntity> TochucChucvus { get { return GetOrCreateEntityCollection<TochucChucvuEntity, TochucChucvuEntityFactory>("Dmtochuc", true, false, ref _tochucChucvus); } }

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace Authorize.Dal
{
	public enum DmtochucFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>Ten. </summary>
		Ten,
		///<summary>Trangthai. </summary>
		Trangthai,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace Authorize.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Dmtochuc. </summary>
	public partial class DmtochucRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between DmtochucEntity and SysunitEntity over the 1:n relation they have, using the relation between the fields: Dmtochuc.Id - Sysunit.IdTochuc</summary>
		public virtual IEntityRelation SysunitEntityUsingIdTochuc
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "Sysunits", true, new[] { DmtochucFields.Id, SysunitFields.IdTochuc }); }
		}

		/// <summary>Returns a new IEntityRelation object, between DmtochucEntity and TochucChucvuEntity over the 1:n relation they have, using the relation between the fields: Dmtochuc.Id - TochucChucvu.IdTochuc</summary>
		public virtual IEntityRelation TochucChucvuEntityUsingIdTochuc
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "TochucChucvus", true, new[] { DmtochucFields.Id, TochucChucvuFields.IdTochuc }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticDmtochucRelations
	{
		internal static readonly IEntityRelation SysunitEntityUsingIdTochucStatic = new DmtochucRelations().SysunitEntityUsingIdTochuc;
		internal static readonly IEntityRelation TochucChucvuEntityUsingIdTochucStatic = new DmtochucRelations().TochucChucvuEntityUsingIdTochuc;

		/// <summary>CTor</summary>
		static StaticDmtochucRelations() { }
	}
}
