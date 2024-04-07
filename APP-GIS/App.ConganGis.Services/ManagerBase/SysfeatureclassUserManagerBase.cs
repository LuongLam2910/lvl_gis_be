
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
    #region SysfeatureclassUser View 

    public class SysfeatureclassUserVM
    {
        #region SysfeatureclassUser ItemSysfeatureclassUser 
        public class ItemSysfeatureclassUser
        {
            private int _Id;
            private int? _featureclassid;
            private object? _username;
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

            public int? Featureclassid
            {
                get { return _featureclassid; }
                set { _featureclassid = value; }
            }

            public object? Username
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
        public class PageSysfeatureclassUser
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region SysfeatureclassUser Function 

    public class SysfeatureclassUserManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysfeatureclassUserEntity, SysfeatureclassUserVM.ItemSysfeatureclassUser>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysfeatureclassUserVM.ItemSysfeatureclassUser, SysfeatureclassUserEntity>();
        });

        #endregion

        public SysfeatureclassUserManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysfeatureclassUserVM.ItemSysfeatureclassUser _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysfeatureclassUserEntity _SysfeatureclassUserEntity = configVMtoEntity.CreateMapper().Map<SysfeatureclassUserEntity>(_Model);
                    Insert(_SysfeatureclassUserEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysfeatureclassUserEntity Insert(SysfeatureclassUserEntity _SysfeatureclassUserEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysfeatureclassUserEntity, true);
                }
                return _SysfeatureclassUserEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysfeatureclassUserVM.ItemSysfeatureclassUser _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysfeatureclassUserEntity _SysfeatureclassUserEntity = configVMtoEntity.CreateMapper().Map<SysfeatureclassUserEntity>(_Model);
                    Update(_SysfeatureclassUserEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysfeatureclassUserEntity _SysfeatureclassUserEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysfeatureclassUserFields.Id == _SysfeatureclassUserEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysfeatureclassUserEntity, filter);
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
                    SysfeatureclassUserEntity _SysfeatureclassUserEntity = new SysfeatureclassUserEntity(id);
                    if (adapter.FetchEntity(_SysfeatureclassUserEntity))
                    {
                        adapter.DeleteEntity(_SysfeatureclassUserEntity);
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
                        SysfeatureclassUserEntity _SysfeatureclassUserEntity = new SysfeatureclassUserEntity(id);
                        if (adapter.FetchEntity(_SysfeatureclassUserEntity))
                        {
                            adapter.DeleteEntity(_SysfeatureclassUserEntity);
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

        public SysfeatureclassUserEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysfeatureclassUserEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysfeatureclassUserFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysfeatureclassUserEntity());
        }

        public async Task<ApiResponse<SysfeatureclassUserVM.ItemSysfeatureclassUser>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysfeatureclassUserEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysfeatureclassUserVM.ItemSysfeatureclassUser toReturn = mapper.Map<SysfeatureclassUserVM.ItemSysfeatureclassUser>(_entity);
                        return ApiResponse<SysfeatureclassUserVM.ItemSysfeatureclassUser>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysfeatureclassUserVM.ItemSysfeatureclassUser>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysfeatureclassUserEntity> _Collection = SelectAll();
                    List<SysfeatureclassUserVM.ItemSysfeatureclassUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassUserEntity, SysfeatureclassUserVM.ItemSysfeatureclassUser>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysfeatureclassUserEntity> SelectAll()
        {
            EntityCollection<SysfeatureclassUserEntity> _Collection = new EntityCollection<SysfeatureclassUserEntity>(new SysfeatureclassUserEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> SelectByfeatureclassidAsync(int? _featureclassid)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassUserEntity> _Collection = SelectByfeatureclassid(_featureclassid);
                    List<SysfeatureclassUserVM.ItemSysfeatureclassUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassUserEntity, SysfeatureclassUserVM.ItemSysfeatureclassUser>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> SelectByusernameAsync(object? _username)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassUserEntity> _Collection = SelectByusername(_username);
                    List<SysfeatureclassUserVM.ItemSysfeatureclassUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassUserEntity, SysfeatureclassUserVM.ItemSysfeatureclassUser>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> SelectByconfigAsync(int? _config)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassUserEntity> _Collection = SelectByconfig(_config);
                    List<SysfeatureclassUserVM.ItemSysfeatureclassUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassUserEntity, SysfeatureclassUserVM.ItemSysfeatureclassUser>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> SelectByusercreatedAsync(int? _usercreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassUserEntity> _Collection = SelectByusercreated(_usercreated);
                    List<SysfeatureclassUserVM.ItemSysfeatureclassUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassUserEntity, SysfeatureclassUserVM.ItemSysfeatureclassUser>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> SelectBydatecreatedAsync(DateTime? _datecreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassUserEntity> _Collection = SelectBydatecreated(_datecreated);
                    List<SysfeatureclassUserVM.ItemSysfeatureclassUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassUserEntity, SysfeatureclassUserVM.ItemSysfeatureclassUser>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> SelectBydatemodifiedAsync(DateTime? _datemodified)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassUserEntity> _Collection = SelectBydatemodified(_datemodified);
                    List<SysfeatureclassUserVM.ItemSysfeatureclassUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassUserEntity, SysfeatureclassUserVM.ItemSysfeatureclassUser>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> SelectBygroupidAsync(int? _groupid)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassUserEntity> _Collection = SelectBygroupid(_groupid);
                    List<SysfeatureclassUserVM.ItemSysfeatureclassUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassUserEntity, SysfeatureclassUserVM.ItemSysfeatureclassUser>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> SelectBypermissionAsync(string _permission)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassUserEntity> _Collection = SelectBypermission(_permission);
                    List<SysfeatureclassUserVM.ItemSysfeatureclassUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassUserEntity, SysfeatureclassUserVM.ItemSysfeatureclassUser>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysfeatureclassUserEntity> SelectByfeatureclassid(int? _featureclassid)
        {
            EntityCollection<SysfeatureclassUserEntity> _Collection = new EntityCollection<SysfeatureclassUserEntity>(new SysfeatureclassUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassUserFields.Featureclassid == _featureclassid);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassUserEntity> SelectByusername(object? _username)
        {
            EntityCollection<SysfeatureclassUserEntity> _Collection = new EntityCollection<SysfeatureclassUserEntity>(new SysfeatureclassUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassUserFields.Username == _username);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassUserEntity> SelectByconfig(int? _config)
        {
            EntityCollection<SysfeatureclassUserEntity> _Collection = new EntityCollection<SysfeatureclassUserEntity>(new SysfeatureclassUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassUserFields.Config == _config);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassUserEntity> SelectByusercreated(int? _usercreated)
        {
            EntityCollection<SysfeatureclassUserEntity> _Collection = new EntityCollection<SysfeatureclassUserEntity>(new SysfeatureclassUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassUserFields.Usercreated == _usercreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassUserEntity> SelectBydatecreated(DateTime? _datecreated)
        {
            EntityCollection<SysfeatureclassUserEntity> _Collection = new EntityCollection<SysfeatureclassUserEntity>(new SysfeatureclassUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassUserFields.Datecreated == _datecreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassUserEntity> SelectBydatemodified(DateTime? _datemodified)
        {
            EntityCollection<SysfeatureclassUserEntity> _Collection = new EntityCollection<SysfeatureclassUserEntity>(new SysfeatureclassUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassUserFields.Datemodified == _datemodified);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassUserEntity> SelectBygroupid(int? _groupid)
        {
            EntityCollection<SysfeatureclassUserEntity> _Collection = new EntityCollection<SysfeatureclassUserEntity>(new SysfeatureclassUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassUserFields.Groupid == _groupid);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassUserEntity> SelectBypermission(string _permission)
        {
            EntityCollection<SysfeatureclassUserEntity> _Collection = new EntityCollection<SysfeatureclassUserEntity>(new SysfeatureclassUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassUserFields.Permission == _permission);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysfeatureclassUserVM.ItemSysfeatureclassUser>>>> SelectPaging(SysfeatureclassUserVM.PageSysfeatureclassUser _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysfeatureclassUserFields.Featureclassid).Like(_model.strKey)
                            .Or(SysfeatureclassUserFields.Permission.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysfeatureclassUserEntity>();
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
                        SortExpression sort = new SortExpression(SysfeatureclassUserFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysfeatureclassUserVM.ItemSysfeatureclassUser>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysfeatureclassUserVM.ItemSysfeatureclassUser>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassUserEntity, SysfeatureclassUserVM.ItemSysfeatureclassUser>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysfeatureclassUserVM.ItemSysfeatureclassUser>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassUserEntityFactory());
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


        public DataTable SelectByfeatureclassidRDT(int? _featureclassid)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassUserFields.Featureclassid == _featureclassid);
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


        public DataTable SelectByusernameRDT(object? _username)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassUserFields.Username == _username);
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassUserFields.Config == _config);
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassUserFields.Usercreated == _usercreated);
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassUserFields.Datecreated == _datecreated);
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassUserFields.Datemodified == _datemodified);
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassUserFields.Groupid == _groupid);
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassUserFields.Permission == _permission);
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

