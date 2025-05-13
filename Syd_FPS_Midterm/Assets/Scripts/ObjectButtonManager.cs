using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
   
    public void ButtonPressCheck()
    {
        Debug.Log("button was pressed");
    }

    public void DresserButtonChange()
    {
        SceneManager.LoadScene(7);

    }


}
