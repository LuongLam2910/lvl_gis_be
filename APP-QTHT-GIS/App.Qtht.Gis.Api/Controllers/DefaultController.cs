using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Services.Manager;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.QTHTGis.Api.Controllers;

[EnableCors("AllowOrigin")]
public class DefaultController<TEntity> : Controller where TEntity : CommonEntityBase, new()
{
    private readonly DefaultManager<TEntity> _manager;

    public DefaultController()
    {
        _manager = new DefaultManager<TEntity>();
    }

    [HttpPost]
    [Route("Paging")]
    public async virtual Task<ApiResponse<PageModelView<IEnumerable<object>>>> PostPaging(
        [FromBody] PageModel pageModel)
    {
        return await _manager.Paging(pageModel, SetSelectModelPaging(), SetFieldConditionPaging(), SetSortForPaging(),
            SetFilterPaging());
    }

    protected virtual SortClause SetSortForPaging()
    {
        EntityField2 fieldSort = SetFieldKeyUpdateOrDelete();
        if (!Equals(fieldSort, null))
        {
            return fieldSort | SortOperator.Descending;
        }

        return null;
    }

    protected virtual EntityField2 SetFieldConditionPaging()
    {
        return null;
    }

    protected virtual Func<TEntity, object> SetSelectModelPaging()
    {
        return null;
    }

    protected virtual EntityField2[] SetFilterPaging()
    {
        return null;
    }

    [HttpGet]
    [Route("SelectOne/{key}")]
    public async virtual Task<ApiResponse<TEntity>> SelectOne([FromRoute] int key)
    {
        return await _manager.SelectOne(SetColumnKeySelectOne(), key);
    }

    protected virtual EntityField2 SetColumnKeySelectOne()
    {
        return null;
    }

    [HttpGet]
    [Route("SelectAll")]
    public async virtual Task<ApiResponse<IEnumerable<object>>> SelectAll()
    {
        return await _manager.SelectAll(SetModelSelectAll(), SetSortSelectAll());
    }

    protected virtual SortClause SetSortSelectAll()
    {
        EntityField2 fieldSort = SetFieldKeyUpdateOrDelete();
        if (!Equals(fieldSort, null))
        {
            return fieldSort | SortOperator.Descending;
        }

        return null;
    }

    protected virtual Func<TEntity, object> SetModelSelectAll()
    {
        return null;
    }

    [HttpPost]
    [Route("Insert")]
    public async virtual Task<ApiResponse> Insert([FromBody] TEntity entity)
    {
        return await _manager.Insert(entity, CheckDuplicateInsert(entity));
    }

    protected virtual Predicate CheckDuplicateInsert(TEntity entity)
    {
        return null;
    }

    [HttpPut]
    [Route("Update")]
    public async virtual Task<ApiResponse> PostUpdate([FromBody] TEntity entity)
    {
        TEntity targetUpdate = SetValueEntityUpdate(entity);
        return await _manager.Update(targetUpdate, CheckExistForUpdate(entity));
    }

    protected virtual Predicate CheckExistForUpdate(TEntity entity)
    {
        return null;
    }

    protected virtual TEntity SetValueEntityUpdate(TEntity source)
    {
        return source;
    }

    protected virtual EntityField2 SetFieldKeyUpdateOrDelete()
    {
        return null;
    }

    [HttpDelete]
    [Route("Delete/{key}")]
    public async virtual Task<ApiResponse> PostDelete([FromRoute] int key)
    {
        return await _manager.Delete(SetFieldKeyUpdateOrDelete(), key);
    }
}