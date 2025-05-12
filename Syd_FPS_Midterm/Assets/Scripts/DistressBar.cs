using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistressBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    public void SetInitialDistress(int dis)
    {
        slider.minValue = dis;
        slider.value = dis;

    }

    public void SetDistress(int dis)
    {
        slider.value = dis;
    }
}
