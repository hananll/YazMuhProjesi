using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MagdurKonusma : MonoBehaviour
{
    public GameObject magdurKonusmaPanel;
    public TextMeshProUGUI magdurMetinText;
    public TextMeshProUGUI magdurAdiText;
    public Button magdurKapatButon;
    public Button magdurButon;
    public Button magdurDevamEtButon;


    public float harfHiz = 0.05f;

    public List<KonusmaMetniData> magdurDiyalogMetinleri;

    private int mevcutMetinIndex = 0;
    private Coroutine mevcutMetinAnimasyonu;

    public SanikveHakimKonusma sanikveHakimKonusma;
    public SavciKonusma savciKonusma;
    public San�kAvukat�Konusma san�kAvukat�Konusma;
    public MagdurAvukatiKonusma magdurAvukatiKonusma;

    void Start()
    {

        sanikveHakimKonusma.mikrofonButon.interactable = true;
        savciKonusma.savciButon.interactable = true;
        san�kAvukat�Konusma.sanikAvukatiButon.interactable = true;
        magdurAvukatiKonusma.magdurAvukatiButon.interactable= true;

        magdurKonusmaPanel.SetActive(false);

        magdurButon.onClick.AddListener(KonusmayiBaslat);
        magdurKapatButon.onClick.AddListener(KonusmayiBitir);
        magdurDevamEtButon.onClick.AddListener(SonrakiMetniGoster);
        magdurDevamEtButon.gameObject.SetActive(false);




        void KonusmayiBaslat()
        {
            if (magdurDiyalogMetinleri.Count > 0)
            {
                magdurKonusmaPanel.SetActive(true);

                sanikveHakimKonusma.mikrofonButon.interactable = false;
                savciKonusma.savciButon.interactable = false;
                san�kAvukat�Konusma.sanikAvukatiButon.interactable = false;
                magdurAvukatiKonusma.magdurAvukatiButon.interactable = false;



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
            if (mevcutMetinIndex < magdurDiyalogMetinleri.Count)
            {
                KonusmaMetniData mevcutKonusma = magdurDiyalogMetinleri[mevcutMetinIndex];


                if (mevcutMetinAnimasyonu != null)
                {
                    StopCoroutine(mevcutMetinAnimasyonu);
                }

                magdurMetinText.text = ""; //New Text alan�n� kald�rm�� olduk.
                magdurMetinText.text = mevcutKonusma.konusmaciAdi;
                mevcutMetinAnimasyonu = StartCoroutine(MetniHarfHarfGoster(magdurMetinText, mevcutKonusma.metin));
                magdurDevamEtButon.gameObject.SetActive(false);
                //Metine Ba�lad���m�zda devam et butonuna t�klayamamay� sa�lad�k.

            }

            else
            {
                KonusmayiBitir();  // T�m metinler g�steriliyse konusmay� bitir. 
            }
        }

             sanikveHakimKonusma.mikrofonButon.interactable = true;
             savciKonusma.savciButon.interactable = true;
             san�kAvukat�Konusma.sanikAvukatiButon.interactable = true;
             magdurAvukatiKonusma.magdurAvukatiButon.interactable = true;



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
        magdurDevamEtButon.gameObject.SetActive(true); // Metin tamamland�ktan sonra devam et butonu aktif
    }

    void KonusmayiBitir()
    {
        magdurKonusmaPanel.SetActive(false);
        sanikveHakimKonusma.mikrofonButon.interactable = true;
        savciKonusma.savciButon.interactable = true;
        san�kAvukat�Konusma.sanikAvukatiButon.interactable = true;
        magdurAvukatiKonusma.magdurAvukatiButon.interactable = true;


        magdurButon.interactable = true;
        mevcutMetinIndex = 0;

    }


}
