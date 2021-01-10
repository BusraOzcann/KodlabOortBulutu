using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunYoneticisi : MonoBehaviour
{
    public GameObject startBtn;
    public GameObject playerGemi;
    public GameObject dusmanYaratici;
    public GameObject oyunBitti;
    public GameObject dinamikSkorTxtObje;

    public enum OyunYoneticisiDurumu
    {
        Baslangic,
        OyunHali,
        OyunSonu,
    }

    OyunYoneticisiDurumu oyunDurumu;

    void Start()
    {
        oyunDurumu = OyunYoneticisiDurumu.Baslangic;
    }

    void OyunDurumuGuncelle()
    {
        switch (oyunDurumu)
        {
            case OyunYoneticisiDurumu.Baslangic:
                startBtn.SetActive(true);
                oyunBitti.SetActive(false);
                break;

            case OyunYoneticisiDurumu.OyunHali:
                dinamikSkorTxtObje.GetComponent<OyunSkor>().Skor = 0;
                startBtn.SetActive(false);
                oyunBitti.SetActive(false);
                playerGemi.GetComponent<PlayerKontrol>().ilkDurum();
                dusmanYaratici.GetComponent<DusmanYaratici>().DusmanYaratmayiBaslat();
                break;

            case OyunYoneticisiDurumu.OyunSonu:
                dusmanYaratici.GetComponent<DusmanYaratici>().DusmanYaratmayiDurdur();
                oyunBitti.SetActive(true);
                startBtn.SetActive(false);
                Invoke("MevcutDurumuDegistir", 8f);
                break;
        }
    }

    public void OyunDurumuAyarla(OyunYoneticisiDurumu durum)
    {
        oyunDurumu = durum;
        OyunDurumuGuncelle();
    }

    public void OyunuBaslat()
    {
        oyunDurumu = OyunYoneticisiDurumu.OyunHali;
        OyunDurumuGuncelle();
    }

    public void MevcutDurumuDegistir()
    {
        OyunDurumuAyarla(OyunYoneticisiDurumu.Baslangic);
    }

}
