using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SoalManager : MonoBehaviour
{
    public string soalURL;
    public Soal soalsoal;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetSoal(soalURL));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator GetSoal(string URL)
    {
        using (var request = UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();

            Debug.Log(request.downloadHandler.text);
        }
    }
}
