using System.Runtime.Serialization;

namespace NSRetail.Helper_Code.Wrapper
{
    [DataContract]
    [KnownTypeAttribute(typeof(APIResponse))]
    public class APIResponse
    {
        [DataMember(EmitDefaultValue = true)]
        public ApiError error { get; set; }

        [DataMember(EmitDefaultValue = true)]
        public object data { get; set; }

        public APIResponse(object data = null, ApiError apiError = null)
        {
            this.data = data;
            this.error = apiError;
        }
    }
    
}
