using AquaTestFramework.CommonFramework.BitrixCPinteraction;
using atFrameWork2.BaseFramework;
using atFrameWork2.TestEntities;
using System.Xml.Linq;

namespace ATframework3demo.TestEntities
{
    public class RoomfyPost
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public void CreatePost(PortalInfo testPotal, User user)
        {
            var phpExecutor = new PHPcommandLineExecutor(TestCase.RunningTestCase.TestPortal.PortalUri,
                TestCase.RunningTestCase.TestPortal.PortalAdmin.LoginAkaEmail,
                TestCase.RunningTestCase.TestPortal.PortalAdmin.Password);
            string postCreationPhpCode = $"require_once($_SERVER['DOCUMENT_ROOT'].'/bitrix/modules/main/include/prolog_before.php');" +
                $"\r\n\r\nuse Up\\Roomfy\\Model\\PostsTable;\r\nuse Up\\Roomfy\\Model\\PostLocationTable;\r\nuse Bitrix\\Main\\Type\\DateTime;" +
                $"\r\n\r\nfunction getUserIdByUsername($username)\r\n" +
                $"{{\r\n\r\n    $rsUser = \\Bitrix\\Main\\UserTable::getList([\r\n        'select' => ['ID']," +
                $"\r\n        'filter' => ['LOGIN' => $username],\r\n        'limit' => 1,\r\n    ]);" +
                $"\r\n\r\n    if ($arUser = $rsUser->fetch()) {{\r\n        return $arUser['ID'];" +
                $"\r\n    }} else {{\r\n        echo \"Пользователь с именем '$username' не найден.\\n\";" +
                $"\r\n        return false;\r\n    }}\r\n}}" +
                $"\r\n\r\nfunction createTestPostWithLocation($username, $postTypeId, $title, $description = '', $isActive = true, $isPromoted = false)" +
                $"\r\n{{\r\n    $userId = getUserIdByUsername($username);\r\n    if (!$userId) {{\r\n        echo \"Не удалось найти ID пользователя для имени '$username'.\\n\";" +
                $"\r\n        return false;\r\n    }}\r\n\r\n    $postResult = PostsTable::add([\r\n        'USER_ID' => $userId,\r\n        'POST_TYPE_ID' => $postTypeId," +
                $"\r\n        'TITLE' => $title,\r\n        'DESCRIPTION' => $description,\r\n        'IS_ACTIVE' => $isActive ? 'Y' : 'N'," +
                $"\r\n        'IS_PROMOTED' => $isPromoted ? 'Y' : 'N',\r\n        'CREATED_AT' => new DateTime(),\r\n        'UPDATED_AT' => new DateTime(),\r\n    ]);" +
                $"\r\n    \r\n    if (!$postResult->isSuccess())\r\n    {{\r\n        echo \"Ошибка при создании объявления: \" . implode(', ', $postResult->getErrorMessages()) . \"\\n\";" +
                $"\r\n        return false;\r\n    }}\r\n    \r\n    $postId = $postResult->getId();\r\n    echo \"Объявление успешно создано, ID: $postId\\n\";" +
                $"\r\n\r\n    $locationResult = PostLocationTable::add([\r\n        'POST_ID' => $postId,\r\n        'LATITUDE' => 54.7104,\r\n        'LONGITUDE' => 20.5106," +
                $"\r\n        'COUNTRY' => 'Россия',\r\n        'CITY' => 'Калининград',\r\n        'STREET' =>  '',\r\n        'HOUSE_NUMBER' => '',\r\n        'RADIUS' => 1.0,\r\n    ]);" +
                $"\r\n    \r\n    if ($locationResult->isSuccess())\r\n    {{\r\n        echo \"Локация для объявления успешно добавлена\\n\";\r\n        return $postId;\r\n    }}" +
                $"\r\n    else\r\n    {{\r\n        echo \"Ошибка при добавлении локации: \" . implode(', ', $locationResult->getErrorMessages()) . \"\\n\";" +
                $"\r\n        PostsTable::delete($postId);\r\n        return false;\r\n    }}\r\n}}\r\n\r\n$username = '{user.Login}';" +
                $"\r\n$postTypeId = 1; \r\nfor ($i = 1; $i <= 1; $i++) {{\r\n\t$title = \"{Title}\";\r\n\t$description = \"{Description}\";" +
                $"\r\n    $postId = createTestPostWithLocation(\r\n        $username, \r\n        $postTypeId, \r\n        $title, \r\n        $description, \r\n        true, \r\n        false\r\n    );\r\n}}";
            phpExecutor.Execute(postCreationPhpCode);

        }


    }
}
