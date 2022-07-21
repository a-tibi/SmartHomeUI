using System;

using System.Collections.Generic;
using System.Text;

namespace Ground_Floor
{
    class CGlobal
    {
        // CONSTANTS ...
        public const string INCREASE = "increase";
        public const string DECREASE = "decrease";

        public const int CATEGORY_LIGHTING = 0;
        public const int CATEGORY_BLIND = 1;
        public const int CATEGORY_AC = 2;
        public const int CATEGORY_AV = 3;
        public const int CATEGORY_MS = 4;   // Motion Sensor

        public const int ONOFF_DEVICE = 0;
        public const int DIMMING_DEVICE = 1;
        public const int BLIND_DEVICE = 2;
        public const int MSENSOR_DEVICE = 3;
        public const int SCENE_DEVICE = 4;

        public const int BLIND_DEFAULT_VALUE = 0;

        public const int GLOBAL_ZONE = 28;   // Global (Zone_NO = 28)

        public const int MODE = 29;
        public const int MODE_AWAY = 1;    // Mode(Tag) 1 = AWAY_MODE
    }
}
