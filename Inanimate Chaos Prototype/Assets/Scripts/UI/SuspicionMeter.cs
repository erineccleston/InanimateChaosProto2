using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionMeter : MonoBehaviour {

    public Image meter;
    public Text MeterPercentage;
    //public Vector3 person;
    public Death DeathScript;
    public SkinnedMeshRenderer person;

    public CameraController TheCamera;

    private float LowerLimit = 0;
    private float HigherLimit = 5000;
    private float CurrentMeter = 1;

    private void UpdateMeterDisplay()
    {
        if (CurrentMeter > HigherLimit)
        {
            MeterPercentage.text = "Suspicion: 100%";
            MeterPercentage.GraphicUpdateComplete();
            DeathScript.IsDead = true;
            return;
        }

        else if (CurrentMeter < LowerLimit)
            CurrentMeter = LowerLimit + 1;


        float percentage = CurrentMeter / HigherLimit;
        meter.rectTransform.localScale = new Vector3(percentage, 1, 1);
        percentage = (float)System.Math.Round(percentage, 2);
        MeterPercentage.text = "Suspicion: " + percentage * 100 + "%";

        if (percentage > .5f)
        {
            if (person.GetBlendShapeWeight(0) < 99)
            {
                person.SetBlendShapeWeight(0, person.GetBlendShapeWeight(0) + 5);
            }

        }
        else
        {
            if (person.GetBlendShapeWeight(0) > 1)
                person.SetBlendShapeWeight(0, person.GetBlendShapeWeight(0) - 5);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        while (!DeathScript.IsDead)
        {
            System.Random r = new System.Random();
            float maginitude = Vector3.Magnitude(TheCamera.FollowObject.GetComponent<Rigidbody>().velocity);
            //float dist = 1 / Vector3.Distance(TheCamera.FollowObject.GetComponent<Rigidbody>().position);

            print(maginitude);
            if (maginitude > .1)
            {
                int randomInt = r.Next(20, 150);
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
