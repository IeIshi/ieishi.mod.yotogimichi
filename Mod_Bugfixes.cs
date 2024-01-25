using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ieishi.mod.yotogimichi
{
    /// <summary>
    /// Fix for wrong DOF distance
    /// </summary>
    [HarmonyPatch(typeof(SceneControl), nameof(SceneControl.mainProcess))]
    public class Fix_SceneControl_mainProcess
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

            return true;
        }
    }


    /// <summary>
    /// Fix for: When underware set as matched, 'isEcchi' will be wrong set
    /// </summary>
    [HarmonyPatch(typeof(TextureControl), nameof(TextureControl.setDressObj))]
    public class Fix_TextureControl_setDressObj
    {
        public static void Postfix(TextureControl __instance, string part, int num_set)
        {
            if (Plugin.Configuration.UnderwareEcchiFix)
            {
                try
                {
                    if (__instance.OBJ_DRESS[2].GetComponent<DressItem>().DESIGN == "変態")
                    {
                        __instance.GIRL.BURA_ANAAKI_IS = true;
                    }
                    else
                    {
                        __instance.GIRL.BURA_ANAAKI_IS = false;
                    }

                    if (__instance.OBJ_DRESS[3].GetComponent<DressItem>().DESIGN == "変態")
                    {
                        __instance.GIRL.PANTU_ANAAKI_IS = true;
                    }
                    else
                    {
                        __instance.GIRL.PANTU_ANAAKI_IS = false;
                    }
                }
                catch (Exception e)
                {
                    Logging.Log(e.Message);
                }
            }
        }
    }
}
