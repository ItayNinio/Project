using System;
using System.Collections.Generic;
using Windows.System;

namespace GameEngine.GameServices
{
    public static class Keys
    {
        public static VirtualKey LeftKey { get; set; } = VirtualKey.A;
        public static VirtualKey RightKey { get; set; } = VirtualKey.D;
        public static VirtualKey UpKey { get; set; } = VirtualKey.Up;
        public static VirtualKey DownKey { get; set; } = VirtualKey.Down;
        public static VirtualKey JumpKey { get; set; } = VirtualKey.Space;
        public static VirtualKey ShootKey { get; set; } = VirtualKey.Z;

        /// <summary>
        /// Updates the keys based on the database keybinds.
        /// The keybinds list is assumed to have the following order:
        /// 0 - Shoot, 1 - Jump, 2 - Left, 3 - Right.
        /// </summary>
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
    }
}
