using UnityEngine;
using UnityEditor;

public class AxisFrame
{
    static private GameObject TheFrame = GameObject.Find("AxisFrame");

    static public bool ShowAxisFrame
    {
        get { return TheFrame.activeSelf; }
        set { TheFrame.SetActive(value); }
    }
}