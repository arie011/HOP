using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffObject : MonoBehaviour
{
    public int ID;
    public SpriteRenderer Sr;
    public Image Img;
    public Text Teks_Tulisan;

    private void OnEnable()
    {
        if (Sr != null)
            Sr.sprite = SistemBuff.instance.GamabarBuff[ID];
    }


}
