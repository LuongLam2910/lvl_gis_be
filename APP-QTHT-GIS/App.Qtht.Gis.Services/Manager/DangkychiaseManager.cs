using System.Threading.Tasks;
using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace App.QTHTGis.Services.Manager;

public interface IDangkychiaseManager
{
    public Task<ApiResponse> Insert(DangkychiaseEntity entity);
}

internal class DangkychiaseManager : IDangkychiaseManager
{
    public async Task<ApiResponse> Insert(DangkychiaseEntity entity)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                if (entity != null) await adapter.SaveEntityAsync(entity);
                return GeneralCode.Success;
            }
            catch (ORMException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
    }
}