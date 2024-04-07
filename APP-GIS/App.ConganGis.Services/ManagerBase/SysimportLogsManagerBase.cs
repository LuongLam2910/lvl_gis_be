
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



namespace App.CongAnGis.Services.ManagerBase
{
    #region SysimportLogs View 

    public class SysimportLogsVM
    {
        #region SysimportLogs ItemSysimportLogs 
        public class ItemSysimportLogs
        {
            private int _Id;
            private DateTime? _starttime;
            private DateTime? _endtime;
            private int? _status;
            private int? _count;
            private string _message = string.Empty;
            private string _config = string.Empty;
            private string _tablename = string.Empty;
            private string _unitcode = string.Empty;

            public int Id //	#region PrimaryKey
            {
                get { return _Id; }
                set { _Id = value; }
            }

            public DateTime? Starttime
            {
                get { return _starttime; }
                set { _starttime = value; }
            }

            public DateTime? Endtime
            {
                get { return _endtime; }
                set { _endtime = value; }
            }

            public int? Status
            {
                get { return _status; }
                set { _status = value; }
            }

            public int? Count
            {
                get { return _count; }
                set { _count = value; }
            }

            public string Message
            {
                get { return _message; }
                set { _message = value; }
            }

            public string Config
            {
                get { return _config; }
                set { _config = value; }
            }

            public string Tablename
            {
                get { return _tablename; }
                set { _tablename = value; }
            }

            public string Unitcode
            {
                get { return _unitcode; }
                set { _unitcode = value; }
            }
        }

        #endregion
        public class PageSysimportLogs
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region SysimportLogs Function 

