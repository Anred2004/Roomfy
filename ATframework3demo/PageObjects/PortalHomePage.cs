using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;
using ATframework3demo.TestEntities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.PageObjects
{
    public class PortalHomePage
    {
        public IWebDriver Driver { get; }

        public PortalHomePage(IWebDriver driver = default)
        {
            Driver = driver;
        }

        WebItem btnChat => new WebItem("//a[@href = '/chat']", "Раздел Чат на главной странице");
        WebItem btnHome => new WebItem("//a[@href = '/']", "Кнопка главная страница");

        public RegistryPage OpenRegistryPage()
        {
            var btnRegistry = new WebItem("//a[@href = '/register']", "Кнопка регистрации");
            btnRegistry.WaitElementDisplayed(60);
            btnRegistry.Click();
            return new RegistryPage();
        }

        public RoomfyHomePage Auth(User userData)
        {
            var loginField = new WebItem("//input[@name = 'USER_LOGIN']", "Поле Логин");
            var passwrdField = new WebItem("//input[@name = 'USER_PASSWORD']", "Поле Пароль");
            var btnEnter = new WebItem("//button[@name = 'Login']", "Кнопка Войти");
            loginField.WaitElementDisplayed(60);
            loginField.SendKeys(Keys.Clear);
            loginField.SendKeys(userData.Login);
            passwrdField.WaitElementDisplayed(60);
            passwrdField.SendKeys(Keys.Clear);
            passwrdField.SendKeys(userData.Password);
            btnEnter.WaitElementDisplayed(60);
            btnEnter.Click();

            return new RoomfyHomePage();
        }

        public ChatPage GoToChat()
        {
            btnChat.WaitElementDisplayed(60);
            btnChat.Click();

            return new ChatPage();
        }

        public bool СheckError()
        {
            var checkErrorUser = new WebItem("//div[@class='notification is-danger']", "Сообщение об ошибке");
            if (checkErrorUser.WaitElementDisplayed() == false)
            {
                return false;
            }
            return true;
        }

        public bool IsBlurbDisplayed(RoomfyPromotion link)
        {
            var checkBlurvDisplayed = new WebItem($"//a[@href = '{link.Link}']", "Ссылка на баннер на главной странице");
            if (checkBlurvDisplayed.WaitElementDisplayed() == false)
            {
                return false;
            }
            return true;
        }

        public RoomfyHomePage GoToHomePage()
        {
            btnHome.WaitElementDisplayed(60);
            btnChat.Click();

            return new RoomfyHomePage();
        }

    }
}
