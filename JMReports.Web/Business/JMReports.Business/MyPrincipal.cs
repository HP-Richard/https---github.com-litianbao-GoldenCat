using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JMReports.Entities;
using JMReports.Data;

namespace JMReports.Business
{

    [Serializable()]
    public class MyPrincipal : System.Security.Principal.IPrincipal
    {
        protected ArrayList Permissions;

        protected List<Role> Roles;

        public MyIdentity identity;

        public MyPrincipal(int Id)
        {
            identity = new MyIdentity(Id);

            if (identity.User.Id != 0)
            {
                //Permissions = identity.User.getPermissions();
                //Roles = identity.User.getRoles();

            }
            else
            {
                identity = null;
                Permissions = null;
                Roles = null;

            }
        }


        public MyPrincipal(string UserId)
        {
            identity = new MyIdentity(UserId);

            if (identity.User.Id != 0)
            {
                //Permissions = identity.User.getPermissions();
                //Roles = identity.User.getRoles();

            }
            else
            {
                identity = null;
                Permissions = null;
                Roles = null;

            }

        }


        public System.Security.Principal.IIdentity Identity
        {
            get
            {
                return identity;
            }
        }


        public static MyPrincipal ValidateLogin(string LoginName, string Password)
        {
            MyPrincipal prin = new MyPrincipal(LoginName);

            if (prin.Identity == null)
            {
                return null;
            }

            if (((MyIdentity)prin.Identity).checkPassword(Password))
            {
                return prin;
            }
            else
            {
                return null;
            }

        }


        public bool IsInRole(string role)
        {
            if (role == null)
            {
                return false;
            }

            foreach (Role r in Roles)
            {
                if (r.RoleName.Trim() == role.Trim())
                {
                    return true;
                }
            }

            return false;
        }


        public bool HasPermission(string PermName)
        {
            if (PermName == null)
            {
                return false;
            }

            //foreach (CPermission p in this.Permissions)
            //{
            //    if (p.PermissionName.Trim() == PermName.Trim())
            //    {
            //        return true;
            //    }

            //}

            return false;
        }




    }
}
