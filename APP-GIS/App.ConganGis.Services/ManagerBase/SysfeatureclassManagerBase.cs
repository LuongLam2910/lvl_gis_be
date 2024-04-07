
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
    #region Sysfeatureclass View 

    public class SysfeatureclassVM
    {
        #region Sysfeatureclass ItemSysfeatureclass 
        public class ItemSysfeatureclass
        {
            private int _Id;
            private string _name = string.Empty;
            private string _tablename = string.Empty;
            private int? _datasetid;
            private string _config = string.Empty;
            private int? _usercreated;
            private DateTime? _datacreated;
            private DateTime? _datemodified;
            private int? _usermodified;
            private string _description = string.Empty;
            private string _prj = string.Empty;
            private string _geotype = string.Empty;
            private int? _status;
            private string _unitcode = string.Empty;
            private int? _type;

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

            public string Tablename
            {
                get { return _tablename; }
                set { _tablename = value; }
            }

            public int? Datasetid
            {
                get { return _datasetid; }
                set { _datasetid = value; }
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

            public DateTime? Datacreated
            {
                get { return _datacreated; }
                set { _datacreated = value; }
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

            public string Prj
            {
                get { return _prj; }
                set { _prj = value; }
            }

            public string Geotype
            {
                get { return _geotype; }
                set { _geotype = value; }
            }

            public int? Status
            {
                get { return _status; }
                set { _status = value; }
            }

            public string Unitcode
            {
                get { return _unitcode; }
                set { _unitcode = value; }
            }

            public int? Type
            {
                get { return _type; }
                set { _type = value; }
            }
        }

        #endregion
        public class PageSysfeatureclass
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region Sysfeatureclass Function 

    public class SysfeatureclassManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysfeatureclassVM.ItemSysfeatureclass, SysfeatureclassEntity>();
        });

        #endregion

        public SysfeatureclassManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysfeatureclassVM.ItemSysfeatureclass _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysfeatureclassEntity _SysfeatureclassEntity = configVMtoEntity.CreateMapper().Map<SysfeatureclassEntity>(_Model);
                    Insert(_SysfeatureclassEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysfeatureclassEntity Insert(SysfeatureclassEntity _SysfeatureclassEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysfeatureclassEntity, true);
                }
                return _SysfeatureclassEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysfeatureclassVM.ItemSysfeatureclass _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysfeatureclassEntity _SysfeatureclassEntity = configVMtoEntity.CreateMapper().Map<SysfeatureclassEntity>(_Model);
                    Update(_SysfeatureclassEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysfeatureclassEntity _SysfeatureclassEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysfeatureclassFields.Id == _SysfeatureclassEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysfeatureclassEntity, filter);
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
                    SysfeatureclassEntity _SysfeatureclassEntity = new SysfeatureclassEntity(id);
                    if (adapter.FetchEntity(_SysfeatureclassEntity))
                    {
                        adapter.DeleteEntity(_SysfeatureclassEntity);
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
                        SysfeatureclassEntity _SysfeatureclassEntity = new SysfeatureclassEntity(id);
                        if (adapter.FetchEntity(_SysfeatureclassEntity))
                        {
                            adapter.DeleteEntity(_SysfeatureclassEntity);
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

        public SysfeatureclassEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysfeatureclassEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysfeatureclassFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysfeatureclassEntity());
        }

        public async Task<ApiResponse<SysfeatureclassVM.ItemSysfeatureclass>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysfeatureclassEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysfeatureclassVM.ItemSysfeatureclass toReturn = mapper.Map<SysfeatureclassVM.ItemSysfeatureclass>(_entity);
                        return ApiResponse<SysfeatureclassVM.ItemSysfeatureclass>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysfeatureclassVM.ItemSysfeatureclass>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysfeatureclassEntity> _Collection = SelectAll();
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysfeatureclassEntity> SelectAll()
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {

                var parameters = new QueryParameters()
                {
                    CollectionToFetch = _Collection,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectBynameAsync(string _name)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassEntity> _Collection = SelectByname(_name);
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectBytablenameAsync(string _tablename)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassEntity> _Collection = SelectBytablename(_tablename);
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectBydatasetidAsync(int? _datasetid)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassEntity> _Collection = SelectBydatasetid(_datasetid);
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectByconfigAsync(int? _config)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassEntity> _Collection = SelectByconfig(_config);
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectByusercreatedAsync(int? _usercreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassEntity> _Collection = SelectByusercreated(_usercreated);
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectBydatacreatedAsync(DateTime? _datacreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassEntity> _Collection = SelectBydatacreated(_datacreated);
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectBydatemodifiedAsync(DateTime? _datemodified)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassEntity> _Collection = SelectBydatemodified(_datemodified);
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectByusermodifiedAsync(int? _usermodified)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassEntity> _Collection = SelectByusermodified(_usermodified);
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectBydescriptionAsync(string _description)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassEntity> _Collection = SelectBydescription(_description);
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectByprjAsync(string _prj)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassEntity> _Collection = SelectByprj(_prj);
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectBygeotypeAsync(string _geotype)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassEntity> _Collection = SelectBygeotype(_geotype);
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectBystatusAsync(int? _status)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassEntity> _Collection = SelectBystatus(_status);
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectByunitcodeAsync(string _unitcode)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfeatureclassEntity> _Collection = SelectByunitcode(_unitcode);
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysfeatureclassEntity> SelectByname(string _name)
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassFields.Name == _name);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassEntity> SelectBytablename(string _tablename)
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassFields.Tablename == _tablename);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassEntity> SelectBydatasetid(int? _datasetid)
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassFields.Datasetid == _datasetid);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassEntity> SelectByconfig(int? _config)
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassFields.Config == _config);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassEntity> SelectByusercreated(int? _usercreated)
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassFields.Usercreated == _usercreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassEntity> SelectBydatacreated(DateTime? _datacreated)
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassFields.Datacreated == _datacreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassEntity> SelectBydatemodified(DateTime? _datemodified)
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassFields.Datemodified == _datemodified);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassEntity> SelectByusermodified(int? _usermodified)
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassFields.Usermodified == _usermodified);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassEntity> SelectBydescription(string _description)
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassFields.Description == _description);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassEntity> SelectByprj(string _prj)
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassFields.Prj == _prj);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassEntity> SelectBygeotype(string _geotype)
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassFields.Geotype == _geotype);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassEntity> SelectBystatus(int? _status)
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassFields.Status == _status);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfeatureclassEntity> SelectByunitcode(string _unitcode)
        {
            EntityCollection<SysfeatureclassEntity> _Collection = new EntityCollection<SysfeatureclassEntity>(new SysfeatureclassEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfeatureclassFields.Unitcode == _unitcode);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysfeatureclassVM.ItemSysfeatureclass>>>> SelectPaging(SysfeatureclassVM.PageSysfeatureclass _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysfeatureclassFields.Name).Like(_model.strKey)
                            .Or(SysfeatureclassFields.Tablename.Like(_model.strKey)).Or(SysfeatureclassFields.Description.Like(_model.strKey)).Or(SysfeatureclassFields.Prj.Like(_model.strKey)).Or(SysfeatureclassFields.Geotype.Like(_model.strKey)).Or(SysfeatureclassFields.Unitcode.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysfeatureclassEntity>();
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
                        SortExpression sort = new SortExpression(SysfeatureclassFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysfeatureclassVM.ItemSysfeatureclass>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysfeatureclassVM.ItemSysfeatureclass>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysfeatureclassVM.ItemSysfeatureclass>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassFields.Name == _name);
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassFields.Tablename == _tablename);
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


        public DataTable SelectBydatasetidRDT(int? _datasetid)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassFields.Datasetid == _datasetid);
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassFields.Config == _config);
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassFields.Usercreated == _usercreated);
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


        public DataTable SelectBydatacreatedRDT(DateTime? _datacreated)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassFields.Datacreated == _datacreated);
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassFields.Datemodified == _datemodified);
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassFields.Usermodified == _usermodified);
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassFields.Description == _description);
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


        public DataTable SelectByprjRDT(string _prj)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassFields.Prj == _prj);
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


        public DataTable SelectBygeotypeRDT(string _geotype)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassFields.Geotype == _geotype);
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


        public DataTable SelectBystatusRDT(int? _status)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassFields.Status == _status);
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
                EntityCollection _Collection = new EntityCollection(new SysfeatureclassEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfeatureclassFields.Unitcode == _unitcode);
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

