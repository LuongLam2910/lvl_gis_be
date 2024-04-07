
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
    #region Sysdataset View 

    public class SysdatasetVM
    {
        #region Sysdataset ItemSysdataset 
        public class ItemSysdataset
        {
            private int _Id;
            private string _name = string.Empty;
            private int? _parentid;
            private int? _usercreated;
            private DateTime? _datecreated;
            private DateTime? _datemodified;
            private int? _usermodified;
            private string _description = string.Empty;
            private string _unitcode = string.Empty;
            private int _status;

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

            public int? Parentid
            {
                get { return _parentid; }
                set { _parentid = value; }
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

            public string Description
            {
                get { return _description; }
                set { _description = value; }
            }

            public string Unitcode
            {
                get { return _unitcode; }
                set { _unitcode = value; }
            }
            public int Status
            {
                get { return _status; }
                set { _status = value; }
            }
        }

        #endregion
        public class PageSysdataset
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region Sysdataset Function 

    public class SysdatasetManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysdatasetEntity, SysdatasetVM.ItemSysdataset>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysdatasetVM.ItemSysdataset, SysdatasetEntity>();
        });

        #endregion

        public SysdatasetManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysdatasetVM.ItemSysdataset _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysdatasetEntity _SysdatasetEntity = configVMtoEntity.CreateMapper().Map<SysdatasetEntity>(_Model);
                    Insert(_SysdatasetEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysdatasetEntity Insert(SysdatasetEntity _SysdatasetEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysdatasetEntity, true);
                }
                return _SysdatasetEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysdatasetVM.ItemSysdataset _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysdatasetEntity _SysdatasetEntity = configVMtoEntity.CreateMapper().Map<SysdatasetEntity>(_Model);
                    Update(_SysdatasetEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysdatasetEntity _SysdatasetEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysdatasetFields.Id == _SysdatasetEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysdatasetEntity, filter);
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
                    SysdatasetEntity _SysdatasetEntity = new SysdatasetEntity(id);
                    if (adapter.FetchEntity(_SysdatasetEntity))
                    {
                        adapter.DeleteEntity(_SysdatasetEntity);
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
                        SysdatasetEntity _SysdatasetEntity = new SysdatasetEntity(id);
                        if (adapter.FetchEntity(_SysdatasetEntity))
                        {
                            adapter.DeleteEntity(_SysdatasetEntity);
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

        public SysdatasetEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysdatasetEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysdatasetFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysdatasetEntity());
        }

        public async Task<ApiResponse<SysdatasetVM.ItemSysdataset>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysdatasetEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysdatasetVM.ItemSysdataset toReturn = mapper.Map<SysdatasetVM.ItemSysdataset>(_entity);
                        return ApiResponse<SysdatasetVM.ItemSysdataset>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysdatasetVM.ItemSysdataset>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysdatasetEntity> _Collection = SelectAll();
                    List<SysdatasetVM.ItemSysdataset> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetEntity, SysdatasetVM.ItemSysdataset>(c)).ToList();

                    return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysdatasetEntity> SelectAll()
        {
            EntityCollection<SysdatasetEntity> _Collection = new EntityCollection<SysdatasetEntity>(new SysdatasetEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> SelectBynameAsync(string _name)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetEntity> _Collection = SelectByname(_name);
                    List<SysdatasetVM.ItemSysdataset> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetEntity, SysdatasetVM.ItemSysdataset>(c)).ToList();

                    return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> SelectByparentidAsync(int? _parentid)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetEntity> _Collection = SelectByparentid(_parentid);
                    List<SysdatasetVM.ItemSysdataset> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetEntity, SysdatasetVM.ItemSysdataset>(c)).ToList();

                    return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> SelectByusercreatedAsync(int? _usercreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetEntity> _Collection = SelectByusercreated(_usercreated);
                    List<SysdatasetVM.ItemSysdataset> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetEntity, SysdatasetVM.ItemSysdataset>(c)).ToList();

                    return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> SelectBydatecreatedAsync(DateTime? _datecreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetEntity> _Collection = SelectBydatecreated(_datecreated);
                    List<SysdatasetVM.ItemSysdataset> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetEntity, SysdatasetVM.ItemSysdataset>(c)).ToList();

                    return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> SelectBydatemodifiedAsync(DateTime? _datemodified)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetEntity> _Collection = SelectBydatemodified(_datemodified);
                    List<SysdatasetVM.ItemSysdataset> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetEntity, SysdatasetVM.ItemSysdataset>(c)).ToList();

                    return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> SelectByusermodifiedAsync(int? _usermodified)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetEntity> _Collection = SelectByusermodified(_usermodified);
                    List<SysdatasetVM.ItemSysdataset> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetEntity, SysdatasetVM.ItemSysdataset>(c)).ToList();

                    return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> SelectBydescriptionAsync(string _description)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetEntity> _Collection = SelectBydescription(_description);
                    List<SysdatasetVM.ItemSysdataset> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetEntity, SysdatasetVM.ItemSysdataset>(c)).ToList();

                    return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> SelectByunitcodeAsync(string _unitcode)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysdatasetEntity> _Collection = SelectByunitcode(_unitcode);
                    List<SysdatasetVM.ItemSysdataset> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetEntity, SysdatasetVM.ItemSysdataset>(c)).ToList();

                    return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysdatasetVM.ItemSysdataset>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysdatasetEntity> SelectByname(string _name)
        {
            EntityCollection<SysdatasetEntity> _Collection = new EntityCollection<SysdatasetEntity>(new SysdatasetEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetFields.Name == _name);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdatasetEntity> SelectByparentid(int? _parentid)
        {
            EntityCollection<SysdatasetEntity> _Collection = new EntityCollection<SysdatasetEntity>(new SysdatasetEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetFields.Parentid == _parentid);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }


        public EntityCollection<SysdatasetEntity> SelectByusercreated(int? _usercreated)
        {
            EntityCollection<SysdatasetEntity> _Collection = new EntityCollection<SysdatasetEntity>(new SysdatasetEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetFields.Usercreated == _usercreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdatasetEntity> SelectBydatecreated(DateTime? _datecreated)
        {
            EntityCollection<SysdatasetEntity> _Collection = new EntityCollection<SysdatasetEntity>(new SysdatasetEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetFields.Datecreated == _datecreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdatasetEntity> SelectBydatemodified(DateTime? _datemodified)
        {
            EntityCollection<SysdatasetEntity> _Collection = new EntityCollection<SysdatasetEntity>(new SysdatasetEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetFields.Datemodified == _datemodified);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdatasetEntity> SelectByusermodified(int? _usermodified)
        {
            EntityCollection<SysdatasetEntity> _Collection = new EntityCollection<SysdatasetEntity>(new SysdatasetEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetFields.Usermodified == _usermodified);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdatasetEntity> SelectBydescription(string _description)
        {
            EntityCollection<SysdatasetEntity> _Collection = new EntityCollection<SysdatasetEntity>(new SysdatasetEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetFields.Description == _description);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysdatasetEntity> SelectByunitcode(string _unitcode)
        {
            EntityCollection<SysdatasetEntity> _Collection = new EntityCollection<SysdatasetEntity>(new SysdatasetEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysdatasetFields.Unitcode == _unitcode);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysdatasetVM.ItemSysdataset>>>> SelectPaging(SysdatasetVM.PageSysdataset _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysdatasetFields.Name).Like(_model.strKey)
                            .Or(SysdatasetFields.Description.Like(_model.strKey)).Or(SysdatasetFields.Unitcode.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysdatasetEntity>();
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
                        SortExpression sort = new SortExpression(SysdatasetFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysdatasetVM.ItemSysdataset>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysdatasetVM.ItemSysdataset>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysdatasetEntity, SysdatasetVM.ItemSysdataset>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysdatasetVM.ItemSysdataset>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdatasetEntityFactory());
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
                EntityCollection _Collection = new EntityCollection(new SysdatasetEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetFields.Name == _name);
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
                EntityCollection _Collection = new EntityCollection(new SysdatasetEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetFields.Parentid == _parentid);
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
                EntityCollection _Collection = new EntityCollection(new SysdatasetEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetFields.Usercreated == _usercreated);
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
                EntityCollection _Collection = new EntityCollection(new SysdatasetEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetFields.Datecreated == _datecreated);
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
                EntityCollection _Collection = new EntityCollection(new SysdatasetEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetFields.Datemodified == _datemodified);
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
                EntityCollection _Collection = new EntityCollection(new SysdatasetEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetFields.Usermodified == _usermodified);
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


        public DataTable SelectBydescriptionRDT(string _description)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysdatasetEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetFields.Description == _description);
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
                EntityCollection _Collection = new EntityCollection(new SysdatasetEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysdatasetFields.Unitcode == _unitcode);
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

