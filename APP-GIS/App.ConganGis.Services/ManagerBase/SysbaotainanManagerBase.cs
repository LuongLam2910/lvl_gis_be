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
using Microsoft.AspNetCore.SignalR;

namespace App.ConganGis.Services.ManagerBase
{
    #region Sysbaotainan View 

    public class SysbaotainanVM
    {
        #region Sysbaotainan ItemSysbaotainan 
        public class ItemSysbaotainan
        {
            private int _Id;
            private int? _iddata;
            private int? _createbyid;
            private object? _createby;
            private string _createdate;
            private string _address = string.Empty;
            private string _phonenumber = string.Empty;
            private object? _reasonfire;
            private int? _status;
            private string _updateat;
            private string _tablename = string.Empty;
            private string _shape = string.Empty;
            private string _icon = string.Empty;
            private string _mahuyen = string.Empty;
            private string _maxa = string.Empty;
            private int? _nguoitiepnhan ;
            public int Id //	#region PrimaryKey
            {
                get { return _Id; }
                set { _Id = value; }
            }

            public int? Iddata
            {
                get { return _iddata; }
                set { _iddata = value; }
            }

            public int? Createbyid
            {
                get { return _createbyid; }
                set { _createbyid = value; }
            }

            public object? Createby
            {
                get { return _createby; }
                set { _createby = value; }
            }

            public string Createdate
            {
                get { return _createdate; }
                set { _createdate = value; }
            }

            public string Address
            {
                get { return _address; }
                set { _address = value; }
            }

            public string Phonenumber
            {
                get { return _phonenumber; }
                set { _phonenumber = value; }
            }

            public object? Reasonfire
            {
                get { return _reasonfire; }
                set { _reasonfire = value; }
            }

            public int? Status
            {
                get { return _status; }
                set { _status = value; }
            }

            public string Updateat
            {
                get { return _updateat; }
                set { _updateat = value; }
            }

            public string Tablename
            {
                get { return _tablename; }
                set { _tablename = value; }
            }

            public string Shape
            {
                get { return _shape; }
                set { _shape = value; }
            }

            public string Icon
            {
                get { return _icon; }
                set { _icon = value; }
            }
            public string Mahuyen
            {
                get { return _mahuyen; }
                set { _mahuyen = value; }
            }
            public string Maxa
            {
                get { return _maxa; }
                set { _maxa = value; }
            }
            public int? Nguoitiepnhan
            {
                get { return _nguoitiepnhan; }
                set { _nguoitiepnhan = value; }
            }
        }

