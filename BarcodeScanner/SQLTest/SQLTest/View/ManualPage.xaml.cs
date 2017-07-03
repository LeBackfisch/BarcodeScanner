using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLTest.ViewModel;
using Xamarin.Forms;

namespace SQLTest.View
{
    public partial class ManualPage : ContentPage
    {
        private readonly ManualEntryViewModel _viewModel;
        public ManualPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ManualEntryViewModel();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            const string help1 = "https://api.outpan.com/v2/products/";
            const string help2 = "?apikey=cfd74f8529dd92dfacb7fce5e1b82ba7";
            string text = MainEntry.Text;
            _viewModel.URI = help1 + text + help2;

            _viewModel.GetCommand.Execute(null);

            if (_viewModel.Finished)
            {
                Navigation.PushAsync(new SearchedItemView());
            }
            else
            {
                Failed.Text = "No Product was found under this code";
            }
            
        }
    }

    
}
