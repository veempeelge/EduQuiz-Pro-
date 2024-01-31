using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Import : MonoBehaviour
{
    [SerializeField] TMP_Text soalApa;
    [SerializeField] GameObject jawabanButtonPrefabs;
    [SerializeField] GameObject JawabanLayout;
    [SerializeField] string linkSoal;
    TextMeshPro jawabanText;

    SoalSoal soalsoal;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetSoal(linkSoal));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var randomId = UnityEngine.Random.Range(0, soalsoal.soalsoal.Length);
            SoalShow(soalsoal.soalsoal[randomId]);
        }
       
    }

    private IEnumerator GetSoal(string url)
    {
        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.responseCode == 200)
            {
                Debug.Log(request.downloadHandler.text);
                soalsoal = JsonUtility.FromJson<SoalSoal>(request.downloadHandler.text);

                SoalShow(soalsoal.soalsoal[0]);
            }
        }
    }
    private void SoalShow(Soal soal)
    {

        soalApa.SetText(soal.soal);
        for (int i = JawabanLayout.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(JawabanLayout.transform.GetChild(i).gameObject);

        }
        foreach (var jawaban in soal.jawaban)
        {
            var button = Instantiate(jawabanButtonPrefabs, JawabanLayout.transform);
            button.GetComponentInChildren<TMPro.TMP_Text>().SetText(jawaban.jawaban);
            button.name = jawaban.benar.ToString();
    
        }
        //soal.SetText(Soal);

        //jawabanText.SetText(Jawaban);
    }

    private void Jawaban()
    {
       
    }
}
