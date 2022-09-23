using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SistemBuff : MonoBehaviour
{
    public static SistemBuff instance;

    public float SpawnBuffTime;
   
    public Vector2 BatasMin, BatasMax;


    public string[] TulisanBuff;
    public int[] PenambahanBuff;
    public Sprite[] GamabarBuff;
    
    public GameObject[] ObjBuff, UIBuff;


    WaveManager Wm;
    float s;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        Wm = FindObjectOfType<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Wm.gameRunning)
        {
            s += Time.deltaTime;
            if (s >= SpawnBuffTime)
            {
                s = 0;
                GameObject gg = Instantiate(ObjBuff[Random.Range(0, ObjBuff.Length)], new Vector2(Random.Range(BatasMin.x, BatasMax.x), Random.Range(BatasMin.y, BatasMax.y)), transform.rotation);
                gg.transform.localScale = Vector2.zero;
                gg.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBack);

            }
        }
    }

    public void SpawnBuffGui(int id)
    {
        for (int i = 0; i < UIBuff.Length; i++)
        {
            UIBuff[i].SetActive(false);
        }

        UIBuff[id].SetActive(true);
        UIBuff[id].transform.localPosition = new Vector2(0, -400f);
        UIBuff[id].GetComponent<RectTransform>().DOLocalMoveY(-240f, 0.5f).SetEase(Ease.OutBack);
        UIBuff[id].GetComponent<BuffObject>().Img.sprite = GamabarBuff[id];
        UIBuff[id].GetComponent<BuffObject>().Teks_Tulisan.text = TulisanBuff[id];
        //UIBuff[id].rec(240f, 0.5f).SetEase(Ease.OutBack);
        //UIBuff[id].transform.position = new Vector2(0, transform.position.y);

        StartCoroutine(AutoDisable());
    }

    IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < UIBuff.Length; i++)
        {
            UIBuff[i].SetActive(false);
        }
    }
}