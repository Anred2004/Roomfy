using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;
using ATframework3demo.BaseFramework;
using ATframework3demo.PageObjects;
using ATframework3demo.TestEntities;

namespace ATframework3demo.TestCases
{
    public class Case_Roomfy_Login : CaseCollectionBuilder
    {

        protected override List<TestCase> GetCases()
        {
            return new List<TestCase>
            {
                new TestCase("Авторизация пользователя", homePage => Auth(homePage)),
                new TestCase("Регистрация пользователя", homePage => Registry(homePage)),
            };
        }

        public User newUser;

        public Case_Roomfy_Login()
        {
            newUser = new User { Login = "login" + HelperMethods.GetDateTimeSaltString(), Password = "passwd" + HelperMethods.GetDateTimeSaltString(), 
                Email = "Email" + HelperMethods.GetDateTimeSaltString() + "@mail.ru" };
        }

        void Auth(PortalHomePage homePage)
        {

            newUser.CreateUser(TestCase.RunningTestCase.TestPortal);

            //открыть страницу авторизации
            var authPage = homePage.
                //авторизоваться
                Auth(newUser);

            //проверить, что пользователь авторизован
            bool isUserAuth = authPage.IsUserRegister();
            if (isUserAuth == false)
            {
                throw new Exception("Пользователь не авторизован");
            }
            Log.Info("Пользователь успешно авторизован");

        }

        void Registry(PortalHomePage homePage)
        {

            //открыть страницу регистрации
            var authPage = homePage.
                OpenRegistryPage()
                //зарегистрироваться
                .Register(newUser);
            //проверить, что пользователь зарегистрирован
            bool isUserRegister = authPage.IsUserRegister();
            if (isUserRegister == false)
            {
                throw new Exception("Пользователь не зарегистрирован");
            }
            Log.Info("Пользователь успешно зарегистрирован");

        }

    }
}
