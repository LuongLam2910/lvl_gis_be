
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
    #region Sysfield View 

    public class SysfieldVM
    {
        #region Sysfield ItemSysfield 
        public class ItemSysfield
        {
            private int _Id;
            private string _name = string.Empty;
            private string _datatype = string.Empty;
            private string _datalength = string.Empty;
            private int? _show ;
            private string _fieldname = string.Empty;
            private int? _featureclass;
            private string _config = string.Empty;
            private int? _usercreated;
            private DateTime? _datecreated;
            private DateTime? _datemodified;
            private int? _usermodified;
            private string _unitcode = string.Empty;
            private int? _status;

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

            public string Datatype
            {
                get { return _datatype; }
                set { _datatype = value; }
            }

            public string Datalength
            {
                get { return _datalength; }
                set { _datalength = value; }
            }

            public int? Show
            {
                get { return _show; }
                set { _show = value; }
            }

            public string Fieldname
            {
                get { return _fieldname; }
                set { _fieldname = value; }
            }

            public int? Featureclass
            {
                get { return _featureclass; }
                set { _featureclass = value; }
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

            public string Unitcode
            {
                get { return _unitcode; }
                set { _unitcode = value; }
            }

            public int? Status
            {
                get { return _status; }
                set { _status = value; }
            }
        }

        #endregion
        public class PageSysfield
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region Sysfield Function 

    public class SysfieldManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysfieldEntity, SysfieldVM.ItemSysfield>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysfieldVM.ItemSysfield, SysfieldEntity>();
        });

        #endregion

        public SysfieldManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysfieldVM.ItemSysfield _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysfieldEntity _SysfieldEntity = configVMtoEntity.CreateMapper().Map<SysfieldEntity>(_Model);
                    Insert(_SysfieldEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysfieldEntity Insert(SysfieldEntity _SysfieldEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysfieldEntity, true);
                }
                return _SysfieldEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysfieldVM.ItemSysfield _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysfieldEntity _SysfieldEntity = configVMtoEntity.CreateMapper().Map<SysfieldEntity>(_Model);
                    Update(_SysfieldEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysfieldEntity _SysfieldEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysfieldFields.Id == _SysfieldEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysfieldEntity, filter);
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
                    SysfieldEntity _SysfieldEntity = new SysfieldEntity(id);
                    if (adapter.FetchEntity(_SysfieldEntity))
                    {
                        adapter.DeleteEntity(_SysfieldEntity);
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
                        SysfieldEntity _SysfieldEntity = new SysfieldEntity(id);
                        if (adapter.FetchEntity(_SysfieldEntity))
                        {
                            adapter.DeleteEntity(_SysfieldEntity);
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

        public SysfieldEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysfieldEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysfieldFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysfieldEntity());
        }

        public async Task<ApiResponse<SysfieldVM.ItemSysfield>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysfieldEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysfieldVM.ItemSysfield toReturn = mapper.Map<SysfieldVM.ItemSysfield>(_entity);
                        return ApiResponse<SysfieldVM.ItemSysfield>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysfieldVM.ItemSysfield>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysfieldEntity> _Collection = SelectAll();
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysfieldEntity> SelectAll()
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectBynameAsync(string _name)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfieldEntity> _Collection = SelectByname(_name);
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectBydatatypeAsync(string _datatype)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfieldEntity> _Collection = SelectBydatatype(_datatype);
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectBydatalengthAsync(string _datalength)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfieldEntity> _Collection = SelectBydatalength(_datalength);
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectByshowAsync(string _show)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfieldEntity> _Collection = SelectByshow(_show);
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectByfieldnameAsync(string _fieldname)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfieldEntity> _Collection = SelectByfieldname(_fieldname);
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectByfeatureclassAsync(int? _featureclass)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfieldEntity> _Collection = SelectByfeatureclass(_featureclass);
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectByconfigAsync(int? _config)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfieldEntity> _Collection = SelectByconfig(_config);
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectByusercreatedAsync(int? _usercreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfieldEntity> _Collection = SelectByusercreated(_usercreated);
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectBydatecreatedAsync(DateTime? _datecreated)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfieldEntity> _Collection = SelectBydatecreated(_datecreated);
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectBydatemodifiedAsync(DateTime? _datemodified)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfieldEntity> _Collection = SelectBydatemodified(_datemodified);
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectByusermodifiedAsync(int? _usermodified)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfieldEntity> _Collection = SelectByusermodified(_usermodified);
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectByunitcodeAsync(string _unitcode)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfieldEntity> _Collection = SelectByunitcode(_unitcode);
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> SelectBystatusAsync(int? _status)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysfieldEntity> _Collection = SelectBystatus(_status);
                    List<SysfieldVM.ItemSysfield> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList();

                    return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfieldVM.ItemSysfield>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysfieldEntity> SelectByname(string _name)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfieldFields.Name == _name);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfieldEntity> SelectBydatatype(string _datatype)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfieldFields.Datatype == _datatype);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfieldEntity> SelectBydatalength(string _datalength)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfieldFields.Datalength == _datalength);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfieldEntity> SelectByshow(string _show)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfieldFields.Show == _show);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfieldEntity> SelectByfieldname(string _fieldname)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfieldFields.Fieldname == _fieldname);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfieldEntity> SelectByfeatureclass(int? _featureclass)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfieldFields.Featureclass == _featureclass);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfieldEntity> SelectByconfig(int? _config)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfieldFields.Config == _config);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfieldEntity> SelectByusercreated(int? _usercreated)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfieldFields.Usercreated == _usercreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfieldEntity> SelectBydatecreated(DateTime? _datecreated)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfieldFields.Datecreated == _datecreated);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfieldEntity> SelectBydatemodified(DateTime? _datemodified)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfieldFields.Datemodified == _datemodified);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfieldEntity> SelectByusermodified(int? _usermodified)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfieldFields.Usermodified == _usermodified);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfieldEntity> SelectByunitcode(string _unitcode)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfieldFields.Unitcode == _unitcode);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysfieldEntity> SelectBystatus(int? _status)
        {
            EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>(new SysfieldEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysfieldFields.Status == _status);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysfieldVM.ItemSysfield>>>> SelectPaging(SysfieldVM.PageSysfield _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysfieldFields.Name).Like(_model.strKey)
                            .Or(SysfieldFields.Datatype.Like(_model.strKey)).Or(SysfieldFields.Datalength.Like(_model.strKey)).Or(SysfieldFields.Show.Like(_model.strKey)).Or(SysfieldFields.Fieldname.Like(_model.strKey)).Or(SysfieldFields.Unitcode.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysfieldEntity>();
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
                        SortExpression sort = new SortExpression(SysfieldFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysfieldVM.ItemSysfield>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysfieldVM.ItemSysfield>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysfieldEntity, SysfieldVM.ItemSysfield>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysfieldVM.ItemSysfield>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
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
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfieldFields.Name == _name);
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


        public DataTable SelectBydatatypeRDT(string _datatype)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfieldFields.Datatype == _datatype);
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


        public DataTable SelectBydatalengthRDT(string _datalength)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfieldFields.Datalength == _datalength);
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


        public DataTable SelectByshowRDT(string _show)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfieldFields.Show == _show);
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


        public DataTable SelectByfieldnameRDT(string _fieldname)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfieldFields.Fieldname == _fieldname);
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


        public DataTable SelectByfeatureclassRDT(int? _featureclass)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfieldFields.Featureclass == _featureclass);
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
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfieldFields.Config == _config);
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
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfieldFields.Usercreated == _usercreated);
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
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfieldFields.Datecreated == _datecreated);
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
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfieldFields.Datemodified == _datemodified);
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
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfieldFields.Usermodified == _usermodified);
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
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfieldFields.Unitcode == _unitcode);
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
                EntityCollection _Collection = new EntityCollection(new SysfieldEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysfieldFields.Status == _status);
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

