using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiLaundering.Model;

namespace AntiLaundering.Control.ResourceManagement
{
    public class LoggedReport
    {
        public DateTime? DateLogged { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string EMAIL { get; set; }
        public string Address { get; set; }
        public string EmployeeId { get; set; }

    }
    public class UserAcLog
    {
        AntiMoney_UMEntities sms;
        public void InserActionLog(DateTime logTime, string username, string Actiontype, string logDetail)
        {
            sms = new AntiMoney_UMEntities();
            UserLog use = new UserLog();
            use.LogTime = logTime;
            use.LogUser = username;
            use.LogType = Actiontype;
            use.LogDetail = logDetail;
            use.LodId = Guid.NewGuid();
            sms.UserLogs.Add(use);
            sms.SaveChanges();
            
        }
        public List<UserLog> getAllActionDaily(DateTime dateTo)
        {
            sms = new AntiMoney_UMEntities();
            var act = (from u in sms.UserLogs
                       where System.Data.Entity.DbFunctions.TruncateTime(u.LogTime) == System.Data.Entity.DbFunctions.TruncateTime(dateTo)
                       select u).ToList();
            return act;
        }
        public List<UserLog> getAllActionDailyByUser(DateTime dateTo, string userName)
        {
            sms = new AntiMoney_UMEntities();
            var act = (from u in sms.UserLogs
                       where System.Data.Entity.DbFunctions.TruncateTime(u.LogTime) == System.Data.Entity.DbFunctions.TruncateTime(dateTo) && u.LogUser==userName
                       select u).ToList();
            return act;
        }
        public List<UserLog> getAllActionRange(DateTime dateTo,DateTime dateFrom)
        {
            sms = new AntiMoney_UMEntities();
            var act = (from u in sms.UserLogs
                       where (System.Data.Entity.DbFunctions.TruncateTime(u.LogTime) >= System.Data.Entity.DbFunctions.TruncateTime(dateFrom)
                        && System.Data.Entity.DbFunctions.TruncateTime(u.LogTime) <= System.Data.Entity.DbFunctions.TruncateTime(dateTo))
                       select u).ToList();
            return act;
        }
        public List<UserLog> getAllActionRangeByUser( DateTime dateFrom, DateTime dateTo, string userName)
        {
            sms = new AntiMoney_UMEntities();
            var act = (from u in sms.UserLogs
                       where (System.Data.Entity.DbFunctions.TruncateTime(u.LogTime) >= System.Data.Entity.DbFunctions.TruncateTime(dateFrom)
                        && System.Data.Entity.DbFunctions.TruncateTime(u.LogTime) <= System.Data.Entity.DbFunctions.TruncateTime(dateTo) && u.LogUser==userName)
                       select u).ToList();
            return act;
        }








