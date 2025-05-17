using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MagdurAvukatiKonusma : MonoBehaviour
{

    public GameObject magdurAvukatiKonusmaPanel;
    public TextMeshProUGUI magdurAvukatiMetinText;
    public TextMeshProUGUI magdurAvukatiAdiText;
    public Button magdurAvukatiKapatButon;
    public Button magdurAvukatiButon;
    public Button magdurAvukatiDevamEtButon;


    public float harfHiz = 0.05f;

    public List<KonusmaMetniData> magdurAvukatiDiyalogMetinleri;

    private int mevcutMetinIndex = 0;
    private Coroutine mevcutMetinAnimasyonu;


    public SanikveHakimKonusma sanikveHakimKonusma;
    public SavciKonusma savciKonusma;
    public SanýkAvukatýKonusma sanýkAvukatýKonusma;
    public MagdurKonusma magdurKonusma;

    void Start()
    {

        sanikveHakimKonusma.mikrofonButon.interactable = true;
        savciKonusma.savciButon.interactable = true;
        sanýkAvukatýKonusma.sanikAvukatiButon.interactable = true;
        magdurKonusma.magdurButon.interactable = true;


        magdurAvukatiButon.onClick.AddListener(KonusmayiBaslat);
        magdurAvukatiKapatButon.onClick.AddListener(KonusmayiBitir);
        magdurAvukatiDevamEtButon.onClick.AddListener(SonrakiMetniGoster);
        magdurAvukatiDevamEtButon.gameObject.SetActive(false);


        void KonusmayiBaslat()
        {
            if (magdurAvukatiDiyalogMetinleri.Count > 0)
            {
                magdurAvukatiKonusmaPanel.SetActive(true);

                sanikveHakimKonusma.mikrofonButon.interactable = false;
                savciKonusma.savciButon.interactable = false;
                sanýkAvukatýKonusma.sanikAvukatiButon.interactable = false;
                magdurKonusma.magdurButon.interactable = false;


                mevcutMetinIndex = 0;
                MevcutMetniGoster();

            }
        }

        void SonrakiMetniGoster()
        {
            mevcutMetinIndex++;
            MevcutMetniGoster();
        }

        void MevcutMetniGoster()
        {
            if (mevcutMetinIndex < magdurAvukatiDiyalogMetinleri.Count)
            {
                KonusmaMetniData mevcutKonusma = magdurAvukatiDiyalogMetinleri[mevcutMetinIndex];


                if (mevcutMetinAnimasyonu != null)
                {
                    StopCoroutine(mevcutMetinAnimasyonu);
                }

                magdurAvukatiMetinText.text = ""; //New Text alanýný kaldýrmýþ olduk.
                magdurAvukatiAdiText.text = mevcutKonusma.konusmaciAdi;
                mevcutMetinAnimasyonu = StartCoroutine(MetniHarfHarfGoster(magdurAvukatiMetinText, mevcutKonusma.metin));
                magdurAvukatiDevamEtButon.gameObject.SetActive(false);
                //Metine Baþladýðýmýzda devam et butonuna týklayamamayý saðladýk.

            }

            else
            {
                KonusmayiBitir();  // Tüm metinler gösteriliyse konusmayý bitir. 
            }

        }

        sanikveHakimKonusma.mikrofonButon.interactable = true;
        savciKonusma.savciButon.interactable = true;
        sanýkAvukatýKonusma.sanikAvukatiButon.interactable = true;
        magdurKonusma.magdurButon.interactable = true;





    }

    IEnumerator MetniHarfHarfGoster(TextMeshProUGUI metinAlani, string metin)
    {
        int harfSayisi = 0;
        metinAlani.text = "";
        while (harfSayisi < metin.Length)
        {
            harfSayisi++;
            metinAlani.text = metin.Substring(0, harfSayisi);
            yield return new WaitForSeconds(harfHiz);
        }
        magdurAvukatiDevamEtButon.gameObject.SetActive(true); // Metin tamamlandýktan sonra devam et butonu aktif
    }


    void KonusmayiBitir()
    {
        magdurAvukatiKonusmaPanel.SetActive(false);

        sanikveHakimKonusma.mikrofonButon.interactable = true;
        savciKonusma.savciButon.interactable = true;
        magdurAvukatiButon.interactable = true;
        sanýkAvukatýKonusma.sanikAvukatiButon.interactable = true;
        magdurKonusma.magdurButon.interactable = true;


        mevcutMetinIndex = 0;

    }
}
