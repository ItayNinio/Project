using System;
using System.Collections.Generic;
using Windows.System;

namespace GameEngine.GameServices
{
    public static class Keys // מחלקה סטטית המנהלת את המקשים שהשחקן משתמש בהם במשחק
    {
        // משתנה סטטי למקש שמאלה (ברירת מחדל: A)
        public static VirtualKey LeftKey { get; set; } = VirtualKey.A;

        // משתנה סטטי למקש ימינה (ברירת מחדל: D)
        public static VirtualKey RightKey { get; set; } = VirtualKey.D;

        // משתנה סטטי למקש למעלה (ברירת מחדל: חץ למעלה)
        public static VirtualKey UpKey { get; set; } = VirtualKey.Up;

        // משתנה סטטי למקש למטה (ברירת מחדל: חץ למטה)
        public static VirtualKey DownKey { get; set; } = VirtualKey.Down;

        // משתנה סטטי למקש קפיצה (ברירת מחדל: רווח)
        public static VirtualKey JumpKey { get; set; } = VirtualKey.Space;

        // משתנה סטטי למקש ירי (ברירת מחדל: Z)
        public static VirtualKey ShootKey { get; set; } = VirtualKey.Z;

        public static void UpdateKeysFromDatabase(List<string> keybinds)
        {
            if (keybinds == null || keybinds.Count < 4)
                return;

            // Parse and update the Shoot key.
            if (Enum.TryParse(keybinds[0], true, out VirtualKey shoot))
                ShootKey = shoot;

            // Parse and update the Jump key.
            if (Enum.TryParse(keybinds[1], true, out VirtualKey jump))
                JumpKey = jump;

            // Parse and update the Left key.
            if (Enum.TryParse(keybinds[2], true, out VirtualKey left))
                LeftKey = left;

            // Parse and update the Right key.
            if (Enum.TryParse(keybinds[3], true, out VirtualKey right))
                RightKey = right;
        }
        // פעולה שמעדכנת את מקשי השחקן לפי נתונים ממסד הנתונים

    }
}
