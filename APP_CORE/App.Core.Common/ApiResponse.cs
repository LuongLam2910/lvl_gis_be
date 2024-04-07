using System;

namespace App.Core.Common;

public class ApiResponse : ApiResponse<bool>
{
    private ApiResponse()
    {
    }

    protected ApiResponse(Enum code) : base(true, code)
    {
    }

    protected ApiResponse(Enum code, string message) : base(code, message)
    {
    }

    public new static ApiResponse Generate(Enum code)
    {
        return new ApiResponse(code);
    }

    public new static ApiResponse Generate(Enum code, string message)
    {
        return new ApiResponse(code, message);
    }

    public static implicit operator ApiResponse(Enum code)
    {
        return new ApiResponse(code);
    }
}

public class ApiResponse<TData>
{
    public ApiResponse()
    {
    }

    protected ApiResponse(TData data, Enum code, string message)
    {
        Code = code.GenerateString();
        Message = message;
        Data = data;
    }

    protected ApiResponse((TData data, Enum code, string message) data) : this(data.data, data.code, data.message)
    {
    }

    protected ApiResponse((TData data, Enum code) data) : this(data.data, data.code)
    {
    }

    protected ApiResponse(TData data, Enum code) : this(data, code, null)
    {
    }

    protected ApiResponse(TData data) : this(data, data != null ? GeneralCode.Success : GeneralCode.Error)
    {
    }

    protected ApiResponse(Enum code) : this(default, code)
    {
    }

    protected ApiResponse(Enum code, string message) : this(default, code, message)
    {
    }

    public string Code { get; set; }
    public string Message { get; set; }
    public TData Data { get; set; }

    public static ApiResponse<TData> Generate(TData data, Enum code, string message)
    {
        return new ApiResponse<TData>(data, code, message);
    }

    public static ApiResponse<TData> Generate(TData data, Enum code)
    {
        return new ApiResponse<TData>(data, code);
    }

    public static ApiResponse<TData> Generate(TData data)
    {
        return new ApiResponse<TData>(data);
    }

    public static ApiResponse<TData> Generate(Enum code)
    {
        return new ApiResponse<TData>(code);
    }

    public static ApiResponse<TData> Generate(Enum code, string message)
    {
        return new ApiResponse<TData>(code, message);
    }

    public static implicit operator ApiResponse<TData>((TData data, Enum code, string message) data)
    {
        return new ApiResponse<TData>(data);
    }

    public static implicit operator ApiResponse<TData>((TData data, Enum code) data)
    {
        return new ApiResponse<TData>(data);
    }

    public static implicit operator ApiResponse<TData>(TData data)
    {
        return new ApiResponse<TData>(data);
    }

    public static implicit operator ApiResponse<TData>(Enum code)
    {
        return new ApiResponse<TData>(code);
    }

    //public static implicit operator ApiResponse<TData>(StatusResult<TData> data)
    //{
    //    return new ApiResponse<TData>(data.Data, data.Status);
    //}
}