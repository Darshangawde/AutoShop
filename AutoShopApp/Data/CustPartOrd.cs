namespace AutoShopApp.Data
{
    public class CustPartOrd
    {
        public int OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; } = String.Empty;
        public string PartName { get; set; } = String.Empty;
        public int Qty { get; set; }
        public double Amount { get; set; }

    }
}
