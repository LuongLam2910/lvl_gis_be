
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
    #region Sysmap View 

    public class SysmapVM
    {
        #region Sysmap ItemSysmap 
        public class ItemSysmap
        {
            private int _Id;
            private string _name = string.Empty;
            private string _config = string.Empty;
            private int? _usercreated;
            private DateTime? _datecreated;
            private DateTime? _datemodified;
            private int? _usermodified;
            private string _status = string.Empty;
            private string _iconhome = string.Empty;
            private string _unitcode = string.Empty;

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

            public string Status
            {
                get { return _status; }
                set { _status = value; }
            }

            public string Iconhome
            {
                get { return _iconhome; }
                set { _iconhome = value; }
            }

            public string Unitcode
            {
                get { return _unitcode; }
                set { _unitcode = value; }
            }
        }

        #endregion
        public class PageSysmap
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region Sysmap Function 

    public class SysmapManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysmapEntity, SysmapVM.ItemSysmap>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysmapVM.ItemSysmap, SysmapEntity>();
        });

        #endregion

        public SysmapManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysmapVM.ItemSysmap _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysmapEntity _SysmapEntity = configVMtoEntity.CreateMapper().Map<SysmapEntity>(_Model);
                    Insert(_SysmapEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysmapEntity Insert(SysmapEntity _SysmapEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysmapEntity, true);
                }
                return _SysmapEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysmapVM.ItemSysmap _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysmapEntity _SysmapEntity = configVMtoEntity.CreateMapper().Map<SysmapEntity>(_Model);
                    Update(_SysmapEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysmapEntity _SysmapEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysmapFields.Id == _SysmapEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysmapEntity, filter);
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
                    SysmapEntity _SysmapEntity = new SysmapEntity(id);
                    if (adapter.FetchEntity(_SysmapEntity))
                    {
                        adapter.DeleteEntity(_SysmapEntity);
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
                        SysmapEntity _SysmapEntity = new SysmapEntity(id);
                        if (adapter.FetchEntity(_SysmapEntity))
                        {
                            adapter.DeleteEntity(_SysmapEntity);
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

        public SysmapEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysmapEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysmapFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysmapEntity());
        }

        public async Task<ApiResponse<SysmapVM.ItemSysmap>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysmapEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysmapVM.ItemSysmap toReturn = mapper.Map<SysmapVM.ItemSysmap>(_entity);
                        return ApiResponse<SysmapVM.ItemSysmap>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysmapVM.ItemSysmap>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysmapEntity> _Collection = SelectAll();
                    List<SysmapVM.ItemSysmap> toReturn = _Collection.Where(c => c.Status != "private").Select(c => configEntitytoVM.CreateMapper().Map<SysmapEntity, SysmapVM.ItemSysmap>(c)).ToList();

                    return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysmapEntity> SelectAll()
        {
            EntityCollection<SysmapEntity> _Collection = new EntityCollection<SysmapEntity>(new SysmapEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> SelectBynameAsync(string _name)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapEntity> _Collection = SelectByname(_name);
                    List<SysmapVM.ItemSysmap> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapEntity, SysmapVM.ItemSysmap>(c)).ToList();

                    return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> SelectByconfigAsync(int? _config)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapEntity> _Collection = SelectByconfig(_config);
                    List<SysmapVM.ItemSysmap> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapEntity, SysmapVM.ItemSysmap>(c)).ToList();

                    return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> SelectByusercreatedAsync(int? _usercreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapEntity> _Collection = SelectByusercreated(_usercreated);
                    List<SysmapVM.ItemSysmap> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapEntity, SysmapVM.ItemSysmap>(c)).ToList();

                    return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> SelectBydatecreatedAsync(DateTime? _datecreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapEntity> _Collection = SelectBydatecreated(_datecreated);
                    List<SysmapVM.ItemSysmap> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapEntity, SysmapVM.ItemSysmap>(c)).ToList();

                    return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> SelectBydatemodifiedAsync(DateTime? _datemodified)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapEntity> _Collection = SelectBydatemodified(_datemodified);
                    List<SysmapVM.ItemSysmap> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapEntity, SysmapVM.ItemSysmap>(c)).ToList();

                    return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> SelectByusermodifiedAsync(int? _usermodified)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapEntity> _Collection = SelectByusermodified(_usermodified);
                    List<SysmapVM.ItemSysmap> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapEntity, SysmapVM.ItemSysmap>(c)).ToList();

                    return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> SelectBystatusAsync(string _status)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapEntity> _Collection = SelectBystatus(_status);
                    List<SysmapVM.ItemSysmap> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapEntity, SysmapVM.ItemSysmap>(c)).ToList();

                    return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> SelectByiconhomeAsync(string _iconhome)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapEntity> _Collection = SelectByiconhome(_iconhome);
                    List<SysmapVM.ItemSysmap> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapEntity, SysmapVM.ItemSysmap>(c)).ToList();

                    return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> SelectByunitcodeAsync(string _unitcode)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysmapEntity> _Collection = SelectByunitcode(_unitcode);
                    List<SysmapVM.ItemSysmap> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapEntity, SysmapVM.ItemSysmap>(c)).ToList();

                    return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysmapEntity> SelectByname(string _name)
        {
            EntityCollection<SysmapEntity> _Collection = new EntityCollection<SysmapEntity>(new SysmapEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapFields.Name == _name);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapEntity> SelectByconfig(int? _config)
        {
            EntityCollection<SysmapEntity> _Collection = new EntityCollection<SysmapEntity>(new SysmapEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapFields.Config == _config);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapEntity> SelectByusercreated(int? _usercreated)
        {
            EntityCollection<SysmapEntity> _Collection = new EntityCollection<SysmapEntity>(new SysmapEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapFields.Usercreated == _usercreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapEntity> SelectBydatecreated(DateTime? _datecreated)
        {
            EntityCollection<SysmapEntity> _Collection = new EntityCollection<SysmapEntity>(new SysmapEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapFields.Datecreated == _datecreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapEntity> SelectBydatemodified(DateTime? _datemodified)
        {
            EntityCollection<SysmapEntity> _Collection = new EntityCollection<SysmapEntity>(new SysmapEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapFields.Datemodified == _datemodified);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapEntity> SelectByusermodified(int? _usermodified)
        {
            EntityCollection<SysmapEntity> _Collection = new EntityCollection<SysmapEntity>(new SysmapEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapFields.Usermodified == _usermodified);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapEntity> SelectBystatus(string _status)
        {
            EntityCollection<SysmapEntity> _Collection = new EntityCollection<SysmapEntity>(new SysmapEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapFields.Status == _status);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapEntity> SelectByiconhome(string _iconhome)
        {
            EntityCollection<SysmapEntity> _Collection = new EntityCollection<SysmapEntity>(new SysmapEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapFields.Iconhome == _iconhome);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysmapEntity> SelectByunitcode(string _unitcode)
        {
            EntityCollection<SysmapEntity> _Collection = new EntityCollection<SysmapEntity>(new SysmapEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysmapFields.Unitcode == _unitcode);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysmapVM.ItemSysmap>>>> SelectPaging(SysmapVM.PageSysmap _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysmapFields.Name).Like(_model.strKey)
                            .Or(SysmapFields.Status.Like(_model.strKey)).Or(SysmapFields.Iconhome.Like(_model.strKey)).Or(SysmapFields.Unitcode.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysmapEntity>();
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
                        SortExpression sort = new SortExpression(SysmapFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysmapVM.ItemSysmap>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysmapVM.ItemSysmap>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysmapEntity, SysmapVM.ItemSysmap>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysmapVM.ItemSysmap>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysmapEntityFactory());
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
                EntityCollection _Collection = new EntityCollection(new SysmapEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapFields.Name == _name);
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
                EntityCollection _Collection = new EntityCollection(new SysmapEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapFields.Config == _config);
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
                EntityCollection _Collection = new EntityCollection(new SysmapEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapFields.Usercreated == _usercreated);
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
                EntityCollection _Collection = new EntityCollection(new SysmapEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapFields.Datecreated == _datecreated);
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
                EntityCollection _Collection = new EntityCollection(new SysmapEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapFields.Datemodified == _datemodified);
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
                EntityCollection _Collection = new EntityCollection(new SysmapEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapFields.Usermodified == _usermodified);
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


        public DataTable SelectBystatusRDT(string _status)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysmapEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapFields.Status == _status);
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


        public DataTable SelectByiconhomeRDT(string _iconhome)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysmapEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapFields.Iconhome == _iconhome);
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
                EntityCollection _Collection = new EntityCollection(new SysmapEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysmapFields.Unitcode == _unitcode);
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

