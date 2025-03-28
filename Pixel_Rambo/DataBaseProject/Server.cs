using DataBaseProject.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage;
using Windows.System;




namespace DataBaseProject
{
    //המחלקה מכילה פעולות המאפשרות לטפל במסד נתונים
    public static class Server
    {
        private static string dbpath = ApplicationData.Current.LocalFolder.Path; //נתיב מסד הנתונים במחשב
        private static string connectionString = "Filename=" + dbpath + "\\DB_Path.db"; //הנתיב שדרכו התוכנית מתחברת למסד הנתונים
        public static GameUser AddNewUser(string name, string password, string mail)
        {
            int? userId = ValidateUser(name, password); //האפ המשתמש כבר נמצא במערכת
            if (userId != null)
                return null;
            //אם המשכנו זאת אומרת שלא נמצא חשבון כזה במאגר ולכן נוסיף אותו
            string query = $"INSERT INTO [User] (Username, UserPassword, Email) VALUES ('{name}','{password}','{mail}')";
            Execute(query);
            userId = ValidateUser(mail, password); //קבלת UserId של המשתמש לאחר הוספתו לטבלה
            AddGameData(userId.Value);

            //AddUserProdact(userId.Value);
            return GetUser(userId.Value);


        }

        public static int GetLives(int userId)
        {
            // Query counts the number of rows where UserId is @userId and ProductId is 1
            string query = "SELECT COUNT(*) FROM [Storage] WHERE UserId = @userId AND ProductId = 1";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    long count = (long)command.ExecuteScalar();
                    // If there's at least one record, return 4, otherwise return 3
                    return count > 0 ? 4 : 3;
                }
            }
        }
        public static int CheckSpeed(int userId)
        {
            // Query counts the number of rows where UserId is @userId and ProductId is 1
            string query = "SELECT COUNT(*) FROM [Storage] WHERE UserId = @userId AND ProductId = 2";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    long count = (long)command.ExecuteScalar();
                    // If there's at least one record, return 4, otherwise return 3
                    return count > 0 ? 5 : 7;
                }
            }
        }

        private static void AddGameData(int value)
        {

            string query = $"INSERT INTO [GameData] (UserId, CurrentLevelId, CurrentSkinId, MaxLevelId, Money) VALUES ({value}, '1','1','1','0')";
            Execute(query);
            string query2 = $"INSERT INTO [KeyBinds] (UserId, Shoot, Jump, Left, [Right]) VALUES ({value}, 'Z','Space','Left Arrow','Right Arrow')";
            Execute(query2);


        }

        public static bool HasPowerBullet(int userId)
        {
            // SQL query to count rows with the specified userId and ProductId equal to 2
            string query = "SELECT COUNT(*) FROM [Storage] WHERE UserId = @userId AND ProductId = 3";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    // ExecuteScalar returns an object; cast it to long
                    long count = (long)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public static List<string> GetKeyBinds(int userId)
        {

            List<string> keybinds = new List<string>();
            string query = "SELECT Shoot, Jump, Left, [Right] FROM [KeyBinds] WHERE UserId = @UserId";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            keybinds.Add(reader.GetString(0)); // Shoot
                            keybinds.Add(reader.GetString(1)); // Jump
                            keybinds.Add(reader.GetString(2)); // Left
                            keybinds.Add(reader.GetString(3)); // Right
                        }
                    }
                }
            }
            return keybinds;
        }

        /*
     ושולפת ממנה את מספרי המוצרים שנמצאים בבעלותו של השחקן, כלומר storage הפעולה ניגשת לטבלת המחסן 
     מספרי המוצרים שהשחקן קנה בעבר. הפעולה מחזירה את רשימת מספרי המוצרים הללו
     משתמשים בפעולה זו כדי למנוע מהשחקן לקנות מוצר שכבר קנה בעבר
    */
        public static List<int> GetOwnProductId(GameUser gameUser)
        {
            List<int> ownProductsIds = new List<int>();

            // If the user is null, return an empty list.


            string query = $"SELECT ProductId FROM [Storage] WHERE UserId={gameUser.UserId}";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();

                // Loop through any rows; if no rows exist, this loop won't run.
                while (reader.Read())
                {
                    // Check for DBNull to ensure a valid value
                    if (!reader.IsDBNull(0))
                    {

                        ownProductsIds.Add(reader.GetInt32(0));
                    }
                }
            }

            return ownProductsIds;
        }

        public static void ResetGuestUser()
        {
            string Query = "UPDATE [GameData] SET CurrentLevelId = 1, CurrentSkinId = 1, MaxLevelId = 1, Money = 0 WHERE UserId = '0'";
            Execute(Query);
            string Query2 = "UPDATE [KeyBinds] SET Shoot = 'Z', Jump = 'Space', Left = 'A', [Right] = 'D' WHERE UserId = '0'";
            Execute(Query2);
            string Query3 = "DELETE FROM [Storage] WHERE UserId = '0'";
            Execute(Query3);
        }

        public static void AddProduct(GameUser user, int productId)
        {


            string query = "INSERT INTO [Storage] (UserId, ProductId) VALUES (@UserId, @ProductId)";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.ExecuteNonQuery();
                }
            }
        }



        public static int Get_Tree_Delay(int levelId)
        {
            int delay = 0;
            string query = "SELECT Tree_Bullet_Delay FROM [GameLevel] WHERE LevelId = @LevelId";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LevelId", levelId); // Add this line to bind the parameter

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Use if instead of while, since we expect one row
                            delay = reader.GetInt32(0);
                    }
                }
            }
            return delay;
        }

        /*
הפעולה מחזירה משתמש אשר כל שדותיו מלאים
הפעולה אוספת נתונים מ- 4 טבלאות וממלאה באמצעותם את המשתמש 
ולוקחת משם User כדי שיוכל לגשת למשחק. בשלב התחלתי הפעולה ניגשת לטבלת
של המשתמש Id,Name,Mail
הממשיכה למלא את נתוני המשתמש,SetUser לאחר מכן הפעולה נעזרת בפעולת עזר 
*/
        public static int? ValidateUser(string email, string UserPassword)
        {
            string query = $"SELECT UserId FROM [User] WHERE Email='{email}' AND UserPassword='{UserPassword}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return reader.GetInt32(0);

                }
                return null;
            }
        }

        public static void SaveKeyBinds(int index, int currentUserId, string newkey)
        {
            // Convert newkey string to VirtualKey.
            if (!Enum.TryParse(newkey, true, out VirtualKey parsedKey))
            {
                // If the key string can't be parsed, do nothing.
                return;
            }

            // Update the in-memory virtual key based on the provided index.


            // Retrieve the current user ID (assuming 0 means not logged in).

            //if (currentUserId == 0)
            //{
            //    // User is not logged in; only update the in-memory key.
            //    return;
            //}

            // Determine which column to update based on the index.
            string columnName = "";
            switch (index)
            {
                case 1:
                    columnName = "Shoot";
                    break;
                case 2:
                    columnName = "Jump";
                    break;
                case 3:
                    columnName = "Left";
                    break;
                case 4:
                    // "Right" is a reserved word in some SQL dialects; use square brackets.
                    columnName = "[Right]";
                    break;
                default:
                    return;
            }

            // Build the update query.
            string query = $"UPDATE [KeyBinds] SET {columnName} = @newkey WHERE UserId = @userId";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newkey", newkey);
                    command.Parameters.AddWithValue("@userId", currentUserId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Execute(string query)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();

            }
        }


        public static void UpdateUserPassword(string username, string email, string newPassword)
        {
            // Use a parameterized query to safely update the password
            string query = "UPDATE [User] SET UserPassword = @newPassword WHERE UserName = @username AND Email = @Email";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newPassword", newPassword);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.ExecuteNonQuery();
                }
            }
        }


        public static List<string> GetTop5RichestUsers()
        {
            List<KeyValuePair<int, int>> userMoneyList = new List<KeyValuePair<int, int>>();
            List<string> topUsers = new List<string>();

            string query = "SELECT UserId, Money FROM [GameData]";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    int money = reader.GetInt32(1);
                    userMoneyList.Add(new KeyValuePair<int, int>(userId, money));
                }
            }

            // Find the top 5 users
            for (int i = 0; i < 5; i++)
            {
                if (userMoneyList.Count == 0)
                    break;

                // Find user with the max money
                KeyValuePair<int, int> richestUser = userMoneyList[0];
                foreach (var user in userMoneyList)
                {
                    if (user.Value > richestUser.Value)
                    {
                        richestUser = user;
                    }
                }

                // Get username and add to the list
                string username = GetUsernameById(richestUser.Key);
                if (!string.IsNullOrEmpty(username))
                {
                    topUsers.Add(username);
                }

                // Remove the richest user from the list
                userMoneyList.Remove(richestUser);
            }

            return topUsers;
        }

        private static string GetUsernameById(int userId)
        {
            string username = null;
            string query = $"SELECT UserName FROM [User] WHERE UserId = {userId}";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    username = reader.GetString(0);
                }
            }

            return username;
        }




        public static GameUser GetUser(int userId)
        {
            GameUser user = null;

            string query = $"SELECT UserId, UserName, UserPassword, Email FROM [User] WHERE UserId={userId}";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new GameUser
                    {
                        UserId = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        UserPassword = reader.GetString(2),
                        UserEmail = reader.GetString(3),

                    };
                }



                if (user != null)
                {

                    SetUser(user);
                }
                return user;

            }

        }
        /*
 הפעולה ממשיכה למלא את שדותיו של המשתמש. בשלב הראשון
 MaxLevel,Money,CurrentLevelId,CurrentProductId :ושולפת משם את נתוני המשחק של המשתמש GameData היא ניגשת לטבלת 
 נכנסים וממלאים משתמש MaxLevel,Money ,כמו כן 
 במשתמש CurrentLevel -על מנת למלא את ה Level ניגשים לטבלת CurrentLevelId לאחר מכן באמצעות 
 SetCurrentLevel על זה תהיה אחראית פעולת עזר  
 GameData ששלפנו מהטבלה currentProductId בשלב הבא בעזרת 
 אשר השחקן שיחק בפעם האחרונה Feature -כדי לשלוף ממנה את השם ה Product ניגשים לטבלה 
 SetCurrentProduct על זה תהיה אחראית הפעולה .GameUser -הנתון הזה גם יכנס ל
 GameUser לסיכום, באופן מדורג נאספו הנתונים מארבע טבלאות ומילאו את העצם   
 כעת יוכל המשתמש לגשת למשחק
 */
        private static void SetUser(GameUser user)
        {
            int currentLevelId = 0;

            string query = $"SELECT CurrentLevelId, MaxLevelId, Money, CurrentSkinId FROM [GameData] WHERE UserId={user.UserId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user.MaxLevel = reader.GetInt32(1);
                    user.Money = reader.GetInt32(2);
                    user.CurrentSkinId = reader.GetInt32(3);
                    currentLevelId = reader.GetInt32(0);
                }
            }
            SetCurrentLevel(user, currentLevelId);

        }
        /*
 ,שולפת ממנה את נתוני רמת הקושי currentLevelId ולפי Level הפעולה ניגשת לטבלת 
  ומכניסה אותו לתוך המשתמש GameLevel בשלב הבא הפעולה בונה עצם מסוג 
  מפני שנעזר בה בעקבות החלפת רמת הקושי public הדגש: הפעולה
 */
        public static void SetCurrentLevel(GameUser user, int currentLevelId)
        {
            string query = $"SELECT LevelId,LevelNumber, Tree_Bullet_Delay, Chicken_Speed, Tree_Lifes, MushroomSpeed, BulletSpeed FROM [GameLevel] WHERE LevelId={currentLevelId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user.CurrentLevel = new GameLevel
                    {
                        LevelId = reader.GetInt32(0),
                        LevelNumber = reader.GetInt32(1),
                        Tree_Bullet_Delay = reader.GetInt32(2),
                        Chicken_Speed = reader.GetInt32(3),
                        Tree_Lifes = reader.GetInt32(4),
                        Mushroom_Speed = reader.GetInt32(5),
                        Bullet_Speed = reader.GetInt32(6),
                    };
                }
            }
        }
        public static bool DoesUserExist(string username, string email)
        {
            string query = "SELECT UserId FROM [User] WHERE UserName = @username AND Email = @Email";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@Email", email);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
      
        public static bool IsStrongPassword(string password)
        {
            // Check for password strength: minimum 8 characters, 1 uppercase, 1 lowercase, 1 digit, 1 special character.
            return System.Text.RegularExpressions.Regex.IsMatch(
                password,
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }

        public static void SaveData(GameUser user)
        {
            string query = $"UPDATE GameData SET  CurrentLevelId = '{user.CurrentLevel.LevelNumber}',  MaxLevelId = '{user.MaxLevel}', Money = '{user.Money}' WHERE UserId = {user.UserId}; ";
            Execute(query);
        }

    }
}