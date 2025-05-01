using atFrameWork2.BaseFramework;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.BaseFramework;
using ATframework3demo.PageObjects;
using ATframework3demo.TestEntities;
using atFrameWork2.BaseFramework.LogTools;

namespace ATframework3demo.TestCases
{
    public class Case_Roomfy_Chat : CaseCollectionBuilder
    {

        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("Отправка личного сообщения пользователю через главную страницу", homePage => SendMessge(homePage)));
            return caseCollection;
        }

        public User newUser1;
        public User newUser2;

        public Case_Roomfy_Chat()
        {
            newUser1 = new User { Login = "login" + HelperMethods.GetDateTimeSaltString(), Password = "passwd" + HelperMethods.GetDateTimeSaltString(),
                Message = "Привет мой друг!" + HelperMethods.GetDateTimeSaltString() };
            newUser2 = new User { Login = "login" + HelperMethods.GetDateTimeSaltString(), Password = "passwd" + HelperMethods.GetDateTimeSaltString() };
        }

        void SendMessge(PortalHomePage homePage)
        {

            newUser2.CreateUser(TestCase.RunningTestCase.TestPortal);
            newUser1.CreateUser(TestCase.RunningTestCase.TestPortal);

            var newPost = new RoomfyPost
            {
                Title = "Обьявление" + HelperMethods.GetDateTimeSaltString(),
                Description = "Описание" + HelperMethods.GetDateTimeSaltString(),

            };
            newPost.CreatePost(TestCase.RunningTestCase.TestPortal, newUser2);


            //открыть страницу авторизации
            var authPage = homePage.
                //авторизоваться
                Auth(newUser1)
            //перейти к обьявлению
                .GoToPost()
            //перейти в чат с юзером
                .OpenChatWithUser(newPost)
            //отправить сообщение
                .SendMessage(newUser1);

            //залогинеться другим юзером 

            WebItem.DefaultDriver.Quit();
            WebItem.DefaultDriver = null;

            new PortalLoginPage(TestCase.RunningTestCase.TestPortal)
                .LoginUser(newUser2);

            var chatPage = homePage.
                //перейти в чат
                GoToChat()
            //открыть чат с пользователем
                .OpenChat(newUser1);

            //проверить наличие сообщения
            bool isMessageDisplayed = chatPage.IsMessageDisplayed(newUser1);
            if (isMessageDisplayed == false)
            {
                throw new Exception("Сообщение не отображается");
            }
            Log.Info("Сообщение отображается");

        }
    }
}
