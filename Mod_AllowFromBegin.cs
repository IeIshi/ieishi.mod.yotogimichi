using HarmonyLib;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Physics;
using Live2D.Cubism.Framework.Raycasting;
using Live2D.Cubism.Framework;
using Live2D.Cubism.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace ieishi.mod.yotogimichi
{

    /// <summary>
    /// enables shops, cloths
    /// overrides code from Assembly-CSharp.dll
    /// </summary>
    [HarmonyPatch(typeof(GameControl), nameof(GameControl.titleBtCheck))]
    public class GameControl_titleBtCheck
    {
        public static bool Prefix(GameControl __instance)
        {
            // Shops
            if (Plugin.Configuration.FromBeginning_Shops)
            {
                __instance.BT_SHOP.SetActive(__instance.GIRL.SHOP.SC.SHOP_NUMBER_IN_SCENE != -1);
            }
            else
            {
                __instance.BT_SHOP.SetActive(__instance.GIRL.SHOP.checkExistShop());
            }


            if (__instance.GIRL.SC.checkDropItemIsExist())
            {
                __instance.BT_CHECK.SetActive(value: true);
            }
            else
            {
                __instance.BT_CHECK.SetActive(value: false);
            }
            if ((__instance.GIRL.STATE.LIFE_POINT < 20f) | (__instance.TIME_CONTROL.TIME >= 0.85f))
            {
                __instance.BT_SLEEP.SetActive(value: true);
            }
            else
            {
                __instance.BT_SLEEP.SetActive(value: false);
            }
            if (__instance.GIRL.SAVE_DATA.DATAS_STORY[101])
            {
                __instance.BT_H.SetActive(value: true);
            }
            else
            {
                __instance.BT_H.SetActive(value: false);
            }


            // Cloths
            if (Plugin.Configuration.FromBeginning_AllCloths)
            {
                if ((__instance.GIRL.SC.MAP_NUMBER == 0) | (__instance.GIRL.SC.MAP_NUMBER == 15) | (__instance.GIRL.SC.MAP_NUMBER == 13))
                {
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
                    __instance.BT_KISEKAE.SetActive(value: false);
                }
            }
            else
            {
                if (__instance.GIRL.SAVE_DATA.DATAS_STORY[8] & ((__instance.GIRL.SC.MAP_NUMBER == 0) | (__instance.GIRL.SC.MAP_NUMBER == 15) | (__instance.GIRL.SC.MAP_NUMBER == 13)))
                {
                    __instance.BT_KISEKAE.SetActive(value: true);
                }
                else
                {
                    __instance.BT_KISEKAE.SetActive(value: false);
                }
                if (__instance.GIRL.SAVE_DATA.DATAS_STORY[118])
                {
                    __instance.BT_KISEKAE_KAMIKAZARI.SetActive(value: true);
                }
                else
                {
                    __instance.BT_KISEKAE_KAMIKAZARI.SetActive(value: false);
                }
                if (__instance.GIRL.SAVE_DATA.DATAS_STORY[119])
                {
                    __instance.BT_KISEKAE_HUKU.SetActive(value: true);
                    __instance.BT_KISEKAE_HUKU_RIBON.SetActive(value: true);
                    __instance.BT_KISEKAE_SKIRT.SetActive(value: true);
                }
                else
                {
                    __instance.BT_KISEKAE_HUKU.SetActive(value: false);
                    __instance.BT_KISEKAE_HUKU_RIBON.SetActive(value: false);
                    __instance.BT_KISEKAE_SKIRT.SetActive(value: false);
                }
                if (__instance.GIRL.SAVE_DATA.DATAS_STORY[126])
                {
                    __instance.BT_KISEKAE_EYE.SetActive(value: true);
                }
                else
                {
                    __instance.BT_KISEKAE_EYE.SetActive(value: false);
                }
                if (__instance.GIRL.SAVE_DATA.DATAS_STORY[125])
                {
                    __instance.BT_KISEKAE_HAIR.SetActive(value: true);
                }
                else
                {
                    __instance.BT_KISEKAE_HAIR.SetActive(value: false);
                }
            }


            // Work
            if (Plugin.Configuration.FromBeginning_Work)
            {
                if ((__instance.GIRL.SC.MAP_NUMBER == 9) | (__instance.GIRL.SC.MAP_NUMBER == 12) | (__instance.GIRL.SC.MAP_NUMBER == 13) | (__instance.GIRL.SC.MAP_NUMBER == 14) | (__instance.GIRL.SC.MAP_NUMBER == 15))
                {
                    __instance.BT_RYOKOU.SetActive(value: true);
                }
                else
                {
                    __instance.BT_RYOKOU.SetActive(value: false);
                }
            }
            else
            {
                if (__instance.GIRL.SAVE_DATA.DATAS_STORY[140] & ((__instance.GIRL.SC.MAP_NUMBER == 9) | (__instance.GIRL.SC.MAP_NUMBER == 12) | (__instance.GIRL.SC.MAP_NUMBER == 13) | (__instance.GIRL.SC.MAP_NUMBER == 14) | (__instance.GIRL.SC.MAP_NUMBER == 15)))
                {
                    __instance.BT_RYOKOU.SetActive(value: true);
                }
                else
                {
                    __instance.BT_RYOKOU.SetActive(value: false);
                }
            }


            // Travel
            if (Plugin.Configuration.FromBeginning_Travel)
            {
                if ((__instance.GIRL.SC.SCENE_DATA[__instance.GIRL.SC.scene_num].BAITO_NUMBER != -1) & (__instance.TIME_CONTROL.TIME < 1f))
                {
                    __instance.BT_BAITO.SetActive(value: true);
                }
                else
                {
                    __instance.BT_BAITO.SetActive(value: false);
                }
            }
            else
            {
                if (__instance.GIRL.SAVE_DATA.DATAS_STORY[106] & (__instance.GIRL.SC.SCENE_DATA[__instance.GIRL.SC.scene_num].BAITO_NUMBER != -1) & (__instance.TIME_CONTROL.TIME < 1f))
                {
                    __instance.BT_BAITO.SetActive(value: true);
                }
                else
                {
                    __instance.BT_BAITO.SetActive(value: false);
                }
            }

            if (__instance.TIME_CONTROL.isTimeOut())
            {
                __instance.BT_MAP_MOVE.SetActive(value: false);
            }
            else
            {
                __instance.BT_MAP_MOVE.SetActive(value: true);
            }
            if (__instance.GIRL.SC.checkLunchIsExist())
            {
                __instance.BT_LUNCH.SetActive(value: true);
            }
            else
            {
                __instance.BT_LUNCH.SetActive(value: false);
            }

            return false;
        }
    }


    /// <summary>
    /// enables all skills, postions and loads cloth textures
    /// </summary>
    [HarmonyPatch(typeof(GameSaveControl), nameof(GameSaveControl.loadData))]
    public class GameSaveControl_loadData
    {
        public static void Postfix(GameSaveControl __instance)
        {
            //// skills
            if (Plugin.Configuration.FromBeginning_AllSkills)
            {
                for (int i = 0; i < __instance.GIRL.SAVE_DATA.DATAS_SKILL.Length; i++)
                {
                    __instance.GIRL.SAVE_DATA.DATAS_SKILL[i] = true;
                }
            }

            //// postions
            if (Plugin.Configuration.FromBeginning_AllPositions)
            {
                for (int i = 0; i < __instance.GIRL.SAVE_DATA.DATAS_TAII.Length; i++)
                {
                    __instance.GIRL.SAVE_DATA.DATAS_TAII[i] = true;
                }
            }


            //clothes
            if (Plugin.Configuration.FromBeginning_AllCloths)
            {
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
    }


    /// <summary>
    /// enables 'choose by yourself' buttons in cloths
    /// </summary>
    [HarmonyPatch(typeof(TextureControl), nameof(TextureControl.kisekaeKoteiToggleCheck))]
    public class TextureControl_kisekaeKoteiToggleCheck
    {
        public static void Postfix(TextureControl __instance)
        {
            if (Plugin.Configuration.FromBeginning_AllCloths)
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
    }


    /// <summary>
    /// let her cum if lovepoints >800 before confession
    /// </summary>
    [HarmonyPatch(typeof(GameControl), nameof(GameControl.eventH))]
    public class GameControl_eventH
    {
        public static void Prefix(GameControl __instance, ref bool __runOriginal)
        {
            if (Plugin.Configuration.CumBeforeConfession)
            {
                if (__instance.GIRL.STATE.LOVE_POINT >= 800)
                {
                    __runOriginal = false;
                    return;
                }
            }
            __runOriginal = true;
        }
    }


    /// <summary>
    /// lovepoints only for log
    /// use fillEffectCheck instead update() cause its private...
    /// code from Assembly-CSharp.dll
    /// </summary>
    [HarmonyPatch(typeof(avgMsg), nameof(avgMsg.fillEffectCheck))]
    public class avgMsg_fillEffectCheck
    {
        public static void Postfix(avgMsg __instance)
        {
            if (!Plugin.Configuration.ShowLovePointsInConsole) { return; }

            if (__instance.SENTAKUSI_ON)
            {
                return;
            }
            if (__instance.LOAD_END & !__instance.STORY_MODE_CONTINUE & __instance.STORY_MODE & !__instance.STORY_EFECT_ON)
            {
                if (__instance.IsCompleteDisplayText)
                {
                    if (__instance.currentLine >= __instance.avgs.Count && !__instance.STORY_MODE_CONTINUE)
                    {
                        Logging.Log("Lovepoint: " + __instance.GIRL.STATE.LOVE_POINT);
                    }
                }

            }
        }
    }


    /// <summary>
    /// Fill up without penetration
    /// </summary>
    [HarmonyPatch(typeof(AfureTuyuControl), nameof(AfureTuyuControl.releaseShot))]
    public class AfureTuyuControl_releaseShot
    {
        public static bool Prefix(AfureTuyuControl __instance)
        {
            if (Plugin.Configuration.RoundBellyWithoutPenetration)
            {
                __instance.MAX_OBJ = 55;
            }
            return true;
        }
    }


    /// <summary>
    /// set max value to 44 like original
    /// overrides code from Assembly-CSharp.dll
    /// </summary>
    [HarmonyPatch(typeof(AfureTuyuControl), nameof(AfureTuyuControl.getNakadasiOkuCount))]
    public class AfureTuyuControl_getNakadasiOkuCount
    {
        public static bool Prefix(AfureTuyuControl __instance, ref float __result)
        {
            if (!Plugin.Configuration.RoundBellyWithoutPenetration)
            {
                return true;
            }

            float num = __instance.AFURE_OBJCS.Count - 10;
            if (num < 0f)
            {
                num = 0f;
            }
            __result = __instance.AFURE_OKU_OBJCS.Count + num;
            if (__result > 44)
            {
                __result = 44;
            }

            return false;
        }
    }


    /// <summary>
    /// enables all waypoints on big map
    /// overrides code from Assembly-CSharp.dll
    /// </summary>
    [HarmonyPatch(typeof(MapMoveControl), nameof(MapMoveControl.setMapPoint))]
    public class MapMoveControl_setMapPoint
    {
        public static bool Prefix(MapMoveControl __instance)
        {
            if (!Plugin.Configuration.FromBeginning_AllWaypoints)
            {
                return true;
            }

            for (int i = 0; i < __instance.MAP_POINT.Length; i++)
            {
                if (__instance.GIRL.SC.MAP_NUMBER == i)
                {
                    __instance.ICON_MAP_ACTUAL_POSITION.transform.position = __instance.MAP_POINT[i].transform.position;
                    __instance.MAP_POINT[i].SetActive(value: false);
                }
                else if (__instance.MAP_POINT[i].GetComponent<btMenu>().item_kaihou_number == -1)
                {
                    __instance.MAP_POINT[i].SetActive(value: true);
                }
                else
                {
                    __instance.MAP_POINT[i].SetActive(value: true);
                    //__instance.MAP_POINT[i].SetActive(__instance.GIRL.SAVE_DATA.MAP_POINT[__instance.MAP_POINT[i].GetComponent<btMenu>().item_kaihou_number]);
                }
            }
            return false;
        }
    }


    /// <summary>
    /// enables all waypoints on mini map
    /// overrides code from Assembly-CSharp.dll
    /// </summary>
    [HarmonyPatch(typeof(MapMiniControl), nameof(MapMiniControl.setMapPoint))]
    public class MapMiniControl_setMapPoint
    {
        public static bool Prefix(MapMiniControl __instance)
        {
            if (!Plugin.Configuration.FromBeginning_AllWaypoints)
            {
                return true;
            }

            //if (!__instance.IS_FURO_SCENE | (__instance.IS_FURO_SCENE & __instance.GIRL.GAME_CONTROL.MAP_CONTROL.FURO_MOVE_OK))
            //{
            for (int i = 0; i < __instance.MAP_POINT.Length; i++)
            {
                if (__instance.GIRL.SC.scene_num == i)
                {
                    __instance.ICON_MAP_ACTUAL_POSITION.transform.position = __instance.MAP_POINT[i].transform.position;
                    __instance.MAP_POINT[i].SetActive(value: false);
                }
                else if (__instance.MAP_POINT[i].GetComponent<btMenu>().item_kaihou_number == -1)
                {
                    __instance.MAP_POINT[i].SetActive(value: true);
                }
                else
                {
                    __instance.MAP_POINT[i].SetActive(__instance.GIRL.SAVE_DATA.MAP_POINT[__instance.MAP_POINT[i].GetComponent<btMenu>().item_kaihou_number]);
                }
            }
            //    return false;
            //}
            for (int j = 0; j < __instance.MAP_POINT.Length; j++)
            {
                if (j == 0)
                {
                    if (__instance.GIRL.SC.scene_num == j)
                    {
                        __instance.ICON_MAP_ACTUAL_POSITION.transform.position = __instance.MAP_POINT[j].transform.position;
                        __instance.MAP_POINT[j].SetActive(value: false);
                    }
                    else if (__instance.MAP_POINT[j].GetComponent<btMenu>().item_kaihou_number == -1)
                    {
                        __instance.MAP_POINT[j].SetActive(value: true);
                    }
                    else
                    {
                        __instance.MAP_POINT[j].SetActive(value: true);
                        //__instance.MAP_POINT[j].SetActive(__instance.GIRL.SAVE_DATA.MAP_POINT[__instance.MAP_POINT[j].GetComponent<btMenu>().item_kaihou_number]);
                    }
                }
                else
                {
                    __instance.MAP_POINT[j].SetActive(value: true);
                    //__instance.MAP_POINT[j].SetActive(value: false);
                }
                if (__instance.GIRL.GAME_CONTROL.H_IS)
                {
                    __instance.bts[j].enabled = true;
                    //__instance.bts[j].enabled = false;
                }
                else
                {
                    __instance.bts[j].enabled = true;
                }
            }
            return false;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    [HarmonyPatch(typeof(SceneControl), nameof(SceneControl.mainProcess))]
    public class SceneControl_mainProcess
    {

        public static bool Prefix(SceneControl __instance)
        {
            if (Plugin.Configuration.DOF_Fix)
            {
                try
                {
                    GameObject camera = GameObject.Find("Main Camera");
                    if (camera == null)
                    {
                        Logging.Log("no Main Camera");
                    }
                    else
                    {
                        float distance = Vector3.Distance(__instance.GIRL_OBJ.transform.position, camera.transform.position);

                        PostProcessLayer postProcessLayer = camera.GetComponent<PostProcessLayer>();
                        if (postProcessLayer == null)
                        {
                            Logging.Log("no PostProcessLayer");
                        }
                        else
                        {
                            List<PostProcessVolume> volList = new();
                            PostProcessManager.instance.GetActiveVolumes(postProcessLayer, volList, true, true);

                            foreach (PostProcessVolume vol in volList)
                            {
                                PostProcessProfile ppp = vol.profile;
                                if (ppp)
                                {
                                    if (ppp.TryGetSettings<DepthOfField>(out DepthOfField dph))
                                    {
                                        dph.focusDistance.value = distance;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logging.Log(e.Message);
                }
            }

            if (Plugin.Configuration.FromBeginning_MultipleH)
            {
                __instance.GIRL.GAME_CONTROL.H_ONEGAI_OK_IS = true;
            }

            __instance.VCC_SET_DATA.MAX_MOVE_Z = Plugin.Configuration.MaxZoom;

            return true;
        }
    }

    /// <summary>
    /// show girl stats in notes
    /// overrides code from Assembly-CSharp.dll
    /// </summary>
    [HarmonyPatch(typeof(ResultMenuControl), nameof(ResultMenuControl.openMemo))]
    public class ResultMenuControl_openMemo
    {
        public static bool Prefix(ResultMenuControl __instance)
        {
            if (!Plugin.Configuration.FromBeginning_ShowGirlStats)
            {
                return true;
            }

            __instance.W_MEMO.SetActive(value: true);
            __instance.TX_MEMO.text = "";
            __instance.TX_MEMO_2.text = "";
            __instance.TX_MEMO_3.text = "";
            __instance.TX_MEMO_5.text = "";
            if (GameDataMain.OPTIONS[99] == 0f)
            {
                Text tX_MEMO_;
                //if (__instance.GIRL.SAVE_DATA.DATAS_STORY[7])
                //{
                __instance.TX_MEMO_2.text = "<size=\"60\">妹ちゃん調教メモ</size>\nおっぱい右: " + __instance.GIRL.STATE.KANDO_MAX[0] + "\n乳首右: " + __instance.GIRL.STATE.KANDO_MAX[1] + "\nおっぱい左: " + __instance.GIRL.STATE.KANDO_MAX[2] + "\n乳首左: " + __instance.GIRL.STATE.KANDO_MAX[3] + "\nおしり右: " + __instance.GIRL.STATE.KANDO_MAX[16] + "\nおしり左: " + __instance.GIRL.STATE.KANDO_MAX[17] + "\n耳右: " + __instance.GIRL.STATE.KANDO_MAX[20] + "\n耳左: " + __instance.GIRL.STATE.KANDO_MAX[21] + "\n首: " + __instance.GIRL.STATE.KANDO_MAX[22] + "\n背中: " + __instance.GIRL.STATE.KANDO_MAX[18] + "\n頭: " + __instance.GIRL.STATE.KANDO_MAX[28] + "\n顔: " + __instance.GIRL.STATE.KANDO_MAX[23] + "\nお口: " + __instance.GIRL.STATE.KANDO_MAX[19] + "\nおなか: " + __instance.GIRL.STATE.KANDO_MAX[29] + "\n足右: " + __instance.GIRL.STATE.KANDO_MAX[24] + "\n足左: " + __instance.GIRL.STATE.KANDO_MAX[25] + "\n腕右: " + __instance.GIRL.STATE.KANDO_MAX[26] + "\n腕左: " + __instance.GIRL.STATE.KANDO_MAX[27] + "\n脇右: " + __instance.GIRL.STATE.KANDO_MAX[30] + "\n脇左: " + __instance.GIRL.STATE.KANDO_MAX[31] + "\n";
                __instance.TX_MEMO_3.text = "大事なところ: " + __instance.GIRL.STATE.KANDO_MAX[15] + "\nクリちゃん: " + __instance.GIRL.STATE.KANDO_MAX[4] + "\nチツちゃん: " + __instance.GIRL.STATE.KANDO_MAX[6] + "\nチツちゃんの中: " + __instance.GIRL.STATE.KANDO_MAX[10] + "\nGちゃん: " + __instance.GIRL.STATE.KANDO_MAX[11] + "\n子宮ちゃん: " + __instance.GIRL.STATE.KANDO_MAX[12] + "\n尿道ちゃん: " + __instance.GIRL.STATE.KANDO_MAX[5] + "\n尿道ちゃんの中: " + __instance.GIRL.STATE.KANDO_MAX[8] + "\n尿道ちゃんの奥: " + __instance.GIRL.STATE.KANDO_MAX[9] + "\nアナルちゃん: " + __instance.GIRL.STATE.KANDO_MAX[7] + "\nアナルちゃんの中: " + __instance.GIRL.STATE.KANDO_MAX[13] + "\nアナルちゃんの奥: " + __instance.GIRL.STATE.KANDO_MAX[14] + "\nチツちゃん拡張率: " + __instance.GIRL.STATE.KANDO_MAX[50] + "\n尿道ちゃん拡張率: " + __instance.GIRL.STATE.KANDO_MAX[52] + "\nアナルちゃん拡張率: " + __instance.GIRL.STATE.KANDO_MAX[51] + "\n";
                if (!__instance.GIRL.GAME_CONTROL.H_IS)
                {
                    if (__instance.GIRL.SAVE_DATA.GIRL_RESULT[20] > 0f)
                    {
                        __instance.TX_MEMO.text = __instance.TX_MEMO.text + "Hした回数: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[20] + "回\n";
                    }
                    if (__instance.GIRL.SAVE_DATA.GIRL_RESULT[0] > 0f)
                    {
                        __instance.TX_MEMO.text = __instance.TX_MEMO.text + "絶頂回数: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[0] + "回\n";
                    }
                    if (__instance.GIRL.SAVE_DATA.GIRL_RESULT[1] > 0f)
                    {
                        __instance.TX_MEMO.text = __instance.TX_MEMO.text + "連続絶頂: 最高" + __instance.GIRL.SAVE_DATA.GIRL_RESULT[1] + "回\n";
                    }
                    if (__instance.GIRL.SAVE_DATA.GIRL_RESULT[21] > 0f)
                    {
                        __instance.TX_MEMO.text = __instance.TX_MEMO.text + "壊れた回数: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[21] + "回\n";
                    }
                    if (__instance.GIRL.SAVE_DATA.GIRL_RESULT[2] > 0f)
                    {
                        __instance.TX_MEMO.text = __instance.TX_MEMO.text + "ぶっかけ: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[2].ToString("f2") + "ml\n";
                    }
                    if (__instance.GIRL.SAVE_DATA.GIRL_RESULT[3] > 0f)
                    {
                        __instance.TX_MEMO.text = __instance.TX_MEMO.text + "中出しお口: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[3].ToString("f2") + "ml\n";
                    }
                    if (__instance.GIRL.SAVE_DATA.GIRL_RESULT[4] > 0f)
                    {
                        __instance.TX_MEMO.text = __instance.TX_MEMO.text + "中出しあそこ: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[4].ToString("f2") + "ml\n";
                    }
                    if (__instance.GIRL.SAVE_DATA.GIRL_RESULT[5] > 0f)
                    {
                        __instance.TX_MEMO.text = __instance.TX_MEMO.text + "中出しおしり: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[5].ToString("f2") + "ml\n";
                    }
                    if (__instance.GIRL.SAVE_DATA.GIRL_RESULT[6] > 0f)
                    {
                        __instance.TX_MEMO.text = __instance.TX_MEMO.text + "中出し尿道: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[6].ToString("f2") + "ml\n";
                    }
                    if (__instance.GIRL.SAVE_DATA.GIRL_RESULT[7] > 0f)
                    {
                        __instance.TX_MEMO.text = __instance.TX_MEMO.text + "潮吹き: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[7].ToString("f2") + "ml\n";
                    }
                    if (__instance.GIRL.SAVE_DATA.GIRL_RESULT[8] > 0f)
                    {
                        __instance.TX_MEMO.text = __instance.TX_MEMO.text + "おしっこ: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[8].ToString("f2") + "ml\n";
                    }
                    if (__instance.GIRL.SAVE_DATA.GIRL_RESULT[9] > 0f)
                    {
                        __instance.TX_MEMO.text = __instance.TX_MEMO.text + "ミルク: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[9].ToString("f2") + "ml\n";
                    }
                    if (__instance.TX_MEMO.text != "")
                    {
                        __instance.TX_MEMO.text = "<size=\"60\">えっちの記録</size>\n" + __instance.TX_MEMO.text;
                    }
                    //}
                    __instance.TX_MEMO_5.text = "<size=\"60\">妹ちゃん体力</size>\n";
                    tX_MEMO_ = __instance.TX_MEMO_5;
                    tX_MEMO_.text = tX_MEMO_.text + __instance.GIRL.STATE.LIFE_POINT + "/" + __instance.GIRL.STATE.LIFE_POINT_MAX + "\n";
                    tX_MEMO_ = __instance.TX_MEMO_5;
                    tX_MEMO_.text = tX_MEMO_.text + "(" + (int)(__instance.GIRL.STATE.LIFE_POINT * 100f / __instance.GIRL.STATE.LIFE_POINT_MAX) + "%)\n";
                    if (__instance.GIRL.SAVE_DATA.DATAS_STORY[128])
                    {
                        __instance.TX_MEMO_5.text += "<size=\"60\">持久力</size>: ";
                        tX_MEMO_ = __instance.TX_MEMO_5;
                        tX_MEMO_.text = tX_MEMO_.text + __instance.GIRL.SAVE_DATA.MAN_STATES[30] / 2 + "/" + 1000 + "\n";
                    }
                }
                __instance.TX_MEMO_4.text = "<size=\"60\">お兄ちゃんステータス</size>\n";
                tX_MEMO_ = __instance.TX_MEMO_4;
                tX_MEMO_.text = tX_MEMO_.text + "スタミナ: " + __instance.GIRL.SAVE_DATA.MAN_STATES[10] + "/" + __instance.GIRL.SAVE_DATA.MAN_STATES[11] + "\n";
                tX_MEMO_ = __instance.TX_MEMO_4;
                tX_MEMO_.text = tX_MEMO_.text + "手先の器用さ: " + __instance.GIRL.SAVE_DATA.MAN_STATES[0] + "\n";
                tX_MEMO_ = __instance.TX_MEMO_4;
                tX_MEMO_.text = tX_MEMO_.text + "鋭敏な味覚: " + __instance.GIRL.SAVE_DATA.MAN_STATES[1] + "\n";
                tX_MEMO_ = __instance.TX_MEMO_4;
                tX_MEMO_.text = tX_MEMO_.text + "男の色気: " + __instance.GIRL.SAVE_DATA.MAN_STATES[2] + "\n";
            }
            else if (GameDataMain.OPTIONS[99] == 1f)
            {
                __instance.TX_MEMO.text = "NUMBER OF TIMES A BATH: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[20] + "\nFINISH: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[0] + "\nECSTASY: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[1] + "\nSPERM: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[2].ToString("f2") + "ml\nBUKKAKE: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[3].ToString("f2") + "ml\nCUMSHOT: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[4].ToString("f2") + "ml\nCREAMPIE: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[5].ToString("f2") + "ml\nCREAMPIE ANAL: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[6].ToString("f2") + "ml\nPEACH EXTRACT: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[7].ToString("f2") + "ml\nPEACH JUICE: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[8].ToString("f2") + "ml\nPEACH MILK: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[9].ToString("f2") + "ml\nPAPUKO: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[10].ToString("f2") + "ml\nPAPUKO ADULT: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[11].ToString("f2") + "ml\nPOTION: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[12].ToString("f2") + "ml\nPOTION ADULT: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[13].ToString("f2") + "ml\n\nTOTAL GET MONEY: " + __instance.GIRL.SAVE_DATA.GIRL_RESULT[14] + "￥\n";
            }
            return false;
        }
    }
}
