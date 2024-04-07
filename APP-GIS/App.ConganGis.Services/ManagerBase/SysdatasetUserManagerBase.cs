
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
    #region SysdatasetUser View 

    public class SysdatasetUserVM
    {
        #region SysdatasetUser ItemSysdatasetUser 
        public class ItemSysdatasetUser
        {
            private int _Id;
            private int? _datasetid;
            private string _username = string.Empty;
            private string _config = string.Empty;
            private int? _usercreated;
            private DateTime? _datecreated;
            private DateTime? _datemodified;
            private int? _groupid;
            private string _permission = string.Empty;

            public int Id //	#region PrimaryKey
            {
                get { return _Id; }
                set { _Id = value; }
            }

            public int? Datasetid
            {
                get { return _datasetid; }
                set { _datasetid = value; }
            }

            public string Username
            {
                get { return _username; }
                set { _username = value; }
            }

            public string Config
            {
                get { return _config; }
                set { _config = value; }
            }

            public int? Usercreated
            {
                get { return _usercreated; }
                set { _usercreated = value; }
            }

            public DateTime? Datecreated
            {
                get { return _datecreated; }
                set { _datecreated = value; }
            }

            public DateTime? Datemodified
            {
                get { return _datemodified; }
                set { _datemodified = value; }
            }

            public int? Groupid
            {
                get { return _groupid; }
                set { _groupid = value; }
            }

            public string Permission
            {
                get { return _permission; }
                set { _permission = value; }
            }
        }

        #endregion
        public class PageSysdatasetUser
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region SysdatasetUser Function 

    public class SysdatasetUserManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysdatasetUserEntity, SysdatasetUserVM.ItemSysdatasetUser>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysdatasetUserVM.ItemSysdatasetUser, SysdatasetUserEntity>();
        });

        #endregion

        public SysdatasetUserManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysdatasetUserVM.ItemSysdatasetUser _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysdatasetUserEntity _SysdatasetUserEntity = configVMtoEntity.CreateMapper().Map<SysdatasetUserEntity>(_Model);
                    Insert(_SysdatasetUserEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysdatasetUserEntity Insert(SysdatasetUserEntity _SysdatasetUserEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysdatasetUserEntity, true);
                }
                return _SysdatasetUserEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysdatasetUserVM.ItemSysdatasetUser _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysdatasetUserEntity _SysdatasetUserEntity = configVMtoEntity.CreateMapper().Map<SysdatasetUserEntity>(_Model);
                    Update(_SysdatasetUserEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysdatasetUserEntity _SysdatasetUserEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysdatasetUserFields.Id == _SysdatasetUserEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysdatasetUserEntity, filter);
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
                    SysdatasetUserEntity _SysdatasetUserEntity = new SysdatasetUserEntity(id);
                    if (adapter.FetchEntity(_SysdatasetUserEntity))
                    {
                        adapter.DeleteEntity(_SysdatasetUserEntity);
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
                        SysdatasetUserEntity _SysdatasetUserEntity = new SysdatasetUserEntity(id);
                        if (adapter.FetchEntity(_SysdatasetUserEntity))
                        {
                            adapter.DeleteEntity(_SysdatasetUserEntity);
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

        public SysdatasetUserEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysdatasetUserEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysdatasetUserFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysdatasetUserEntity());
        }

        public async Task<ApiResponse<SysdatasetUserVM.ItemSysdatasetUser>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysdatasetUserEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysdatasetUserVM.ItemSysdatasetUser toReturn = mapper.Map<SysdatasetUserVM.ItemSysdatasetUser>(_entity);
                        return ApiResponse<SysdatasetUserVM.ItemSysdatasetUser>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysdatasetUserVM.ItemSysdatasetUser>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysdatasetUserEntity> _Collection = SelectAll();
                    List<SysdatasetUserVM.ItemSysdatasetUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetUserEntity, SysdatasetUserVM.ItemSysdatasetUser>(c)).ToList();

                    return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysdatasetUserEntity> SelectAll()
        {
            EntityCollection<SysdatasetUserEntity> _Collection = new EntityCollection<SysdatasetUserEntity>(new SysdatasetUserEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> SelectBydatasetidAsync(int? _datasetid)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetUserEntity> _Collection = SelectBydatasetid(_datasetid);
                    List<SysdatasetUserVM.ItemSysdatasetUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetUserEntity, SysdatasetUserVM.ItemSysdatasetUser>(c)).ToList();

                    return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> SelectByusernameAsync(string _username)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetUserEntity> _Collection = SelectByusername(_username);
                    List<SysdatasetUserVM.ItemSysdatasetUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetUserEntity, SysdatasetUserVM.ItemSysdatasetUser>(c)).ToList();

                    return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> SelectByconfigAsync(int? _config)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetUserEntity> _Collection = SelectByconfig(_config);
                    List<SysdatasetUserVM.ItemSysdatasetUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetUserEntity, SysdatasetUserVM.ItemSysdatasetUser>(c)).ToList();

                    return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> SelectByusercreatedAsync(int? _usercreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetUserEntity> _Collection = SelectByusercreated(_usercreated);
                    List<SysdatasetUserVM.ItemSysdatasetUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetUserEntity, SysdatasetUserVM.ItemSysdatasetUser>(c)).ToList();

                    return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> SelectBydatecreatedAsync(DateTime? _datecreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetUserEntity> _Collection = SelectBydatecreated(_datecreated);
                    List<SysdatasetUserVM.ItemSysdatasetUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetUserEntity, SysdatasetUserVM.ItemSysdatasetUser>(c)).ToList();

                    return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> SelectBydatemodifiedAsync(DateTime? _datemodified)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetUserEntity> _Collection = SelectBydatemodified(_datemodified);
                    List<SysdatasetUserVM.ItemSysdatasetUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetUserEntity, SysdatasetUserVM.ItemSysdatasetUser>(c)).ToList();

                    return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> SelectBygroupidAsync(int? _groupid)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetUserEntity> _Collection = SelectBygroupid(_groupid);
                    List<SysdatasetUserVM.ItemSysdatasetUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetUserEntity, SysdatasetUserVM.ItemSysdatasetUser>(c)).ToList();

                    return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> SelectBypermissionAsync(string _permission)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetUserEntity> _Collection = SelectBypermission(_permission);
                    List<SysdatasetUserVM.ItemSysdatasetUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetUserEntity, SysdatasetUserVM.ItemSysdatasetUser>(c)).ToList();

                    return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysdatasetUserEntity> SelectBydatasetid(int? _datasetid)
        {
            EntityCollection<SysdatasetUserEntity> _Collection = new EntityCollection<SysdatasetUserEntity>(new SysdatasetUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetUserFields.Datasetid == _datasetid);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdatasetUserEntity> SelectByusername(string _username)
        {
            EntityCollection<SysdatasetUserEntity> _Collection = new EntityCollection<SysdatasetUserEntity>(new SysdatasetUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetUserFields.Username == _username);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdatasetUserEntity> SelectByconfig(int? _config)
        {
            EntityCollection<SysdatasetUserEntity> _Collection = new EntityCollection<SysdatasetUserEntity>(new SysdatasetUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetUserFields.Config == _config);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdatasetUserEntity> SelectByusercreated(int? _usercreated)
        {
            EntityCollection<SysdatasetUserEntity> _Collection = new EntityCollection<SysdatasetUserEntity>(new SysdatasetUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetUserFields.Usercreated == _usercreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdatasetUserEntity> SelectBydatecreated(DateTime? _datecreated)
        {
            EntityCollection<SysdatasetUserEntity> _Collection = new EntityCollection<SysdatasetUserEntity>(new SysdatasetUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetUserFields.Datecreated == _datecreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdatasetUserEntity> SelectBydatemodified(DateTime? _datemodified)
        {
            EntityCollection<SysdatasetUserEntity> _Collection = new EntityCollection<SysdatasetUserEntity>(new SysdatasetUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetUserFields.Datemodified == _datemodified);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdatasetUserEntity> SelectBygroupid(int? _groupid)
        {
            EntityCollection<SysdatasetUserEntity> _Collection = new EntityCollection<SysdatasetUserEntity>(new SysdatasetUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetUserFields.Groupid == _groupid);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdatasetUserEntity> SelectBypermission(string _permission)
        {
            EntityCollection<SysdatasetUserEntity> _Collection = new EntityCollection<SysdatasetUserEntity>(new SysdatasetUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetUserFields.Permission == _permission);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysdatasetUserVM.ItemSysdatasetUser>>>> SelectPaging(SysdatasetUserVM.PageSysdatasetUser _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysdatasetUserFields.Datasetid).Like(_model.strKey)
                            .Or(SysdatasetUserFields.Username.Like(_model.strKey)).Or(SysdatasetUserFields.Permission.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysdatasetUserEntity>();
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
                        SortExpression sort = new SortExpression(SysdatasetUserFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysdatasetUserVM.ItemSysdatasetUser>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysdatasetUserVM.ItemSysdatasetUser>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetUserEntity, SysdatasetUserVM.ItemSysdatasetUser>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysdatasetUserVM.ItemSysdatasetUser>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdatasetUserEntityFactory());
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


        public DataTable SelectBydatasetidRDT(int? _datasetid)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdatasetUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetUserFields.Datasetid == _datasetid);
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


        public DataTable SelectByusernameRDT(string _username)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdatasetUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetUserFields.Username == _username);
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


        public DataTable SelectByconfigRDT(int? _config)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdatasetUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetUserFields.Config == _config);
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


        public DataTable SelectByusercreatedRDT(int? _usercreated)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdatasetUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetUserFields.Usercreated == _usercreated);
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


        public DataTable SelectBydatecreatedRDT(DateTime? _datecreated)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdatasetUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetUserFields.Datecreated == _datecreated);
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


        public DataTable SelectBydatemodifiedRDT(DateTime? _datemodified)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdatasetUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetUserFields.Datemodified == _datemodified);
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


        public DataTable SelectBygroupidRDT(int? _groupid)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdatasetUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetUserFields.Groupid == _groupid);
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


        public DataTable SelectBypermissionRDT(string _permission)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdatasetUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetUserFields.Permission == _permission);
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

