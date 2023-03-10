using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Exit : MonoBehaviour //패널 열기/닫기에 사용되는 클래스
{
    public GameObject SoundEffectManager;
    public GameObject JoyStick;
    public Interaction Inter;

    public Camera Camera;
    public Camera FarmCamera;

    public void ExitBtn(GameObject exitThis)    //해당 오브젝트를 비활성화
    {
        GameObject SoundManager = GameObject.Find("SoundManager");
        SoundEffectManager.GetComponent<SoundEffect>().Sound("ClickBack");
        if (exitThis.name == "GachaUI")
        {
            SoundManager.GetComponent<SoundManager>().Sound("BGMField");
        }
        if (Inter)
            if (Inter.Farm)
            {
                FarmCamera.enabled = false;
                Camera.enabled = true;
                JoyStick.SetActive(true);
            }
        exitThis.SetActive(false);
    }
     
    public void EnterBtn(GameObject enterThis)  //해당 오브젝트를 활성화
    {
        SoundEffectManager.GetComponent<SoundEffect>().Sound("ClickIcon");
        enterThis.SetActive(true);
    }
}
