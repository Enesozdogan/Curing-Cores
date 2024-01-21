using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveAndLoadFuns 
{
    private static string speedVal = "speedVal";
    private static string strVal = "strVal";
    private static string tpVal = "tpVal";
    private static string lvlIndexVal = "LvlIndexVal";
    private static string canLoadFlag = "canLoadFlag";
    private static string gravityJVal = "gravityJVal";
    private static string upForceVal = "upForceVal";
    private static string ringVal = "ringVal";
    public static void SaveLevelIndex(int index)
    {
        PlayerPrefs.SetInt(lvlIndexVal, index);
        PlayerPrefs.SetInt(canLoadFlag, 1);
    }
    public static int LoadFlag()
    {
        return PlayerPrefs.GetInt(canLoadFlag);
    }
    public static int LoadLevelIndex()
    {
        return PlayerPrefs.GetInt(lvlIndexVal);
    }
    public static void SaveSpeedVal(float speed)
    {
        PlayerPrefs.SetFloat(speedVal, speed);
    }
    public static float LoadSpeedVal()
    {
        return PlayerPrefs.GetFloat(speedVal);
    }

    public static void SaveGravityJVal(float jVal)
    {
        PlayerPrefs.SetFloat(gravityJVal, jVal);
    }
    public static float LoadGravityJVal()
    {
        return PlayerPrefs.GetFloat(gravityJVal);
    }

    public static void SaveUpForceVal(float upForce)
    {
        PlayerPrefs.SetFloat(upForceVal, upForce);
    }
    public static float LoadUpForceVal()
    {
        return PlayerPrefs.GetFloat(upForceVal);
    }

    public static void SaveStrVal(int str)
    {
        PlayerPrefs.SetInt(strVal, str);
    }
    public static int LoadStrVal()
    {
        return PlayerPrefs.GetInt(strVal);
    }
    public static void SaveTpVal(float tp)
    {
        PlayerPrefs.SetFloat(tpVal, tp);
    }
    public static float LoadTpVal()
    {
        return PlayerPrefs.GetFloat(tpVal);
    }

    public static void SaveRingVal(int ringIndex)
    {
        PlayerPrefs.SetFloat(ringVal, ringIndex);
    }
    public static int LoadRingVal()
    {
        return PlayerPrefs.GetInt(ringVal);
    }

    public static void DeleteSaves()
    {
        PlayerPrefs.DeleteKey(lvlIndexVal);
        PlayerPrefs.DeleteKey(strVal);
        PlayerPrefs.DeleteKey(speedVal);
        PlayerPrefs.DeleteKey(tpVal);
        PlayerPrefs.DeleteKey(gravityJVal);
        PlayerPrefs.DeleteKey(upForceVal);
        PlayerPrefs.DeleteKey(canLoadFlag);
        PlayerPrefs.DeleteKey(ringVal);
    }

}
