
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
    #region Sysfilemanager View 

    public class SysfilemanagerVM
    {
        #region Sysfilemanager ItemSysfilemanager 
        public class ItemSysfilemanager
        {
            private int _Id;
            private string _tablename = string.Empty;
            private string _filename = string.Empty;
            private string _filepath = string.Empty;
            private DateTime? _createddate;
            private int? _createuserid;
            private int? _idddata;
            private string _typefile = string.Empty;

            public int Id //	#region PrimaryKey
            {
                get { return _Id; }
                set { _Id = value; }
            }

            public string Tablename
            {
                get { return _tablename; }
                set { _tablename = value; }
            }

            public string Filename
            {
                get { return _filename; }
                set { _filename = value; }
            }

            public string Filepath
            {
                get { return _filepath; }
                set { _filepath = value; }
            }

            public DateTime? Createddate
            {
                get { return _createddate; }
                set { _createddate = value; }
            }

            public int? Createuserid
            {
                get { return _createuserid; }
                set { _createuserid = value; }
            }

            public int? Idddata
            {
                get { return _idddata; }
                set { _idddata = value; }
            }

            public string Typefile
            {
                get { return _typefile; }
                set { _typefile = value; }
            }
        }

        #endregion
        public class PageSysfilemanager
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region Sysfilemanager Function 

    public class SysfilemanagerManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysfilemanagerEntity, SysfilemanagerVM.ItemSysfilemanager>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysfilemanagerVM.ItemSysfilemanager, SysfilemanagerEntity>();
        });

        #endregion

        public SysfilemanagerManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysfilemanagerVM.ItemSysfilemanager _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysfilemanagerEntity _SysfilemanagerEntity = configVMtoEntity.CreateMapper().Map<SysfilemanagerEntity>(_Model);
                    Insert(_SysfilemanagerEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysfilemanagerEntity Insert(SysfilemanagerEntity _SysfilemanagerEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysfilemanagerEntity, true);
                }
                return _SysfilemanagerEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysfilemanagerVM.ItemSysfilemanager _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysfilemanagerEntity _SysfilemanagerEntity = configVMtoEntity.CreateMapper().Map<SysfilemanagerEntity>(_Model);
                    Update(_SysfilemanagerEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysfilemanagerEntity _SysfilemanagerEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysfilemanagerFields.Id == _SysfilemanagerEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysfilemanagerEntity, filter);
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
                    SysfilemanagerEntity _SysfilemanagerEntity = new SysfilemanagerEntity(id);
                    if (adapter.FetchEntity(_SysfilemanagerEntity))
                    {
                        adapter.DeleteEntity(_SysfilemanagerEntity);
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
                        SysfilemanagerEntity _SysfilemanagerEntity = new SysfilemanagerEntity(id);
                        if (adapter.FetchEntity(_SysfilemanagerEntity))
                        {
                            adapter.DeleteEntity(_SysfilemanagerEntity);
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

        public SysfilemanagerEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysfilemanagerEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysfilemanagerFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysfilemanagerEntity());
        }

        public async Task<ApiResponse<SysfilemanagerVM.ItemSysfilemanager>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysfilemanagerEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysfilemanagerVM.ItemSysfilemanager toReturn = mapper.Map<SysfilemanagerVM.ItemSysfilemanager>(_entity);
                        return ApiResponse<SysfilemanagerVM.ItemSysfilemanager>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysfilemanagerVM.ItemSysfilemanager>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysfilemanagerEntity> _Collection = SelectAll();
                    List<SysfilemanagerVM.ItemSysfilemanager> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfilemanagerEntity, SysfilemanagerVM.ItemSysfilemanager>(c)).ToList();

                    return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysfilemanagerEntity> SelectAll()
        {
            EntityCollection<SysfilemanagerEntity> _Collection = new EntityCollection<SysfilemanagerEntity>(new SysfilemanagerEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> SelectBytablenameAsync(string _tablename)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfilemanagerEntity> _Collection = SelectBytablename(_tablename);
                    List<SysfilemanagerVM.ItemSysfilemanager> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfilemanagerEntity, SysfilemanagerVM.ItemSysfilemanager>(c)).ToList();

                    return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> SelectByfilenameAsync(string _filename)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfilemanagerEntity> _Collection = SelectByfilename(_filename);
                    List<SysfilemanagerVM.ItemSysfilemanager> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfilemanagerEntity, SysfilemanagerVM.ItemSysfilemanager>(c)).ToList();

                    return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> SelectByfilepathAsync(string _filepath)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfilemanagerEntity> _Collection = SelectByfilepath(_filepath);
                    List<SysfilemanagerVM.ItemSysfilemanager> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfilemanagerEntity, SysfilemanagerVM.ItemSysfilemanager>(c)).ToList();

                    return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> SelectBycreateddateAsync(DateTime? _createddate)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfilemanagerEntity> _Collection = SelectBycreateddate(_createddate);
                    List<SysfilemanagerVM.ItemSysfilemanager> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfilemanagerEntity, SysfilemanagerVM.ItemSysfilemanager>(c)).ToList();

                    return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> SelectBycreateuseridAsync(int? _createuserid)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfilemanagerEntity> _Collection = SelectBycreateuserid(_createuserid);
                    List<SysfilemanagerVM.ItemSysfilemanager> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfilemanagerEntity, SysfilemanagerVM.ItemSysfilemanager>(c)).ToList();

                    return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> SelectByidddataAsync(int? _idddata)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfilemanagerEntity> _Collection = SelectByidddata(_idddata);
                    List<SysfilemanagerVM.ItemSysfilemanager> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfilemanagerEntity, SysfilemanagerVM.ItemSysfilemanager>(c)).ToList();

                    return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> SelectBytypefileAsync(string _typefile)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfilemanagerEntity> _Collection = SelectBytypefile(_typefile);
                    List<SysfilemanagerVM.ItemSysfilemanager> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfilemanagerEntity, SysfilemanagerVM.ItemSysfilemanager>(c)).ToList();

                    return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysfilemanagerEntity> SelectBytablename(string _tablename)
        {
            EntityCollection<SysfilemanagerEntity> _Collection = new EntityCollection<SysfilemanagerEntity>(new SysfilemanagerEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfilemanagerFields.Tablename == _tablename);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfilemanagerEntity> SelectByfilename(string _filename)
        {
            EntityCollection<SysfilemanagerEntity> _Collection = new EntityCollection<SysfilemanagerEntity>(new SysfilemanagerEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfilemanagerFields.Filename == _filename);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfilemanagerEntity> SelectByfilepath(string _filepath)
        {
            EntityCollection<SysfilemanagerEntity> _Collection = new EntityCollection<SysfilemanagerEntity>(new SysfilemanagerEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfilemanagerFields.Filepath == _filepath);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfilemanagerEntity> SelectBycreateddate(DateTime? _createddate)
        {
            EntityCollection<SysfilemanagerEntity> _Collection = new EntityCollection<SysfilemanagerEntity>(new SysfilemanagerEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfilemanagerFields.Createddate == _createddate);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfilemanagerEntity> SelectBycreateuserid(int? _createuserid)
        {
            EntityCollection<SysfilemanagerEntity> _Collection = new EntityCollection<SysfilemanagerEntity>(new SysfilemanagerEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfilemanagerFields.Createuserid == _createuserid);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfilemanagerEntity> SelectByidddata(int? _idddata)
        {
            EntityCollection<SysfilemanagerEntity> _Collection = new EntityCollection<SysfilemanagerEntity>(new SysfilemanagerEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfilemanagerFields.Idddata == _idddata);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfilemanagerEntity> SelectBytypefile(string _typefile)
        {
            EntityCollection<SysfilemanagerEntity> _Collection = new EntityCollection<SysfilemanagerEntity>(new SysfilemanagerEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfilemanagerFields.Typefile == _typefile);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysfilemanagerVM.ItemSysfilemanager>>>> SelectPaging(SysfilemanagerVM.PageSysfilemanager _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysfilemanagerFields.Tablename).Like(_model.strKey)
                            .Or(SysfilemanagerFields.Filename.Like(_model.strKey)).Or(SysfilemanagerFields.Filepath.Like(_model.strKey)).Or(SysfilemanagerFields.Typefile.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysfilemanagerEntity>();
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
                        SortExpression sort = new SortExpression(SysfilemanagerFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysfilemanagerVM.ItemSysfilemanager>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysfilemanagerVM.ItemSysfilemanager>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysfilemanagerEntity, SysfilemanagerVM.ItemSysfilemanager>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysfilemanagerVM.ItemSysfilemanager>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfilemanagerEntityFactory());
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


        public DataTable SelectBytablenameRDT(string _tablename)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfilemanagerEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfilemanagerFields.Tablename == _tablename);
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


        public DataTable SelectByfilenameRDT(string _filename)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfilemanagerEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfilemanagerFields.Filename == _filename);
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


        public DataTable SelectByfilepathRDT(string _filepath)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfilemanagerEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfilemanagerFields.Filepath == _filepath);
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


        public DataTable SelectBycreateddateRDT(DateTime? _createddate)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfilemanagerEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfilemanagerFields.Createddate == _createddate);
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


        public DataTable SelectBycreateuseridRDT(int? _createuserid)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfilemanagerEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfilemanagerFields.Createuserid == _createuserid);
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


        public DataTable SelectByidddataRDT(int? _idddata)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfilemanagerEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfilemanagerFields.Idddata == _idddata);
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


        public DataTable SelectBytypefileRDT(string _typefile)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfilemanagerEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfilemanagerFields.Typefile == _typefile);
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

