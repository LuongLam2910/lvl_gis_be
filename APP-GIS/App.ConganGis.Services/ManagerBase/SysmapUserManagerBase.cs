
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
    #region SysmapUser View 

    public class SysmapUserVM
    {
        #region SysmapUser ItemSysmapUser 
        public class ItemSysmapUser
        {
            private int _Id;
            private int? _mapid;
            private string _username = string.Empty;
            private string _config = string.Empty;
            private int? _usercreated;
            private DateTime? _datecreated;
            private DateTime? _datemodified;
            private int? _groupid;
            private string _permission = string.Empty;
            private int? _userid;

            public int Id //	#region PrimaryKey
            {
                get { return _Id; }
                set { _Id = value; }
            }

            public int? Mapid
            {
                get { return _mapid; }
                set { _mapid = value; }
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

            public int? Userid
            {
                get { return _userid; }
                set { _userid = value; }
            }
        }

        #endregion
        public class PageSysmapUser
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region SysmapUser Function 

    public class SysmapUserManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysmapUserEntity, SysmapUserVM.ItemSysmapUser>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysmapUserVM.ItemSysmapUser, SysmapUserEntity>();
        });

        #endregion

        public SysmapUserManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysmapUserVM.ItemSysmapUser _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysmapUserEntity _SysmapUserEntity = configVMtoEntity.CreateMapper().Map<SysmapUserEntity>(_Model);
                    Insert(_SysmapUserEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysmapUserEntity Insert(SysmapUserEntity _SysmapUserEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysmapUserEntity, true);
                }
                return _SysmapUserEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysmapUserVM.ItemSysmapUser _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysmapUserEntity _SysmapUserEntity = configVMtoEntity.CreateMapper().Map<SysmapUserEntity>(_Model);
                    Update(_SysmapUserEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysmapUserEntity _SysmapUserEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysmapUserFields.Id == _SysmapUserEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysmapUserEntity, filter);
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
                    SysmapUserEntity _SysmapUserEntity = new SysmapUserEntity(id);
                    if (adapter.FetchEntity(_SysmapUserEntity))
                    {
                        adapter.DeleteEntity(_SysmapUserEntity);
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
                        SysmapUserEntity _SysmapUserEntity = new SysmapUserEntity(id);
                        if (adapter.FetchEntity(_SysmapUserEntity))
                        {
                            adapter.DeleteEntity(_SysmapUserEntity);
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

        public SysmapUserEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysmapUserEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysmapUserFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysmapUserEntity());
        }

        public async Task<ApiResponse<SysmapUserVM.ItemSysmapUser>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysmapUserEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysmapUserVM.ItemSysmapUser toReturn = mapper.Map<SysmapUserVM.ItemSysmapUser>(_entity);
                        return ApiResponse<SysmapUserVM.ItemSysmapUser>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysmapUserVM.ItemSysmapUser>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysmapUserEntity> _Collection = SelectAll();
                    List<SysmapUserVM.ItemSysmapUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapUserEntity, SysmapUserVM.ItemSysmapUser>(c)).ToList();

                    return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysmapUserEntity> SelectAll()
        {
            EntityCollection<SysmapUserEntity> _Collection = new EntityCollection<SysmapUserEntity>(new SysmapUserEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> SelectBymapidAsync(int? _mapid)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapUserEntity> _Collection = SelectBymapid(_mapid);
                    List<SysmapUserVM.ItemSysmapUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapUserEntity, SysmapUserVM.ItemSysmapUser>(c)).ToList();

                    return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> SelectByusernameAsync(string _username)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapUserEntity> _Collection = SelectByusername(_username);
                    List<SysmapUserVM.ItemSysmapUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapUserEntity, SysmapUserVM.ItemSysmapUser>(c)).ToList();

                    return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> SelectByconfigAsync(int? _config)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapUserEntity> _Collection = SelectByconfig(_config);
                    List<SysmapUserVM.ItemSysmapUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapUserEntity, SysmapUserVM.ItemSysmapUser>(c)).ToList();

                    return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> SelectByusercreatedAsync(int? _usercreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapUserEntity> _Collection = SelectByusercreated(_usercreated);
                    List<SysmapUserVM.ItemSysmapUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapUserEntity, SysmapUserVM.ItemSysmapUser>(c)).ToList();

                    return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> SelectBydatecreatedAsync(DateTime? _datecreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapUserEntity> _Collection = SelectBydatecreated(_datecreated);
                    List<SysmapUserVM.ItemSysmapUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapUserEntity, SysmapUserVM.ItemSysmapUser>(c)).ToList();

                    return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> SelectBydatemodifiedAsync(DateTime? _datemodified)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapUserEntity> _Collection = SelectBydatemodified(_datemodified);
                    List<SysmapUserVM.ItemSysmapUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapUserEntity, SysmapUserVM.ItemSysmapUser>(c)).ToList();

                    return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> SelectBygroupidAsync(int? _groupid)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapUserEntity> _Collection = SelectBygroupid(_groupid);
                    List<SysmapUserVM.ItemSysmapUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapUserEntity, SysmapUserVM.ItemSysmapUser>(c)).ToList();

                    return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> SelectBypermissionAsync(string _permission)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapUserEntity> _Collection = SelectBypermission(_permission);
                    List<SysmapUserVM.ItemSysmapUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapUserEntity, SysmapUserVM.ItemSysmapUser>(c)).ToList();

                    return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> SelectByuseridAsync(int? _userid)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapUserEntity> _Collection = SelectByuserid(_userid);
                    List<SysmapUserVM.ItemSysmapUser> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapUserEntity, SysmapUserVM.ItemSysmapUser>(c)).ToList();

                    return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapUserVM.ItemSysmapUser>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysmapUserEntity> SelectBymapid(int? _mapid)
        {
            EntityCollection<SysmapUserEntity> _Collection = new EntityCollection<SysmapUserEntity>(new SysmapUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapUserFields.Mapid == _mapid);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapUserEntity> SelectByusername(string _username)
        {
            EntityCollection<SysmapUserEntity> _Collection = new EntityCollection<SysmapUserEntity>(new SysmapUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapUserFields.Username == _username);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapUserEntity> SelectByconfig(int? _config)
        {
            EntityCollection<SysmapUserEntity> _Collection = new EntityCollection<SysmapUserEntity>(new SysmapUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapUserFields.Config == _config);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapUserEntity> SelectByusercreated(int? _usercreated)
        {
            EntityCollection<SysmapUserEntity> _Collection = new EntityCollection<SysmapUserEntity>(new SysmapUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapUserFields.Usercreated == _usercreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapUserEntity> SelectBydatecreated(DateTime? _datecreated)
        {
            EntityCollection<SysmapUserEntity> _Collection = new EntityCollection<SysmapUserEntity>(new SysmapUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapUserFields.Datecreated == _datecreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapUserEntity> SelectBydatemodified(DateTime? _datemodified)
        {
            EntityCollection<SysmapUserEntity> _Collection = new EntityCollection<SysmapUserEntity>(new SysmapUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapUserFields.Datemodified == _datemodified);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapUserEntity> SelectBygroupid(int? _groupid)
        {
            EntityCollection<SysmapUserEntity> _Collection = new EntityCollection<SysmapUserEntity>(new SysmapUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapUserFields.Groupid == _groupid);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapUserEntity> SelectBypermission(string _permission)
        {
            EntityCollection<SysmapUserEntity> _Collection = new EntityCollection<SysmapUserEntity>(new SysmapUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapUserFields.Permission == _permission);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapUserEntity> SelectByuserid(int? _userid)
        {
            EntityCollection<SysmapUserEntity> _Collection = new EntityCollection<SysmapUserEntity>(new SysmapUserEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapUserFields.Userid == _userid);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysmapUserVM.ItemSysmapUser>>>> SelectPaging(SysmapUserVM.PageSysmapUser _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysmapUserFields.Mapid).Like(_model.strKey)
                            .Or(SysmapUserFields.Username.Like(_model.strKey)).Or(SysmapUserFields.Permission.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysmapUserEntity>();
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
                        SortExpression sort = new SortExpression(SysmapUserFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysmapUserVM.ItemSysmapUser>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysmapUserVM.ItemSysmapUser>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysmapUserEntity, SysmapUserVM.ItemSysmapUser>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysmapUserVM.ItemSysmapUser>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysmapUserEntityFactory());
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


        public DataTable SelectBymapidRDT(int? _mapid)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysmapUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapUserFields.Mapid == _mapid);
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
                EntityCollection _Collection = new EntityCollection(new SysmapUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapUserFields.Username == _username);
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
                EntityCollection _Collection = new EntityCollection(new SysmapUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapUserFields.Config == _config);
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
                EntityCollection _Collection = new EntityCollection(new SysmapUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapUserFields.Usercreated == _usercreated);
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
                EntityCollection _Collection = new EntityCollection(new SysmapUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapUserFields.Datecreated == _datecreated);
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
                EntityCollection _Collection = new EntityCollection(new SysmapUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapUserFields.Datemodified == _datemodified);
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
                EntityCollection _Collection = new EntityCollection(new SysmapUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapUserFields.Groupid == _groupid);
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
                EntityCollection _Collection = new EntityCollection(new SysmapUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapUserFields.Permission == _permission);
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


        public DataTable SelectByuseridRDT(int? _userid)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysmapUserEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapUserFields.Userid == _userid);
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

