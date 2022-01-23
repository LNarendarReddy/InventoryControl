using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using NSRetail.Helper_Code.OAuth2;
using NSRetail.Helper_Code.Wrapper;
using NSRetail.Models;

namespace NSRetail.Controllers
{
    //[Authorize]
    [CustomAuthorization]
    public class UsersController : ApiController
    {
        public List<TBLUSER> Login(string email, string password)
        {
            //HttpResponseMessage message = new HttpResponseMessage();

            using (NSRetail_CloudEntities entities = new NSRetail_CloudEntities())
            {
                string encryptpassword = AesBase64Wrapper.Encrypt(password);
                //entities.Configuration.ProxyCreationEnabled = false;

                var query = (from users in entities.TBLUSERs
                                 //join roles in entities.Roles on users.RoleID equals roles.RoleID
                                 //from prod2 in prodGroup
                             where users.USERNAME == email && users.PASSWORDSTRING == encryptpassword && users.DELETEDDATE == null
                             select users).ToList();

                return query;
            }
        }

        // GET api/users
        public HttpResponseMessage Get(int Id)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            ApiError apiError = null;
            APIResponse apiResponse = null;
            NSRetail_CloudEntities entities = new NSRetail_CloudEntities();
            try
            {
                var userslist = (from users in entities.TBLUSERs
                                 join branch in entities.BRANCHes on users.BRANCHID equals branch.BRANCHID
                                 where users.USERID == Id && users.DELETEDDATE == null && branch.DELETEDDATE == null
                                 select new { users.USERID, users.USERNAME, users.FULLNAME, users.EMAIL, users.GENDER, branch.BRANCHID, branch.BRANCHNAME, branch.ISWAREHOUSE }).ToList();
                if (userslist.Count > 0)
                {
                    apiResponse = new APIResponse(userslist, null);
                    message = Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                }
                else
                {
                    apiError = new ApiError("User Not Found", "201");
                    apiResponse = new APIResponse(null, apiError);
                    message = Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                }
            }

            catch (Exception ex)
            {
                apiError = new ApiError("Exception: " + ex.Message, "500");
                apiResponse = new APIResponse(null, apiError);
                return Request.CreateResponse(HttpStatusCode.OK, apiResponse);
            }
            return message;
        }
    }
}
