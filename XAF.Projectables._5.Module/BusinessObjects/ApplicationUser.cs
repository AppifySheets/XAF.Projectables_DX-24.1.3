using System.Collections.ObjectModel;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using EntityFrameworkCore.Projectables;

namespace XAF.Projectables._5.Module.BusinessObjects;

[DefaultProperty(nameof(UserName))]
public class ApplicationUser : PermissionPolicyUser, ISecurityUserWithLoginInfo, ISecurityUserLockout {
    [Browsable(false)]
    public virtual int AccessFailedCount { get; set; }

    [Browsable(false)]
    public virtual DateTime LockoutEnd { get; set; }

    [Projectable] public int RolesCountProjectable => Roles.Count();
    public int RolesCount_NON_Projectable => Roles.Count();

    [Browsable(false)]
    [DevExpress.ExpressApp.DC.Aggregated]
    public virtual IList<ApplicationUserLoginInfo> UserLogins { get; set; } = new ObservableCollection<ApplicationUserLoginInfo>();

    IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => UserLogins.OfType<ISecurityUserLoginInfo>();

    ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey) {
        ApplicationUserLoginInfo result = ((IObjectSpaceLink)this).ObjectSpace.CreateObject<ApplicationUserLoginInfo>();
        result.LoginProviderName = loginProviderName;
        result.ProviderUserKey = providerUserKey;
        result.User = this;
        return result;
    }
}
