using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using AntiLaundering.Model;
using MyUserManagement;
namespace AntiLaundering.Control.UserManagement
{
    public class UserRegiManagement
    {
        AntiMoney_UMEntities hrm = new AntiMoney_UMEntities();
        public Boolean UserExists(String UName)
        {
            AntiMoney_UMEntities entities = new AntiMoney_UMEntities();
            try
            {
                var rop = (from l in entities.Users where l.USERNAME == UName select l).FirstOrDefault();
                if (rop != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<User> getAllUsers()
        {
            try
            {
                var user = (from u in hrm.Users select u).ToList();
                return user;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public List<User> getAllUsers(String RoleName)
        {
            try
            {
                var inroles = (from u in hrm.Usersinroles where u.ROLENAME == RoleName select u.USERNAME).ToList();
                var user = (from u in hrm.Users where inroles.Contains(u.USERNAME) select u).ToList();
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public User getUserByName(string userName)
        {

            try
            {
                var user = (from u in hrm.Users
                            where u.USERNAME == userName
                            select u).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Boolean updateUserLogIn(string userName, DateTime lastLogIn)
        {
            int change = -1;

            try
            {
                var user = (from u in hrm.Users
                            where u.USERNAME == userName
                            select u).FirstOrDefault();
                if (user != null)
                {
                    user.LASTLOGINDATE = lastLogIn;
                    user.LASTACTIVITYDATE = lastLogIn;
                    change = hrm.SaveChanges();
                }
                if (change > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Boolean updateUserStatus(string EmployeeId, bool status)
        {
            int change = -1;

            try
            {
                var user = (from u in hrm.Users
                            where u.EmployeeId == EmployeeId
                            select u).FirstOrDefault();
                if (user != null)
                {
                    user.ISLOCKEDOUT = status;
                    change = hrm.SaveChanges();
                }
                if (change > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}