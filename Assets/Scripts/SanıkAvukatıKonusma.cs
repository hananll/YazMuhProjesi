using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SanıkAvukatıKonusma : MonoBehaviour
{

    public GameObject sanikAvukatiKonusmaPanel;
    public TextMeshProUGUI sanikAvukatiMetinText;
    public TextMeshProUGUI sanikAvukatiAdiText;
    public Button sanikAvukatiKapatButon;
    public Button sanikAvukatiButon;
    public Button sanikAvukatiDevamEtButon;


    public float harfHiz = 0.05f;

    public List<KonusmaMetniData> sanikAvukatiDiyalogMetinleri;

    private int mevcutMetinIndex = 0;
    private Coroutine mevcutMetinAnimasyonu;

    private void Start()
    {

        sanikAvukatiKonusmaPanel.SetActive(false);

        sanikAvukatiButon.onClick.AddListener(KonusmayiBaslat);
        sanikAvukatiKapatButon.onClick.AddListener(KonusmayiBitir);
        sanikAvukatiDevamEtButon.onClick.AddListener(SonrakiMetniGoster);
        sanikAvukatiDevamEtButon.gameObject.SetActive(false);


        void KonusmayiBaslat()
        {
            if (sanikAvukatiDiyalogMetinleri.Count > 0)
            {
                sanikAvukatiKonusmaPanel.SetActive(true);
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
            if (mevcutMetinIndex < sanikAvukatiDiyalogMetinleri.Count)
            {
                KonusmaMetniData mevcutKonusma = sanikAvukatiDiyalogMetinleri[mevcutMetinIndex];


                if (mevcutMetinAnimasyonu != null)
                {
                    StopCoroutine(mevcutMetinAnimasyonu);
                }

                sanikAvukatiMetinText.text = ""; //New Text alanını kaldırmış olduk.
                sanikAvukatiAdiText.text = mevcutKonusma.konusmaciAdi;
                mevcutMetinAnimasyonu = StartCoroutine(MetniHarfHarfGoster(sanikAvukatiMetinText, mevcutKonusma.metin));
                sanikAvukatiDevamEtButon.gameObject.SetActive(false);
                //Metine Başladığımızda devam et butonuna tıklayamamayı sağladık.

              }

            else
            {
                KonusmayiBitir();  // Tüm metinler gösteriliyse konusmayı bitir. 
            }

        }
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
            sanikAvukatiDevamEtButon.gameObject.SetActive(true); // Metin tamamlandıktan sonra devam et butonu aktif
        }

      
      void KonusmayiBitir()
      {
        sanikAvukatiKonusmaPanel.SetActive(false);
        sanikAvukatiButon.interactable = true;
        mevcutMetinIndex = 0;

    }
        

    }





