using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TombolSoal : MonoBehaviour
{
    public int IDTombol;
    public Text TeksTulisanJawaban;


    public void CheckJawaban()
    {
        int IDJawaban = SistemSoal.instance.IDJawaban;

        if(IDTombol == IDJawaban)
        {
            // jawaban benar
            Debug.Log("Jawaban Benar");

            SistemSoal.instance.CheckJawaban(true);
        }
        else
        {
            // jawaban saalah;
            Debug.Log("Jawaban Salah");
            SistemSoal.instance.CheckJawaban(false);
        }
    }
}
