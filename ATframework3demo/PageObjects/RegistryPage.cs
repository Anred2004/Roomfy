using System.Reflection.Metadata;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using OpenQA.Selenium;

namespace ATframework3demo.PageObjects
{
    public class RegistryPage
    {

        WebItem loginField => new WebItem($"//input[@name = 'USER_LOGIN']", "Поле Логин");
        WebItem passwrdField => new WebItem($"//input[@name = 'USER_PASSWORD']", "Поле Пароль");
        WebItem confirmPasswrdField => new WebItem($"//input[@name = 'USER_CONFIRM_PASSWORD']", "Поле Подтверждение пароля");
        WebItem emailField => new WebItem($"//input[@name = 'USER_EMAIL']", "Поле Email");
        WebItem btnRegistry => new WebItem($"//button[@name = 'Register']", "Кнопка Зарегистрироваться");

        public RoomfyHomePage Register(User userDate)
        {
            loginField.WaitElementDisplayed(20);
            loginField.SendKeys(userDate.Login);
            passwrdField.WaitElementDisplayed(20);
            passwrdField.SendKeys(userDate.Password);
            confirmPasswrdField.WaitElementDisplayed(20);
            confirmPasswrdField.SendKeys(userDate.Password);
            emailField.WaitElementDisplayed(20);
            emailField.SendKeys(userDate.Email);
            btnRegistry.WaitElementDisplayed(20);
            btnRegistry.Click();
            

            return new RoomfyHomePage();
        }
    }
}
