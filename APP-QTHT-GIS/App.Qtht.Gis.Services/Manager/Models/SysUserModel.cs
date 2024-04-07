using System;
using System.Collections.Generic;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace App.Qtht.Services.Models;

public class SysUserModel
{
    public class ChangePasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ResetPasswordModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserUpdateDetailViewModel
    {
        public SysuserEntity User { get; set; }
        public List<UserGroupModel> UserGroups { get; set; }
        public List<UserUnitViewModel> Units { get; set; }
        public List<UserTuDienViewModel> TuDien { get; set; }
        public List<UserViewAppModel> App { get; set; }
        public DmChucVuModel.ChucVuSelectModel ChucVu { get; set; }

        public class UserTuDienViewModel
        {
            public int TuDienId { get; set; }
            public string TenTuDien { get; set; }
        }

        public class UserUnitViewModel
        {
            public string UnitCode { get; set; }
            public string UnitName { get; set; }
        }

        public class UserViewAppModel
        {
            public int Id { get; set; }
            public string AppName { get; set; }
            public decimal AppId { get; set; }
        }


        public class UserGroupModel
        {
            public int UserId { get; set; }
            public int GroupId { get; set; }
        }
    }

    public class RegisterUserAppCongAnModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string UnitCode { get; set; }
        public bool TrangThai { get; set; }
        public int? Type { get; set; } // Check loại đăng ký\
        public bool IsNew { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }
    }

    public class UserInsertViewModel
    {
        /**
             * 2021-05-07 - Unused, remove logic
            */
        //public IEnumerable<UnitForUserModel> ListUnit { get; set; }
        public IEnumerable<AppForUserModel> ListApp { get; set; }

        /**
             * 2021-05-07 - Unused, remove logic
            */
        //public IEnumerable<TudienForUserModel> ListTudien { get; set; }
        public IEnumerable<GroupForUserModel> ListGroup { get; set; }

        public class UnitForUserModel
        {
            public string UnitCode { get; set; }
            public string UnitName { get; set; }
        }

        public class AppForUserModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string DinhDanhApp { get; set; }
        }

        public class TudienForUserModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class GroupForUserModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Appid { get; set; }
        }
    }

    public class InsertUpdateModel
    {
        public SysuserEntity UserData { get; set; }
        public List<int> ListGroupId { get; set; }
    }

    public class SysuserUpdateModel
    {
        public string appid { get; set; }
        public string email { get; set; }
        public string fullname { get; set; }
        public int? idchucvu { get; set; }
        public string password { get; set; }
        public string phongban { get; set; }
        public short trangthai { get; set; }
        public string unitcode { get; set; }
        public int userid { get; set; }
        public string username { get; set; }
        public string phone { get; set; }
        public short? loaiuser { get; set; }
        public string diachi { get; set; }
    }

    public class UpdateUserModel
    {
        public SysuserUpdateModel UserData { get; set; }
        public List<int> ListGroupId { get; set; }
    }

    public class SelectOneUser
    {
        public SysuserEntity SelectUserData { get; set; }
        public EntityCollection<SysgroupEntity> SelectListGroup { get; set; }
    }

    public class SelectByAppIdModel
    {
        public string AppId { get; set; }
        public int UserId { get; set; }
    }

    public class SelectListUserGroup
    {
        public int UserId { get; set; }
        public List<int> ListGroupId { get; set; }
    }

    public class UserDetailModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Fullname { get; set; }

        public string Phone { get; set; }

        public string Trangthai { get; set; }

        public string AppName { get; set; }

        public string UnitName { get; set; }

        public string PhongBanName { get; set; }

        public string ChucVuName { get; set; }
        public short? Loaiuser { get; set; }
    }

    public class DetailUserModel
    {
        public long Userid { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Fullname { get; set; }

        public string Phone { get; set; }

        public string Trangthai { get; set; }

        public string AppName { get; set; }

        public string UnitName { get; set; }

        public string PhongBanName { get; set; }

        public string ChucVuName { get; set; }

        public string DiaChi { get; set; }

        public string Unitcode { get; set; }

        public string Password { get; set; }

        public string imgAvatar { get; set; }

        public DateTime? NgaySinh { get; set; }
    }

    public class UserSelect
    {
        public long Userid { get; set; }
        public string Unitcode { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Phongban { get; set; }
    }

    public class UserInfoModel
    {
        public long Userid { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Unitcode { get; set; }
        public string Username { get; set; }
        public string Appid { get; set; }
        public int? Loaiuser { get; set; }
        public long? IdChucvu { get; set; }
    }

    public class CanBoSelectModel
    {
        public long Value { get; set; }

        public string Name { get; set; }
    }

    public class CanBoSelectAndUserLevelModel
    {
        public int? Cap { get; set; }

        public List<CanBoSelectModel> lstCanBoCapTren { get; set; }
    }
}

public class DoiMatKhauModel
{
    public long Iduser { get; set; }
    public string Oldpassword { get; set; }
    public string Newpassword { get; set; }
    public string Confirmpassword { get; set; }
}

public class CongAnList
{
    public string Unitcode { get; set; }
    public string Ten { get; set; }
    public string MaDonViCongAnCha { get; set; }
    public string TenDonViCongAnCha { get; set; }
}

public class FileUserAvatarModel
{  
    public string Imagavatar { get; set; }
    public string Fullname { get; set; }
    public string folder { get; set; }
    public string type { get; set; } // Type = "Icon" upload icon ; type = "Image" upload image,video
}