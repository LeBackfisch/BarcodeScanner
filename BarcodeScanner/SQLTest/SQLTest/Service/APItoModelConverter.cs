using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLTest.Model;

namespace SQLTest.Service
{
    public class ApItoModelConverter
    {
        public async Task<bool> GetAsync(string url)
        {
            try
            {
                var httpclient = new HttpClient();
                var json = await httpclient.GetStringAsync(url);
                var scanned = JsonConvert.DeserializeObject<ScannedItemModel>(json);
                scanned.DateSearched = DateTime.Today;
                var itemmodel = new ItemModel
                {
                    Name = scanned.Name,
                    Brand = scanned.Attributes.Brand,
                    DateSearched = scanned.DateSearched,
                };
                var connection = new ScannedItemModelDb();
                connection.AddItem(itemmodel);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
           
            
        }
    }
}
