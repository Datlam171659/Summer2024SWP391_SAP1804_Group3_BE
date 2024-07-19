using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.DataAccess.Utils
{
    public static class AccountRolesData
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Staff = "Staff";
        
        public const int AdminEnum = 0;
        public const int ManagerEnum = 1;
        public const int StaffEnum = 2;
    }

    public enum AccountRoles
    {
        Admin = AccountRolesData.AdminEnum,
        Manager = AccountRolesData.ManagerEnum,
        Staff = AccountRolesData.StaffEnum,
    }

    public static class RoleMapper
    {
        public static string GetRoleName(int? role)
        {
            return role switch
            {
                0 => "Admin",
                1 => "Manager",
                2 => "Staff",
                _ => "Default"
            };
        }
    }
}
