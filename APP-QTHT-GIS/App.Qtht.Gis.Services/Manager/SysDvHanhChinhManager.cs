using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;

namespace App.QTHTGis.Services.Manager;

public interface ISysDvHanhChinhManager
{
    Task<ApiResponse<IEnumerable<object>>> SelectMaAndTenDbhc();
}

public class SysDvHanhChinhManager : ISysDvHanhChinhManager
{
    public async Task<ApiResponse<IEnumerable<object>>> SelectMaAndTenDbhc()
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var filter = new RelationPredicateBucket((SysdvhanhchinhFields.MaDbhcCha == "24") &
                                                             (SysdvhanhchinhFields.CapDbhc == 3));
                    var collection = new EntityCollection<SysdvhanhchinhEntity>();
                    var collectionXa = new EntityCollection<SysdvhanhchinhEntity>();
                    SortExpression sortExpression = sortExpression =
                        new SortExpression(SysdvhanhchinhFields.MaDbhc | SortOperator.Ascending);
                    adapter.FetchEntityCollection(collection, filter, 0, sortExpression);
                    var filterXa = new RelationPredicateBucket(
                        (Predicate)SysdvhanhchinhFields.MaDbhcCha.In(collection.Select(x => x.MaDbhc)) &
                        (SysdvhanhchinhFields.CapDbhc == 4));
                    adapter.FetchEntityCollection(collectionXa, filterXa, 0, sortExpression);
                    var result = collection.Concat(collectionXa).Select(x => new
                    {
                        x.MaDbhc,
                        x.MaDbhcCha,
                        x.TenDbhc,
                        x.CapDbhc
                    });
                    return ApiResponse<IEnumerable<object>>.Generate(result, GeneralCode.Success);
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<object>>.Generate(GeneralCode.Error, ex.Message);
        }
    }
}