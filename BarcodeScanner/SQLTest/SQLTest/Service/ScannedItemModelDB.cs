using System.Collections.Generic;
using System.Linq;
using SQLite;
using SQLTest.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinForms.SQLite.SQLite;

namespace SQLTest.Service
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    class ScannedItemModelDb
    {
        private readonly SQLiteConnection _sqLiteConnection;

        public ScannedItemModelDb()
        {
            _sqLiteConnection = DependencyService.Get<ISQLite>().GetConnection();

            _sqLiteConnection.CreateTable<ItemModel>();

        }

        public void AddItem(ItemModel item)
        {
            _sqLiteConnection.Insert(item);
        }

        public List<ItemModel> GetAllEntries()
        {
            return _sqLiteConnection.Table<ItemModel>().ToList();
        }

        public ItemModel GetLastEntry()
        {
            return _sqLiteConnection.Table<ItemModel>().Last();
        }

        public ContentPage GetListPage()
        {
            var listView = new ListView
            {
                 ItemsSource = _sqLiteConnection.Table<ItemModel>(),
            };

            var deleteButton = new Button
            {
                Text = "Delete History"
            };
            deleteButton.Clicked += (s, e) =>
            {
                _sqLiteConnection.DeleteAll<ItemModel>();
                listView.ItemsSource = _sqLiteConnection.Table<ItemModel>();
            };

            var contentPage = new ContentPage
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label
                        {
                            Text = "Scan History",
                            FontSize = 50
                        },
                        new StackLayout
                        {
                           
                        },

                        deleteButton,
                        listView,
                    }
                }
            };
            return contentPage;
        }
    }
}
