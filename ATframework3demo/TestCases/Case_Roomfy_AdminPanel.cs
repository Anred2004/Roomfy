using atFrameWork2.BaseFramework;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using ATframework3demo.TestEntities;

namespace ATframework3demo.TestCases
{
    public class Case_Roomfy_AdminPanel : CaseCollectionBuilder
    {
         
        protected override List<TestCase> GetCases()
        {
            return new List<TestCase>
            {
                new TestCase("Блокировка пользователя через Админ-панель", homePage => BlockUser(homePage)),
                new TestCase("Удаление пользователя через Админ-панель", homePage => DeleteUser(homePage)),
                new TestCase("Добавление рекламы через Админ-панель", homePage => AddBlurb(homePage)),
            };
        }


        private User admin;
        public User newUser;

        public Case_Roomfy_AdminPanel()
        {
            admin = new User{ Login = "nastya.redkina.2004@mail.ru", Password = "1234root"};
            newUser = new User{Login = "login" + HelperMethods.GetDateTimeSaltString(), Password = "passwd" + HelperMethods.GetDateTimeSaltString(),};
        }

        void BlockUser(PortalHomePage homePage)
        {

            admin.DeleteUsers(TestCase.RunningTestCase.TestPortal);
            newUser.CreateUser(TestCase.RunningTestCase.TestPortal);

            var authPage = homePage.
                //авторизоваться админом
                Auth(admin)
            //перейти в админ панель
                .GoToAdminPanel()
            //заблокировать пользователя
                .BlockedUser(newUser)
            //подтвердить блокировку
                .ConfirmBlocking()
            //перейти в раздел заблокированных пользователей
                .GoToSectionBloked();

            bool isUserBlocked = authPage.IsUserBlocked(newUser);
            if (isUserBlocked == false)
            {
                throw new Exception("Пользователь не заблокирован");
            }
            Log.Info("Пользователь заблокирован");

            //авторизоваться пользователем
            WebItem.DefaultDriver.Quit();
            WebItem.DefaultDriver = null;

            new PortalLoginPage(TestCase.RunningTestCase.TestPortal)
                .LoginUser(newUser);

            //проверить, что вход не осуществляется
            bool checkBlock = homePage.СheckError();
            if (checkBlock == false)
            {
                throw new Exception("Пользователь не заблокирован");
            }
            Log.Info("Пользователь заблокирован");
        }

        void DeleteUser(PortalHomePage homePage)
        {

            admin.DeleteUsers(TestCase.RunningTestCase.TestPortal);
            newUser.CreateUser(TestCase.RunningTestCase.TestPortal);

            var authPage = homePage.
                //авторизоваться админом 
                Auth(admin)
            //перейти в админ панель
                .GoToAdminPanel()
            //удалить пользователя
                .DeletedUser(newUser)
            //подтвердить удаление пользователя
                .ConfirmDeleting()
            //перейти в раздел удаленных пользователей
                .GoToSectionDeleted();

            bool isUserDeleted = authPage.IsUserDeleted(newUser);
            if (isUserDeleted == false)
            {
                throw new Exception("Пользователь не удален");
            }
            Log.Info("Пользователь удален");

            //авторизоваться пользователем
            WebItem.DefaultDriver.Quit();
            WebItem.DefaultDriver = null;

            new PortalLoginPage(TestCase.RunningTestCase.TestPortal)
                .LoginUser(newUser);
            //проверить, что вход не осуществляется
            bool checkDelete = homePage.СheckError();
            if (checkDelete == false)
            {
                throw new Exception("Пользователь не удален");
            }
            Log.Info("Пользователь удален");
        }

        void AddBlurb(PortalHomePage homePage)
        {
            var imagePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Resources", "example.jpg");

            var blurb = new RoomfyPromotion
            { 
                Link = "https://www.bitrix24" + HelperMethods.GetDateTimeSaltString() + ".ru/",
                Date = "02022026",
                Path = imagePath
            };

            newUser.CreateUser(TestCase.RunningTestCase.TestPortal);

            var authPage = homePage.
                //авторизоваться админом 
                Auth(admin)
            //перейти в админ панель
                .GoToAdminPanel()
            //перейти в раздел Добавить рекламу
                .GoToAddBlurb()
            //опубликовать рекламу
                .PublishBlurb(blurb)
            //перейти в раздел рекламные посты
                .GoToSectionBlurbPosts();
            //проверить наличие рекламы
            bool checkBlurb = authPage.CheckBlurb(blurb);
            if (checkBlurb == false)
            {
                throw new Exception("Реклама не добавлена");
            }
            Log.Info("Реклама успешно добавлена");

            //авторизоваться юзером
            WebItem.DefaultDriver.Quit();
            WebItem.DefaultDriver = null;

            new PortalLoginPage(TestCase.RunningTestCase.TestPortal)
                .LoginUser(newUser);
            //проверить наличие рекламы
            bool isBlurbDisplayed = homePage.IsBlurbDisplayed(blurb);
            if (isBlurbDisplayed == false)
            {
                throw new Exception("Реклама не добавлена");
            }
            Log.Info("Реклама успешно добавлена");

            //удалить рекламу
            WebItem.DefaultDriver.Quit();
            WebItem.DefaultDriver = null;

            new PortalLoginPage(TestCase.RunningTestCase.TestPortal)
                .LoginUser(admin);

            var adminHomePage = homePage
                .GoToHomePage()
                .GoToAdminPanel()
                .GoToAddBlurb()
                .GoToSectionBlurbPosts()
                .DeleteBlurb();
        }
    }
}
