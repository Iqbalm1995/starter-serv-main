using starter_serv.Constant;

namespace starter_serv.Helper
{
    public class StatusHelper
    {
        public static string getStatusProjectDetail(string statusProject)
        {
            string result = "";

            if (statusProject == ApplicationConstant.PROJECT_STATUS_CODE_OPEN.ToString())
            {
                result = "Not Started";
                return result;
            }

            if (statusProject == ApplicationConstant.PROJECT_STATUS_CODE_IN_PROGRESS.ToString())
            {
                result = "On Schedule";
                return result;
            }

            if (statusProject == ApplicationConstant.PROJECT_STATUS_CODE_ON_HOLD.ToString())
            {
                result = "Delay";
                return result;
            }

            if (statusProject == ApplicationConstant.PROJECT_STATUS_CODE_CANCEL.ToString())
            {
                result = "Drop Out";
                return result;
            }

            if (statusProject == ApplicationConstant.PROJECT_STATUS_CODE_COMPLETED.ToString())
            {
                result = "Done";
                return result;
            }

            return result;
        }

        
    }
}
