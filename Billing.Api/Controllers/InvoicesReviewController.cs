﻿using Billing.Api.Models;
using System;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    public class InvoicesReviewController : BaseController
    {
        public IHttpActionResult Post(RequestModel request)
        {
            try
            {
                return Ok(Reports.InvoiceReview.Report(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
