using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_4_4_MyScript : MonoBehaviour
{
    public GameObject P0, P1, P2;    // V1=P1-P0 and V2=P2-p1
    private MyVector ShowV1atP0, ShowV2atV1, // Show V1 at P0 and V2 at head of V1
                     ShowV2atP0, ShowV1atV2, // Show V2 at P0 and V1 at head of V2
                     ShowSumV12, ShowSumV21, // V1+V2, and V2+V1
                     ShowSubV12,         // V1-V2
                     ShowNegV2;          // -V2

    private MyVector PosV1, PosV2, PosSum, PosSub, PosNegV2; // Show as position vectors

    public bool DrawAxisFrame = true;
    public bool DrawV12 = false, DrawV21 = false;
    public bool DrawSum = false;
    public bool DrawSub = false, DrawNegV2 = false;
    public bool DrawPosVec = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(P0 != null);
        Debug.Assert(P1 != null);
        Debug.Assert(P2 != null);

        ShowV1atP0 = new MyVector()
        {
            VectorColor = Color.red
        };
        ShowV1atV2 = new MyVector()
        {
            VectorColor = Color.red
        };
        PosV1 = new MyVector()
        {
            VectorAt = Vector3.zero,  // always show at the origin
            VectorColor = Color.red
        };

        ShowV2atP0 = new MyVector()
        {
            VectorColor = Color.blue
        };
        ShowV2atV1 = new MyVector()
        {
            VectorColor = Color.blue
        };
        PosV2 = new MyVector()
        {
            VectorAt = Vector3.zero,
            VectorColor = Color.blue
        };

        ShowSumV12 = new MyVector()
        {
            VectorColor = Color.green
        };
        ShowSumV21 = new MyVector()
        {
            VectorColor = Color.green
        };
        PosSum = new MyVector()
        {
            VectorAt = Vector3.zero,
            VectorColor = Color.green
        };

        ShowSubV12 = new MyVector()
        {
            VectorColor = Color.gray
        };
        PosSub = new MyVector()
        {
            VectorAt = Vector3.zero,
            VectorColor = Color.gray
        };

        ShowNegV2 = new MyVector()
        {
            VectorColor = new Color(0.9f, 0.9f, 0.2f, 1.0f)
        };
        PosNegV2 = new MyVector()
        {
            VectorAt = Vector3.zero,
            VectorColor = new Color(0.9f, 0.9f, 0.2f, 1.0f)
        };
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 V1 = P1.transform.localPosition - P0.transform.localPosition;
        Vector3 V2 = P2.transform.localPosition - P1.transform.localPosition;
        Vector3 sumV12 = V1 + V2;
        Vector3 sumV21 = V2 + V1;
        Vector3 negV2 = -V2;
        Vector3 subV12 = V1 + negV2;

        #region Draw control: switch on/off what to show
        AxisFrame.ShowAxisFrame = DrawAxisFrame;    // Draw or Hide Axis Frame
        ShowV1atP0.DrawVector = DrawV12;
        ShowV2atV1.DrawVector = DrawV12;
        ShowSumV12.DrawVector = DrawSum;
        ShowV2atP0.DrawVector = DrawV21;
        ShowV1atV2.DrawVector = DrawV21;
        ShowSumV21.DrawVector = DrawSum;
        ShowSubV12.DrawVector = DrawSub;
        ShowNegV2.DrawVector = DrawNegV2;
        PosV1.DrawVector = DrawPosVec && (DrawV12 || DrawV21);
        PosV2.DrawVector = DrawPosVec && (DrawV12 || DrawV21);
        PosSum.DrawVector = DrawPosVec && DrawSum;
        PosSub.DrawVector = DrawPosVec && DrawSub;
        PosNegV2.DrawVector = DrawPosVec && DrawNegV2;
        #endregion

        #region V1: Show V1 at P0 and head of V2
        ShowV1atP0.VectorAt = P0.transform.localPosition;
        ShowV1atP0.Direction = V1;
        ShowV1atP0.Magnitude = V1.magnitude;

        ShowV1atV2.VectorAt = P0.transform.localPosition + V2;
        ShowV1atV2.Direction = V1;
        ShowV1atV2.Magnitude = V1.magnitude;

        PosV1.Direction = V1;
        PosV1.Magnitude = V1.magnitude;
        #endregion

        #region V2: show V2 at P0 and head of V1
        ShowV2atP0.VectorAt = P0.transform.localPosition;
        ShowV2atP0.Direction = V2;
        ShowV2atP0.Magnitude = V2.magnitude;

        ShowV2atV1.VectorAt = P0.transform.localPosition + V1;
        ShowV2atV1.Direction = V2;
        ShowV2atV1.Magnitude = V2.magnitude;

        PosV2.Direction = V2;
        PosV2.Magnitude = V2.magnitude;
        #endregion

        #region Sum: show V1+V2 and V2+V1
        ShowSumV12.VectorAt = P0.transform.localPosition;
        ShowSumV12.Direction = sumV12;
        ShowSumV12.Magnitude = sumV12.magnitude;

        ShowSumV21.VectorAt = P0.transform.localPosition;
        ShowSumV21.Direction = sumV21;
        ShowSumV21.Magnitude = sumV21.magnitude;

        PosSum.Direction = sumV12;
        PosSum.Magnitude = sumV12.magnitude;
        #endregion

        #region Sub: show V1-V2 
        ShowSubV12.VectorAt = P0.transform.localPosition;
        ShowSubV12.Direction = subV12;
        ShowSubV12.Magnitude = subV12.magnitude;

        PosSub.Direction = subV12;
        PosSub.Magnitude = subV12.magnitude;
        #endregion

        #region Negative vectors: -V2
        ShowNegV2.VectorAt = ShowV2atV1.VectorAt;
        ShowNegV2.Direction = negV2;
        ShowNegV2.Magnitude = negV2.magnitude;

        PosNegV2.Direction = negV2;
        PosNegV2.Magnitude = negV2.magnitude;
        #endregion 
    }
}
