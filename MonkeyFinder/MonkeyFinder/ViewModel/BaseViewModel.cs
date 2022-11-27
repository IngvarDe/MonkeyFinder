using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MonkeyFinder.ViewModel
{
    //sellest on veel parem viis teha
    //[INotifyPropertyChanged]
    public partial class BaseViewModel : ObservableObject//: INotifyPropertyChanged vana viis
    {


        public BaseViewModel() 
        {

        }

        [ObservableProperty]
        //[AlsoNotifyChangeFor(nameof(IsNotBusy))] vanasti
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !isBusy;

        //Vanasti pidi niimoodi tegema
        //public bool IsBusy
        //{
        //    get => isBusy;
        //    set
        //    {
        //        if (isBusy == value)
        //            return;

        //        isBusy = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public string Title
        //{
        //    get => title;
        //    set
        //    {
        //        if (title == value)
        //            return;
        //        title = value;
        //        OnPropertyChanged();
        //        OnPropertyChanged(nameof(IsNotBusy));
        //    }
        //}

        //public bool IsNotBusy => !isBusy;


        //public event PropertyChangedEventHandler PropertyChanged;

        //public void OnPropertyChanged([CallerMemberName]string name = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        //}
    }
}
