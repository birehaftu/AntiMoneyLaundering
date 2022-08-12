using System;
using System.Web.Security;

namespace MyUserManagement
{
    public sealed class MyCustomMembershipUser : MembershipUser
    {
        //private String _Department;
        //private String _PalmIp;
        private String _EmployeeId;
        private int _FAILEDPASSWORDATTEMPTCOUNT;
        public int FAILEDPASSWORDATTEMPTCOUNT
        {
            get { return _FAILEDPASSWORDATTEMPTCOUNT; }
            set { _FAILEDPASSWORDATTEMPTCOUNT = value; }
        }
        public String EmployeeId
        {
            get { return _EmployeeId; }
            set { _EmployeeId = value; }
        }

      
        private string _FullName;
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        public MyCustomMembershipUser(string providername,
                                  string username,
                                  object providerUserKey,
                                  string email,
                                  string passwordQuestion,
                                  string comment,
                                  bool isApproved,
                                  bool isLockedOut,
                                  DateTime creationDate,
                                  DateTime lastLoginDate,
                                  DateTime lastActivityDate,
                                  DateTime lastPasswordChangedDate,
                                  DateTime lastLockedOutDate,
                                  String employeeId, int fAILEDPASSWORDATTEMPTCOUNT) :
            base(providername,
                                        username,
                                        providerUserKey,
                                        email,
                                        passwordQuestion,
                                        comment,
                                        isApproved,
                                        isLockedOut,
                                        creationDate,
                                        lastLoginDate,
                                        lastActivityDate,
                                        lastPasswordChangedDate,
                                        lastLockedOutDate)
        {
            this.EmployeeId = employeeId;
            this.FAILEDPASSWORDATTEMPTCOUNT = fAILEDPASSWORDATTEMPTCOUNT;
        }
        public MyCustomMembershipUser(MembershipUser mu) :
            base(mu.ProviderName,
             mu.UserName,
             mu.ProviderUserKey,
             mu.Email,
             mu.PasswordQuestion,
             mu.Comment,
             mu.IsApproved,
             mu.IsLockedOut,
             mu.CreationDate,
             mu.LastLoginDate,
             mu.LastActivityDate,
             mu.LastPasswordChangedDate, mu.LastLockoutDate)
        {

        }

    }
}
