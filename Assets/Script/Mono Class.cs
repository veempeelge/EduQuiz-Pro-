using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class MonoClass : MonoBehaviour
{
    public TextAsset jsonSoal;
    public SoalSoal soalsoal;
    public string soalURL;

    // Start is called before the first frame update
    void Start()
    {

        //var json = JsonUtility.ToJson(soalsoal, true);
        //Debug.Log(json);
 
        StartCoroutine(GetSoalOnline(soalURL));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GetSoalOnline(string url)
    {
        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            Debug.Log(request.downloadHandler.text);
            soalsoal = JsonUtility.FromJson<SoalSoal>(request.downloadHandler.text);
        }
    }
}
