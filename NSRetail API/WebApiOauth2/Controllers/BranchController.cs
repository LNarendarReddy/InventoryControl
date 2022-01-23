using NSRetail.Helper_Code.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NSRetail.Models;
using NSRetail.Helper_Code.OAuth2;

namespace NSRetail.Controllers
{
    [CustomAuthorization]
    public class BranchController : ApiController
    {
        public HttpResponseMessage Get()
        {
            HttpResponseMessage message = new HttpResponseMessage();
            ApiError apiError = null;
            APIResponse apiResponse = null;
            NSRetail_CloudEntities entities = new NSRetail_CloudEntities();
            try
            {
                var brancheslist = (from branches in entities.BRANCHes
                                    where branches.DELETEDDATE == null
                                    select new
                                    {
                                        branches.BRANCHID,
                                        branches.BRANCHNAME,
                                        branches.BRANCHCODE
                                    }
                                         ).ToList();
                if (brancheslist.Count > 0)
                {
                    apiResponse = new APIResponse(brancheslist, null);
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
        public HttpResponseMessage getBranchDetails(string userID)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            ApiError apiError = null;
            APIResponse apiResponse = null;
            NSRetail_CloudEntities entities = new NSRetail_CloudEntities();
            try
            {
                string branchID = (from user in entities.TBLUSERs
                                   where user.USERID == Convert.ToInt32(userID)
                                   select new
                                   {
                                       user.BRANCHID
                                   }).ToString();
                int nBranchID = Convert.ToInt32(branchID);

                var branchname = (from branch in entities.BRANCHes
                                  where branch.BRANCHID == nBranchID && branch.DELETEDDATE == null
                                  select new
                                  {
                                      branch.BRANCHNAME
                                  }).ToList();

                if (branchname.Count > 0)
                {
                    apiResponse = new APIResponse(branchname, null);
                    message = Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                }
                else
                {
                    apiError = new ApiError("No Sub Category / Product(s) found", "201");
                    apiResponse = new APIResponse(null, apiError);
                    return Request.CreateResponse(HttpStatusCode.OK, apiResponse);
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
