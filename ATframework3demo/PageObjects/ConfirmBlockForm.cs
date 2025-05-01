using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class ConfirmBlockForm
    {

        WebItem blockReazonField => new WebItem("//div[@class='modal is-active']//textarea[@name='block_reason']", "Поле Причина блокировки");
        WebItem btnConfirmBlock => new WebItem("//div[@class='modal is-active']//button[@name='block_user']", "Кнопка подтвердить");

        public HomeAdminPanel ConfirmBlocking()
        {
            blockReazonField.WaitElementDisplayed(60);
            blockReazonField.SendKeys("Вы писали негативные комментарии");
            btnConfirmBlock.WaitElementDisplayed(60);
            btnConfirmBlock.Click();

            return new HomeAdminPanel();
        }
    }
}
