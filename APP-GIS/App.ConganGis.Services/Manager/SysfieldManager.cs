
using App.CongAnGis.Services.ManagerBase;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using App.Core.Common;
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.FactoryClasses;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Services;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using App.CongAnGis.Dal.Linq;
using static App.CongAnGis.Services.Model.SysFieldModel;

namespace App.CongAnGis.Services.Manager
{
    public class SysfieldManager : SysfieldManagerBase
    {
        public SysfieldManager()
        { }
        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectAllFieldAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {

                        EntityCollection<SysfieldEntity> _Collection = SelectAllField(key);
                        List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                        return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                    });

                }
                catch (ORMException ex)
                {
                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }
        public EntityCollection<SysfieldEntity> SelectAllField(int _id)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = _Collection,
                    FilterToUse = SysfieldFields.Featureclass == _id,
                    CacheResultset = false,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            return _Collection;
        }

        public async Task<ApiResponse> InsertAsyncCustom(SysfieldVM.ItemSysfield _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                    {
                        var Metadata = new LinqMetaData(adapter);
                        var data = Metadata.Sysfeatureclass.FirstOrDefault(x => x.Id == _Model.Featureclass);
                        var tableName = data.Tablename;
                        var show = Metadata.Sysfield.Where(x => x.Featureclass == _Model.Featureclass && x.Show != 1000).OrderByDescending(x => x.Show).Take(1).ToList();
                        SysfieldEntity _SysfieldEntity = configVMtoEntity.CreateMapper().Map<SysfieldEntity>(_Model);
                        if (show.Count <= 0)
                        {
                            _SysfieldEntity.Show = 1;
                        } else
                        {
                            _SysfieldEntity.Show = show[0].Show + 1;
                        }
                        InsertCustom(_SysfieldEntity, tableName);
                        adapter.SaveEntity(_SysfieldEntity, true);
                        adapter.Commit();
                        return GeneralCode.Success;
                    }
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysfieldEntity InsertCustom(SysfieldEntity _SysfieldEntity, string tableName)
        {
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var query = string.Format(
                    "ALTER TABLE appcongangis.{0} ADD COLUMN {1} {2}",
                    tableName,
                    _SysfieldEntity.Fieldname,
                    _SysfieldEntity.Datatype);
                try
                {
                    adapter.ExecuteSQL(query);
                    adapter.Commit();
                    return _SysfieldEntity;
                }
                catch (ORMEntityValidationException ex)
                {
                    return null;
                }
            }
        }


        public async Task<ApiResponse> DeleteAsyncCustom(int id)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                    {
                        SysfieldEntity _SysfieldEntity = new SysfieldEntity(id);
                        if (adapter.FetchEntity(_SysfieldEntity))
                        {
                            var Metadata = new LinqMetaData(adapter);
                            var data = Metadata.Sysfield.FirstOrDefault(x => x.Id == id);

                            var tableName = Metadata.Sysfeatureclass.FirstOrDefault(x => x.Id == data.Featureclass).Tablename;

                            adapter.DeleteEntity(_SysfieldEntity);

                            var query = string.Format(
                                "ALTER TABLE appcongangis.{0} DROP COLUMN {1}", tableName, data.Fieldname);

                            adapter.ExecuteSQL(query);
                            adapter.Commit();


                        }
                        return GeneralCode.Success;
                    }
                });
            }
            catch (ORMEntityIsDeletedException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<FieldModel>>> GetFieldsByTableName(string tableName)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        EntityCollection<SysfieldEntity> collect = new EntityCollection<SysfieldEntity>();

                        RelationPredicateBucket filter = new RelationPredicateBucket();

                        var metaData = new LinqMetaData(adapter);
                        var fe = metaData.Sysfeatureclass.First(c => c.Tablename.Equals(tableName.ToLower(), StringComparison.CurrentCultureIgnoreCase));
                        if (fe == null)
                        {
                            return ApiResponse<IEnumerable<FieldModel>>.Generate(null, GeneralCode.Error, null);
                        }
                        adapter.FetchEntityCollection(collect, filter);

                        try
                        {
                            var data = collect.Where(x => x.Featureclass == fe.Id && x.Status == 1).Select(c => new FieldModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Datalength = c.Datalength,
                                Datatype = c.Datatype,
                                Fieldname = c.Fieldname,
                                Show = c.Show,
                                FeatureclassId = c.Featureclass,
                                Config = c.Config,
                                Status = (short?)c.Status
                            }).OrderBy(x => x.Show).ToList();
                            return ApiResponse<IEnumerable<FieldModel>>.Generate(data, GeneralCode.Success, null);
                        }
                        catch (Exception ex)
                        {
                            return ApiResponse<IEnumerable<FieldModel>>.Generate(null, GeneralCode.Error, null);
                        }

                    }
                });
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<FieldModel>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<FieldModel>>> GetFieldsForInsertOrUpdate(string tableName)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        EntityCollection<SysfieldEntity> collect = new EntityCollection<SysfieldEntity>();

                        RelationPredicateBucket filter = new RelationPredicateBucket();

                        var metaData = new LinqMetaData(adapter);
                        var fe = metaData.Sysfeatureclass.First(c => c.Tablename.ToLower().Equals(tableName.ToLower(), StringComparison.CurrentCultureIgnoreCase));

                        adapter.FetchEntityCollection(collect, filter);

                        var data = collect.Where(x => x.Featureclass == fe.Id).OrderBy(x => x.Show).Select(c => new FieldModel
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Datalength = c.Datalength,
                            Datatype = c.Datatype,
                            Fieldname = c.Fieldname,
                            Show = c.Show,
                            FeatureclassId = c.Featureclass,
                            Config = c.Config,
                            Status = (short?)c.Status
                        }).ToList();

                        return ApiResponse<IEnumerable<FieldModel>>.Generate(data, GeneralCode.Success, null);
                    }
                });
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<FieldModel>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }
}

