using MonkeyFinder.Models;
using System.Collections.ObjectModel;
using MonkeyFinder.Services;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using MonkeyFinder.View;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MonkeyFinder.ViewModel
{
    public partial class MonkeysViewModel : BaseViewModel
    {
        MonkeyService monkeyService;

        public ObservableCollection<Monkey> Monkeys { get; } = new();

        //public Command GetMonkeysCommand { get; }
        IConnectivity connectivity;
        IGeolocation geolocation;

        public MonkeysViewModel
            (
                MonkeyService monkeyService,
                IConnectivity connectivity,
                IGeolocation geolocation
            )
        {
            Title = "Monkey Finder";
            this.monkeyService = monkeyService;
            this.connectivity = connectivity;
            this.geolocation = geolocation;
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GetClosestMonkeyAsync()
        {
            if (IsBusy || Monkeys.Count == 0)
            {
                return;
            }

            try
            {
                var location = await geolocation.GetLastKnownLocationAsync();
                
                if (location == null)
                {
                    location = await geolocation.GetLocationAsync(
                        new GeolocationRequest
                        {
                            DesiredAccuracy = GeolocationAccuracy.Medium,
                            Timeout = TimeSpan.FromSeconds(30)
                        });
                }

                var first = Monkeys.OrderBy(x => 
                    location.CalculateDistance(x.Latitude, x.Longitude, DistanceUnits.Kilometers))
                    .FirstOrDefault();

                if (first == null)
                {
                    return;
                }

                await Shell.Current.DisplayAlert("Closest monkey",
                    $"{first.Name} in {first.Location}", "OK");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to get closest monkeys: {ex.Message}"
                    , "OK");
            }
        }


        [RelayCommand]
        async Task GoToDetalisAsync(Monkey monkey)
        {
            if (monkey is null)
            {
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?id={monkey.Name}", true,
                new Dictionary<string, object>
                {
                    {"Monkey", monkey}
                });
        }


        [RelayCommand]
        async Task GetMonkeysAsync()
        {
            IsRefreshing = false;

            if (IsBusy)
                return;
            try
            {
                //kui ei ole interneti yhendust, siis kuva seda stringi
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Internet Error!",
                        $"Please check internet and try again.", "OK");

                    return;
                }


                IsBusy = true;
                var monkeys = await monkeyService.GetMonkeys();

                if ( Monkeys.Count != 0)
                    Monkeys.Clear();

                foreach (var monkey in monkeys)
                {
                    Monkeys.Add(monkey);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to get monkeys: { ex.Message}"
                    , "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}
