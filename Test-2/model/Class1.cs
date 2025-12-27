using System;

namespace Question1
{
    public class SaleTransaction
    {
        #region Description
        public string? InvoiceNo { get; set; }
        public string CustomerName { get; set; } = "";
        public string ItemName { get; set; } = "";
        public int Quantity { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal SellingAmount { get; set; }

        public string ProfitOrLossStatus { get; set; } = "";
        public decimal ProfitOrLossAmount { get; set; }
        public decimal ProfitMarginPercent { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Calculates profit or loss details.
        /// </summary>
        public void CalculateProfitOrLoss()
        {
            if (SellingAmount > PurchaseAmount)
            {
                ProfitOrLossStatus = "PROFIT";
                ProfitOrLossAmount = SellingAmount - PurchaseAmount;
            }
            else if (SellingAmount < PurchaseAmount)
            {
                ProfitOrLossStatus = "LOSS";
                ProfitOrLossAmount = PurchaseAmount - SellingAmount;
            }
            else
            {
                ProfitOrLossStatus = "BREAK-EVEN";
                ProfitOrLossAmount = 0;
            }

            ProfitMarginPercent = PurchaseAmount > 0
                ? ProfitOrLossAmount / PurchaseAmount * 100
                : 0;
        }

        /// <summary>
        /// Prints the transaction details.
        /// </summary>
        public void PrintTransaction()
        {
            Console.WriteLine("-------------- Last Transaction --------------");
            Console.WriteLine($"Invoice No: {InvoiceNo}");
            Console.WriteLine($"Customer: {CustomerName}");
            Console.WriteLine($"Item: {ItemName}");
            Console.WriteLine($"Quantity: {Quantity}");
            Console.WriteLine($"Purchase Amount: {PurchaseAmount:F2}");
            Console.WriteLine($"Selling Amount: {SellingAmount:F2}");
            Console.WriteLine($"Status: {ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {ProfitMarginPercent:F2}");
            Console.WriteLine("--------------------------------------------");
        }
        #endregion
    }
}