using NSRetail.Helper_Code.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NSRetail.Models;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using NSRetail.Helper_Code.OAuth2;

namespace NSRetail.Controllers
{
    [CustomAuthorization]
    public class StockCountingController : ApiController
    {
        public HttpResponseMessage Get(int stockCountDetailId, int userId, int branchId)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            ApiError apiError = null;
            APIResponse apiResponse = null;
            try
            {
                using (NSRetail_CloudEntities entities = new NSRetail_CloudEntities())
                {
                    if (stockCountDetailId > 0)
                    {
                        var stockList = (from stockdetail in entities.CLOUD_STOCKCOUNTINGDETAIL
                                         join stockcount in entities.CLOUD_STOCKCOUNTING on stockdetail.STOCKCOUNTINGID equals stockcount.STOCKCOUNTINGID
                                         join price in entities.ITEMPRICEs on stockdetail.ITEMPRICEID equals price.ITEMPRICEID
                                         join code in entities.ITEMCODEs on price.ITEMCODEID equals code.ITEMCODEID
                                         join item in entities.ITEMs on code.ITEMID equals item.ITEMID
                                         where stockdetail.DELETEDDATE == null && price.DELETEDDATE == null && code.DELETEDDATE == null && item.DELETEDDATE == null
                                         && stockdetail.STOCKCOUNTINGDETAILID == stockCountDetailId && stockcount.BRANCHID == branchId && stockcount.CREATEDBY == userId
                                         && stockcount.STATUS != true
                                         orderby stockdetail.STOCKCOUNTINGDETAILID descending
                                         select new
                                         {
                                             stockdetail.STOCKCOUNTINGDETAILID,
                                             stockdetail.ITEMPRICEID,
                                             stockdetail.QUANTITY,
                                             stockdetail.STOCKCOUNTINGID,
                                             price.MRP,
                                             price.SALEPRICE,
                                             code.ITEMCODE1,
                                             item.ITEMNAME
                                         }).ToList();
                        if (stockList.Count > 0)
                        {
                            apiResponse = new APIResponse(stockList, null);
                            message = Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                        }
                        else
                        {
                            apiError = new ApiError("stock counting details are not found", "201");
                            apiResponse = new APIResponse(null, apiError);
                            return Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                        }
                    }
                    else
                    {
                        var stockList = (from stockdetail in entities.CLOUD_STOCKCOUNTINGDETAIL
                                         join stockcount in entities.CLOUD_STOCKCOUNTING on stockdetail.STOCKCOUNTINGID equals stockcount.STOCKCOUNTINGID
                                         join price in entities.ITEMPRICEs on stockdetail.ITEMPRICEID equals price.ITEMPRICEID
                                         join code in entities.ITEMCODEs on price.ITEMCODEID equals code.ITEMCODEID
                                         join item in entities.ITEMs on code.ITEMID equals item.ITEMID
                                         where stockdetail.DELETEDDATE == null && price.DELETEDDATE == null && code.DELETEDDATE == null && item.DELETEDDATE == null
                                          && stockcount.BRANCHID == branchId && stockcount.CREATEDBY == userId && stockcount.STATUS != true
                                         select new
                                         {
                                             stockdetail.STOCKCOUNTINGDETAILID,
                                             stockdetail.ITEMPRICEID,
                                             stockdetail.QUANTITY,
                                             stockdetail.STOCKCOUNTINGID,
                                             price.MRP,
                                             price.SALEPRICE,
                                             code.ITEMCODE1,
                                             item.ITEMNAME
                                         }).ToList();
                        if (stockList.Count > 0)
                        {
                            apiResponse = new APIResponse(stockList, null);
                            message = Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                        }
                        else
                        {
                            apiError = new ApiError("stock counting details are not found", "201");
                            apiResponse = new APIResponse(null, apiError);
                            return Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return message;
        }
        [HttpPost]
        [Route("api/StockCounting/UpdateStatus/{nStockCounting}/{UserID}")]
        public HttpResponseMessage UpdateStatus([FromUri] int nStockCounting , [FromUri] int UserID)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            ApiError apiError = null;
            APIResponse apiResponse = null;
            using (NSRetail_CloudEntities entities = new NSRetail_CloudEntities())
            {
                try
                {
                    if (nStockCounting != 0)
                    {
                        try
                        {
                            // FIRST create a blank object
                            CLOUD_STOCKCOUNTING sc = entities.CLOUD_STOCKCOUNTING.Create();

                            // SECOND set the ID
                            sc.STOCKCOUNTINGID = nStockCounting;
                            // THIRD attach the thing (id is not marked as modified)
                            entities.CLOUD_STOCKCOUNTING.Attach(sc);

                            // FOURTH set the fields you want updated.
                            sc.UPDATEDDATE = DateTime.Now;
                            sc.UPDATEDBY = UserID;
                            sc.STATUS = true;

                            // FIFTH save that thing
                            Nullable<int> output = entities.SaveChanges();
                            if (output == 1)
                            {
                                apiResponse = new APIResponse(null);
                                message = Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                                return message;
                            }
                            else
                            {
                                apiError = new ApiError("Record is not deleted", "202");
                                apiResponse = new APIResponse(null, apiError);
                                return Request.CreateResponse(HttpStatusCode.BadRequest, apiResponse);
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        apiError = new ApiError("Some parameters missing", "202");
                        apiResponse = new APIResponse(null, apiError);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, apiResponse);
                    }
                }
                catch (Exception ex)
                {
                    apiError = new ApiError("Exception: " + ex.Message, "500");
                    apiResponse = new APIResponse(null, apiError);
                    return Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                }
            }
            return message;
        }
        [HttpPost]
        [Route("api/StockCounting/InsertStockCounting/{stockCountingID}/{stockCountDetailId}/{branchId}/{userId}/{itemPriceId}/{quantity}")]
        public HttpResponseMessage InsertStockCounting([FromUri] int stockCountingID, [FromUri] int stockCountDetailId, [FromUri] int branchId, 
            [FromUri] int userId, [FromUri] int itemPriceId, [FromUri] int quantity)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            ApiError apiError = null;
            APIResponse apiResponse = null;
            Nullable<int> nStockCountOutput = 0, nStockCountDetailOutput = 0;
            DateTime dtCreatedDate = DateTime.Now;
            using (NSRetail_CloudEntities entities = new NSRetail_CloudEntities())
            {
                try
                {
                    if (branchId != 0 && userId != 0 && itemPriceId != 0 && quantity != 0)
                    {
                        try
                        {
                            if (stockCountingID == 0)
                            {
                                nStockCountOutput = entities.CLOUD_USP_CU_STOCKCOUNTING(stockCountingID, branchId, userId, dtCreatedDate).FirstOrDefault();
                                if (nStockCountOutput != 0)
                                {
                                    nStockCountDetailOutput = 
                                        entities.CLOUD_USP_CU_STOCKCOUNTINGDETAIL(stockCountDetailId, nStockCountOutput,itemPriceId, quantity,0, dtCreatedDate).FirstOrDefault();
                                }
                            }
                            else
                            {
                                nStockCountDetailOutput = 
                                    entities.CLOUD_USP_CU_STOCKCOUNTINGDETAIL(stockCountDetailId, stockCountingID, itemPriceId, quantity,0, dtCreatedDate).FirstOrDefault();
                            }

                            if (nStockCountDetailOutput != 0)
                            {
                                apiResponse = new APIResponse(null);
                                message = Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                            }
                            else
                            {
                                apiError = new ApiError("Some parameters are missing", "202");
                                apiResponse = new APIResponse(null, apiError);
                                return Request.CreateResponse(HttpStatusCode.NoContent, apiResponse);
                            }
                            return message;

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        apiError = new ApiError("Some parameters are missing", "202");
                        apiResponse = new APIResponse(null, apiError);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, apiResponse);
                    }
                }
                catch (Exception ex)
                {
                    apiError = new ApiError("Exception: " + ex.Message, "500");
                    apiResponse = new APIResponse(null, apiError);
                    return Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                }
            }
            return message;
        }
        [HttpPost]
        [Route("api/StockCounting/DeleteStockCounting/{nStockCounting}")]
        public HttpResponseMessage DeleteStockCounting([FromUri] int nStockCounting)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            ApiError apiError = null;
            APIResponse apiResponse = null;
            using (NSRetail_CloudEntities entities = new NSRetail_CloudEntities())
            {
                try
                {
                    if (nStockCounting != 0)
                    {
                        try
                        {
                            {
                                Nullable<int> output = entities.CLOUD_USP_D_STOCKCOUNTINGDETAIL1(nStockCounting);
                                if (output == 1)
                                {
                                    apiResponse = new APIResponse(null);
                                    message = Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                                    return message;
                                }
                                else
                                {
                                    apiError = new ApiError("Record is not deleted", "202");
                                    apiResponse = new APIResponse(null, apiError);
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, apiResponse);
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        apiError = new ApiError("Some parameters missing", "202");
                        apiResponse = new APIResponse(null, apiError);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, apiResponse);
                    }
                }
                catch (Exception ex)
                {
                    apiError = new ApiError("Exception: " + ex.Message, "500");
                    apiResponse = new APIResponse(null, apiError);
                    return Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                }
            }
            return message;
        }
    }
}
