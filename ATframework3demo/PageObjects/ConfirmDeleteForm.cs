using atFrameWork2.SeleniumFramework;

namespace ATframework3demo.PageObjects
{
    public class ConfirmDeleteForm
    {
        WebItem btnConfirmDelete => new WebItem("//div[@class='modal is-active']//button[@class='button is-danger']", "Кнопка подтвердить удаление");
        public HomeAdminPanel ConfirmDeleting()
        {
            btnConfirmDelete.WaitElementDisplayed(20);
            btnConfirmDelete.Click();
            return new HomeAdminPanel();
        }
    }
}
