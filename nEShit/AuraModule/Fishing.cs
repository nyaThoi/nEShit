using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraModule
{
    public class Fishing
    {
        public static void GetUnwandtedFishType()
        {
            //Check FishingWnd
            if (!WindowManager.GetFishingWindow.IsValid) return;
            //Callback~
            Callback.Fishing.GetVal += FilterSystem;
        }
        private static void FilterSystem(Callback.Fishing.FishType fishType)
        {
            if (Callback.Fishing.FishType.White == fishType)
                UnwandtedFishType_White = true;
            else UnwandtedFishType_White = false;
            if (Callback.Fishing.FishType.Green == fishType)
                UnwandtedFishType_Green = true;
            else UnwandtedFishType_Green = false;
            if (Callback.Fishing.FishType.Orange == fishType)
                UnwandtedFishType_Orange = true;
            else UnwandtedFishType_Orange = false;
            if (Callback.Fishing.FishType.MiracleCube == fishType)
                UnwandtedFishType_MiracleCube = true;
            else UnwandtedFishType_MiracleCube = false;
            if (Callback.Fishing.FishType.FishKing == fishType)
                UnwandtedFishType_FishKinge = true;
            else UnwandtedFishType_FishKinge = false;
            if (Callback.Fishing.FishType.Unknown == fishType)
                Debug.WriteLine($"FilterSystem find Unknown FishType");
        }
        public static bool UnwandtedFishType_White { get; private set; }
        public static bool UnwandtedFishType_Green { get; private set; }
        public static bool UnwandtedFishType_Orange { get; private set; }
        public static bool UnwandtedFishType_MiracleCube { get; private set; }
        public static bool UnwandtedFishType_FishKinge { get; private set; }
    }

}
