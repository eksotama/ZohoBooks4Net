#region License
/*
 * Copyright 2017 Brandon James
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */
#endregion

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ZohoBooks4Net.Domain.Entities
{
    public class Expense
    {
        [JsonProperty("expense_id")]
        public long ExpenseId { get; set; }

        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }

        [JsonProperty("transaction_type")]
        public string TransactionType { get; set; }

        [JsonProperty("gst_no")]
        public string GstNo { get; set; }

        [JsonProperty("gst_treatment")]
        public string GstTreatment { get; set; }

        [JsonProperty("destination_of_supply")]
        public string DestinationOfSupply { get; set; }

        [JsonProperty("destination_of_supply_state")]
        public string DestinationOfSupplyState { get; set; }

        [JsonProperty("hsn_or_sac")]
        public int HsnOrSac { get; set; }

        [JsonProperty("source_of_supply")]
        public string SourceOfSupply { get; set; }

        [JsonProperty("paid_through_account_id")]
        public string PaidThroughAccountId { get; set; }

        [JsonProperty("paid_through_account_name")]
        public string PaidThroughAccountName { get; set; }

        [JsonProperty("reverse_charge_tax_id")]
        public long ReverseChargeTaxId { get; set; }

        [JsonProperty("reverse_charge_tax_name")]
        public string ReverseChargeTaxName { get; set; }

        [JsonProperty("reverse_charge_tax_percentage")]
        public int ReverseChargeTaxPercentage { get; set; }

        [JsonProperty("reverse_charge_tax_amount")]
        public int ReverseChargeTaxAmount { get; set; }

        [JsonProperty("tax_amount")]
        public double TaxAmount { get; set; }

        [JsonProperty("is_itemized_expense")]
        public bool IsItemizedExpense { get; set; }

        [JsonProperty("is_pre_gst")]
        public string IsPreGst { get; set; }

        [JsonProperty("trip_id")]
        public string TripId { get; set; }

        [JsonProperty("trip_number")]
        public string TripNumber { get; set; }

        [JsonProperty("reverse_charge_vat_total")]
        public double ReverseChargeVatTotal { get; set; }

        [JsonProperty("acquisition_vat_total")]
        public int AcquisitionVatTotal { get; set; }

        [JsonProperty("acquisition_vat_summary")]
        public IList<VatSummary> AcquisitionVatSummary { get; set; }

        [JsonProperty("reverse_charge_vat_summary")]
        public IList<VatSummary> ReverseChargeVatSummary { get; set; }

        [JsonProperty("expense_item_id")]
        public long ExpenseItemId { get; set; }

        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("tax_id")]
        public long TaxId { get; set; }

        [JsonProperty("tax_name")]
        public string TaxName { get; set; }

        [JsonProperty("tax_percentage")]
        public double TaxPercentage { get; set; }

        [JsonProperty("currency_id")]
        public long CurrencyId { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("exchange_rate")]
        public int ExchangeRate { get; set; }

        [JsonProperty("sub_total")]
        public int SubTotal { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("bcy_total")]
        public int BcyTotal { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("is_inclusive_tax")]
        public bool IsInclusiveTax { get; set; }

        [JsonProperty("reference_number")]
        public object ReferenceNumber { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_billable")]
        public bool IsBillable { get; set; }

        [JsonProperty("is_personal")]
        public bool IsPersonal { get; set; }

        [JsonProperty("customer_id")]
        public long CustomerId { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("expense_receipt_name")]
        public string ExpenseReceiptName { get; set; }

        [JsonProperty("expense_receipt_type")]
        public string ExpenseReceiptType { get; set; }

        [JsonProperty("last_modified_time")]
        public string LastModifiedTime { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("project_id")]
        public long ProjectId { get; set; }

        [JsonProperty("project_name")]
        public string ProjectName { get; set; }

        [JsonProperty("mileage_rate")]
        public string MileageRate { get; set; }

        [JsonProperty("mileage_type")]
        public string MileageType { get; set; }

        [JsonProperty("expense_type")]
        public string ExpenseType { get; set; }

        [JsonProperty("start_reading")]
        public string StartReading { get; set; }

        [JsonProperty("end_reading")]
        public string EndReading { get; set; }

        [JsonProperty("line_item")]
        public LineItem LineItem { get; set; }
    }
    
    public class VatSummary
    {
        [JsonProperty("tax")]
        public Tax Tax { get; set; }
    }
}
