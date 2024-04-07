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
	/// <summary>Entity class which represents the entity 'Sysgroup'.<br/><br/></summary>
	[Serializable]
	public partial class SysgroupEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<SysgroupmenuEntity> _sysgroupmenus;
		private EntityCollection<SysusergroupEntity> _sysusergroups;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static SysgroupEntityStaticMetaData _staticMetaData = new SysgroupEntityStaticMetaData();
		private static SysgroupRelations _relationsFactory = new SysgroupRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name Sysgroupmenus</summary>
			public static readonly string Sysgroupmenus = "Sysgroupmenus";
			/// <summary>Member name Sysusergroups</summary>
			public static readonly string Sysusergroups = "Sysusergroups";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SysgroupEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SysgroupEntityStaticMetaData()
			{
				SetEntityCoreInfo("SysgroupEntity", InheritanceHierarchyType.None, false, (int)Authorize.Dal.EntityType.SysgroupEntity, typeof(SysgroupEntity), typeof(SysgroupEntityFactory), false);
				AddNavigatorMetaData<SysgroupEntity, EntityCollection<SysgroupmenuEntity>>("Sysgroupmenus", a => a._sysgroupmenus, (a, b) => a._sysgroupmenus = b, a => a.Sysgroupmenus, () => new SysgroupRelations().SysgroupmenuEntityUsingGroupid, typeof(SysgroupmenuEntity), (int)Authorize.Dal.EntityType.SysgroupmenuEntity);
				AddNavigatorMetaData<SysgroupEntity, EntityCollection<SysusergroupEntity>>("Sysusergroups", a => a._sysusergroups, (a, b) => a._sysusergroups = b, a => a.Sysusergroups, () => new SysgroupRelations().SysusergroupEntityUsingGroupid, typeof(SysusergroupEntity), (int)Authorize.Dal.EntityType.SysusergroupEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static SysgroupEntity()
		{
		}

		/// <summary> CTor</summary>
		public SysgroupEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SysgroupEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SysgroupEntity</param>
		public SysgroupEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="groupid">PK value for Sysgroup which data should be fetched into this Sysgroup object</param>
		public SysgroupEntity(System.Int64 groupid) : this(groupid, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="groupid">PK value for Sysgroup which data should be fetched into this Sysgroup object</param>
		/// <param name="validator">The custom validator object for this SysgroupEntity</param>
		public SysgroupEntity(System.Int64 groupid, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Groupid = groupid;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SysgroupEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Sysgroupmenu' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSysgroupmenus() { return CreateRelationInfoForNavigator("Sysgroupmenus"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Sysusergroup' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSysusergroups() { return CreateRelationInfoForNavigator("Sysusergroups"); }
		
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
		/// <param name="validator">The validator object for this SysgroupEntity</param>
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
		public static SysgroupRelations Relations { get { return _relationsFactory; } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Sysgroupmenu' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSysgroupmenus { get { return _staticMetaData.GetPrefetchPathElement("Sysgroupmenus", CommonEntityBase.CreateEntityCollection<SysgroupmenuEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Sysusergroup' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSysusergroups { get { return _staticMetaData.GetPrefetchPathElement("Sysusergroups", CommonEntityBase.CreateEntityCollection<SysusergroupEntity>()); } }

		/// <summary>The Appid property of the Entity Sysgroup<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysgroup"."appid".<br/>Table field type characteristics (type, precision, scale, length): Bigint, 20, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int64> Appid
		{
			get { return (Nullable<System.Int64>)GetValue((int)SysgroupFieldIndex.Appid, false); }
			set	{ SetValue((int)SysgroupFieldIndex.Appid, value); }
		}

		/// <summary>The Description property of the Entity Sysgroup<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysgroup"."description".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 250.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Description
		{
			get { return (System.String)GetValue((int)SysgroupFieldIndex.Description, true); }
			set	{ SetValue((int)SysgroupFieldIndex.Description, value); }
		}

		/// <summary>The Groupid property of the Entity Sysgroup<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysgroup"."groupid".<br/>Table field type characteristics (type, precision, scale, length): Bigint, 20, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int64 Groupid
		{
			get { return (System.Int64)GetValue((int)SysgroupFieldIndex.Groupid, true); }
			set { SetValue((int)SysgroupFieldIndex.Groupid, value); }		}

		/// <summary>The Groupname property of the Entity Sysgroup<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysgroup"."groupname".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 200.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Groupname
		{
			get { return (System.String)GetValue((int)SysgroupFieldIndex.Groupname, true); }
			set	{ SetValue((int)SysgroupFieldIndex.Groupname, value); }
		}

		/// <summary>The Level property of the Entity Sysgroup<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysgroup"."level".<br/>Table field type characteristics (type, precision, scale, length): Bigint, 20, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int64> Level
		{
			get { return (Nullable<System.Int64>)GetValue((int)SysgroupFieldIndex.Level, false); }
			set	{ SetValue((int)SysgroupFieldIndex.Level, value); }
		}

		/// <summary>The Status property of the Entity Sysgroup<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysgroup"."status".<br/>Table field type characteristics (type, precision, scale, length): Smallint, 5, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int16> Status
		{
			get { return (Nullable<System.Int16>)GetValue((int)SysgroupFieldIndex.Status, false); }
			set	{ SetValue((int)SysgroupFieldIndex.Status, value); }
		}

		/// <summary>The Unitcode property of the Entity Sysgroup<br/><br/></summary>
		/// <remarks>Mapped on  table field: "sysgroup"."unitcode".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Unitcode
		{
			get { return (System.String)GetValue((int)SysgroupFieldIndex.Unitcode, true); }
			set	{ SetValue((int)SysgroupFieldIndex.Unitcode, value); }
		}

		/// <summary>Gets the EntityCollection with the related entities of type 'SysgroupmenuEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SysgroupmenuEntity))]
		public virtual EntityCollection<SysgroupmenuEntity> Sysgroupmenus { get { return GetOrCreateEntityCollection<SysgroupmenuEntity, SysgroupmenuEntityFactory>("Sysgroup", true, false, ref _sysgroupmenus); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'SysusergroupEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SysusergroupEntity))]
		public virtual EntityCollection<SysusergroupEntity> Sysusergroups { get { return GetOrCreateEntityCollection<SysusergroupEntity, SysusergroupEntityFactory>("Sysgroup", true, false, ref _sysusergroups); } }

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace Authorize.Dal
{
	public enum SysgroupFieldIndex
	{
		///<summary>Appid. </summary>
		Appid,
		///<summary>Description. </summary>
		Description,
		///<summary>Groupid. </summary>
		Groupid,
		///<summary>Groupname. </summary>
		Groupname,
		///<summary>Level. </summary>
		Level,
		///<summary>Status. </summary>
		Status,
		///<summary>Unitcode. </summary>
		Unitcode,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace Authorize.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Sysgroup. </summary>
	public partial class SysgroupRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between SysgroupEntity and SysgroupmenuEntity over the 1:n relation they have, using the relation between the fields: Sysgroup.Groupid - Sysgroupmenu.Groupid</summary>
		public virtual IEntityRelation SysgroupmenuEntityUsingGroupid
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "Sysgroupmenus", true, new[] { SysgroupFields.Groupid, SysgroupmenuFields.Groupid }); }
		}

		/// <summary>Returns a new IEntityRelation object, between SysgroupEntity and SysusergroupEntity over the 1:n relation they have, using the relation between the fields: Sysgroup.Groupid - Sysusergroup.Groupid</summary>
		public virtual IEntityRelation SysusergroupEntityUsingGroupid
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "Sysusergroups", true, new[] { SysgroupFields.Groupid, SysusergroupFields.Groupid }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSysgroupRelations
	{
		internal static readonly IEntityRelation SysgroupmenuEntityUsingGroupidStatic = new SysgroupRelations().SysgroupmenuEntityUsingGroupid;
		internal static readonly IEntityRelation SysusergroupEntityUsingGroupidStatic = new SysgroupRelations().SysusergroupEntityUsingGroupid;

		/// <summary>CTor</summary>
		static StaticSysgroupRelations() { }
	}
}
