﻿using MonkeyFinder.Models;
using System.Collections.ObjectModel;
using MonkeyFinder.Services;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;

namespace MonkeyFinder.ViewModel
{
    public partial class MonkeysViewModel : BaseViewModel
    {
        MonkeyService monkeyService;

        public ObservableCollection<Monkey> Monkeys { get; } = new();

        //public Command GetMonkeysCommand { get; }

        public MonkeysViewModel
            (
                MonkeyService monkeyService
            )
        {
            Title = "Monkey Finder";
            this.monkeyService = monkeyService;
        }


        [RelayCommand]
        async Task GetMonkeysAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var monkeys = await monkeyService.Getmonkeys();

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
            }
        }
    }
}