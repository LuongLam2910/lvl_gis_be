
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
    #region Sysfolder View 

    public class SysfolderVM
    {
        #region Sysfolder ItemSysfolder 
        public class ItemSysfolder
        {
            private int _Id;
            private string _name = string.Empty;
            private string _path = string.Empty;
            private string _createby = string.Empty;
            private DateTime? _createdate;
            private int? _parentid;
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

            public string Path
            {
                get { return _path; }
                set { _path = value; }
            }

            public string Createby
            {
                get { return _createby; }
                set { _createby = value; }
            }

            public DateTime? Createdate
            {
                get { return _createdate; }
                set { _createdate = value; }
            }

            public int? Parentid
            {
                get { return _parentid; }
                set { _parentid = value; }
            }

            public string Unitcode
            {
                get { return _unitcode; }
                set { _unitcode = value; }
            }
        }

        #endregion
        public class PageSysfolder
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region Sysfolder Function 

    public class SysfolderManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysfolderEntity, SysfolderVM.ItemSysfolder>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysfolderVM.ItemSysfolder, SysfolderEntity>();
        });

        #endregion

        public SysfolderManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysfolderVM.ItemSysfolder _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysfolderEntity _SysfolderEntity = configVMtoEntity.CreateMapper().Map<SysfolderEntity>(_Model);
                    Insert(_SysfolderEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysfolderEntity Insert(SysfolderEntity _SysfolderEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysfolderEntity, true);
                }
                return _SysfolderEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysfolderVM.ItemSysfolder _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysfolderEntity _SysfolderEntity = configVMtoEntity.CreateMapper().Map<SysfolderEntity>(_Model);
                    Update(_SysfolderEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysfolderEntity _SysfolderEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysfolderFields.Id == _SysfolderEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysfolderEntity, filter);
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
                    SysfolderEntity _SysfolderEntity = new SysfolderEntity(id);
                    if (adapter.FetchEntity(_SysfolderEntity))
                    {
                        adapter.DeleteEntity(_SysfolderEntity);
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
                        SysfolderEntity _SysfolderEntity = new SysfolderEntity(id);
                        if (adapter.FetchEntity(_SysfolderEntity))
                        {
                            adapter.DeleteEntity(_SysfolderEntity);
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

        public SysfolderEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysfolderEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysfolderFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysfolderEntity());
        }

        public async Task<ApiResponse<SysfolderVM.ItemSysfolder>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysfolderEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysfolderVM.ItemSysfolder toReturn = mapper.Map<SysfolderVM.ItemSysfolder>(_entity);
                        return ApiResponse<SysfolderVM.ItemSysfolder>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysfolderVM.ItemSysfolder>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysfolderVM.ItemSysfolder>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysfolderEntity> _Collection = SelectAll();
                    List<SysfolderVM.ItemSysfolder> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfolderEntity, SysfolderVM.ItemSysfolder>(c)).ToList();

                    return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysfolderEntity> SelectAll()
        {
            EntityCollection<SysfolderEntity> _Collection = new EntityCollection<SysfolderEntity>(new SysfolderEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysfolderVM.ItemSysfolder>>> SelectBynameAsync(string _name)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfolderEntity> _Collection = SelectByname(_name);
                    List<SysfolderVM.ItemSysfolder> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfolderEntity, SysfolderVM.ItemSysfolder>(c)).ToList();

                    return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfolderVM.ItemSysfolder>>> SelectBypathAsync(string _path)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfolderEntity> _Collection = SelectBypath(_path);
                    List<SysfolderVM.ItemSysfolder> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfolderEntity, SysfolderVM.ItemSysfolder>(c)).ToList();

                    return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfolderVM.ItemSysfolder>>> SelectBycreatebyAsync(string _createby)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfolderEntity> _Collection = SelectBycreateby(_createby);
                    List<SysfolderVM.ItemSysfolder> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfolderEntity, SysfolderVM.ItemSysfolder>(c)).ToList();

                    return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfolderVM.ItemSysfolder>>> SelectBycreatedateAsync(DateTime? _createdate)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfolderEntity> _Collection = SelectBycreatedate(_createdate);
                    List<SysfolderVM.ItemSysfolder> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfolderEntity, SysfolderVM.ItemSysfolder>(c)).ToList();

                    return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfolderVM.ItemSysfolder>>> SelectByparentidAsync(int? _parentid)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfolderEntity> _Collection = SelectByparentid(_parentid);
                    List<SysfolderVM.ItemSysfolder> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfolderEntity, SysfolderVM.ItemSysfolder>(c)).ToList();

                    return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfolderVM.ItemSysfolder>>> SelectByunitcodeAsync(string _unitcode)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfolderEntity> _Collection = SelectByunitcode(_unitcode);
                    List<SysfolderVM.ItemSysfolder> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfolderEntity, SysfolderVM.ItemSysfolder>(c)).ToList();

                    return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfolderVM.ItemSysfolder>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysfolderEntity> SelectByname(string _name)
        {
            EntityCollection<SysfolderEntity> _Collection = new EntityCollection<SysfolderEntity>(new SysfolderEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfolderFields.Name == _name);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfolderEntity> SelectBypath(string _path)
        {
            EntityCollection<SysfolderEntity> _Collection = new EntityCollection<SysfolderEntity>(new SysfolderEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfolderFields.Path == _path);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfolderEntity> SelectBycreateby(string _createby)
        {
            EntityCollection<SysfolderEntity> _Collection = new EntityCollection<SysfolderEntity>(new SysfolderEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfolderFields.Createby == _createby);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfolderEntity> SelectBycreatedate(DateTime? _createdate)
        {
            EntityCollection<SysfolderEntity> _Collection = new EntityCollection<SysfolderEntity>(new SysfolderEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfolderFields.Createdate == _createdate);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfolderEntity> SelectByparentid(int? _parentid)
        {
            EntityCollection<SysfolderEntity> _Collection = new EntityCollection<SysfolderEntity>(new SysfolderEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfolderFields.Parentid == _parentid);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfolderEntity> SelectByunitcode(string _unitcode)
        {
            EntityCollection<SysfolderEntity> _Collection = new EntityCollection<SysfolderEntity>(new SysfolderEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfolderFields.Unitcode == _unitcode);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysfolderVM.ItemSysfolder>>>> SelectPaging(SysfolderVM.PageSysfolder _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysfolderFields.Name).Like(_model.strKey)
                            .Or(SysfolderFields.Path.Like(_model.strKey)).Or(SysfolderFields.Createby.Like(_model.strKey)).Or(SysfolderFields.Unitcode.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysfolderEntity>();
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
                        SortExpression sort = new SortExpression(SysfolderFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysfolderVM.ItemSysfolder>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysfolderVM.ItemSysfolder>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysfolderEntity, SysfolderVM.ItemSysfolder>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysfolderVM.ItemSysfolder>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfolderEntityFactory());
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
                EntityCollection _Collection = new EntityCollection(new SysfolderEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfolderFields.Name == _name);
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


        public DataTable SelectBypathRDT(string _path)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfolderEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfolderFields.Path == _path);
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


        public DataTable SelectBycreatebyRDT(string _createby)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfolderEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfolderFields.Createby == _createby);
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


        public DataTable SelectBycreatedateRDT(DateTime? _createdate)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfolderEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfolderFields.Createdate == _createdate);
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


        public DataTable SelectByparentidRDT(int? _parentid)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfolderEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfolderFields.Parentid == _parentid);
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
                EntityCollection _Collection = new EntityCollection(new SysfolderEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfolderFields.Unitcode == _unitcode);
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

