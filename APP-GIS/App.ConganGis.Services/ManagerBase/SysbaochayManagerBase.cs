
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

namespace App.CongAnGis.Services.ManagerBase
{
    #region Sysbaochay View 

    public class SysbaochayVM
    {
        #region Sysbaochay ItemSysbaochay 
        public class ItemSysbaochay
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
        }

        #endregion
        public class PageSysbaochay
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }

    }
    #endregion

    #region Sysbaochay Function 

    public class SysbaochayManagerBase
    {
        #region Config Automaper

        public MapperConfiguration configEntitytoVM = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>();
        });

        public MapperConfiguration configVMtoEntity = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SysbaochayVM.ItemSysbaochay, SysbaochayEntity>();
        });

        #endregion

        public SysbaochayManagerBase()
        {

        }


        public async Task<ApiResponse> InsertAsync(SysbaochayVM.ItemSysbaochay _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysbaochayEntity _SysbaochayEntity = configVMtoEntity.CreateMapper().Map<SysbaochayEntity>(_Model);
                    Insert(_SysbaochayEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public SysbaochayEntity Insert(SysbaochayEntity _SysbaochayEntity)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    adapter.SaveEntity(_SysbaochayEntity, true);
                }
                return _SysbaochayEntity;
            }
            catch (ORMEntityValidationException ex)
            {
                return null;
            }

        }


        #region Update

        public async Task<ApiResponse> UpdateAsync(SysbaochayVM.ItemSysbaochay _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysbaochayEntity _SysbaochayEntity = configVMtoEntity.CreateMapper().Map<SysbaochayEntity>(_Model);
                    Update(_SysbaochayEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }


        public bool Update(SysbaochayEntity _SysbaochayEntity)
        {
            bool toReturn = false;
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    IPredicateExpression _PredicateExpression = new PredicateExpression();
                    _PredicateExpression.Add(SysbaochayFields.Id == _SysbaochayEntity.Id);

                    filter.PredicateExpression.Add(_PredicateExpression);

                    adapter.UpdateEntitiesDirectly(_SysbaochayEntity, filter);
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
                    SysbaochayEntity _SysbaochayEntity = new SysbaochayEntity(id);
                    if (adapter.FetchEntity(_SysbaochayEntity))
                    {
                        adapter.DeleteEntity(_SysbaochayEntity);
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
                        SysbaochayEntity _SysbaochayEntity = new SysbaochayEntity(id);
                        if (adapter.FetchEntity(_SysbaochayEntity))
                        {
                            adapter.DeleteEntity(_SysbaochayEntity);
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

        public SysbaochayEntity SelectOne(int _id)
        {
            var toReturn = new EntityCollection<SysbaochayEntity>();
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                var parameters = new QueryParameters()
                {
                    CollectionToFetch = toReturn,
                    FilterToUse = SysbaochayFields.Id == _id,
                    CacheResultset = true,
                    CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                };
                adapter.FetchEntityCollection(parameters);
            }
            if (toReturn != null && toReturn.Items.Count > 0) return toReturn.Items[0];
            return (new SysbaochayEntity());
        }

        public async Task<ApiResponse<SysbaochayVM.ItemSysbaochay>> SelectOneAsync(int key)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        SysbaochayEntity _entity = SelectOne(key);
                        var mapper = new Mapper(configEntitytoVM);
                        SysbaochayVM.ItemSysbaochay toReturn = mapper.Map<SysbaochayVM.ItemSysbaochay>(_entity);
                        return ApiResponse<SysbaochayVM.ItemSysbaochay>.Generate(toReturn, GeneralCode.Success);
                    });
                }
                catch (ORMException ex)
                {
                    return ApiResponse<SysbaochayVM.ItemSysbaochay>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }


        #endregion


        #region Select by cloumn Return EntityCollection



        public async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysbaochayEntity> _Collection = SelectAll();
                    List<SysbaochayVM.ItemSysbaochay> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList();

                    return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public EntityCollection<SysbaochayEntity> SelectAll()
        {
            EntityCollection<SysbaochayEntity> _Collection = new EntityCollection<SysbaochayEntity>(new SysbaochayEntityFactory());
            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, null, 0, null);
            }
            return _Collection;
        }



        public async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> SelectByiddataAsync(int? _iddata)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaochayEntity> _Collection = SelectByiddata(_iddata);
                    List<SysbaochayVM.ItemSysbaochay> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList();

                    return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> SelectBycreatebyidAsync(int? _createbyid)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaochayEntity> _Collection = SelectBycreatebyid(_createbyid);
                    List<SysbaochayVM.ItemSysbaochay> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList();

                    return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> SelectBycreatebyAsync(object? _createby)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaochayEntity> _Collection = SelectBycreateby(_createby);
                    List<SysbaochayVM.ItemSysbaochay> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList();

                    return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> SelectBycreatedateAsync(DateTime? _createdate)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaochayEntity> _Collection = SelectBycreatedate(_createdate);
                    List<SysbaochayVM.ItemSysbaochay> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList();

                    return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> SelectByaddressAsync(string _address)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaochayEntity> _Collection = SelectByaddress(_address);
                    List<SysbaochayVM.ItemSysbaochay> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList();

                    return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> SelectByphonenumberAsync(string _phonenumber)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaochayEntity> _Collection = SelectByphonenumber(_phonenumber);
                    List<SysbaochayVM.ItemSysbaochay> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList();

                    return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> SelectByreasonfireAsync(object? _reasonfire)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaochayEntity> _Collection = SelectByreasonfire(_reasonfire);
                    List<SysbaochayVM.ItemSysbaochay> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList();

                    return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> SelectBystatusAsync(int? _status)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaochayEntity> _Collection = SelectBystatus(_status);
                    List<SysbaochayVM.ItemSysbaochay> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList();

                    return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> SelectByupdateatAsync(DateTime? _updateat)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaochayEntity> _Collection = SelectByupdateat(_updateat);
                    List<SysbaochayVM.ItemSysbaochay> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList();

                    return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> SelectBytablenameAsync(string _tablename)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaochayEntity> _Collection = SelectBytablename(_tablename);
                    List<SysbaochayVM.ItemSysbaochay> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList();

                    return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> SelectBylnglatAsync(string _lnglat)
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaochayEntity> _Collection = SelectBylnglat(_lnglat);
                    List<SysbaochayVM.ItemSysbaochay> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList();

                    return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(toReturn, GeneralCode.Success);

                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysbaochayVM.ItemSysbaochay>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }




        public EntityCollection<SysbaochayEntity> SelectByiddata(int? _iddata)
        {
            EntityCollection<SysbaochayEntity> _Collection = new EntityCollection<SysbaochayEntity>(new SysbaochayEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaochayFields.Iddata == _iddata);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaochayEntity> SelectBycreatebyid(int? _createbyid)
        {
            EntityCollection<SysbaochayEntity> _Collection = new EntityCollection<SysbaochayEntity>(new SysbaochayEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaochayFields.Createbyid == _createbyid);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaochayEntity> SelectBycreateby(object? _createby)
        {
            EntityCollection<SysbaochayEntity> _Collection = new EntityCollection<SysbaochayEntity>(new SysbaochayEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaochayFields.Createby == _createby);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaochayEntity> SelectBycreatedate(DateTime? _createdate)
        {
            EntityCollection<SysbaochayEntity> _Collection = new EntityCollection<SysbaochayEntity>(new SysbaochayEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaochayFields.Createdate == _createdate);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaochayEntity> SelectByaddress(string _address)
        {
            EntityCollection<SysbaochayEntity> _Collection = new EntityCollection<SysbaochayEntity>(new SysbaochayEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaochayFields.Address == _address);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaochayEntity> SelectByphonenumber(string _phonenumber)
        {
            EntityCollection<SysbaochayEntity> _Collection = new EntityCollection<SysbaochayEntity>(new SysbaochayEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaochayFields.Phonenumber == _phonenumber);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaochayEntity> SelectByreasonfire(object? _reasonfire)
        {
            EntityCollection<SysbaochayEntity> _Collection = new EntityCollection<SysbaochayEntity>(new SysbaochayEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaochayFields.Reasonfire == _reasonfire);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaochayEntity> SelectBystatus(int? _status)
        {
            EntityCollection<SysbaochayEntity> _Collection = new EntityCollection<SysbaochayEntity>(new SysbaochayEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaochayFields.Status == _status);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaochayEntity> SelectByupdateat(DateTime? _updateat)
        {
            EntityCollection<SysbaochayEntity> _Collection = new EntityCollection<SysbaochayEntity>(new SysbaochayEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaochayFields.Updateat == _updateat);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaochayEntity> SelectBytablename(string _tablename)
        {
            EntityCollection<SysbaochayEntity> _Collection = new EntityCollection<SysbaochayEntity>(new SysbaochayEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaochayFields.Tablename == _tablename);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        public EntityCollection<SysbaochayEntity> SelectBylnglat(string _lnglat)
        {
            EntityCollection<SysbaochayEntity> _Collection = new EntityCollection<SysbaochayEntity>(new SysbaochayEntityFactory());
            RelationPredicateBucket filter = new RelationPredicateBucket();

            IPredicateExpression _PredicateExpression = new PredicateExpression();
            _PredicateExpression.Add(SysbaochayFields.Shape == _lnglat);
            filter.PredicateExpression.Add(_PredicateExpression);

            using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
            {
                adapter.FetchEntityCollection(_Collection, filter, 0, null);
            }
            return _Collection;
        }




        #endregion


        #region Select by cloumn Return ApiResponse<DataTable>

        public async Task<ApiResponse<PageModelView<IEnumerable<SysbaochayVM.ItemSysbaochay>>>> SelectPaging(SysbaochayVM.PageSysbaochay _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysbaochayFields.Iddata).Like(_model.strKey)
                            .Or(SysbaochayFields.Address.Like(_model.strKey)).Or(SysbaochayFields.Phonenumber.Like(_model.strKey)).Or(SysbaochayFields.Tablename.Like(_model.strKey)).Or(SysbaochayFields.Shape.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysbaochayEntity>();
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
                        SortExpression sort = new SortExpression(SysbaochayFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysbaochayVM.ItemSysbaochay>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysbaochayVM.ItemSysbaochay>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysbaochayVM.ItemSysbaochay>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public DataTable SelectAllRDT()
        {
            try
            {
                DataTable toReturn = new DataTable();
                EntityCollection _Collection = new EntityCollection(new SysbaochayEntityFactory());
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
                EntityCollection _Collection = new EntityCollection(new SysbaochayEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaochayFields.Iddata == _iddata);
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
                EntityCollection _Collection = new EntityCollection(new SysbaochayEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaochayFields.Createbyid == _createbyid);
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
                EntityCollection _Collection = new EntityCollection(new SysbaochayEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaochayFields.Createby == _createby);
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
                EntityCollection _Collection = new EntityCollection(new SysbaochayEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaochayFields.Createdate == _createdate);
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
                EntityCollection _Collection = new EntityCollection(new SysbaochayEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaochayFields.Address == _address);
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
                EntityCollection _Collection = new EntityCollection(new SysbaochayEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaochayFields.Phonenumber == _phonenumber);
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
                EntityCollection _Collection = new EntityCollection(new SysbaochayEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaochayFields.Reasonfire == _reasonfire);
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
                EntityCollection _Collection = new EntityCollection(new SysbaochayEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaochayFields.Status == _status);
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
                EntityCollection _Collection = new EntityCollection(new SysbaochayEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaochayFields.Updateat == _updateat);
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
                EntityCollection _Collection = new EntityCollection(new SysbaochayEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaochayFields.Tablename == _tablename);
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
                EntityCollection _Collection = new EntityCollection(new SysbaochayEntityFactory());
                RelationPredicateBucket filter = new RelationPredicateBucket();

                IPredicateExpression _PredicateExpression = new PredicateExpression();
                _PredicateExpression.Add(SysbaochayFields.Shape == _lnglat);
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