        #endregion
        public class PageSysbaotainan
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }
    }

    #endregion

    #region Sysbaotainan Function 

    public class SysbaotainanManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysbaotainanVM.ItemSysbaotainan, SysbaotainanEntity>();
        });

        #endregion

        public SysbaotainanManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysbaotainanVM.ItemSysbaotainan _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysbaotainanEntity _SysbaotainanEntity = configVMtoEntity.CreateMapper().Map<SysbaotainanEntity>(_Model);
                    Insert(_SysbaotainanEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysbaotainanEntity Insert(SysbaotainanEntity _SysbaotainanEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysbaotainanEntity, true);
                }
                return _SysbaotainanEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysbaotainanVM.ItemSysbaotainan _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysbaotainanEntity _SysbaotainanEntity = configVMtoEntity.CreateMapper().Map<SysbaotainanEntity>(_Model);
                    Update(_SysbaotainanEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysbaotainanEntity _SysbaotainanEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysbaotainanFields.Id == _SysbaotainanEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysbaotainanEntity, filter);
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
                    SysbaotainanEntity _SysbaotainanEntity = new SysbaotainanEntity(id);
                    if (adapter.FetchEntity(_SysbaotainanEntity))
                    {
                        adapter.DeleteEntity(_SysbaotainanEntity);
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
                        SysbaotainanEntity _SysbaotainanEntity = new SysbaotainanEntity(id);
                        if (adapter.FetchEntity(_SysbaotainanEntity))
                        {
                            adapter.DeleteEntity(_SysbaotainanEntity);
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

        public SysbaotainanEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysbaotainanEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysbaotainanFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysbaotainanEntity());
        }

        public async Task<ApiResponse<SysbaotainanVM.ItemSysbaotainan>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysbaotainanEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysbaotainanVM.ItemSysbaotainan toReturn = mapper.Map<SysbaotainanVM.ItemSysbaotainan>(_entity);
                        return ApiResponse<SysbaotainanVM.ItemSysbaotainan>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysbaotainanVM.ItemSysbaotainan>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysbaotainanEntity> _Collection = SelectAll();
                    List<SysbaotainanVM.ItemSysbaotainan> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList();

                    return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysbaotainanEntity> SelectAll()
        {
            EntityCollection<SysbaotainanEntity> _Collection = new EntityCollection<SysbaotainanEntity>(new SysbaotainanEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> SelectByiddataAsync(int? _iddata)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaotainanEntity> _Collection = SelectByiddata(_iddata);
                    List<SysbaotainanVM.ItemSysbaotainan> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList();

                    return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> SelectBycreatebyidAsync(int? _createbyid)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaotainanEntity> _Collection = SelectBycreatebyid(_createbyid);
                    List<SysbaotainanVM.ItemSysbaotainan> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList();

                    return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> SelectBycreatebyAsync(object? _createby)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaotainanEntity> _Collection = SelectBycreateby(_createby);
                    List<SysbaotainanVM.ItemSysbaotainan> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList();

                    return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> SelectBycreatedateAsync(DateTime? _createdate)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaotainanEntity> _Collection = SelectBycreatedate(_createdate);
                    List<SysbaotainanVM.ItemSysbaotainan> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList();

                    return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> SelectByaddressAsync(string _address)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaotainanEntity> _Collection = SelectByaddress(_address);
                    List<SysbaotainanVM.ItemSysbaotainan> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList();

                    return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> SelectByphonenumberAsync(string _phonenumber)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaotainanEntity> _Collection = SelectByphonenumber(_phonenumber);
                    List<SysbaotainanVM.ItemSysbaotainan> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList();

                    return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> SelectByreasonfireAsync(object? _reasonfire)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaotainanEntity> _Collection = SelectByreasonfire(_reasonfire);
                    List<SysbaotainanVM.ItemSysbaotainan> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList();

                    return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> SelectBystatusAsync(int? _status)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaotainanEntity> _Collection = SelectBystatus(_status);
                    List<SysbaotainanVM.ItemSysbaotainan> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList();

                    return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> SelectByupdateatAsync(DateTime? _updateat)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaotainanEntity> _Collection = SelectByupdateat(_updateat);
                    List<SysbaotainanVM.ItemSysbaotainan> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList();

                    return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> SelectBytablenameAsync(string _tablename)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaotainanEntity> _Collection = SelectBytablename(_tablename);
                    List<SysbaotainanVM.ItemSysbaotainan> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList();

                    return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> SelectBylnglatAsync(string _lnglat)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaotainanEntity> _Collection = SelectBylnglat(_lnglat);
                    List<SysbaotainanVM.ItemSysbaotainan> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList();

                    return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysbaotainanEntity> SelectByiddata(int? _iddata)
        {
            EntityCollection<SysbaotainanEntity> _Collection = new EntityCollection<SysbaotainanEntity>(new SysbaotainanEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaotainanFields.Iddata == _iddata);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaotainanEntity> SelectBycreatebyid(int? _createbyid)
        {
            EntityCollection<SysbaotainanEntity> _Collection = new EntityCollection<SysbaotainanEntity>(new SysbaotainanEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaotainanFields.Createbyid == _createbyid);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaotainanEntity> SelectBycreateby(object? _createby)
        {
            EntityCollection<SysbaotainanEntity> _Collection = new EntityCollection<SysbaotainanEntity>(new SysbaotainanEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaotainanFields.Createby == _createby);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaotainanEntity> SelectBycreatedate(DateTime? _createdate)
        {
            EntityCollection<SysbaotainanEntity> _Collection = new EntityCollection<SysbaotainanEntity>(new SysbaotainanEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaotainanFields.Createdate == _createdate);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaotainanEntity> SelectByaddress(string _address)
        {
            EntityCollection<SysbaotainanEntity> _Collection = new EntityCollection<SysbaotainanEntity>(new SysbaotainanEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaotainanFields.Address == _address);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaotainanEntity> SelectByphonenumber(string _phonenumber)
        {
            EntityCollection<SysbaotainanEntity> _Collection = new EntityCollection<SysbaotainanEntity>(new SysbaotainanEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaotainanFields.Phonenumber == _phonenumber);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaotainanEntity> SelectByreasonfire(object? _reasonfire)
        {
            EntityCollection<SysbaotainanEntity> _Collection = new EntityCollection<SysbaotainanEntity>(new SysbaotainanEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaotainanFields.Reasonfire == _reasonfire);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaotainanEntity> SelectBystatus(int? _status)
        {
            EntityCollection<SysbaotainanEntity> _Collection = new EntityCollection<SysbaotainanEntity>(new SysbaotainanEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaotainanFields.Status == _status);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaotainanEntity> SelectByupdateat(DateTime? _updateat)
        {
            EntityCollection<SysbaotainanEntity> _Collection = new EntityCollection<SysbaotainanEntity>(new SysbaotainanEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaotainanFields.Updateat == _updateat);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaotainanEntity> SelectBytablename(string _tablename)
        {
            EntityCollection<SysbaotainanEntity> _Collection = new EntityCollection<SysbaotainanEntity>(new SysbaotainanEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaotainanFields.Tablename == _tablename);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaotainanEntity> SelectBylnglat(string _lnglat)
        {
            EntityCollection<SysbaotainanEntity> _Collection = new EntityCollection<SysbaotainanEntity>(new SysbaotainanEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaotainanFields.Shape == _lnglat);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysbaotainanVM.ItemSysbaotainan>>>> SelectPaging(SysbaotainanVM.PageSysbaotainan _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysbaotainanFields.Iddata).Like(_model.strKey)
                            .Or(SysbaotainanFields.Address.Like(_model.strKey)).Or(SysbaotainanFields.Phonenumber.Like(_model.strKey)).Or(SysbaotainanFields.Tablename.Like(_model.strKey)).Or(SysbaotainanFields.Shape.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysbaotainanEntity>();
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
                        SortExpression sort = new SortExpression(SysbaotainanFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysbaotainanVM.ItemSysbaotainan>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysbaotainanVM.ItemSysbaotainan>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysbaotainanVM.ItemSysbaotainan>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysbaotainanEntityFactory());
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


        public DataTable SelectByiddataRDT(int? _iddata)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysbaotainanEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaotainanFields.Iddata == _iddata);
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


        public DataTable SelectBycreatebyidRDT(int? _createbyid)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysbaotainanEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaotainanFields.Createbyid == _createbyid);
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


        public DataTable SelectBycreatebyRDT(object? _createby)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysbaotainanEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaotainanFields.Createby == _createby);
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
                EntityCollection _Collection = new EntityCollection(new SysbaotainanEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaotainanFields.Createdate == _createdate);
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


        public DataTable SelectByaddressRDT(string _address)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysbaotainanEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaotainanFields.Address == _address);
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


        public DataTable SelectByphonenumberRDT(string _phonenumber)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysbaotainanEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaotainanFields.Phonenumber == _phonenumber);
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


        public DataTable SelectByreasonfireRDT(object? _reasonfire)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysbaotainanEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaotainanFields.Reasonfire == _reasonfire);
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
                EntityCollection _Collection = new EntityCollection(new SysbaotainanEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaotainanFields.Status == _status);
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


        public DataTable SelectByupdateatRDT(DateTime? _updateat)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysbaotainanEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaotainanFields.Updateat == _updateat);
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
                EntityCollection _Collection = new EntityCollection(new SysbaotainanEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaotainanFields.Tablename == _tablename);
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


        public DataTable SelectBylnglatRDT(string _lnglat)
        {
            try
            {

                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysbaotainanEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaotainanFields.Shape == _lnglat);
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
