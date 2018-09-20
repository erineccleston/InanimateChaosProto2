using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionMeter : MonoBehaviour {

    public Image meter;
    public Text MeterPercentage;
    public bool death = false;

    //public int meter = 0;

    public CameraController TheCamera;

    private float LowerLimit = 0;
    private float HigherLimit = 8000;
    private float CurrentMeter = 1;

    private void UpdateMeterDisplay()
    {
        if (CurrentMeter > HigherLimit)
        {
            death = true;
            return;
        }

        else if (CurrentMeter < LowerLimit)
            CurrentMeter = LowerLimit + 1;

        float percentage = CurrentMeter / HigherLimit;
        meter.rectTransform.localScale = new Vector3(percentage, 1, 1);
        MeterPercentage.text = "Suspicion: " + System.Math.Round(percentage, 3) * 100 + "%";
    }
	
	// Update is called once per frame
	void Update ()
    {
        while (!death)
        {
            System.Random r = new System.Random();
            float maginitude = Vector3.Magnitude(TheCamera.FollowObject.GetComponent<Rigidbody>().velocity);


            if (maginitude > 0)
            {
                int randomInt = r.Next(20, 50);
                float meterFilled = CurrentMeter / HigherLimit;
                int add = System.Convert.ToInt32(System.Math.Ceiling(randomInt * meterFilled));
                CurrentMeter += add;
                UpdateMeterDisplay();
                return;
            }

            else
            {
                int randomInt = r.Next(5, 20);
                float meterFilled = (HigherLimit - CurrentMeter) / HigherLimit;
                int add = System.Convert.ToInt32(System.Math.Ceiling(randomInt * meterFilled));
                CurrentMeter -= add;
                UpdateMeterDisplay();
                return;
            }
        }
    }
}
