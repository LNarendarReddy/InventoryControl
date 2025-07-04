﻿using NSRetailPOS.Gateway;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetailPOS.Entity
{
    public class Bill : ICloneable
    {
        public object BillID { get; set; }

        public object BillNumber { get; set; }

        public object LastBillID { get; set; }
        public object LastBilledAmount { get; set; }

        public object LastBilledQuantity { get; set; }

        public object Amount { get; set; }

        public object Quantity { get; set; }

        public DataTable dtBillDetails { get; set; }

        public object CustomerNumber { get; set; }

        public object CustomerName { get; set; }

        public object CustomerGST { get; set; }

        public object Rounding { get; set; }

        public object TenderedCash { get; set; }
        
        public object TenderedChange { get; set; }

        public object IsDoorDelivery { get; set; }

        public DataTable dtMopValues { get; set; }

        public object PaymentMode { get; set; }

        public List<CompletedTransactionData> CompletedTransactions { get; set; }

        public Bill()
        {
            CompletedTransactions = new List<CompletedTransactionData>();
        }

        public object Clone()
        {
            Bill clonedBillObj = new Bill();
            clonedBillObj.BillID = BillID;
            clonedBillObj.BillNumber = BillNumber;
            clonedBillObj.Rounding = Rounding;
            clonedBillObj.CustomerName  = CustomerName;
            clonedBillObj.CustomerNumber = CustomerNumber;
            clonedBillObj.CustomerGST = CustomerGST;
            clonedBillObj.TenderedCash = TenderedCash;
            clonedBillObj.TenderedChange = TenderedChange;
            clonedBillObj.IsDoorDelivery = IsDoorDelivery;
            clonedBillObj.dtBillDetails = dtBillDetails.Copy();
            clonedBillObj.dtMopValues = dtMopValues.Copy();
            return clonedBillObj;
        }
    }
}
