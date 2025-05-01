using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class BlockedAdminPanel
    {
        public bool IsUserBlocked(User data)
        {
            var checkBlockUser = new WebItem($"//a[@class='tag is-medium is-link' and contains(text(), '{data.Login}')]", "Поле с логином заблокированного пользователя");
            if (checkBlockUser.WaitElementDisplayed() == false)
            {
                return false;
            }
            return true;
        }

    }
}
