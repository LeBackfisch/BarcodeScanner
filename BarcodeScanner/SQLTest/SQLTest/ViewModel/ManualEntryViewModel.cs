using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLTest.Service;
using Xamarin.Forms;

namespace SQLTest.ViewModel
{
    public class ManualEntryViewModel : INotifyPropertyChanged
    {
        private string _url;
        private readonly ApItoModelConverter _genericRestClient;
        private bool _isBusy = false;
        private bool _finished;

        public ManualEntryViewModel()
        {
            _genericRestClient = new ApItoModelConverter();
        }
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;

                    await DownloadDataAsync();

                    IsBusy = false;
                });
            }
        }

        public bool Finished
        {
            get { return _finished; }
            set { _finished = value; }
        }

        public string URI { get { return _url; } set { _url = value; } }

        public async Task DownloadDataAsync()
        {

              _finished = await _genericRestClient.GetAsync(URI);
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
