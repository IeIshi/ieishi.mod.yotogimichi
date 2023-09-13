using HarmonyLib;

namespace ieishi.mod.yotogimichi
{
    [HarmonyPatch(typeof(TextureControl), nameof(TextureControl.setActualSetImage))]
    public class TextureControl_setActualSetImage
    {
        public static void Postfix(TextureControl __instance, string part)
        {
            if (__instance.KISEKAE_SHOP_MODE_IS)
            {
                return;
            }

            int num = 0;

            switch (part)
            {
                case "skirt":
                    num = 1;
                    __instance.KISEKAE_ACTIVE_BT_OBJ.SetActive(value: true);
                    //Logging.Log("skirt Undress Button enabled");
                    break;
                case "bra":
                    num = 2;
                    __instance.KISEKAE_ACTIVE_BT_OBJ.SetActive(value: true);
                    //Logging.Log("bra Undress Button enabled");
                    break;
                case "pants":
                    num = 3;
                    __instance.KISEKAE_ACTIVE_BT_OBJ.SetActive(value: true);
                    //Logging.Log("pants Undress Button enabled");
                    break;
                case "hair_acc":
                    num = 4;
                    __instance.KISEKAE_ACTIVE_BT_OBJ.SetActive(value: true);
                    //Logging.Log("hair_acc Undress Button enabled");
                    break;
                case "clothes_acc":
                    num = 5;
                    __instance.KISEKAE_ACTIVE_BT_OBJ.SetActive(value: true);
                    //Logging.Log("clothes_acc Undress Button enabled");
                    break;
            }

            if (__instance.OBJ_DRESS[num] == null)
            {
                __instance.KISEKAE_ACTIVE_BT_OBJ.SetActive(value: false);
            }

        }
    }

    [HarmonyPatch(typeof(avgMsg), nameof(avgMsg.sentakusiOn))]
    public class avgMsg_sentakusiOn
    {
        public static void Postfix(avgMsg __instance, int num)
        {
            //Logging.Log(num);

            switch (num)
            {
                case 180:
                    {
                        Logging.Log("Replace Now!");

                        if (__instance.GIRL.TEXTURE_DATA.OBJ_DRESS[2] == null && __instance.GIRL.TEXTURE_DATA.OBJ_DRESS[3] == null)
                        {
                            Logging.Log("No Bra and Pantie");

                            int[] array67 = new int[2];
                            string[] array68 = new string[2] { "Hしたい！", null };
                            array67[0] = 3;
                            __instance.METHOD_SENTAKUSI[0] = __instance.GIRL.GAME_CONTROL.startHforStory;
                            array68[1] = "何でもない！";
                            array67[1] = 4;
                            __instance.METHOD_SENTAKUSI[1] = __instance.GIRL.GAME_CONTROL.title;
                            __instance.sentakusiSet(array68, __instance.story_num, array67);
                        }
                        else if (__instance.GIRL.TEXTURE_DATA.OBJ_DRESS[2] == null)
                        {
                            Logging.Log("No Bra");

                            int[] array67 = new int[3];
                            string[] array68 = new string[3] { "Hしたい！", null, null };
                            array67[0] = 3;
                            __instance.METHOD_SENTAKUSI[0] = __instance.GIRL.GAME_CONTROL.startHforStory;
                            array68[1] = "パンツ何色？";
                            array67[1] = 1;
                            __instance.METHOD_SENTAKUSI[1] = __instance.GIRL.GAME_CONTROL.title;
                            array68[2] = "何でもない！";
                            array67[2] = 4;
                            __instance.METHOD_SENTAKUSI[2] = __instance.GIRL.GAME_CONTROL.title;
                            __instance.sentakusiSet(array68, __instance.story_num, array67);
                        }
                        else if (__instance.GIRL.TEXTURE_DATA.OBJ_DRESS[3] == null)
                        {
                            Logging.Log("No Pantie");

                            int[] array67 = new int[3];
                            string[] array68 = new string[3] { "Hしたい！", null, null };
                            array67[0] = 3;
                            __instance.METHOD_SENTAKUSI[0] = __instance.GIRL.GAME_CONTROL.startHforStory;
                            array68[1] = "ブラ何色？";
                            array67[1] = 2;
                            __instance.METHOD_SENTAKUSI[1] = __instance.GIRL.GAME_CONTROL.title;
                            array68[2] = "何でもない！";
                            array67[2] = 4;
                            __instance.METHOD_SENTAKUSI[2] = __instance.GIRL.GAME_CONTROL.title;
                            __instance.sentakusiSet(array68, __instance.story_num, array67);
                        }

                        break;
                    }
            }
        }
    }
}

