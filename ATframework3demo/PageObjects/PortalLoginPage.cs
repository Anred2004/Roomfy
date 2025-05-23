﻿using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ATframework3demo.PageObjects;

namespace atFrameWork2.PageObjects
{
    class PortalLoginPage : BaseLoginPage
    {
        IWebDriver Driver { get; }

        WebItem loginField => new WebItem("//input[@name = 'USER_LOGIN']", "Поле Логин");
        WebItem passwrdField => new WebItem("//input[@name = 'USER_PASSWORD']", "Поле Пароль");
        WebItem btnEnter => new WebItem("//button[@name = 'Login']", "Кнопка Войти");

        public PortalLoginPage(PortalInfo portal, IWebDriver driver = default) : base(portal)
        {
            Driver = driver;
        }
        public PortalHomePage Login(User admin)
        {
            WebDriverActions.OpenUri(portalInfo.PortalUri, Driver);

            return new PortalHomePage(Driver);
        }
        public PortalHomePage LoginUser(User user)
        {
            WebDriverActions.OpenUri(portalInfo.PortalUri, Driver);
            loginField.SendKeys(user.Login, Driver);
            passwrdField.SendKeys(user.Password, Driver);
            btnEnter.SendKeys(Keys.Enter, Driver);
            return new PortalHomePage(Driver);
        }
    }
}
