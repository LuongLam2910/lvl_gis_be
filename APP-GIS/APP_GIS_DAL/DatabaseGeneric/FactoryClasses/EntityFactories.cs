﻿//////////////////////////////////////////////////////////////
// <auto-generated>This code was generated by LLBLGen Pro 5.10.</auto-generated>
//////////////////////////////////////////////////////////////
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
//////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Dal.RelationClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace App.CongAnGis.Dal.FactoryClasses
{
	// __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
	// __LLBLGENPRO_USER_CODE_REGION_END

	/// <summary>general base class for the generated factories</summary>
	[Serializable]
	public partial class EntityFactoryBase2<TEntity> : EntityFactoryCore2
		where TEntity : EntityBase2, IEntity2
	{
		private readonly bool _isInHierarchy;

		/// <summary>CTor</summary>
		/// <param name="entityName">Name of the entity.</param>
		/// <param name="typeOfEntity">The type of entity.</param>
		/// <param name="isInHierarchy">If true, the entity of this factory is in an inheritance hierarchy, false otherwise</param>
		public EntityFactoryBase2(string entityName, App.CongAnGis.Dal.EntityType typeOfEntity, bool isInHierarchy) : base(entityName, (int)typeOfEntity)
		{
			_isInHierarchy = isInHierarchy;
		}
		
		/// <inheritdoc/>
		public override IEntityFields2 CreateFields() { return ModelInfoProviderSingleton.GetInstance().GetEntityFields(this.ForEntityName); }
		
		/// <inheritdoc/>
		public override IEntity2 CreateEntityFromEntityTypeValue(int entityTypeValue) {	return GeneralEntityFactory.Create((App.CongAnGis.Dal.EntityType)entityTypeValue); }

		/// <inheritdoc/>
		public override IRelationCollection CreateHierarchyRelations(string objectAlias) { return ModelInfoProviderSingleton.GetInstance().GetHierarchyRelations(this.ForEntityName, objectAlias); }

		/// <inheritdoc/>
		public override IEntityFactory2 GetEntityFactory(object[] fieldValues, Dictionary<string, int> entityFieldStartIndexesPerEntity) 
		{
			return (IEntityFactory2)ModelInfoProviderSingleton.GetInstance().GetEntityFactory(this.ForEntityName, fieldValues, entityFieldStartIndexesPerEntity) ?? this;
		}
		
		/// <inheritdoc/>
		public override IPredicateExpression GetEntityTypeFilter(bool negate, string objectAlias) {	return ModelInfoProviderSingleton.GetInstance().GetEntityTypeFilter(this.ForEntityName, objectAlias, negate);	}
						
		/// <inheritdoc/>
		public override IEntityCollection2 CreateEntityCollection()	{ return new EntityCollection<TEntity>(this); }
		
		/// <inheritdoc/>
		public override IEntityFields2 CreateHierarchyFields() 
		{
			return _isInHierarchy ? new EntityFields2(ModelInfoProviderSingleton.GetInstance().GetHierarchyFields(this.ForEntityName), ModelInfoProviderSingleton.GetInstance(), null) : base.CreateHierarchyFields();
		}
		
		/// <inheritdoc/>
		protected override Type ForEntityType { get { return typeof(TEntity); } }
	}

	/// <summary>Factory to create new, empty SysbaochayEntity objects.</summary>
	[Serializable]
	public partial class SysbaochayEntityFactory : EntityFactoryBase2<SysbaochayEntity> 
	{
		/// <summary>CTor</summary>
		public SysbaochayEntityFactory() : base("SysbaochayEntity", App.CongAnGis.Dal.EntityType.SysbaochayEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysbaochayEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysbaotainanEntity objects.</summary>
	[Serializable]
	public partial class SysbaotainanEntityFactory : EntityFactoryBase2<SysbaotainanEntity> 
	{
		/// <summary>CTor</summary>
		public SysbaotainanEntityFactory() : base("SysbaotainanEntity", App.CongAnGis.Dal.EntityType.SysbaotainanEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysbaotainanEntity(fields); }
	}

	/// <summary>Factory to create new, empty SyscauhinhEntity objects.</summary>
	[Serializable]
	public partial class SyscauhinhEntityFactory : EntityFactoryBase2<SyscauhinhEntity> 
	{
		/// <summary>CTor</summary>
		public SyscauhinhEntityFactory() : base("SyscauhinhEntity", App.CongAnGis.Dal.EntityType.SyscauhinhEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SyscauhinhEntity(fields); }
	}

	/// <summary>Factory to create new, empty SyscsgtEntity objects.</summary>
	[Serializable]
	public partial class SyscsgtEntityFactory : EntityFactoryBase2<SyscsgtEntity> 
	{
		/// <summary>CTor</summary>
		public SyscsgtEntityFactory() : base("SyscsgtEntity", App.CongAnGis.Dal.EntityType.SyscsgtEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SyscsgtEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysdatasetEntity objects.</summary>
	[Serializable]
	public partial class SysdatasetEntityFactory : EntityFactoryBase2<SysdatasetEntity> 
	{
		/// <summary>CTor</summary>
		public SysdatasetEntityFactory() : base("SysdatasetEntity", App.CongAnGis.Dal.EntityType.SysdatasetEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysdatasetEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysdatasetUserEntity objects.</summary>
	[Serializable]
	public partial class SysdatasetUserEntityFactory : EntityFactoryBase2<SysdatasetUserEntity> 
	{
		/// <summary>CTor</summary>
		public SysdatasetUserEntityFactory() : base("SysdatasetUserEntity", App.CongAnGis.Dal.EntityType.SysdatasetUserEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysdatasetUserEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysdmtrangthietbipcccEntity objects.</summary>
	[Serializable]
	public partial class SysdmtrangthietbipcccEntityFactory : EntityFactoryBase2<SysdmtrangthietbipcccEntity> 
	{
		/// <summary>CTor</summary>
		public SysdmtrangthietbipcccEntityFactory() : base("SysdmtrangthietbipcccEntity", App.CongAnGis.Dal.EntityType.SysdmtrangthietbipcccEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysdmtrangthietbipcccEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysfeatureclassEntity objects.</summary>
	[Serializable]
	public partial class SysfeatureclassEntityFactory : EntityFactoryBase2<SysfeatureclassEntity> 
	{
		/// <summary>CTor</summary>
		public SysfeatureclassEntityFactory() : base("SysfeatureclassEntity", App.CongAnGis.Dal.EntityType.SysfeatureclassEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysfeatureclassEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysfeatureclassUserEntity objects.</summary>
	[Serializable]
	public partial class SysfeatureclassUserEntityFactory : EntityFactoryBase2<SysfeatureclassUserEntity> 
	{
		/// <summary>CTor</summary>
		public SysfeatureclassUserEntityFactory() : base("SysfeatureclassUserEntity", App.CongAnGis.Dal.EntityType.SysfeatureclassUserEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysfeatureclassUserEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysfieldEntity objects.</summary>
	[Serializable]
	public partial class SysfieldEntityFactory : EntityFactoryBase2<SysfieldEntity> 
	{
		/// <summary>CTor</summary>
		public SysfieldEntityFactory() : base("SysfieldEntity", App.CongAnGis.Dal.EntityType.SysfieldEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysfieldEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysfileEntity objects.</summary>
	[Serializable]
	public partial class SysfileEntityFactory : EntityFactoryBase2<SysfileEntity> 
	{
		/// <summary>CTor</summary>
		public SysfileEntityFactory() : base("SysfileEntity", App.CongAnGis.Dal.EntityType.SysfileEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysfileEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysfilemanagerEntity objects.</summary>
	[Serializable]
	public partial class SysfilemanagerEntityFactory : EntityFactoryBase2<SysfilemanagerEntity> 
	{
		/// <summary>CTor</summary>
		public SysfilemanagerEntityFactory() : base("SysfilemanagerEntity", App.CongAnGis.Dal.EntityType.SysfilemanagerEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysfilemanagerEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysfolderEntity objects.</summary>
	[Serializable]
	public partial class SysfolderEntityFactory : EntityFactoryBase2<SysfolderEntity> 
	{
		/// <summary>CTor</summary>
		public SysfolderEntityFactory() : base("SysfolderEntity", App.CongAnGis.Dal.EntityType.SysfolderEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysfolderEntity(fields); }
	}

	/// <summary>Factory to create new, empty SyshistoryuserlineEntity objects.</summary>
	[Serializable]
	public partial class SyshistoryuserlineEntityFactory : EntityFactoryBase2<SyshistoryuserlineEntity> 
	{
		/// <summary>CTor</summary>
		public SyshistoryuserlineEntityFactory() : base("SyshistoryuserlineEntity", App.CongAnGis.Dal.EntityType.SyshistoryuserlineEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SyshistoryuserlineEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysimportLogEntity objects.</summary>
	[Serializable]
	public partial class SysimportLogEntityFactory : EntityFactoryBase2<SysimportLogEntity> 
	{
		/// <summary>CTor</summary>
		public SysimportLogEntityFactory() : base("SysimportLogEntity", App.CongAnGis.Dal.EntityType.SysimportLogEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysimportLogEntity(fields); }
	}

	/// <summary>Factory to create new, empty SyslinhsucanhbaoEntity objects.</summary>
	[Serializable]
	public partial class SyslinhsucanhbaoEntityFactory : EntityFactoryBase2<SyslinhsucanhbaoEntity> 
	{
		/// <summary>CTor</summary>
		public SyslinhsucanhbaoEntityFactory() : base("SyslinhsucanhbaoEntity", App.CongAnGis.Dal.EntityType.SyslinhsucanhbaoEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SyslinhsucanhbaoEntity(fields); }
	}

	/// <summary>Factory to create new, empty SyslogactiongisEntity objects.</summary>
	[Serializable]
	public partial class SyslogactiongisEntityFactory : EntityFactoryBase2<SyslogactiongisEntity> 
	{
		/// <summary>CTor</summary>
		public SyslogactiongisEntityFactory() : base("SyslogactiongisEntity", App.CongAnGis.Dal.EntityType.SyslogactiongisEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SyslogactiongisEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysmapEntity objects.</summary>
	[Serializable]
	public partial class SysmapEntityFactory : EntityFactoryBase2<SysmapEntity> 
	{
		/// <summary>CTor</summary>
		public SysmapEntityFactory() : base("SysmapEntity", App.CongAnGis.Dal.EntityType.SysmapEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysmapEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysmapUserEntity objects.</summary>
	[Serializable]
	public partial class SysmapUserEntityFactory : EntityFactoryBase2<SysmapUserEntity> 
	{
		/// <summary>CTor</summary>
		public SysmapUserEntityFactory() : base("SysmapUserEntity", App.CongAnGis.Dal.EntityType.SysmapUserEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysmapUserEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysphieuchienthuatEntity objects.</summary>
	[Serializable]
	public partial class SysphieuchienthuatEntityFactory : EntityFactoryBase2<SysphieuchienthuatEntity> 
	{
		/// <summary>CTor</summary>
		public SysphieuchienthuatEntityFactory() : base("SysphieuchienthuatEntity", App.CongAnGis.Dal.EntityType.SysphieuchienthuatEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysphieuchienthuatEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysphuonganaEntity objects.</summary>
	[Serializable]
	public partial class SysphuonganaEntityFactory : EntityFactoryBase2<SysphuonganaEntity> 
	{
		/// <summary>CTor</summary>
		public SysphuonganaEntityFactory() : base("SysphuonganaEntity", App.CongAnGis.Dal.EntityType.SysphuonganaEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysphuonganaEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysphuonganbEntity objects.</summary>
	[Serializable]
	public partial class SysphuonganbEntityFactory : EntityFactoryBase2<SysphuonganbEntity> 
	{
		/// <summary>CTor</summary>
		public SysphuonganbEntityFactory() : base("SysphuonganbEntity", App.CongAnGis.Dal.EntityType.SysphuonganbEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysphuonganbEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysphuongancEntity objects.</summary>
	[Serializable]
	public partial class SysphuongancEntityFactory : EntityFactoryBase2<SysphuongancEntity> 
	{
		/// <summary>CTor</summary>
		public SysphuongancEntityFactory() : base("SysphuongancEntity", App.CongAnGis.Dal.EntityType.SysphuongancEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysphuongancEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysphuongandEntity objects.</summary>
	[Serializable]
	public partial class SysphuongandEntityFactory : EntityFactoryBase2<SysphuongandEntity> 
	{
		/// <summary>CTor</summary>
		public SysphuongandEntityFactory() : base("SysphuongandEntity", App.CongAnGis.Dal.EntityType.SysphuongandEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysphuongandEntity(fields); }
	}

	/// <summary>Factory to create new, empty SyssettingEntity objects.</summary>
	[Serializable]
	public partial class SyssettingEntityFactory : EntityFactoryBase2<SyssettingEntity> 
	{
		/// <summary>CTor</summary>
		public SyssettingEntityFactory() : base("SyssettingEntity", App.CongAnGis.Dal.EntityType.SyssettingEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SyssettingEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysttphieuchienthuatEntity objects.</summary>
	[Serializable]
	public partial class SysttphieuchienthuatEntityFactory : EntityFactoryBase2<SysttphieuchienthuatEntity> 
	{
		/// <summary>CTor</summary>
		public SysttphieuchienthuatEntityFactory() : base("SysttphieuchienthuatEntity", App.CongAnGis.Dal.EntityType.SysttphieuchienthuatEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysttphieuchienthuatEntity(fields); }
	}

	/// <summary>Factory to create new, empty SystrangthietbidoituongEntity objects.</summary>
	[Serializable]
	public partial class SystrangthietbidoituongEntityFactory : EntityFactoryBase2<SystrangthietbidoituongEntity> 
	{
		/// <summary>CTor</summary>
		public SystrangthietbidoituongEntityFactory() : base("SystrangthietbidoituongEntity", App.CongAnGis.Dal.EntityType.SystrangthietbidoituongEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SystrangthietbidoituongEntity(fields); }
	}

	/// <summary>Factory to create new, empty SysuserhistoryEntity objects.</summary>
	[Serializable]
	public partial class SysuserhistoryEntityFactory : EntityFactoryBase2<SysuserhistoryEntity> 
	{
		/// <summary>CTor</summary>
		public SysuserhistoryEntityFactory() : base("SysuserhistoryEntity", App.CongAnGis.Dal.EntityType.SysuserhistoryEntity, false) { }
		/// <inheritdoc/>
		protected override IEntity2 CreateImpl(IEntityFields2 fields) { return new SysuserhistoryEntity(fields); }
	}

	/// <summary>Factory to create new, empty Entity objects based on the entity type specified. Uses  entity specific factory objects</summary>
	[Serializable]
	public partial class GeneralEntityFactory
	{
		/// <summary>Creates a new, empty Entity object of the type specified</summary>
		/// <param name="entityTypeToCreate">The entity type to create.</param>
		/// <returns>A new, empty Entity object.</returns>
		public static IEntity2 Create(App.CongAnGis.Dal.EntityType entityTypeToCreate)
		{
			var factoryToUse = EntityFactoryFactory.GetFactory(entityTypeToCreate);
			IEntity2 toReturn = null;
			if(factoryToUse != null)
			{
				toReturn = factoryToUse.Create();
			}
			return toReturn;
		}		
	}
		
	/// <summary>Class which is used to obtain the entity factory based on the .NET type of the entity. </summary>
	[Serializable]
	public static class EntityFactoryFactory
	{
		private static Dictionary<Type, IEntityFactory2> _factoryPerType = new Dictionary<Type, IEntityFactory2>();

		/// <summary>Initializes the <see cref="EntityFactoryFactory"/> class.</summary>
		static EntityFactoryFactory()
		{
			foreach(int entityTypeValue in Enum.GetValues(typeof(App.CongAnGis.Dal.EntityType)))
			{
				var factory = GetFactory((App.CongAnGis.Dal.EntityType)entityTypeValue);
				_factoryPerType.Add(factory.ForEntityType ?? factory.Create().GetType(), factory);
			}
		}

		/// <summary>Gets the factory of the entity with the .NET type specified</summary>
		/// <param name="typeOfEntity">The type of entity.</param>
		/// <returns>factory to use or null if not found</returns>
		public static IEntityFactory2 GetFactory(Type typeOfEntity) { return _factoryPerType.GetValue(typeOfEntity); }

		/// <summary>Gets the factory of the entity with the App.CongAnGis.Dal.EntityType specified</summary>
		/// <param name="typeOfEntity">The type of entity.</param>
		/// <returns>factory to use or null if not found</returns>
		public static IEntityFactory2 GetFactory(App.CongAnGis.Dal.EntityType typeOfEntity)
		{
			switch(typeOfEntity)
			{
				case App.CongAnGis.Dal.EntityType.SysbaochayEntity:
					return new SysbaochayEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysbaotainanEntity:
					return new SysbaotainanEntityFactory();
				case App.CongAnGis.Dal.EntityType.SyscauhinhEntity:
					return new SyscauhinhEntityFactory();
				case App.CongAnGis.Dal.EntityType.SyscsgtEntity:
					return new SyscsgtEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysdatasetEntity:
					return new SysdatasetEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysdatasetUserEntity:
					return new SysdatasetUserEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysdmtrangthietbipcccEntity:
					return new SysdmtrangthietbipcccEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysfeatureclassEntity:
					return new SysfeatureclassEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysfeatureclassUserEntity:
					return new SysfeatureclassUserEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysfieldEntity:
					return new SysfieldEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysfileEntity:
					return new SysfileEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysfilemanagerEntity:
					return new SysfilemanagerEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysfolderEntity:
					return new SysfolderEntityFactory();
				case App.CongAnGis.Dal.EntityType.SyshistoryuserlineEntity:
					return new SyshistoryuserlineEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysimportLogEntity:
					return new SysimportLogEntityFactory();
				case App.CongAnGis.Dal.EntityType.SyslinhsucanhbaoEntity:
					return new SyslinhsucanhbaoEntityFactory();
				case App.CongAnGis.Dal.EntityType.SyslogactiongisEntity:
					return new SyslogactiongisEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysmapEntity:
					return new SysmapEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysmapUserEntity:
					return new SysmapUserEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysphieuchienthuatEntity:
					return new SysphieuchienthuatEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysphuonganaEntity:
					return new SysphuonganaEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysphuonganbEntity:
					return new SysphuonganbEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysphuongancEntity:
					return new SysphuongancEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysphuongandEntity:
					return new SysphuongandEntityFactory();
				case App.CongAnGis.Dal.EntityType.SyssettingEntity:
					return new SyssettingEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysttphieuchienthuatEntity:
					return new SysttphieuchienthuatEntityFactory();
				case App.CongAnGis.Dal.EntityType.SystrangthietbidoituongEntity:
					return new SystrangthietbidoituongEntityFactory();
				case App.CongAnGis.Dal.EntityType.SysuserhistoryEntity:
					return new SysuserhistoryEntityFactory();
				default:
					return null;
			}
		}
	}
		
	/// <summary>Element creator for creating project elements from somewhere else, like inside Linq providers.</summary>
	public class ElementCreator : ElementCreatorBase, IElementCreator2
	{
		/// <summary>Gets the factory of the Entity type with the App.CongAnGis.Dal.EntityType value passed in</summary>
		/// <param name="entityTypeValue">The entity type value.</param>
		/// <returns>the entity factory of the entity type or null if not found</returns>
		public IEntityFactory2 GetFactory(int entityTypeValue) { return (IEntityFactory2)this.GetFactoryImpl(entityTypeValue); }
		
		/// <summary>Gets the factory of the Entity type with the .NET type passed in</summary>
		/// <param name="typeOfEntity">The type of entity.</param>
		/// <returns>the entity factory of the entity type or null if not found</returns>
		public IEntityFactory2 GetFactory(Type typeOfEntity) { return (IEntityFactory2)this.GetFactoryImpl(typeOfEntity); }

		/// <summary>Creates a new resultset fields object with the number of field slots reserved as specified</summary>
		/// <param name="numberOfFields">The number of fields.</param>
		/// <returns>ready to use resultsetfields object</returns>
		public IEntityFields2 CreateResultsetFields(int numberOfFields) { return new ResultsetFields(numberOfFields); }
		
		/// <inheritdoc/>
		public override IInheritanceInfoProvider ObtainInheritanceInfoProviderInstance() { return ModelInfoProviderSingleton.GetInstance(); }

		/// <inheritdoc/>
		public override IDynamicRelation CreateDynamicRelation(DerivedTableDefinition leftOperand) { return new DynamicRelation(leftOperand); }

		/// <inheritdoc/>
		public override IDynamicRelation CreateDynamicRelation(DerivedTableDefinition leftOperand, JoinHint joinType, DerivedTableDefinition rightOperand, IPredicate onClause)
		{
			return new DynamicRelation(leftOperand, joinType, rightOperand, onClause);
		}

		/// <inheritdoc/>
		public override IDynamicRelation CreateDynamicRelation(IEntityFieldCore leftOperand, JoinHint joinType, DerivedTableDefinition rightOperand, string aliasLeftOperand, IPredicate onClause)
		{
			return new DynamicRelation(leftOperand, joinType, rightOperand, aliasLeftOperand, onClause);
		}

		/// <inheritdoc/>
		public override IDynamicRelation CreateDynamicRelation(DerivedTableDefinition leftOperand, JoinHint joinType, string rightOperandEntityName, string aliasRightOperand, IPredicate onClause)
		{
			return new DynamicRelation(leftOperand, joinType, (App.CongAnGis.Dal.EntityType)Enum.Parse(typeof(App.CongAnGis.Dal.EntityType), rightOperandEntityName, false), aliasRightOperand, onClause);
		}

		/// <inheritdoc/>
		public override IDynamicRelation CreateDynamicRelation(string leftOperandEntityName, JoinHint joinType, string rightOperandEntityName, string aliasLeftOperand, string aliasRightOperand, IPredicate onClause)
		{
			return new DynamicRelation((App.CongAnGis.Dal.EntityType)Enum.Parse(typeof(App.CongAnGis.Dal.EntityType), leftOperandEntityName, false), joinType, (App.CongAnGis.Dal.EntityType)Enum.Parse(typeof(App.CongAnGis.Dal.EntityType), rightOperandEntityName, false), aliasLeftOperand, aliasRightOperand, onClause);
		}
		
		/// <inheritdoc/>
		public override IDynamicRelation CreateDynamicRelation(IEntityFieldCore leftOperand, JoinHint joinType, string rightOperandEntityName, string aliasLeftOperand, string aliasRightOperand, IPredicate onClause)
		{
			return new DynamicRelation(leftOperand, joinType, (App.CongAnGis.Dal.EntityType)Enum.Parse(typeof(App.CongAnGis.Dal.EntityType), rightOperandEntityName, false), aliasLeftOperand, aliasRightOperand, onClause);
		}
		
		/// <inheritdoc/>
		protected override IEntityFactoryCore GetFactoryImpl(int entityTypeValue) { return EntityFactoryFactory.GetFactory((App.CongAnGis.Dal.EntityType)entityTypeValue); }

		/// <inheritdoc/>
		protected override IEntityFactoryCore GetFactoryImpl(Type typeOfEntity) { return EntityFactoryFactory.GetFactory(typeOfEntity);	}

	}
}
