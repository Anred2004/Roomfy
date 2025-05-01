using atFrameWork2.SeleniumFramework;
using ATframework3demo.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class BlurbPostsPage
    {

        WebItem btnDeleteBlurb => new WebItem("//i[@class = 'fa-solid fa-trash']", "Кнопка удаления рекламы");
        WebItem btnDeleteCheckBlurb => new WebItem("//button[@class = 'button is-danger' and contains(text(), 'Удалить')] ", "Кнопка подтверждения удаления рекламы");

        public bool CheckBlurb(RoomfyPromotion link)
        {
            var checkBlurvDisplayed = new WebItem($"//th[text()='{link.Link}']", "Сообщение об ошибке");
            if (checkBlurvDisplayed.WaitElementDisplayed() == false)
            {
                return false;
            }
            return true;
        }

        public BlurbPostsPage DeleteBlurb()
        {

            btnDeleteBlurb.WaitElementDisplayed(60);
            btnDeleteBlurb.Click();
            btnDeleteCheckBlurb.WaitElementDisplayed(60);
            btnDeleteCheckBlurb.Click();

            return new BlurbPostsPage();
        }
    }
}
