namespace App.Qtht.Services.Models;

public class SysAppModel
{
    public class AppAndCheckUserInAppModel
    {
        public int AppId { get; set; }
        public string DinhDanhApp { get; set; }
        public string TenApp { get; set; }
        public string MoTa { get; set; }
        public bool ExistRoleInUser { get; set; }
    }
}