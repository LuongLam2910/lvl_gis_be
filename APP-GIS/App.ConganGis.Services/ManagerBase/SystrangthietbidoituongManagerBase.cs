
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
    #region Systrangthietbidoituong View 

    public class SystrangthietbidoituongVM
    {
        #region Systrangthietbidoituong ItemSystrangthietbidoituong 
        public class ItemSystrangthietbidoituong
        {
            private int _Id;
            private int _iddoituong;
            private int? _idthietbi;
            private int? _soluong;
            private string _ngaytao = string.Empty;
            private int? _ngaycapnhat;
            private string _tablename = string.Empty;

            public int Id //	#region PrimaryKey
            {
                get { return _Id; }
                set { _Id = value; }
            }

            public int Iddoituong
            {
                get { return _iddoituong; }
                set { _iddoituong = value; }
            }

            public int? Idthietbi
            {
                get { return _idthietbi; }
                set { _idthietbi = value; }
            }

            public int? Soluong
            {
                get { return _soluong; }
                set { _soluong = value; }
            }

            public string Ngaytao
            {
                get { return _ngaytao; }
                set { _ngaytao = value; }
            }

            public int? Ngaycapnhat
            {
                get { return _ngaycapnhat; }
                set { _ngaycapnhat = value; }
            }

            public string Tablename
            {
                get { return _tablename; }
                set { _tablename = value; }
            }
        }

        #endregion
        public class PageSystrangthietbidoituong
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region Systrangthietbidoituong Function 

    public class SystrangthietbidoituongManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SystrangthietbidoituongEntity, SystrangthietbidoituongVM.ItemSystrangthietbidoituong>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SystrangthietbidoituongVM.ItemSystrangthietbidoituong, SystrangthietbidoituongEntity>();
        });

        #endregion

        public SystrangthietbidoituongManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SystrangthietbidoituongVM.ItemSystrangthietbidoituong _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SystrangthietbidoituongEntity _SystrangthietbidoituongEntity = configVMtoEntity.CreateMapper().Map<SystrangthietbidoituongEntity>(_Model);
                    Insert(_SystrangthietbidoituongEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SystrangthietbidoituongEntity Insert(SystrangthietbidoituongEntity _SystrangthietbidoituongEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SystrangthietbidoituongEntity, true);
                }
                return _SystrangthietbidoituongEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SystrangthietbidoituongVM.ItemSystrangthietbidoituong _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SystrangthietbidoituongEntity _SystrangthietbidoituongEntity = configVMtoEntity.CreateMapper().Map<SystrangthietbidoituongEntity>(_Model);
                    Update(_SystrangthietbidoituongEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SystrangthietbidoituongEntity _SystrangthietbidoituongEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SystrangthietbidoituongFields.Id == _SystrangthietbidoituongEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SystrangthietbidoituongEntity, filter);
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
                    SystrangthietbidoituongEntity _SystrangthietbidoituongEntity = new SystrangthietbidoituongEntity(id);
                    if (adapter.FetchEntity(_SystrangthietbidoituongEntity))
                    {
                        adapter.DeleteEntity(_SystrangthietbidoituongEntity);
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
                        SystrangthietbidoituongEntity _SystrangthietbidoituongEntity = new SystrangthietbidoituongEntity(id);
                        if (adapter.FetchEntity(_SystrangthietbidoituongEntity))
                        {
                            adapter.DeleteEntity(_SystrangthietbidoituongEntity);
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

        public SystrangthietbidoituongEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SystrangthietbidoituongEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SystrangthietbidoituongFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SystrangthietbidoituongEntity());
        }

        public async Task<ApiResponse<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SystrangthietbidoituongEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SystrangthietbidoituongVM.ItemSystrangthietbidoituong toReturn = mapper.Map<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>(_entity);
                        return ApiResponse<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SystrangthietbidoituongEntity> _Collection = SelectAll();
                    List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SystrangthietbidoituongEntity, SystrangthietbidoituongVM.ItemSystrangthietbidoituong>(c)).ToList();

                    return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SystrangthietbidoituongEntity> SelectAll()
        {
            EntityCollection<SystrangthietbidoituongEntity> _Collection = new EntityCollection<SystrangthietbidoituongEntity>(new SystrangthietbidoituongEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> SelectByiddoituongAsync(int _iddoituong)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SystrangthietbidoituongEntity> _Collection = SelectByiddoituong(_iddoituong);
                    List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SystrangthietbidoituongEntity, SystrangthietbidoituongVM.ItemSystrangthietbidoituong>(c)).ToList();

                    return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> SelectByidthietbiAsync(int? _idthietbi)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SystrangthietbidoituongEntity> _Collection = SelectByidthietbi(_idthietbi);
                    List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SystrangthietbidoituongEntity, SystrangthietbidoituongVM.ItemSystrangthietbidoituong>(c)).ToList();

                    return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> SelectBysoluongAsync(int? _soluong)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SystrangthietbidoituongEntity> _Collection = SelectBysoluong(_soluong);
                    List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SystrangthietbidoituongEntity, SystrangthietbidoituongVM.ItemSystrangthietbidoituong>(c)).ToList();

                    return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> SelectByngaytaoAsync(string _ngaytao)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SystrangthietbidoituongEntity> _Collection = SelectByngaytao(_ngaytao);
                    List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SystrangthietbidoituongEntity, SystrangthietbidoituongVM.ItemSystrangthietbidoituong>(c)).ToList();

                    return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> SelectByngaycapnhatAsync(int? _ngaycapnhat)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SystrangthietbidoituongEntity> _Collection = SelectByngaycapnhat(_ngaycapnhat);
                    List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SystrangthietbidoituongEntity, SystrangthietbidoituongVM.ItemSystrangthietbidoituong>(c)).ToList();

                    return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> SelectBytablenameAsync(string _tablename)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SystrangthietbidoituongEntity> _Collection = SelectBytablename(_tablename);
                    List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SystrangthietbidoituongEntity, SystrangthietbidoituongVM.ItemSystrangthietbidoituong>(c)).ToList();

                    return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SystrangthietbidoituongEntity> SelectByiddoituong(int _iddoituong)
        {
            EntityCollection<SystrangthietbidoituongEntity> _Collection = new EntityCollection<SystrangthietbidoituongEntity>(new SystrangthietbidoituongEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SystrangthietbidoituongFields.Iddoituong == _iddoituong);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SystrangthietbidoituongEntity> SelectByidthietbi(int? _idthietbi)
        {
            EntityCollection<SystrangthietbidoituongEntity> _Collection = new EntityCollection<SystrangthietbidoituongEntity>(new SystrangthietbidoituongEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SystrangthietbidoituongFields.Idthietbi == _idthietbi);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SystrangthietbidoituongEntity> SelectBysoluong(int? _soluong)
        {
            EntityCollection<SystrangthietbidoituongEntity> _Collection = new EntityCollection<SystrangthietbidoituongEntity>(new SystrangthietbidoituongEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SystrangthietbidoituongFields.Soluong == _soluong);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SystrangthietbidoituongEntity> SelectByngaytao(string _ngaytao)
        {
            EntityCollection<SystrangthietbidoituongEntity> _Collection = new EntityCollection<SystrangthietbidoituongEntity>(new SystrangthietbidoituongEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SystrangthietbidoituongFields.Ngaytao == _ngaytao);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SystrangthietbidoituongEntity> SelectByngaycapnhat(int? _ngaycapnhat)
        {
            EntityCollection<SystrangthietbidoituongEntity> _Collection = new EntityCollection<SystrangthietbidoituongEntity>(new SystrangthietbidoituongEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SystrangthietbidoituongFields.Ngaycapnhat == _ngaycapnhat);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SystrangthietbidoituongEntity> SelectBytablename(string _tablename)
        {
            EntityCollection<SystrangthietbidoituongEntity> _Collection = new EntityCollection<SystrangthietbidoituongEntity>(new SystrangthietbidoituongEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SystrangthietbidoituongFields.Tablename == _tablename);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>>> SelectPaging(SystrangthietbidoituongVM.PageSystrangthietbidoituong _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SystrangthietbidoituongFields.Iddoituong).Like(_model.strKey)
                            .Or(SystrangthietbidoituongFields.Ngaytao.Like(_model.strKey)).Or(SystrangthietbidoituongFields.Tablename.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SystrangthietbidoituongEntity>();
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
                        SortExpression sort = new SortExpression(SystrangthietbidoituongFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SystrangthietbidoituongEntity, SystrangthietbidoituongVM.ItemSystrangthietbidoituong>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SystrangthietbidoituongEntityFactory());
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


        public DataTable SelectByiddoituongRDT(int _iddoituong)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SystrangthietbidoituongEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SystrangthietbidoituongFields.Iddoituong == _iddoituong);
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


        public DataTable SelectByidthietbiRDT(int? _idthietbi)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SystrangthietbidoituongEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SystrangthietbidoituongFields.Idthietbi == _idthietbi);
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


        public DataTable SelectBysoluongRDT(int? _soluong)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SystrangthietbidoituongEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SystrangthietbidoituongFields.Soluong == _soluong);
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


        public DataTable SelectByngaytaoRDT(string _ngaytao)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SystrangthietbidoituongEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SystrangthietbidoituongFields.Ngaytao == _ngaytao);
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


        public DataTable SelectByngaycapnhatRDT(int? _ngaycapnhat)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SystrangthietbidoituongEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SystrangthietbidoituongFields.Ngaycapnhat == _ngaycapnhat);
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
                EntityCollection _Collection = new EntityCollection(new SystrangthietbidoituongEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SystrangthietbidoituongFields.Tablename == _tablename);
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

