using atFrameWork2.SeleniumFramework;
using ATframework3demo.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class AddBlurbPage
    {
        WebItem blurbLinkField => new WebItem("//input[@type = 'url']", "Поле для рекламной ссылки");
        WebItem blurbDate => new WebItem("//input[@id= 'endDate']", "Поле даты окончания рекламы");
        WebItem btnBannerPhoto => new WebItem("//input[@class= 'file-input']", "Кнопка добавления фото баннера");
        WebItem labelBanner => new WebItem("//span[@class = 'file-label']", "Лейбл формы фото баннера");
        WebItem btnSubmit => new WebItem("//button[@type = 'submit']", "Кнопка Опубликовать");
        WebItem btnAdvertising => new WebItem("//a[@href= '/admin/ads']", "Кнопка Реклама");
        public AddBlurbPage PublishBlurb(RoomfyPromotion data)
        {

            blurbLinkField.WaitElementDisplayed(20);
            blurbLinkField.SendKeys(data.Link);
            blurbDate.WaitElementDisplayed(20);
            blurbDate.SendKeys(data.Date);
            labelBanner.WaitElementDisplayed(20);
            btnBannerPhoto.SendKeys(data.Path);
            btnSubmit.WaitElementDisplayed(20);
            btnSubmit.Click();

            return new AddBlurbPage();
        }
        public BlurbPostsPage GoToSectionBlurbPosts()
        {

            btnAdvertising.WaitElementDisplayed(60);
            btnAdvertising.Hover();
            btnAdvertising.Click();

            return new BlurbPostsPage();
        }
    }
}
