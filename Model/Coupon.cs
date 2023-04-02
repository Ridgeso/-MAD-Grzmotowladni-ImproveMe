namespace ImproveMe.Model
{
    public class Coupon
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public uint Price { get; set; }
        public string Code { get; set; }
        public bool Collected { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
