using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FufillmentBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    public void SetInitialFufill(int fufil)
    {
        slider.minValue = fufil;
        slider.value = fufil;

    }

    public void SetFufill(int fufil)
    {
        slider.value = fufil;
    }
}
