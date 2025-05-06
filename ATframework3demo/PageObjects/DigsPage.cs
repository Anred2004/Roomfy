using atFrameWork2.SeleniumFramework;
using ATframework3demo.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class DigsPage

    {
        public ChatPage OpenChatWithUser(RoomfyPost post)
        {
            var btnWrite = new WebItem($"//div[@class='card post-card '][.//span[text()='{post.Title}']]//a[@class='button is-small' and contains(text(), 'Написать')]", "Кнопка Написать");
            var userPost = new WebItem($"//div[contains(@class, 'card post-card ')][.//span[text()='{post.Title}'] and .//a[@class='button is-small']]", "Пост в разделе жилье");
            userPost.WaitElementDisplayed(20);
            userPost.Hover();
            btnWrite.WaitElementDisplayed(20);
            btnWrite.Click();
            return new ChatPage();
        }
    }
}