        public List<LoggedReport> getAllLogInDaily(DateTime dateTo)
        {
            var  Um = new AntiMoney_UMEntities();
            var noti = (from u in Um.Users
                        join ul in Um.LoginHistories on u.USERNAME equals ul.UserName
                        where System.Data.Entity.DbFunctions.TruncateTime(ul.DateLogged) == System.Data.Entity.DbFunctions.TruncateTime(dateTo)
                        orderby ul.DateLogged descending
                        select new LoggedReport
                        {
                            DateLogged = ul.DateLogged,
                            UserName = ul.UserName,
                            FullName = u.FullName,
                            Phone = u.Phone,
                            EMAIL = u.EMAIL,
                            Address = u.Address,
                            EmployeeId = u.EmployeeId,
                        }).ToList();

            return noti;
        }
        public List<LoggedReport> getAllLogInDailyByUser(DateTime dateTo, string userName)
        {
            var Um = new AntiMoney_UMEntities();
            var noti = (from u in Um.Users
                        join ul in Um.LoginHistories on u.USERNAME equals ul.UserName
                        where System.Data.Entity.DbFunctions.TruncateTime(ul.DateLogged) == System.Data.Entity.DbFunctions.TruncateTime(dateTo) && u.USERNAME == userName
                        orderby ul.DateLogged descending
                        select new LoggedReport
                        {
                            DateLogged = ul.DateLogged,
                            UserName = ul.UserName,
                            FullName = u.FullName,
                            Phone = u.Phone,
                            EMAIL = u.EMAIL,
                            Address = u.Address,
                            EmployeeId = u.EmployeeId,
                        }).ToList();

            return noti;
        }
        public List<LoggedReport> getAllLogInRange(DateTime dateTo, DateTime dateFrom)
        {
            var Um = new AntiMoney_UMEntities();
            var noti = (from u in Um.Users
                        join ul in Um.LoginHistories on u.USERNAME equals ul.UserName
                        where (System.Data.Entity.DbFunctions.TruncateTime(ul.DateLogged) >= System.Data.Entity.DbFunctions.TruncateTime(dateFrom)
                        && System.Data.Entity.DbFunctions.TruncateTime(ul.DateLogged) <= System.Data.Entity.DbFunctions.TruncateTime(dateTo))
                        orderby ul.DateLogged descending
                        select new LoggedReport
                        {
                            DateLogged = ul.DateLogged,
                            UserName = ul.UserName,
                            FullName = u.FullName,
                            Phone = u.Phone,
                            EMAIL = u.EMAIL,
                            Address = u.Address,
                            EmployeeId = u.EmployeeId,
                        }).ToList();

            return noti;
        }
        public int getAllLogInRangePrintedCardFiltered(DateTime dateTo, DateTime dateFrom)
        {
            var Um = new AntiMoney_UMEntities();
            var noti = (from ul in Um.UserLogs
                        where (System.Data.Entity.DbFunctions.TruncateTime(ul.LogTime) >= System.Data.Entity.DbFunctions.TruncateTime(dateFrom)
                        && System.Data.Entity.DbFunctions.TruncateTime(ul.LogTime) <= System.Data.Entity.DbFunctions.TruncateTime(dateTo))
                        && ul.LogType == "Student Printed "
                        orderby ul.LogTime descending select ul
                       ).Distinct().ToList();
            var filtered = noti.GroupBy(m => m.LogDetail).Distinct().ToList().Count;

            return filtered;
        }
        public int getAllLogInRangePrintedCard(DateTime dateTo, DateTime dateFrom)
        {
            var Um = new AntiMoney_UMEntities();
            var noti = (from ul in Um.UserLogs
                        where (System.Data.Entity.DbFunctions.TruncateTime(ul.LogTime) >= System.Data.Entity.DbFunctions.TruncateTime(dateFrom)
                        && System.Data.Entity.DbFunctions.TruncateTime(ul.LogTime) <= System.Data.Entity.DbFunctions.TruncateTime(dateTo))
                        && ul.LogType == "Student Printed "
                        orderby ul.LogTime descending
                        select ul
                       ).Distinct().ToList().Count();

            return noti;
        }
        public List<UserLog> getAllLogInRangePrintedCardForStudent(string studentid,DateTime dateTo, DateTime dateFrom)
        {
            var Um = new AntiMoney_UMEntities();
            var noti = (from ul in Um.UserLogs
                        where (System.Data.Entity.DbFunctions.TruncateTime(ul.LogTime) >= System.Data.Entity.DbFunctions.TruncateTime(dateFrom)
                        && System.Data.Entity.DbFunctions.TruncateTime(ul.LogTime) <= System.Data.Entity.DbFunctions.TruncateTime(dateTo))
                        && ul.LogType == "Student Printed " 
                        orderby ul.LogTime descending
                        select ul
                       ).ToList();
            var filtered = noti.Where(p => p.LogDetail.Equals("StudentId: " + studentid)).ToList();

            return filtered;
        }
        public List<LoggedReport> getAllLogInRangeByUser(DateTime dateTo, DateTime dateFrom, string userName)
        {
            var Um = new AntiMoney_UMEntities();
            var noti = (from u in Um.Users
                        join ul in Um.LoginHistories on u.USERNAME equals ul.UserName
                        where (System.Data.Entity.DbFunctions.TruncateTime(ul.DateLogged)>=System.Data.Entity.DbFunctions.TruncateTime(dateFrom)  
                        && System.Data.Entity.DbFunctions.TruncateTime(ul.DateLogged) <= System.Data.Entity.DbFunctions.TruncateTime(dateTo) && ul.UserName == userName)
                        orderby ul.DateLogged descending
                        select new LoggedReport
                        {
                            DateLogged = ul.DateLogged,
                            UserName = ul.UserName,
                            FullName = u.FullName,
                            Phone = u.Phone,
                            EMAIL = u.EMAIL,
                            Address = u.Address,
                            EmployeeId = u.EmployeeId,
                        }).ToList();

            return noti;
        }
    }
}