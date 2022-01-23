using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace NSRetail.Helper_Code.Wrapper
{
    public class ApiError
    {
       
        public string Message { get; set; }
      
        public string code { get; set; }
        
        public ApiError(string message, string code="")
        {            
            this.code = code;
            this.Message = message;
        }        
    }
}
