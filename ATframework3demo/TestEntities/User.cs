using AquaTestFramework.CommonFramework.BitrixCPinteraction;
using atFrameWork2.BaseFramework;
using ATframework3demo.BaseFramework.BitrixCPinterraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.TestEntities
{
    public class User
    {
        public User() { }

        public User(string loginAkaEmail, string password, string name, string lastName, string login, string email, string message)
        {
            LoginAkaEmail = loginAkaEmail;
            Name = name;
            LastName = lastName;
            Login = login;
            Password = password;

            Email = email;
            Message = message;

        }

        public string LoginAkaEmail { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string NameLastName => Name + " " + LastName;





        public string Login { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public void CreateUser(PortalInfo testPotal)
        {
            var phpExecutor = new PHPcommandLineExecutor(TestCase.RunningTestCase.TestPortal.PortalUri,
                TestCase.RunningTestCase.TestPortal.PortalAdmin.LoginAkaEmail,
                TestCase.RunningTestCase.TestPortal.PortalAdmin.Password);
            string userCreationPhpCode = $"require_once($_SERVER['DOCUMENT_ROOT'].'/bitrix/modules/main/include/prolog_before.php');" +
                $"\r\n\r\nuse Bitrix\\Main\\Security\\Password;\r\n\r\nfunction createTestUser($userData) {{\r\n    $user = new CUser;" +
                $"\r\n    \r\n    $arFields = [\r\n        \"NAME\"              => $userData['NAME']," +
                $"\r\n        \"LAST_NAME\"         => $userData['LAST_NAME'],\r\n        \"EMAIL\"             => $userData['EMAIL']," +
                $"\r\n        \"LOGIN\"             => $userData['LOGIN'],\r\n        \"PASSWORD\"          => $userData['PASSWORD']," +
                $"\r\n        \"CONFIRM_PASSWORD\"  => $userData['PASSWORD'],\r\n        \"ACTIVE\"            => \"Y\"," +
                $"\r\n        \"GROUP_ID\"          => $userData['GROUP_ID'] ?? [2], \r\n        \"LID\"               => " +
                $"\"s1\", \r\n        \"PERSONAL_GENDER\"   => $userData['GENDER'] ?? ''," +
                $"\r\n        \"PERSONAL_PHONE\"    => $userData['PHONE'] ?? '',\r\n    ];" +
                $"\r\n    \r\n    $ID = $user->Add($arFields);" +
                $"\r\n    \r\n    if ((int)$ID > 0) {{\r\n        echo \"Пользователь {{$userData['LOGIN']}} успешно создан, ID: {{$ID}}\\n\";" +
                $"\r\n        return $ID;\r\n    }} else {{\r\n        echo \"Ошибка при создании пользователя {{$userData['LOGIN']}}" +
                $": \" . $user->LAST_ERROR . \"\\n\";\r\n        return false;" +
                $"\r\n    }}\r\n}}\r\n    $users = [\r\n        [\r\n\t    'NAME' => '{Name}',\r\n            'EMAIL' => 'ivanov@example.com'," +
                $"\r\n            'LOGIN' => '{Login}',\r\n            'PASSWORD' => '{Password}'\r\n        ]\r\n    ];" +
                $"\r\n\r\n    $createdUsers = [];\r\n    foreach ($users as $userData) {{\r\n        $userId = createTestUser($userData);" +
                $"\r\n        if ($userId) {{\r\n            $createdUsers[] = $userId;\r\n        }}\r\n    }}" +
                $"\r\n\r\n    echo \"Создано пользователей: \" . count($createdUsers) . \"\\n\";" +
                $"\r\n    echo \"ID созданных пользователей: \" . implode(', ', $createdUsers) . \"\\n\";";
            phpExecutor.Execute(userCreationPhpCode);

        }

        public void DeleteUsers(PortalInfo testPotal)
        {
            var phpExecutor = new PHPcommandLineExecutor(TestCase.RunningTestCase.TestPortal.PortalUri,
                TestCase.RunningTestCase.TestPortal.PortalAdmin.LoginAkaEmail,
                TestCase.RunningTestCase.TestPortal.PortalAdmin.Password);
            string userDelitePhpCode = $"require_once($_SERVER['DOCUMENT_ROOT'].'/bitrix/modules/main/include/prolog_before.php');" +
                $"\r\nfunction deleteUserById($userId) {{\r\n    global $DB;" +
                $"\r\n\r\n    $DB->Query(\"DELETE FROM up_roomfy_messages WHERE RECIEVER_ID = $userId\");" +
                $" \r\n    $DB->Query(\"DELETE FROM up_roomfy_posts WHERE USER_ID = $userId\");" +
                $" \r\n    $DB->Query(\"DELETE FROM b_user_group WHERE USER_ID = $userId\");" +
                $" \r\n    $user = new CUser;\r\n    $result = $user->Delete($userId);" +
                $"\r\n\r\n    if ($result) {{\r\n        echo \"Пользователь с ID {{$userId}} успешно удален.\\n\";" +
                $"\r\n        return true;\r\n    }} else {{\r\n        echo \"Ошибка при удалении пользователя с ID {{$userId}}:" +
                $" \" . $user->LAST_ERROR . \"\\n\";\r\n        return false;\r\n    }}\r\n}}" +
                $"\r\nfunction deleteAllUsersExcept($excludedIds) {{\r\n    global $DB;" +
                $"\r\n\r\n    $excludedIdsList = implode(',', $excludedIds); // Преобразуем массив в строку для SQL-запроса" +
                $"\r\n    $query = \"\r\n        SELECT ID \r\n        FROM b_user \r\n        WHERE ID NOT IN ($excludedIdsList)" +
                $"\r\n    \";\r\n    $result = $DB->Query($query);\r\n\r\n    while ($row = $result->Fetch()) " +
                $"{{\r\n        $userId = $row['ID'];\r\n        deleteUserById($userId);\r\n    }}\r\n}}" +
                $"\r\n$excludedUserIds = [1, 20]; // Список ID пользователей, которых не нужно удалять" +
                $"\r\ndeleteAllUsersExcept($excludedUserIds);";
            phpExecutor.Execute(userDelitePhpCode);

        }



        public string GetDBid(Uri portalUri, User adminUser)
        {
            var result = PortalDatabaseExecutor.ExecuteQuery("select ID from b_user where EMAIL = '" + LoginAkaEmail + "'", portalUri, adminUser);
            return result.Count == 0 ? null : result[0].ID;
        }
    }
}
