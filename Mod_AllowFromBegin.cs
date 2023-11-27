using HarmonyLib;
using System.ComponentModel;
using System.IO;
using UnityEngine;

namespace ieishi.mod.yotogimichi
{
    [HarmonyPatch(typeof(GameControl), nameof(GameControl.titleBtCheck))]
    public class GameControl_titleBtCheck
    {
        public static void Postfix(GameControl __instance)
        {
            if ((__instance.GIRL.SC.MAP_NUMBER == 0) | (__instance.GIRL.SC.MAP_NUMBER == 15) | (__instance.GIRL.SC.MAP_NUMBER == 13))
            {
                //Logging.Log("enable bt_kisekae");
                __instance.BT_KISEKAE.SetActive(value: true);
                __instance.BT_KISEKAE_KAMIKAZARI.SetActive(value: true);
                __instance.BT_KISEKAE_HUKU.SetActive(value: true);
                __instance.BT_KISEKAE_HUKU_RIBON.SetActive(value: true);
                __instance.BT_KISEKAE_SKIRT.SetActive(value: true);
                __instance.BT_KISEKAE_EYE.SetActive(value: true);
                __instance.BT_KISEKAE_HAIR.SetActive(value: true);
            }
            else
            {
                //Logging.Log("disable bt_kisekae");
                __instance.BT_KISEKAE.SetActive(value: false);
            }

            __instance.BT_SHOP.SetActive((__instance.GIRL.SHOP.SC.SHOP_NUMBER_IN_SCENE != -1));

        }
    }


    [HarmonyPatch(typeof(GameSaveControl), nameof(GameSaveControl.loadData))]
    public class GameSaveControl_loadData
    {
        public static void Postfix(GameSaveControl __instance)
        {
            Logging.Log("Enable all Skills, Positions and Clothes");

            //for (int i = 0; i < __instance.GIRL.SAVE_DATA.DATAS_SKILL.Length; i++)
            //{
            //    __instance.GIRL.SAVE_DATA.DATAS_SKILL[i] = true;
            //}

            //for (int i = 0; i < __instance.GIRL.SAVE_DATA.DATAS_TAII.Length; i++)
            //{
            //    __instance.GIRL.SAVE_DATA.DATAS_TAII[i] = true;
            //}


            //clothes
            for (int i = 0; i < __instance.GIRL.SAVE_DATA.DATAS_PANTU.Length; i++)
            {
                __instance.GIRL.SAVE_DATA.DATAS_PANTU[i] = false;
            }

            for (int i = 0; i < __instance.GIRL.SAVE_DATA.DATAS_BURA.Length; i++)
            {
                __instance.GIRL.SAVE_DATA.DATAS_BURA[i] = false;
            }

            for (int i = 0; i < __instance.GIRL.SAVE_DATA.DATAS_HUKU.Length; i++)
            {
                __instance.GIRL.SAVE_DATA.DATAS_HUKU[i] = false;
            }

            for (int i = 0; i < __instance.GIRL.SAVE_DATA.DATAS_SKART.Length; i++)
            {
                __instance.GIRL.SAVE_DATA.DATAS_SKART[i] = false;
            }

            for (int i = 0; i < __instance.GIRL.SAVE_DATA.DATAS_KAMIKAZARI.Length; i++)
            {
                __instance.GIRL.SAVE_DATA.DATAS_KAMIKAZARI[i] = false;
            }

            for (int i = 0; i < __instance.GIRL.SAVE_DATA.DATAS_HUKU_RIBON.Length; i++)
            {
                __instance.GIRL.SAVE_DATA.DATAS_HUKU_RIBON[i] = false;
            }

            for (int i = 0; i < __instance.GIRL.SAVE_DATA.DATAS_HAIR.Length; i++)
            {
                __instance.GIRL.SAVE_DATA.DATAS_HAIR[i] = false;
            }

            for (int i = 0; i < __instance.GIRL.SAVE_DATA.DATAS_EYE.Length; i++)
            {
                __instance.GIRL.SAVE_DATA.DATAS_EYE[i] = false;
            }


            string text = Application.dataPath + "/../DATA_DRESS/";

            string[] parts = { "pants", "bra", "skirt", "clothes", "hair_acc", "hair", "eye", "clothes_acc" };
            foreach (var part in parts)
            {
                //Logging.Log(part);
                int num = 0;
                for (int k = 0; k < 100; k++)
                {
                    if (!File.Exists(text + "yotogi_dress_pack_" + part + "_" + k))
                    {
                        continue;
                    }
                    AssetBundle assetBundle = AssetBundle.LoadFromFileAsync(text + "yotogi_dress_pack_" + part + "_" + k).assetBundle;
                    assetBundle.LoadAssetAsync<GameObject>("yotogi_dress_pack_" + part + "_" + k);
                    if (assetBundle.LoadAsset<GameObject>(part + "_" + k * 10) != null)
                    {

                        for (int l = 0; l < 10; l++)
                        {
                            GameObject gameObject = assetBundle.LoadAsset<GameObject>(part + "_" + (k * 10 + l));
                            if (gameObject == null)
                            {
                                break;
                            }

                            DressItem component = gameObject.GetComponent<DressItem>();

                            switch (part)
                            {
                                case "pants":
                                    __instance.GIRL.SAVE_DATA.DATAS_PANTU[component.NUMBER] = true;
                                    break;
                                case "bra":
                                    __instance.GIRL.SAVE_DATA.DATAS_BURA[component.NUMBER] = true;
                                    break;
                                case "skirt":
                                    __instance.GIRL.SAVE_DATA.DATAS_SKART[component.NUMBER] = true;
                                    break;
                                case "clothes":
                                    __instance.GIRL.SAVE_DATA.DATAS_HUKU[component.NUMBER] = true;
                                    break;
                                case "hair_acc":
                                    __instance.GIRL.SAVE_DATA.DATAS_KAMIKAZARI[component.NUMBER] = true;
                                    break;
                                case "hair":
                                    __instance.GIRL.SAVE_DATA.DATAS_HAIR[component.NUMBER] = true;
                                    break;
                                case "eye":
                                    __instance.GIRL.SAVE_DATA.DATAS_EYE[component.NUMBER] = true;
                                    break;
                                case "clothes_acc":
                                    __instance.GIRL.SAVE_DATA.DATAS_HUKU_RIBON[component.NUMBER] = true;
                                    break;
                            }
                        }
                        num++;
                        if (num >= 5)
                        {
                            num = 0;
                        }
                    }
                    else
                    {
                        Debug.Log("ASSETBUNDLE ERROR!!!");
                        Application.Quit();
                    }
                    assetBundle.Unload(unloadAllLoadedObjects: false);
                }
            }
        }
    }

