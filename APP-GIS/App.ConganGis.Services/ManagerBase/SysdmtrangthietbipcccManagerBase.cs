
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
    #region Sysdmtrangthietbipccc View 

    public class SysdmtrangthietbipcccVM
    {
        #region Sysdmtrangthietbipccc ItemSysdmtrangthietbipccc 
        public class ItemSysdmtrangthietbipccc
        {
            private int _Id;
            private string _tenthietbi = string.Empty;
            private int _macha;
            private string _mota = string.Empty;
            private int _loaithietbi;
            private int _trangthai;

            public int Id //	#region PrimaryKey
            {
                get { return _Id; }
                set { _Id = value; }
            }

            public string Tenthietbi
            {
                get { return _tenthietbi; }
                set { _tenthietbi = value; }
            }

            public int Macha
            {
                get { return _macha; }
                set { _macha = value; }
            }

            public string Mota
            {
                get { return _mota; }
                set { _mota = value; }
            }

            public int Loaithietbi
            {
                get { return _loaithietbi; }
                set { _loaithietbi = value; }
            }

            public int Trangthai
            {
                get { return _trangthai; }
                set { _trangthai = value; }
            }
        }

        #endregion
        public class PageSysdmtrangthietbipccc
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region Sysdmtrangthietbipccc Function 

    public class SysdmtrangthietbipcccManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysdmtrangthietbipcccEntity, SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc, SysdmtrangthietbipcccEntity>();
        });

        #endregion

        public SysdmtrangthietbipcccManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysdmtrangthietbipcccEntity _SysdmtrangthietbipcccEntity = configVMtoEntity.CreateMapper().Map<SysdmtrangthietbipcccEntity>(_Model);
                    Insert(_SysdmtrangthietbipcccEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysdmtrangthietbipcccEntity Insert(SysdmtrangthietbipcccEntity _SysdmtrangthietbipcccEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysdmtrangthietbipcccEntity, true);
                }
                return _SysdmtrangthietbipcccEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysdmtrangthietbipcccEntity _SysdmtrangthietbipcccEntity = configVMtoEntity.CreateMapper().Map<SysdmtrangthietbipcccEntity>(_Model);
                    Update(_SysdmtrangthietbipcccEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysdmtrangthietbipcccEntity _SysdmtrangthietbipcccEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysdmtrangthietbipcccFields.Id == _SysdmtrangthietbipcccEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysdmtrangthietbipcccEntity, filter);
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
                    SysdmtrangthietbipcccEntity _SysdmtrangthietbipcccEntity = new SysdmtrangthietbipcccEntity(id);
                    if (adapter.FetchEntity(_SysdmtrangthietbipcccEntity))
                    {
                        adapter.DeleteEntity(_SysdmtrangthietbipcccEntity);
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
                        SysdmtrangthietbipcccEntity _SysdmtrangthietbipcccEntity = new SysdmtrangthietbipcccEntity(id);
                        if (adapter.FetchEntity(_SysdmtrangthietbipcccEntity))
                        {
                            adapter.DeleteEntity(_SysdmtrangthietbipcccEntity);
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

        public SysdmtrangthietbipcccEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysdmtrangthietbipcccEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysdmtrangthietbipcccFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysdmtrangthietbipcccEntity());
        }

        public async Task<ApiResponse<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysdmtrangthietbipcccEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc toReturn = mapper.Map<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>(_entity);
                        return ApiResponse<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysdmtrangthietbipcccEntity> _Collection = SelectAll();
                    List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdmtrangthietbipcccEntity, SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>(c)).ToList();

                    return ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysdmtrangthietbipcccEntity> SelectAll()
        {
            EntityCollection<SysdmtrangthietbipcccEntity> _Collection = new EntityCollection<SysdmtrangthietbipcccEntity>(new SysdmtrangthietbipcccEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>> SelectBytenthietbiAsync(string _tenthietbi)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdmtrangthietbipcccEntity> _Collection = SelectBytenthietbi(_tenthietbi);
                    List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdmtrangthietbipcccEntity, SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>(c)).ToList();

                    return ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>> SelectBymachaAsync(int _macha)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdmtrangthietbipcccEntity> _Collection = SelectBymacha(_macha);
                    List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdmtrangthietbipcccEntity, SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>(c)).ToList();

                    return ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>> SelectBymotaAsync(string _mota)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdmtrangthietbipcccEntity> _Collection = SelectBymota(_mota);
                    List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdmtrangthietbipcccEntity, SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>(c)).ToList();

                    return ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>> SelectByloaithietbiAsync(int _loaithietbi)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdmtrangthietbipcccEntity> _Collection = SelectByloaithietbi(_loaithietbi);
                    List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdmtrangthietbipcccEntity, SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>(c)).ToList();

                    return ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>> SelectBytrangthaiAsync(int _trangthai)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdmtrangthietbipcccEntity> _Collection = SelectBytrangthai(_trangthai);
                    List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdmtrangthietbipcccEntity, SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>(c)).ToList();

                    return ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysdmtrangthietbipcccEntity> SelectBytenthietbi(string _tenthietbi)
        {
            EntityCollection<SysdmtrangthietbipcccEntity> _Collection = new EntityCollection<SysdmtrangthietbipcccEntity>(new SysdmtrangthietbipcccEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdmtrangthietbipcccFields.Tenthietbi == _tenthietbi);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdmtrangthietbipcccEntity> SelectBymacha(int _macha)
        {
            EntityCollection<SysdmtrangthietbipcccEntity> _Collection = new EntityCollection<SysdmtrangthietbipcccEntity>(new SysdmtrangthietbipcccEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdmtrangthietbipcccFields.Macha == _macha);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdmtrangthietbipcccEntity> SelectBymota(string _mota)
        {
            EntityCollection<SysdmtrangthietbipcccEntity> _Collection = new EntityCollection<SysdmtrangthietbipcccEntity>(new SysdmtrangthietbipcccEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdmtrangthietbipcccFields.Mota == _mota);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdmtrangthietbipcccEntity> SelectByloaithietbi(int _loaithietbi)
        {
            EntityCollection<SysdmtrangthietbipcccEntity> _Collection = new EntityCollection<SysdmtrangthietbipcccEntity>(new SysdmtrangthietbipcccEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdmtrangthietbipcccFields.Loaithietbi == _loaithietbi);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdmtrangthietbipcccEntity> SelectBytrangthai(int _trangthai)
        {
            EntityCollection<SysdmtrangthietbipcccEntity> _Collection = new EntityCollection<SysdmtrangthietbipcccEntity>(new SysdmtrangthietbipcccEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdmtrangthietbipcccFields.Trangthai == _trangthai);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>>> SelectPaging(SysdmtrangthietbipcccVM.PageSysdmtrangthietbipccc _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysdmtrangthietbipcccFields.Tenthietbi).Like(_model.strKey)
                            .Or(SysdmtrangthietbipcccFields.Mota.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysdmtrangthietbipcccEntity>();
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
                        SortExpression sort = new SortExpression(SysdmtrangthietbipcccFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysdmtrangthietbipcccEntity, SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdmtrangthietbipcccEntityFactory());
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


        public DataTable SelectBytenthietbiRDT(string _tenthietbi)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdmtrangthietbipcccEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdmtrangthietbipcccFields.Tenthietbi == _tenthietbi);
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


        public DataTable SelectBymachaRDT(int _macha)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdmtrangthietbipcccEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdmtrangthietbipcccFields.Macha == _macha);
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


        public DataTable SelectBymotaRDT(string _mota)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdmtrangthietbipcccEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdmtrangthietbipcccFields.Mota == _mota);
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


        public DataTable SelectByloaithietbiRDT(int _loaithietbi)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdmtrangthietbipcccEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdmtrangthietbipcccFields.Loaithietbi == _loaithietbi);
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


        public DataTable SelectBytrangthaiRDT(int _trangthai)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdmtrangthietbipcccEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdmtrangthietbipcccFields.Trangthai == _trangthai);
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

