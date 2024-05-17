using SignalR.EntityLayer.Entities;

namespace SignalR.DataAccessLayer.Abstract
{
    public interface ICategoryDal : IMessageDal<Category>
    {
        public int CategoryCount();
        int ActiveCategoryCount();
        int PassiveCategoryCount();
    }

}
