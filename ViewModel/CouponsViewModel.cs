using Microsoft.Maui.Networking;

namespace ImproveMe.ViewModel
{
    public partial class CouponsViewModel : BaseViewModel
    {
        private readonly CouponService _couponService;

        public ObservableCollection<Coupon> Coupons { get; } = new();

        [ObservableProperty]
        public bool isRefreshing;
        public CouponsViewModel(CouponService couponService)
        {
            Title = "Nagrody";

            _couponService = couponService;

            GetCoupons();
        }
        [RelayCommand]
        public async Task GetCoupons()
        {
            if (IsBusy)
                return;

            try
            {

                IsBusy = true;

                Coupons.Clear();

                var coupons = await _couponService.GetCouponsAsync();

                foreach (var c in coupons)
                    Coupons.Add(c);

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Wystąpił błąd!", ex.Message, "Dobrze");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}
