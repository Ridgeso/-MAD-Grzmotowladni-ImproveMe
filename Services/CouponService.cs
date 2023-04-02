namespace ImproveMe.Services
{

    public class CouponService
    {
    SQLiteAsyncConnection Database;
        async void Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Coupon>();
            var coupons = await GetCouponsAsync();
            
            if(coupons is null || coupons.Count == 0)
            {
                await GeneratesCouponsAsync();
            }
        }

        public async Task<List<Coupon>> GetCouponsAsync()
        {
            Init();

            return await Database.Table<Coupon>().ToListAsync();
        }

        private async Task<List<Coupon>> GeneratesCouponsAsync()
        {
            Init();

            var coupons = new List<Coupon>()
            {
                new Coupon() 
                {
                    Name = "-20% na karnet siłownię",
                    Description = "-20% na karnet na wszytskie siłownie FitnessPol",
                    CompanyName = "FitnessPol",
                    Price = 10000,
                    Code = "asbkdbksbhds131",
                },
                new Coupon() 
                {
                    Name = "20 zł zniżki w BikeWorld",
                    Description = "20 zł na na opony rowerowe w sklepie BikeWorld.",
                    CompanyName = "BikeWorld",
                    Price = 8000,
                    Code = "hwdhbdkhwabhdwa",
                },
                new Coupon() 
                {
                    Name = "Darmowy przejazd Umber",
                    Description = "Darmowy przejazd do 5 kilomentrów.",
                    CompanyName = "Umber",
                    Price = 2000,
                    Code = "jnajsdjad",
                },
                new Coupon() 
                {
                    Name = "Darmowa wizyta u psychologa",
                    Description = "Pierwsza wizyta za darmo u psychologa.",
                    CompanyName = "Usługi psychologiczne Anna Nowak",
                    Price = 15000,
                    Code = "sdbshakdbsakbd",
                },
                new Coupon() 
                {
                    Name = "10 zł na masaż",
                    Description = "10% zniżki na masażę.",
                    CompanyName = "FizjoMed",
                    Price = 4000,
                    Code = "1273yebsj",
                },
            };

            await Database.InsertAllAsync(coupons);

            return coupons;
        }
    }

   
}
