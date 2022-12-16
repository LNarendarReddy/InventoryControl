using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NSRetail.Helper_Code.OAuth2;
using NSRetail.Helper_Code.Wrapper;
using NSRetail.Models;

namespace NSRetail.Controllers
{
    [CustomAuthorization]
    public class StockItemsController : ApiController
    {
        public HttpResponseMessage getItemName(string itemCode)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            ApiError apiError = null;
            APIResponse apiResponse = null;
            NSRetail_CloudEntities entities = new NSRetail_CloudEntities();
            try
            {
                var itemList = (from price in entities.ITEMPRICEs
                                join code in entities.ITEMCODEs on price.ITEMCODEID equals code.ITEMCODEID
                                join item in entities.ITEMs on code.ITEMID equals item.ITEMID
                                where code.ITEMCODE1 == itemCode 
                                            && code.DELETEDDATE == null 
                                            && item.DELETEDDATE == null 
                                            && price.DELETEDDATE == null
                                            && price.BRANCHID != null
                                select new
                                {
                                    code.ITEMCODEID,
                                    code.ITEMCODE1,
                                    item.ITEMID,
                                    item.ITEMNAME,
                                    price.ITEMPRICEID,
                                    price.MRP,
                                    price.SALEPRICE
                                }).ToList();
                if (itemList.Count > 0)
                {
                    apiResponse = new APIResponse(itemList, null);
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
        public HttpResponseMessage getPriceDetails(int itemCodeid)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            ApiError apiError = null;
            APIResponse apiResponse = null;
            NSRetail_CloudEntities entities = new NSRetail_CloudEntities();
            try
            {
                var itemList = (from price in entities.ITEMPRICEs
                                join code in entities.ITEMCODEs on price.ITEMCODEID equals code.ITEMCODEID
                                where price.ITEMCODEID == itemCodeid 
                                            && price.DELETEDDATE == null 
                                            && code.DELETEDDATE == null 
                                            && price.BRANCHID != null
                                select new
                                {
                                    price.ITEMCODEID,
                                    price.ITEMPRICEID,
                                    price.MRP,
                                    price.SALEPRICE

                                }).ToList();

                if (itemList.Count > 0)
                {
                    apiResponse = new APIResponse(itemList, null);
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
