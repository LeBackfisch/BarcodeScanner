using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLTest.Service;
using SQLTest.View;
using SQLTest.ViewModel;
using Xamarin.Forms;
using XamarinForms.SQLite;
using ZXing.Net.Mobile.Forms;

namespace SQLTest
{
    public partial class MainPage : ContentPage
    {

        private readonly ScannerViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ScannerViewModel();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage();

            scanPage.OnScanResult += (result) => {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(() => {
                    if (FoundProduct(result.Text))
                    {
                        Navigation.PushAsync(new SearchedItemView());
                    }
                    else
                    {
                        Navigation.PopAsync();
                        CodeScanned.Text = result.Text;
                        DisplayAlert("Could not find a product that matched the Barcode", null ,"OK");
                    }
                    
                });
            };
            Navigation.PushAsync(scanPage);
        }

        public bool FoundProduct(string text)
        {
            const string help1 = "https://api.outpan.com/v2/products/";
            const string help2 = "?apikey=cfd74f8529dd92dfacb7fce5e1b82ba7";        
            viewModel.URI = help1 + text + help2;

            viewModel.GetCommand.Execute(null);

            return viewModel.Finished;
        }

        private void List_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScannedItemModelDb().GetListPage());
        }

        private void Single_onclicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManualPage());
        }
    }
}
