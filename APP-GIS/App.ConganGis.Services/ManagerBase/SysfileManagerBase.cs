
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
    #region Sysfile View 

    public class SysfileVM
    {
        #region Sysfile ItemSysfile 
        public class ItemSysfile
        {
            private int _Id;
            private string _name = string.Empty;
            private int? _folder;
            private string _path = string.Empty;
            private string _createby = string.Empty;
            private int? _length;
            private int? _type;
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

            public int? Folder
            {
                get { return _folder; }
                set { _folder = value; }
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

            public int? Length
            {
                get { return _length; }
                set { _length = value; }
            }

            public int? Type
            {
                get { return _type; }
                set { _type = value; }
            }

            public string Unitcode
            {
                get { return _unitcode; }
                set { _unitcode = value; }
            }
        }

        #endregion
        public class PageSysfile
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region Sysfile Function 

    public class SysfileManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysfileEntity, SysfileVM.ItemSysfile>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysfileVM.ItemSysfile, SysfileEntity>();
        });

        #endregion

        public SysfileManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysfileVM.ItemSysfile _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysfileEntity _SysfileEntity = configVMtoEntity.CreateMapper().Map<SysfileEntity>(_Model);
                    Insert(_SysfileEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysfileEntity Insert(SysfileEntity _SysfileEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysfileEntity, true);
                }
                return _SysfileEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysfileVM.ItemSysfile _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysfileEntity _SysfileEntity = configVMtoEntity.CreateMapper().Map<SysfileEntity>(_Model);
                    Update(_SysfileEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysfileEntity _SysfileEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysfileFields.Id == _SysfileEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysfileEntity, filter);
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
                    SysfileEntity _SysfileEntity = new SysfileEntity(id);
                    if (adapter.FetchEntity(_SysfileEntity))
                    {
                        adapter.DeleteEntity(_SysfileEntity);
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
                        SysfileEntity _SysfileEntity = new SysfileEntity(id);
                        if (adapter.FetchEntity(_SysfileEntity))
                        {
                            adapter.DeleteEntity(_SysfileEntity);
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

        public SysfileEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysfileEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysfileFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysfileEntity());
        }

        public async Task<ApiResponse<SysfileVM.ItemSysfile>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysfileEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysfileVM.ItemSysfile toReturn = mapper.Map<SysfileVM.ItemSysfile>(_entity);
                        return ApiResponse<SysfileVM.ItemSysfile>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysfileVM.ItemSysfile>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysfileEntity> _Collection = SelectAll();
                    List<SysfileVM.ItemSysfile> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfileEntity, SysfileVM.ItemSysfile>(c)).ToList();

                    return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysfileEntity> SelectAll()
        {
            EntityCollection<SysfileEntity> _Collection = new EntityCollection<SysfileEntity>(new SysfileEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> SelectBynameAsync(string _name)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfileEntity> _Collection = SelectByname(_name);
                    List<SysfileVM.ItemSysfile> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfileEntity, SysfileVM.ItemSysfile>(c)).ToList();

                    return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> SelectByfolderAsync(int? _folder)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfileEntity> _Collection = SelectByfolder(_folder);
                    List<SysfileVM.ItemSysfile> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfileEntity, SysfileVM.ItemSysfile>(c)).ToList();

                    return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> SelectBypathAsync(string _path)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfileEntity> _Collection = SelectBypath(_path);
                    List<SysfileVM.ItemSysfile> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfileEntity, SysfileVM.ItemSysfile>(c)).ToList();

                    return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> SelectBycreatebyAsync(string _createby)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfileEntity> _Collection = SelectBycreateby(_createby);
                    List<SysfileVM.ItemSysfile> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfileEntity, SysfileVM.ItemSysfile>(c)).ToList();

                    return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> SelectBylengthAsync(int? _length)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfileEntity> _Collection = SelectBylength(_length);
                    List<SysfileVM.ItemSysfile> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfileEntity, SysfileVM.ItemSysfile>(c)).ToList();

                    return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> SelectBytypeAsync(int? _type)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfileEntity> _Collection = SelectBytype(_type);
                    List<SysfileVM.ItemSysfile> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfileEntity, SysfileVM.ItemSysfile>(c)).ToList();

                    return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> SelectByunitcodeAsync(string _unitcode)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfileEntity> _Collection = SelectByunitcode(_unitcode);
                    List<SysfileVM.ItemSysfile> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfileEntity, SysfileVM.ItemSysfile>(c)).ToList();

                    return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfileVM.ItemSysfile>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysfileEntity> SelectByname(string _name)
        {
            EntityCollection<SysfileEntity> _Collection = new EntityCollection<SysfileEntity>(new SysfileEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfileFields.Name == _name);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfileEntity> SelectByfolder(int? _folder)
        {
            EntityCollection<SysfileEntity> _Collection = new EntityCollection<SysfileEntity>(new SysfileEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfileFields.Folder == _folder);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfileEntity> SelectBypath(string _path)
        {
            EntityCollection<SysfileEntity> _Collection = new EntityCollection<SysfileEntity>(new SysfileEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfileFields.Path == _path);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfileEntity> SelectBycreateby(string _createby)
        {
            EntityCollection<SysfileEntity> _Collection = new EntityCollection<SysfileEntity>(new SysfileEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfileFields.Createby == _createby);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfileEntity> SelectBylength(int? _length)
        {
            EntityCollection<SysfileEntity> _Collection = new EntityCollection<SysfileEntity>(new SysfileEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfileFields.Length == _length);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfileEntity> SelectBytype(int? _type)
        {
            EntityCollection<SysfileEntity> _Collection = new EntityCollection<SysfileEntity>(new SysfileEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfileFields.Type == _type);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfileEntity> SelectByunitcode(string _unitcode)
        {
            EntityCollection<SysfileEntity> _Collection = new EntityCollection<SysfileEntity>(new SysfileEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfileFields.Unitcode == _unitcode);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysfileVM.ItemSysfile>>>> SelectPaging(SysfileVM.PageSysfile _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysfileFields.Name).Like(_model.strKey)
                            .Or(SysfileFields.Path.Like(_model.strKey)).Or(SysfileFields.Createby.Like(_model.strKey)).Or(SysfileFields.Unitcode.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysfileEntity>();
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
                        SortExpression sort = new SortExpression(SysfileFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysfileVM.ItemSysfile>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysfileVM.ItemSysfile>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysfileEntity, SysfileVM.ItemSysfile>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysfileVM.ItemSysfile>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfileEntityFactory());
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
                EntityCollection _Collection = new EntityCollection(new SysfileEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfileFields.Name == _name);
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


        public DataTable SelectByfolderRDT(int? _folder)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfileEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfileFields.Folder == _folder);
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
                EntityCollection _Collection = new EntityCollection(new SysfileEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfileFields.Path == _path);
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
                EntityCollection _Collection = new EntityCollection(new SysfileEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfileFields.Createby == _createby);
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


        public DataTable SelectBylengthRDT(int? _length)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfileEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfileFields.Length == _length);
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


        public DataTable SelectBytypeRDT(int? _type)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfileEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfileFields.Type == _type);
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
                EntityCollection _Collection = new EntityCollection(new SysfileEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfileFields.Unitcode == _unitcode);
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

