
using App.QTHTGis.Dal.EntityClasses;

namespace App.Qtht.Services.Authorize;

public class TokenForRefresh
{
    public string Tag { get; set; }
    public string RefreshToken { get; set; }
}

public class RefreshToken : SysRefreshtokenEntity
{
    public string GenerateToken { get; set; }

    public SysRefreshtokenEntity GetIntanceEntity()
    {
        return this;
    }
}