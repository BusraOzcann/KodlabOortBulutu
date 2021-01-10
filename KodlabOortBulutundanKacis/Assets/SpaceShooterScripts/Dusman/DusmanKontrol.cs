using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanKontrol : MonoBehaviour
{
    GameObject skorTextObje;

    public GameObject Patlama;

    float hiz;

    void Start()
    {
        hiz = 2f;

        skorTextObje = GameObject.FindGameObjectWithTag("SkorTxtTag");
    }

    
    void Update()
    {
        Vector2 konum = transform.position;

        konum = new Vector2(konum.x, konum.y - hiz * Time.deltaTime);

        transform.position = konum;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "PlayerGemi") || (collision.tag == "PlayerMermi"))
        {
            PatlamaAnimasyonu();
            skorTextObje.GetComponent<OyunSkor>().Skor += 100;
            Destroy(gameObject);
        }
    }

    void PatlamaAnimasyonu()
    {
        GameObject patlama = (GameObject)Instantiate(Patlama);
        patlama.transform.position = transform.position;
    }

}
