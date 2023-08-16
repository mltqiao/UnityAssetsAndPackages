using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateController : MonoBehaviour
{
    public GameObject objVibrate;
    public static bool Vibratable = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnVibrateButtonClicked()
    {
        if (Vibratable)
        {
            Vibratable = false;
            objVibrate.SetActive(false);
        }
        else if (!Vibratable)
        {
            Vibratable = true;
            objVibrate.SetActive(true);
            VibrateHelper.vibrateSmall();
        }
    }
}
