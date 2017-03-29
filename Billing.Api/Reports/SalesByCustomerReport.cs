﻿using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Billing.Api.Reports
{
    public class SalesByCustomerReport
    {
        private ReportFactory Factory = new ReportFactory();
        private BillingIdentity _identity;
        private UnitOfWork _unitOfWork;
        public SalesByCustomerReport(UnitOfWork unitOfWork, BillingIdentity identity)
        {
            _unitOfWork = unitOfWork;
            _identity = identity;
        }

        public SalesByCustomerModel Report(RequestModel Request)
        {
            List<Invoice> Invoices = _unitOfWork.Invoices.Get().Where(x => x.Date >= Request.StartDate && x.Date <= Request.EndDate).ToList();
            SalesByCustomerModel result = new SalesByCustomerModel()
            {
                StartDate = Request.StartDate,
                EndDate = Request.EndDate,
                GrandTotal = Invoices.Sum(x => x.SubTotal)
            };

            result.Sales = Invoices.OrderBy(x => x.Customer.Id).ToList()
                                   .GroupBy(x => x.Customer.Name)
                                   .Select(x => new CustomerSalesModel()
                                   {
                                       CustomerName = x.Key,
                                       CustomerTurnover = x.Sum(y => y.SubTotal),
                                       CustomerPercent = 100 * x.Sum(y => y.SubTotal) / result.GrandTotal
                                   }).OrderByDescending(x => x.CustomerTurnover)
                                   .ToList();
            return result;
        }
    }
}