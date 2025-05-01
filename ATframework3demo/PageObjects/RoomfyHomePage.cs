using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using OpenQA.Selenium;

namespace ATframework3demo.PageObjects
{
    public class RoomfyHomePage
    {

        public IWebDriver Driver { get; }

        public RoomfyHomePage(IWebDriver driver = default)
        {
            Driver = driver;
        }

        WebItem btnDigsActive => new WebItem("//li[@class='tab-btn is-active ' and @data-tab='find_flat']", "Раздел Жилье активный");
        WebItem btnDigsInactive => new WebItem("//li[@class='tab-btn ' and @data-tab='find_flat']", "Раздел Жилье неактивный");
        WebItem btnAdminPanel => new WebItem("//a[@href= '/admin']", "Кнопка Админ панель");

        public bool IsUserRegister()
        {
            var checkRegistry = new WebItem($"//a[@class = 'navbar-item' and contains(text(), 'Главная')]", "Кнопка перехода на главную страницу");
            if (checkRegistry.WaitElementDisplayed() == false)
            {
                return false;
            }
            return true;
        }

        public DigsPage GoToPost()
        {

            if (btnDigsActive.WaitElementDisplayed(10) == false)
            {
                btnDigsInactive.WaitElementDisplayed(60);
                btnDigsInactive.Click();
            }
            return new DigsPage();
        }

        public HomeAdminPanel GoToAdminPanel()
        {

            btnAdminPanel.WaitElementDisplayed(60);
            btnAdminPanel.Click();

            return new HomeAdminPanel();
        }




    }
}
