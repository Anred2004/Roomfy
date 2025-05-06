using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using Microsoft.Extensions.Hosting;

namespace ATframework3demo.PageObjects
{
    public class ChatPage
    {
        WebItem messgeForm => new WebItem("//textarea[@id='message']", "Форма написания сообщения");
        WebItem btnSend => new WebItem("//button[@id='send-btn']", "Кнопка Отправить");
        public ChatPage SendMessage(User messge)
        {
            messgeForm.WaitElementDisplayed(20);
            messgeForm.Click();
            messgeForm.SendKeys(messge.Message);
            btnSend.WaitElementDisplayed(20);
            btnSend.Click();

            return new ChatPage(); 
        }
        public ChatPage OpenChat(User data)
        {
            var chatForm = new WebItem($"//p[@class = 'subtitle is-6' and contains(text(), '{data.Message}')]", "Форма чата в разделе Чат");
            chatForm.WaitElementDisplayed(20);
            chatForm.Click();

            return new ChatPage();
        }
        public bool IsMessageDisplayed(User data)
        {
            var checkMessage = new WebItem($"//div[@class='message-content']/p[text()='{data.Message}']", "Блок отправленного сообщения");
            if (checkMessage.WaitElementDisplayed() == false)
            {
                return false;
            }
            return true;
        }
    }
}
