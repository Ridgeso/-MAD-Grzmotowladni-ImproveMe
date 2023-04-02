namespace ImproveMe.Services
{

    public class CouponService
    {

        SQLiteAsyncConnection Database;
        private readonly UserService _userService;
        public CouponService(UserService userService)
        {
            _userService = userService;
        }

        async Task Init()
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
            await Init();

            return await Database.Table<Coupon>().ToListAsync();
        }

        private async Task<List<Coupon>> GeneratesCouponsAsync()
        {
            await Init();

            var coupons = new List<Coupon>()
            {
                new Coupon() 
                {
                    Name = "-20% na karnet siłownię",
                    Description = "-20% na karnet na wszytskie siłownie FitnessPol",
                    CompanyName = "FitnessPol",
                    Price = 10000,
                    Code = "asbkdbksbhds131",
                    Collected = false,
                },
                new Coupon() 
                {
                    Name = "20 zł zniżki w BikeWorld",
                    Description = "20 zł na na opony rowerowe w sklepie BikeWorld.",
                    CompanyName = "BikeWorld",
                    Price = 8000,
                    Code = "hwdhbdkhwabhdwa",
                    Collected = false,
                },
                new Coupon() 
                {
                    Name = "Darmowy przejazd Umber",
                    Description = "Darmowy przejazd do 5 kilomentrów.",
                    CompanyName = "Umber",
                    Price = 2000,
                    Code = "jnajsdjad",
                    Collected = false,
                },
                new Coupon() 
                {
                    Name = "Darmowa wizyta u psychologa",
                    Description = "Pierwsza wizyta za darmo u psychologa.",
                    CompanyName = "Usługi psychologiczne Anna Nowak",
                    Price = 15000,
                    Code = "sdbshakdbsakbd",
                    Collected = false,
                },
                new Coupon() 
                {
                    Name = "10 zł na masaż",
                    Description = "10% zniżki na masażę.",
                    CompanyName = "FizjoMed",
                    Price = 4000,
                    Code = "1273yebsj",
                    Collected = false,
                },
            };

            await Database.InsertAllAsync(coupons);

            return coupons;
        }
        public async Task<Coupon> Buy(long id)
        {
            await Init();

            var user = await _userService.GetUserAsync();
            var coupon = await Database.Table<Coupon>().Where(e => e.Id == id).FirstOrDefaultAsync();

            if (user.Coins < coupon.Price)
                throw new Exception("Nie posiadasz wystarczająco dużo monet aby odebrać tę nagrodę.");

            user.Coins -= coupon.Price;
            coupon.Collected = true;
            coupon.ExpirationDate = DateTime.Now.AddDays(14);

            await _userService.UpdateUser(user);
            await Database.UpdateAsync(coupon);

            return coupon;
        }
    }

}