    [HarmonyPatch(typeof(TextureControl), nameof(TextureControl.kisekaeKoteiToggleCheck))]
    public class TextureControl_kisekaeKoteiToggleCheck
    {
        public static void Postfix(TextureControl __instance)
        {
            __instance.TG_SITAGI_PEA.gameObject.SetActive(value: true);
            __instance.TG_HUKU_KOTEI.gameObject.SetActive(value: true);
            __instance.TG_HUKU_KOTEI_OKINI.gameObject.SetActive(value: true);
            __instance.TG_SKIRT_KOTEI.gameObject.SetActive(value: true);
            __instance.TG_SKIRT_KOTEI_OKINI.gameObject.SetActive(value: true);
            __instance.TG_BURA_KOTEI.gameObject.SetActive(value: true);
            __instance.TG_BURA_KOTEI_OKINI.gameObject.SetActive(value: true);
            __instance.TG_PANTU_KOTEI.gameObject.SetActive(value: true);
            __instance.TG_PANTU_KOTEI_OKINI.gameObject.SetActive(value: true);
            __instance.TG_KAMIKAZARI_KOTEI.gameObject.SetActive(value: true);
            __instance.TG_KAMIKAZARI_KOTEI_OKINI.gameObject.SetActive(value: true);
            __instance.TG_HUKU_RIBON_KOTEI.gameObject.SetActive(value: true);
            __instance.TG_HUKU_RIBON_KOTEI_OKINI.gameObject.SetActive(value: true);
            __instance.TG_HAIR_KOTEI.gameObject.SetActive(value: true);
            __instance.TG_HAIR_KOTEI_OKINI.gameObject.SetActive(value: true);
            __instance.TG_EYE_KOTEI.gameObject.SetActive(value: true);
            __instance.TG_EYE_KOTEI_OKINI.gameObject.SetActive(value: true);

        }
    }

    [HarmonyPatch(typeof(GameControl), nameof(GameControl.eventH))]
    public class GameControl_eventH
    {
        public static void Prefix(GameControl __instance, ref bool __runOriginal)
        {
            if (__instance.GIRL.STATE.LOVE_POINT >= 800)
            {
                __runOriginal = false;
                return;
            }
            __runOriginal = true;
        }
    }

    [HarmonyPatch(typeof(GameControl), nameof(GameControl.initH))]
    public class GameControl_initH
    {
        public static void Postfix(GameControl __instance)
        {
            Logging.Log("Lovepoint: " + __instance.GIRL.STATE.LOVE_POINT);
        }
    }
}
