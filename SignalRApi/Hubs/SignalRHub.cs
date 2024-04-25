using Microsoft.AspNetCore.SignalR;
using SignalR.DataAccessLayer.Concrete;

namespace SignalRApi.Hubs
{
    public class SignalRHub:Hub
    {
       SignalRContext context=new SignalRContext();
        //Db'den örnek oluşturdum

        //Genelde Send denir
        //Kategori sayısını bize gönderecek olan metot
        public async Task SendCategoryCount()
        {
            var value=context.Categories.Count();
            //db deki kategori sayısını saydırdım
            await Clients.All.SendAsync("ReceiveCategoryCount", value);
            //Clients -> signalr'ın
            //Clients.All.SendAsync -> gelen değeri client tarafına gönderecek
            //ReceiveCategoryCount -> ismi ve değer olarak da value'dan gelen değeri alacak

            //client tarafında SendCategoryCount'u çağırıcam
            //bu metota invoke ile bu metotu çağır ve bunun içindeki ReceiveCategoryCount'u kullan
            //bu metota abone olucam ve onun içindeki yapıp kullanıcam


        }
    }
}
