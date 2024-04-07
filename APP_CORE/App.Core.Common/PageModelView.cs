using System.Data;

namespace App.Core.Common;

public class PageModel
{
    public string Search { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string Condition { get; set; }
    public string SortColumn { get; set; }

    public string SortExpression { get; set; }

    //Status chỉ dùng cho ThongtinchungEntity
    public string Status { get; set; }

    //Filter theo 1 trường dùng cho trang detail Open Client 
    public string ColumnName { get; set; }
}

public class PageModelView<T>
{
    public T Data { get; set; }

    public DataTable DataReturn { get; set; }
    public int TotalRecord { get; set; }
    public int TotalPage { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int RecordsCount { get; set; }
}

public class PageModelView2<T>
{
    public T Data { get; set; }

    public int TotalRecord { get; set; }
    public int TotalPage { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int RecordsCount { get; set; }
}