    public class SysimportLogsManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysimportLogEntity, SysimportLogsVM.ItemSysimportLogs>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysimportLogsVM.ItemSysimportLogs, SysimportLogEntity>();
        });

        #endregion

        public SysimportLogsManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysimportLogsVM.ItemSysimportLogs _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysimportLogEntity _SysimportLogEntity = configVMtoEntity.CreateMapper().Map<SysimportLogEntity>(_Model);
                    Insert(_SysimportLogEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysimportLogEntity Insert(SysimportLogEntity _SysimportLogEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysimportLogEntity, true);
                }
                return _SysimportLogEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysimportLogsVM.ItemSysimportLogs _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysimportLogEntity _SysimportLogEntity = configVMtoEntity.CreateMapper().Map<SysimportLogEntity>(_Model);
                    Update(_SysimportLogEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysimportLogEntity _SysimportLogEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysimportLogFields.Id == _SysimportLogEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysimportLogEntity, filter);
                    toReturn = true;
                }
                return toReturn;
            }
            catch (ORMEntityValidationException ex)
            {
                return toReturn;
            }
        }

        #endregion

        #region Delete

        public bool Delete(int id)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    SysimportLogEntity _SysimportLogEntity = new SysimportLogEntity(id);
                    if (adapter.FetchEntity(_SysimportLogEntity))
                    {
                        adapter.DeleteEntity(_SysimportLogEntity);
                        toReturn = true;
                    }
                }
                return toReturn;
            }
            catch (ORMEntityValidationException ex)
            {
                return toReturn;
            }
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                    {
                        SysimportLogEntity _SysimportLogEntity = new SysimportLogEntity(id);
                        if (adapter.FetchEntity(_SysimportLogEntity))
                        {
                            adapter.DeleteEntity(_SysimportLogEntity);
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
        #endregion

        #region SelectOne Return Entity

        public SysimportLogEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysimportLogEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysimportLogFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysimportLogEntity());
        }

        public async Task<ApiResponse<SysimportLogsVM.ItemSysimportLogs>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysimportLogEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysimportLogsVM.ItemSysimportLogs toReturn = mapper.Map<SysimportLogsVM.ItemSysimportLogs>(_entity);
                        return ApiResponse<SysimportLogsVM.ItemSysimportLogs>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysimportLogsVM.ItemSysimportLogs>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysimportLogEntity> _Collection = SelectAll();
                    List<SysimportLogsVM.ItemSysimportLogs> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysimportLogEntity, SysimportLogsVM.ItemSysimportLogs>(c)).ToList();

                    return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysimportLogEntity> SelectAll()
        {
            EntityCollection<SysimportLogEntity> _Collection = new EntityCollection<SysimportLogEntity>(new SysimportLogEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> SelectBystarttimeAsync(DateTime? _starttime)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysimportLogEntity> _Collection = SelectBystarttime(_starttime);
                    List<SysimportLogsVM.ItemSysimportLogs> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysimportLogEntity, SysimportLogsVM.ItemSysimportLogs>(c)).ToList();

                    return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> SelectByendtimeAsync(DateTime? _endtime)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysimportLogEntity> _Collection = SelectByendtime(_endtime);
                    List<SysimportLogsVM.ItemSysimportLogs> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysimportLogEntity, SysimportLogsVM.ItemSysimportLogs>(c)).ToList();

                    return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> SelectBystatusAsync(int? _status)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysimportLogEntity> _Collection = SelectBystatus(_status);
                    List<SysimportLogsVM.ItemSysimportLogs> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysimportLogEntity, SysimportLogsVM.ItemSysimportLogs>(c)).ToList();

                    return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> SelectBycountAsync(int? _count)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysimportLogEntity> _Collection = SelectBycount(_count);
                    List<SysimportLogsVM.ItemSysimportLogs> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysimportLogEntity, SysimportLogsVM.ItemSysimportLogs>(c)).ToList();

                    return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> SelectBymessageAsync(string _message)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysimportLogEntity> _Collection = SelectBymessage(_message);
                    List<SysimportLogsVM.ItemSysimportLogs> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysimportLogEntity, SysimportLogsVM.ItemSysimportLogs>(c)).ToList();

                    return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> SelectByconfigAsync(string _config)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysimportLogEntity> _Collection = SelectByconfig(_config);
                    List<SysimportLogsVM.ItemSysimportLogs> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysimportLogEntity, SysimportLogsVM.ItemSysimportLogs>(c)).ToList();

                    return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> SelectBytablenameAsync(string _tablename)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysimportLogEntity> _Collection = SelectBytablename(_tablename);
                    List<SysimportLogsVM.ItemSysimportLogs> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysimportLogEntity, SysimportLogsVM.ItemSysimportLogs>(c)).ToList();

                    return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> SelectByunitcodeAsync(string _unitcode)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysimportLogEntity> _Collection = SelectByunitcode(_unitcode);
                    List<SysimportLogsVM.ItemSysimportLogs> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysimportLogEntity, SysimportLogsVM.ItemSysimportLogs>(c)).ToList();

                    return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysimportLogEntity> SelectBystarttime(DateTime? _starttime)
        {
            EntityCollection<SysimportLogEntity> _Collection = new EntityCollection<SysimportLogEntity>(new SysimportLogEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysimportLogFields.Starttime == _starttime);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysimportLogEntity> SelectByendtime(DateTime? _endtime)
        {
            EntityCollection<SysimportLogEntity> _Collection = new EntityCollection<SysimportLogEntity>(new SysimportLogEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysimportLogFields.Endtime == _endtime);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysimportLogEntity> SelectBystatus(int? _status)
        {
            EntityCollection<SysimportLogEntity> _Collection = new EntityCollection<SysimportLogEntity>(new SysimportLogEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysimportLogFields.Status == _status);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysimportLogEntity> SelectBycount(int? _count)
        {
            EntityCollection<SysimportLogEntity> _Collection = new EntityCollection<SysimportLogEntity>(new SysimportLogEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysimportLogFields.Count == _count);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysimportLogEntity> SelectBymessage(string _message)
        {
            EntityCollection<SysimportLogEntity> _Collection = new EntityCollection<SysimportLogEntity>(new SysimportLogEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysimportLogFields.Message == _message);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysimportLogEntity> SelectByconfig(string _config)
        {
            EntityCollection<SysimportLogEntity> _Collection = new EntityCollection<SysimportLogEntity>(new SysimportLogEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysimportLogFields.Config == _config);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysimportLogEntity> SelectBytablename(string _tablename)
        {
            EntityCollection<SysimportLogEntity> _Collection = new EntityCollection<SysimportLogEntity>(new SysimportLogEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysimportLogFields.Tablename == _tablename);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysimportLogEntity> SelectByunitcode(string _unitcode)
        {
            EntityCollection<SysimportLogEntity> _Collection = new EntityCollection<SysimportLogEntity>(new SysimportLogEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysimportLogFields.Unitcode == _unitcode);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysimportLogsVM.ItemSysimportLogs>>>> SelectPaging(SysimportLogsVM.PageSysimportLogs _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysimportLogFields.Starttime).Like(_model.strKey)
                            .Or(SysimportLogFields.Message.Like(_model.strKey)).Or(SysimportLogFields.Config.Like(_model.strKey)).Or(SysimportLogFields.Tablename.Like(_model.strKey)).Or(SysimportLogFields.Unitcode.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysimportLogEntity>();
                        int totalPagesCount = 0;
                        int totalRecord = (int)adapter.GetDbCount(data, null);
                        int recordsCount = (int)adapter.GetDbCount(data, filter, null);
                        if (recordsCount <= _model.pageSize)
                        {
                            totalPagesCount = 1;
                            _model.currentPage = totalPagesCount;
                        }
                        else
                        {
                            int remainder = 0;
                            totalPagesCount = Math.DivRem(recordsCount, _model.pageSize, out remainder);
                            if (remainder > 0)
                            {
                                totalPagesCount++;
                            }
                        }
                        SortExpression sort = new SortExpression(SysimportLogFields.Id | SortOperator.Descending);
                        try
                        {
                            var parameters = new QueryParameters(_model.currentPage, _model.pageSize, _model.pageSize, filter)
                            {
                                CollectionToFetch = data,
                                CacheResultset = true,
                                SorterToUse = sort,
                                CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                            };
                            adapter.FetchEntityCollection(parameters);
                            //adapter.FetchEntityCollection(data, filter, 0, sort, null, _model.currentPage, _model.pageSize);
                        }
                        catch (Exception ex)
                        {
                            return ApiResponse<PageModelView<IEnumerable<SysimportLogsVM.ItemSysimportLogs>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysimportLogsVM.ItemSysimportLogs>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysimportLogEntity, SysimportLogsVM.ItemSysimportLogs>(c)).ToList(),
                            CurrentPage = _model.currentPage,
                            PageSize = _model.pageSize,
                            TotalPage = totalPagesCount,
                            TotalRecord = totalRecord,
                            RecordsCount = recordsCount
                        };
                    }
                });
            }
            catch (ORMEntityValidationException ex)
            {
                return ApiResponse<PageModelView<IEnumerable<SysimportLogsVM.ItemSysimportLogs>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysimportLogEntityFactory());
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    adapter.FetchTypedList(_Collection.EntityFactoryToUse.CreateFields(), toReturn, null, true);
                }
                return toReturn;

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataTable SelectBystarttimeRDT(DateTime? _starttime)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysimportLogEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysimportLogFields.Starttime == _starttime);
                filter.PredicateExpression.Add(_PredicateExpression);

                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    adapter.FetchTypedList(_Collection.EntityFactoryToUse.CreateFields(), toReturn, filter, true);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataTable SelectByendtimeRDT(DateTime? _endtime)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysimportLogEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysimportLogFields.Endtime == _endtime);
                filter.PredicateExpression.Add(_PredicateExpression);

                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    adapter.FetchTypedList(_Collection.EntityFactoryToUse.CreateFields(), toReturn, filter, true);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataTable SelectBystatusRDT(int? _status)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysimportLogEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysimportLogFields.Status == _status);
                filter.PredicateExpression.Add(_PredicateExpression);

                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    adapter.FetchTypedList(_Collection.EntityFactoryToUse.CreateFields(), toReturn, filter, true);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataTable SelectBycountRDT(int? _count)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysimportLogEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysimportLogFields.Count == _count);
                filter.PredicateExpression.Add(_PredicateExpression);

                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    adapter.FetchTypedList(_Collection.EntityFactoryToUse.CreateFields(), toReturn, filter, true);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataTable SelectBymessageRDT(string _message)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysimportLogEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysimportLogFields.Message == _message);
                filter.PredicateExpression.Add(_PredicateExpression);

                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    adapter.FetchTypedList(_Collection.EntityFactoryToUse.CreateFields(), toReturn, filter, true);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataTable SelectByconfigRDT(string _config)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysimportLogEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysimportLogFields.Config == _config);
                filter.PredicateExpression.Add(_PredicateExpression);

                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    adapter.FetchTypedList(_Collection.EntityFactoryToUse.CreateFields(), toReturn, filter, true);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataTable SelectBytablenameRDT(string _tablename)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysimportLogEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysimportLogFields.Tablename == _tablename);
                filter.PredicateExpression.Add(_PredicateExpression);

                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    adapter.FetchTypedList(_Collection.EntityFactoryToUse.CreateFields(), toReturn, filter, true);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataTable SelectByunitcodeRDT(string _unitcode)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysimportLogEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysimportLogFields.Unitcode == _unitcode);
                filter.PredicateExpression.Add(_PredicateExpression);

                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    adapter.FetchTypedList(_Collection.EntityFactoryToUse.CreateFields(), toReturn, filter, true);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #endregion

    }
}

