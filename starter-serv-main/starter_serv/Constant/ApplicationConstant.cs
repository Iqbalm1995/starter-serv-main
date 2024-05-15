namespace starter_serv.Constant
{
    public class ApplicationConstant
    {
        // timezone
        public const double TIMEZONE_JKT = +7;

        // res status code
        public const int STATUS_CODE_OK = 200;
        public const int STATUS_CODE_CREATED = 201;
        public const int STATUS_CODE_NOT_FOUND = 404;
        public const int STATUS_CODE_BAD_REQUEST = 400;
        public const int STATUS_CODE_ERROR = 500;

        // res status message
        public const string STATUS_MSG_OK = "Ok";
        public const string STATUS_MSG_SUCCESS = "Success";
        public const string STATUS_MSG_CREATED = "Created";
        public const string STATUS_MSG_DELETED = "Deleted";
        public const string STATUS_MSG_NOT_FOUND = "Not Found";
        public const string STATUS_MSG_BAD_REQUEST = "Bad Request";
        public const string STATUS_MSG_ERROR = "Internal Server Error";
        public const string STATUS_MSG_SUCCESS_DELETED = "Data Is Deleted";

        // custom res status message
        public const string STATUS_MSG_USERNAME_IS_USED = "Username Is Already In Use";
        public const string STATUS_MSG_PASSWORD_IS_NOT_SAME = "Password and password confirmation is not same";


        // enum delete
        public const string DELETED = "1";
        public const string UNDELETED = "0";

        // enum project status
        // 1=Open,2=InProgress,3=OnHold,4=Cancel,5=Completed

        public const int PROJECT_STATUS_CODE_OPEN = 1;
        public const int PROJECT_STATUS_CODE_IN_PROGRESS = 2;
        public const int PROJECT_STATUS_CODE_ON_HOLD = 3;
        public const int PROJECT_STATUS_CODE_CANCEL = 4;
        public const int PROJECT_STATUS_CODE_COMPLETED = 5;

        // enum user status
        // 0=NotActivate,1=Active,2=Banned,3=Hold,4=Review,5=Block

        public const string USER_STATUS_CODE_NOT_ACTIVATE = "0";
        public const string USER_STATUS_CODE_ACTIVATE = "1";
        public const string USER_STATUS_CODE_BANNED = "2";
        public const string USER_STATUS_CODE_HOLD = "3";
        public const string USER_STATUS_CODE_REVIEW = "4";
        public const string USER_STATUS_CODE_BLOCK = "5";


        // label startdate/enddate for parameter

        public const string LABEL_PARAM_STARTDATE = "startdate";
        public const string LABEL_PARAM_ENDDATE = "endate";


    }
}
