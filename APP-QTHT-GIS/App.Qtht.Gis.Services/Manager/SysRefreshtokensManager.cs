using System;
using System.Threading.Tasks;
using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using static App.Core.Common.Constants;

namespace App.QTHTGis.Services.Manager;

public interface ISysRefreshtokensManager
{
    Task<ApiResponse> DeleteByUserId(int userId);
}

public class SysRefreshtokensManager : ISysRefreshtokensManager
{
    private readonly ICurrentContext _currentContext;

    public SysRefreshtokensManager(ICurrentContext currentContext)
    {
        _currentContext = currentContext;
    }

    public async Task<ApiResponse> DeleteByUserId(int userId)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var unitOfWork2 = new UnitOfWork2();

                var entities = new EntityCollection<SysRefreshtokenEntity>();
                adapter.FetchEntityCollection(entities,
                    new RelationPredicateBucket(SysRefreshtokenFields.Userid == userId));

                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysRefreshtokenEntity),
                    new RelationPredicateBucket(SysRefreshtokenFields.Userid == userId));

                await unitOfWork2.CommitAsync(adapter, true);

                entities.LogObject(_currentContext.UserId, UserAction.Delete, "Xóa Refreshtoken theo UserId",
                    _currentContext.IpClient, _currentContext.UserName, _currentContext.AppId);

                return GeneralCode.Success;
            }
        }
        catch (Exception ex)
        {
            return ApiResponse.Generate(GeneralCode.Error, ex.Message);
        }
    }
}