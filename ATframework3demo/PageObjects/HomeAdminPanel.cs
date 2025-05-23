﻿using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class HomeAdminPanel
    {
        WebItem activeUsersPanel => new WebItem("//div[@class = 'title is-2 mt-4' and contains(text(), 'Активные пользователи ')]", "Раздел Активные пользователи в админ панеле");
        WebItem btnUsers => new WebItem("//a[@href= '/admin/users']", "Кнопка Пользователи");
        WebItem btnAllUsers => new WebItem("//a[@href= '/admin/users']", "Кнопка Все пользователи");
        WebItem btnBlockedUsers => new WebItem("//a[@href= '/admin/users?status=blocked']", "Кнопка заблокированные пользователи");
        WebItem btnDeletededUsers => new WebItem("//a[@href= '/admin/users?status=deleted']", "Кнопка удаленные пользователи");
        WebItem btnAdvertising => new WebItem("//a[@href= '/admin/ads']", "Кнопка Реклама");
        WebItem btnAddBiurb => new WebItem("//a[@href= '/admin/add-advert']", "Кнопка Добавить рекламу");
        public ConfirmBlockForm BlockedUser(User data)
        {

            var btnBlockUser = new WebItem($"//div[@class='columns is-vcentered']//*[contains(text(), '{data.Login}')]//following::button[@class='button is-warning modal-button']", "Кнопка заблокировать");
            if (activeUsersPanel.WaitElementDisplayed(15) == false)
            {
                btnUsers.WaitElementDisplayed(20);
                btnUsers.Hover();
                btnAllUsers.WaitElementDisplayed(20);
                btnAllUsers.Click();

            }
            btnBlockUser.WaitElementDisplayed(20);
            btnBlockUser.Click();

            return new ConfirmBlockForm();
        }
        public ConfirmDeleteForm DeletedUser(User data)
        {

            var btnDeleteUser = new WebItem($"//div[@class='columns is-vcentered']//*[contains(text(), '{data.Login}')]//following::button[@class='button is-danger modal-button']", "Кнопка удалить");
            if (activeUsersPanel.WaitElementDisplayed(10) == false)
            {
                btnUsers.WaitElementDisplayed(20);
                btnUsers.Hover();
                btnDeletededUsers.WaitElementDisplayed(20);
                btnDeletededUsers.Click();

            }
            btnDeleteUser.WaitElementDisplayed(20);
            btnDeleteUser.Click();

            return new ConfirmDeleteForm();
        }
        public BlockedAdminPanel GoToSectionBloked()
        {
            btnUsers.WaitElementDisplayed(20);
            btnUsers.Hover();
            btnBlockedUsers.WaitElementDisplayed(20);
            btnBlockedUsers.Click();

            return new BlockedAdminPanel();
        }
        public DeletedAdminPanel GoToSectionDeleted()
        {
            btnUsers.WaitElementDisplayed(20);
            btnUsers.Hover();
            btnDeletededUsers.WaitElementDisplayed(20);
            btnDeletededUsers.Click();

            return new DeletedAdminPanel();
        }
        public AddBlurbPage GoToAddBlurb()
        {
            btnAdvertising.WaitElementDisplayed(20);
            btnAdvertising.Hover();
            btnAddBiurb.WaitElementDisplayed(20);
            btnAddBiurb.Click();

            return new AddBlurbPage();
        }
    }
}
