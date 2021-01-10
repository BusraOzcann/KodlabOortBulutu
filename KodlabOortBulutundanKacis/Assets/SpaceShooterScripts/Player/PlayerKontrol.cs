using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKontrol : MonoBehaviour
{
    public GameObject oyunYoneticisiObje;
    public GameObject playerMermi;
    public GameObject mermiKonumSol;
    public GameObject mermiKonumSag;
    public GameObject Patlama;

    public float hiz;

    public Text canText;
    const int MaxCan = 3;
    int canSayisi;

    float baslangicKonumY;

    public void ilkDurum()
    {
        canSayisi = MaxCan;
        canText.text = canSayisi.ToString();
        transform.position = new Vector2(0, 0); // karakterin ortadan baslaması için
        gameObject.SetActive(true);
    }

    void Start()
    {
        baslangicKonumY = Input.acceleration.y;
    }

    
    void Update()
    {

        float x = Input.acceleration.x;
        float y = Input.acceleration.y - baslangicKonumY;

        Vector2 yon = new Vector2(x, y);

        if (yon.sqrMagnitude > 1)
        {
            yon.Normalize();
        }

        Hareket(yon);
    }

    public void AtesEt()
    {
        gameObject.GetComponent<AudioSource>().Play();

        GameObject mermi1 = (GameObject)Instantiate(playerMermi);
        mermi1.transform.position = mermiKonumSol.transform.position;

        GameObject mermi2 = (GameObject)Instantiate(playerMermi);
        mermi2.transform.position = mermiKonumSag.transform.position;   
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "DusmanGemi") || (collision.tag == "DusmanMermi"))
        {
            PatlamaAnimasyonu();
            canSayisi = canSayisi -1;
            canText.text = canSayisi.ToString();

            if (canSayisi == 0)
            {
                oyunYoneticisiObje.GetComponent<OyunYoneticisi>().OyunDurumuAyarla(OyunYoneticisi.OyunYoneticisiDurumu.OyunSonu);
                gameObject.SetActive(false);
            }
        }    
    }

    void PatlamaAnimasyonu()
    {
        GameObject patlama = (GameObject)Instantiate(Patlama);
        patlama.transform.position = transform.position;
    }


    void Hareket(Vector2 yon)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 konum = transform.position;

        konum += yon * hiz * Time.deltaTime;

        konum.x = Mathf.Clamp(konum.x, min.x, max.x);
        konum.y = Mathf.Clamp(konum.y, min.y, max.y);

        transform.position = konum;
    }
}
