using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemText : MonoBehaviour
{

    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI fabricText;
    public TextMeshProUGUI furText;
    public TextMeshProUGUI laceText;
    public TextMeshProUGUI gromText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buttonText.text = "Buttons Collected: " + LootPickUp.numButton;
        fabricText.text = "Fabric Collected: " + LootPickUp.numFabric;
        furText.text = "Fur Collected: " + LootPickUp .numFur;
        laceText.text = "Lace Colleced: " + LootPickUp.numLace;
        gromText.text = "Grommets Collected: " + LootPickUp.numGrom;




    }
}
