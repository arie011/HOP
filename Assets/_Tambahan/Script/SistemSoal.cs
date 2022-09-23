using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemSoal : MonoBehaviour
{
    public static SistemSoal instance;
    public int IDSoal;
    public int SoalSaatIni;
    public int MaxSoalPerWave;

    [Header("Object UI")]
    public GameObject Gui_Soal;
    public GameObject Gui_Benar;
    public GameObject Gui_Salah;

    [Header("Setting UI Soal")]
    public Text Soal_Teks;
    //public Image Soal_Gambar;
    public TombolSoal[] Soal_Tombol_Jawaban;

    [Header("Data Acak Soal")]
    public List<int> AcakSoal = new List<int>();

    [System.Serializable]
    public class Data
    {
        [TextArea(3,10)]
        public string SoalTulisan;
        //public Sprite SoalGambar;
        [System.Serializable]
        public enum DataJawaban
        {
            JawabanA, JawabanB, JawabanC, JawabanD
        }
        public DataJawaban KunciJawaban;

        public string[] Jawaban;
    }

    public int IDJawaban;
    [Header("Bank Soal")]
    public Data[] DataSoal;


    private void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        IDSoal = 0;
        AcakDataSoal();
        
    }



    int rand;
    public void AcakDataSoal()
    {
        AcakSoal.Clear();
        AcakSoal = new List<int>(new int[DataSoal.Length]);

        for (int i = 0; i < AcakSoal.Count; i++)
        {
            rand = Random.Range(1, AcakSoal.Count + 1);
            while(AcakSoal.Contains(rand))
                rand = Random.Range(1, AcakSoal.Count + 1);

            AcakSoal[i] = rand;
        }


    }

    public void BukaSoal()
    {
        Soal_Teks.text = DataSoal[AcakSoal[IDSoal] - 1].SoalTulisan;
        for (int i = 0; i < Soal_Tombol_Jawaban.Length; i++)
        {
            Soal_Tombol_Jawaban[i].TeksTulisanJawaban.text = DataSoal[AcakSoal[IDSoal] - 1].Jawaban[i];
        }

        Gui_Soal.SetActive(true);
        IDJawaban =  (int)SistemSoal.instance.DataSoal[AcakSoal[IDSoal] - 1].KunciJawaban;
    }


    public void CheckSoalPerWave()
    {
        if(SoalSaatIni < MaxSoalPerWave)
        {
            
            BukaSoal();
            SoalSaatIni++;
            
        }
        else
        {
            SoalSaatIni = 0;
            Gui_Soal.SetActive(false);
            GameObject.FindObjectOfType<WaveManager>().NextWaveEdited();
            GameObject.FindObjectOfType<WaveManager>().gameRunning = true;
            // Soal Closa + Lanjut Game
        }
    }

    public void CheckJawaban(bool JawabanBenar)
    {
        StartCoroutine(AnimasiCheckJawaban(JawabanBenar));
    }

    IEnumerator AnimasiCheckJawaban(bool CheckJawabanBenar)
    {
        yield return new WaitForSeconds(0);
        if(CheckJawabanBenar)
        {
            Gui_Benar.SetActive(true);
            // nambah Score
            GameManager.Instance.scoreManager.score += 100;
        }
        else
        {
            Gui_Salah.SetActive(true);
            // Kurang Score
            GameManager.Instance.scoreManager.score -= 100;
        }
        yield return new WaitForSeconds(1.6f);
        Gui_Salah.SetActive(false);
        Gui_Benar.SetActive(false);
        IDSoal++;
        CheckSoalPerWave();

    }

}
