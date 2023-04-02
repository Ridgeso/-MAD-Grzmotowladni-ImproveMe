using Microsoft.Maui.Networking;
using System.Xml.Linq;

namespace ImproveMe.ViewModel
{
    public partial class CouponsViewModel : BaseViewModel
    {
        private readonly CouponService _couponService;

        public ObservableCollection<Coupon> Coupons { get; } = new();
        public ObservableCollection<Coupon> CouponsCollected { get; } = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ShowNotCollected))]
        public bool showCollected = false;

        public bool ShowNotCollected => !ShowCollected;

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
                CouponsCollected.Clear();

                var coupons = await _couponService.GetCouponsAsync();

                foreach (var c in coupons)
                {
                    if(c.Collected)
                    { 
                        CouponsCollected.Add(c);
                    }
                    else
                    {
                        Coupons.Add(c);
                    }
                }

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

        [RelayCommand]
        public async Task Buy(long id)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                
                await _couponService.Buy(id);

                await GetCoupons();

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Wystąpił błąd!", ex.Message, "Zatwierdź");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
