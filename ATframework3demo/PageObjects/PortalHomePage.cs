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
        WebItem btnRegistry => new WebItem("//a[@href = '/register']", "Кнопка регистрации");
        WebItem loginField => new WebItem("//input[@name = 'USER_LOGIN']", "Поле Логин");
        WebItem passwrdField => new WebItem("//input[@name = 'USER_PASSWORD']", "Поле Пароль");
        WebItem btnEnter => new WebItem("//button[@name = 'Login']", "Кнопка Войти");
        WebItem checkErrorUser => new WebItem("//div[@class='notification is-danger']", "Сообщение об ошибке");
        public RegistryPage OpenRegistryPage()
        {
            btnRegistry.WaitElementDisplayed(20);
            btnRegistry.Click();
            return new RegistryPage();
        }
        public RoomfyHomePage Auth(User userData)
        {
            loginField.WaitElementDisplayed(20);
            loginField.SendKeys(userData.Login);
            passwrdField.WaitElementDisplayed(20);
            passwrdField.SendKeys(userData.Password);
            btnEnter.WaitElementDisplayed(20);
            btnEnter.Click();

            return new RoomfyHomePage();
        }
        public ChatPage GoToChat()
        {
            btnChat.WaitElementDisplayed(20);
            btnChat.Click();

            return new ChatPage();
        }
        public bool СheckError()
        {
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
            btnHome.WaitElementDisplayed(20);
            btnChat.Click();

            return new RoomfyHomePage();
        }
    }
}
