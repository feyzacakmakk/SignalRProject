using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface IBasketDal:IMessageDal<Basket>
    {
        List<Basket> GetBasketByMenuTableNumber(int id);
        //masa numarasına göre sepeti getir
        //id->tablenumber ın id'si olacak
    }
}
