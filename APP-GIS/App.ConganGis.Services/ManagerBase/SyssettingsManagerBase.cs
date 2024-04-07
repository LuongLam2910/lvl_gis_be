
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
    #region Syssettings View 

    public class SyssettingsVM
    {
        #region Syssettings ItemSyssettings 
        public class ItemSyssettings
        {
            private int _Id;
            private string _name = string.Empty;
            private string _config = string.Empty;
            private int? _usercreated;
            private DateTime? _datecreated;
            private DateTime? _datemodified;
            private int? _usermodified;
            private string _code = string.Empty;

            public int Id //	#region PrimaryKey
            {
                get { return _Id; }
                set { _Id = value; }
            }

            public string Name
            {
                get { return _name; }
                set { _name = value; }
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

            public int? Usermodified
            {
                get { return _usermodified; }
                set { _usermodified = value; }
            }

            public string Code
            {
                get { return _code; }
                set { _code = value; }
            }
        }

        #endregion
        public class PageSyssettings
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region Syssettings Function 

    public class SyssettingsManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SyssettingEntity, SyssettingsVM.ItemSyssettings>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SyssettingsVM.ItemSyssettings, SyssettingEntity>();
        });

        #endregion

        public SyssettingsManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SyssettingsVM.ItemSyssettings _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SyssettingEntity _SyssettingEntity = configVMtoEntity.CreateMapper().Map<SyssettingEntity>(_Model);
                    Insert(_SyssettingEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SyssettingEntity Insert(SyssettingEntity _SyssettingEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SyssettingEntity, true);
                }
                return _SyssettingEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SyssettingsVM.ItemSyssettings _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SyssettingEntity _SyssettingEntity = configVMtoEntity.CreateMapper().Map<SyssettingEntity>(_Model);
                    Update(_SyssettingEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SyssettingEntity _SyssettingEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SyssettingFields.Id == _SyssettingEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SyssettingEntity, filter);
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
                    SyssettingEntity _SyssettingEntity = new SyssettingEntity(id);
                    if (adapter.FetchEntity(_SyssettingEntity))
                    {
                        adapter.DeleteEntity(_SyssettingEntity);
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
                        SyssettingEntity _SyssettingEntity = new SyssettingEntity(id);
                        if (adapter.FetchEntity(_SyssettingEntity))
                        {
                            adapter.DeleteEntity(_SyssettingEntity);
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

        public SyssettingEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SyssettingEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SyssettingFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SyssettingEntity());
        }

        public async Task<ApiResponse<SyssettingsVM.ItemSyssettings>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SyssettingEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SyssettingsVM.ItemSyssettings toReturn = mapper.Map<SyssettingsVM.ItemSyssettings>(_entity);
                        return ApiResponse<SyssettingsVM.ItemSyssettings>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SyssettingsVM.ItemSyssettings>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SyssettingEntity> _Collection = SelectAll();
                    List<SyssettingsVM.ItemSyssettings> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SyssettingEntity, SyssettingsVM.ItemSyssettings>(c)).ToList();

                    return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SyssettingEntity> SelectAll()
        {
            EntityCollection<SyssettingEntity> _Collection = new EntityCollection<SyssettingEntity>(new SyssettingEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> SelectBynameAsync(string _name)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SyssettingEntity> _Collection = SelectByname(_name);
                    List<SyssettingsVM.ItemSyssettings> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SyssettingEntity, SyssettingsVM.ItemSyssettings>(c)).ToList();

                    return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> SelectByconfigAsync(int? _config)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SyssettingEntity> _Collection = SelectByconfig(_config);
                    List<SyssettingsVM.ItemSyssettings> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SyssettingEntity, SyssettingsVM.ItemSyssettings>(c)).ToList();

                    return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> SelectByusercreatedAsync(int? _usercreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SyssettingEntity> _Collection = SelectByusercreated(_usercreated);
                    List<SyssettingsVM.ItemSyssettings> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SyssettingEntity, SyssettingsVM.ItemSyssettings>(c)).ToList();

                    return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> SelectBydatecreatedAsync(DateTime? _datecreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SyssettingEntity> _Collection = SelectBydatecreated(_datecreated);
                    List<SyssettingsVM.ItemSyssettings> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SyssettingEntity, SyssettingsVM.ItemSyssettings>(c)).ToList();

                    return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> SelectBydatemodifiedAsync(DateTime? _datemodified)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SyssettingEntity> _Collection = SelectBydatemodified(_datemodified);
                    List<SyssettingsVM.ItemSyssettings> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SyssettingEntity, SyssettingsVM.ItemSyssettings>(c)).ToList();

                    return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> SelectByusermodifiedAsync(int? _usermodified)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SyssettingEntity> _Collection = SelectByusermodified(_usermodified);
                    List<SyssettingsVM.ItemSyssettings> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SyssettingEntity, SyssettingsVM.ItemSyssettings>(c)).ToList();

                    return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> SelectBycodeAsync(string _code)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SyssettingEntity> _Collection = SelectBycode(_code);
                    List<SyssettingsVM.ItemSyssettings> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SyssettingEntity, SyssettingsVM.ItemSyssettings>(c)).ToList();

                    return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SyssettingsVM.ItemSyssettings>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SyssettingEntity> SelectByname(string _name)
        {
            EntityCollection<SyssettingEntity> _Collection = new EntityCollection<SyssettingEntity>(new SyssettingEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SyssettingFields.Name == _name);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SyssettingEntity> SelectByconfig(int? _config)
        {
            EntityCollection<SyssettingEntity> _Collection = new EntityCollection<SyssettingEntity>(new SyssettingEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SyssettingFields.Config == _config);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SyssettingEntity> SelectByusercreated(int? _usercreated)
        {
            EntityCollection<SyssettingEntity> _Collection = new EntityCollection<SyssettingEntity>(new SyssettingEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SyssettingFields.Usercreated == _usercreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SyssettingEntity> SelectBydatecreated(DateTime? _datecreated)
        {
            EntityCollection<SyssettingEntity> _Collection = new EntityCollection<SyssettingEntity>(new SyssettingEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SyssettingFields.Datecreated == _datecreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SyssettingEntity> SelectBydatemodified(DateTime? _datemodified)
        {
            EntityCollection<SyssettingEntity> _Collection = new EntityCollection<SyssettingEntity>(new SyssettingEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SyssettingFields.Datemodified == _datemodified);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SyssettingEntity> SelectByusermodified(int? _usermodified)
        {
            EntityCollection<SyssettingEntity> _Collection = new EntityCollection<SyssettingEntity>(new SyssettingEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SyssettingFields.Usermodified == _usermodified);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SyssettingEntity> SelectBycode(string _code)
        {
            EntityCollection<SyssettingEntity> _Collection = new EntityCollection<SyssettingEntity>(new SyssettingEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SyssettingFields.Code == _code);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SyssettingsVM.ItemSyssettings>>>> SelectPaging(SyssettingsVM.PageSyssettings _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SyssettingFields.Name).Like(_model.strKey)
                            .Or(SyssettingFields.Code.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SyssettingEntity>();
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
                        SortExpression sort = new SortExpression(SyssettingFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SyssettingsVM.ItemSyssettings>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SyssettingsVM.ItemSyssettings>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SyssettingEntity, SyssettingsVM.ItemSyssettings>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SyssettingsVM.ItemSyssettings>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SyssettingEntityFactory());
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


        public DataTable SelectBynameRDT(string _name)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SyssettingEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SyssettingFields.Name == _name);
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
                EntityCollection _Collection = new EntityCollection(new SyssettingEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SyssettingFields.Config == _config);
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
                EntityCollection _Collection = new EntityCollection(new SyssettingEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SyssettingFields.Usercreated == _usercreated);
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
                EntityCollection _Collection = new EntityCollection(new SyssettingEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SyssettingFields.Datecreated == _datecreated);
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
                EntityCollection _Collection = new EntityCollection(new SyssettingEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SyssettingFields.Datemodified == _datemodified);
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


        public DataTable SelectByusermodifiedRDT(int? _usermodified)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SyssettingEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SyssettingFields.Usermodified == _usermodified);
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


        public DataTable SelectBycodeRDT(string _code)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SyssettingEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SyssettingFields.Code == _code);
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

