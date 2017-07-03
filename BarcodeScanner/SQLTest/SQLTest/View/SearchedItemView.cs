using SQLTest.Model;
using SQLTest.Service;
using Xamarin.Forms;

namespace SQLTest.View
{
    public class SearchedItemView : ContentPage
    {
        public SearchedItemView()
        {
            var connection = new ScannedItemModelDb();
            Item = connection.GetLastEntry();
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = Item.Name },
                    new Label { Text = Item.Brand},
                    new Label { Text = Item.DateSearched.Date.ToString() }
                }
            };
        }

        public ItemModel Item { get; set; }
    }
}
