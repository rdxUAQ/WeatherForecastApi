using Weather.Base;

namespace Weather.Const
{
    public class ResponseErrors
    {

        public static ErrorBase InvalidApiKey = new ErrorBase { Code = "AUTH001", Description = "INVALID API KEY" };

        public static ErrorBase elementNotFound = new ErrorBase { Code = "ERR001", Description = "Could not find element" };
        //DB ERROR
        public static ErrorBase ErrorWritingDB = new ErrorBase { Code = "ERR002", Description = "Error writing in the database please contact your system admin" };
        //Models
        public static ErrorBase InvalidModel = new ErrorBase { Code = "MDL001", Description = "Could not find element" };
        
    }
}