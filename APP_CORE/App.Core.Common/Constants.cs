namespace App.Core.Common;

public static class Constants
{
    public static class FileBase
    {
        public static readonly string File = "FileProjectBT";
        public static readonly string FileLink = "/File";
        public static readonly string LogPath = "FileProjectBT/Logs";
    }

    public static class ClaimTypes
    {
        public static readonly string UserId = "UserId";
        public static readonly string AppId = "AppId";
        public static readonly string UserName = "UserName";
        public static readonly string FullName = "FullName";
        public static readonly string UnitCode = "UnitCode";
        public static readonly string UnitName = "UnitName";
        public static readonly string Dienthoai = "Dienthoai";
        public static readonly string Cccd = "Cccd";
        public static readonly string Tag = "Tag";
        public static readonly string UserType = "UserType";
    }

    public static class UserAction
    {
        public static readonly string Insert = "Insert";
        public static readonly string Update = "Update";
        public static readonly string Delete = "Delete";
        public static readonly string LogIn = "LogIn";
        public static readonly string LogOut = "LogOut";
        public static readonly string Sync = "Sync";
    }

    public static class SystemMessage
    {
        public static readonly string SystemHasError = "Có lỗi hệ thống! Vui lòng thử lại";
    }
}