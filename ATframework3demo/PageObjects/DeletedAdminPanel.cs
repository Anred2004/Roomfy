using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class DeletedAdminPanel
    {
        public bool IsUserDeleted(User data)
        {
            var checkDeleteUser = new WebItem($"//a[@class='tag is-medium is-link' and contains(text(), '{data.Login}')]", "Поле с логином заблокированного пользователя");
            if (checkDeleteUser.WaitElementDisplayed() == false)
            {
                return false;
            }
            return true;
        }
    }
}
