using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Windows;
using static QuizManager;

public class DropDown : MonoBehaviour
{
    public DropDownData dropdowndata;
    [SerializeField] Dropdown dropDown;
    List<string> m_DropOptions = new List<string>{};
    string optionjsonText;
    [SerializeField] TMPro.TMP_InputField inputCode;

    // Start is called before the first frame update
    void Start()
    {
        dropDown.onValueChanged.AddListener(DropdownValueChanged);
        StartCoroutine(LoadQuizData());
    }

    private void DropdownValueChanged(int arg0)
    {
        var selectedValue = dropDown.options[dropDown.value].text;
        var option = Array.Find(dropdowndata.listOfOptions, x => x.options.Equals(selectedValue));
        inputCode.text = option.code;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator LoadQuizData()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://api.npoint.io/6256411bbf771df5a527");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string optionjsonText = request.downloadHandler.text;
            dropdowndata = JsonUtility.FromJson<DropDownData>(optionjsonText);
           
        }
        else
        {
           Debug.Log("Failed to load JSON: " + request.error);
        }

        for (int i = 0; i < dropdowndata.listOfOptions.Length; i++)
        {
            int index = i;
            m_DropOptions.Add(dropdowndata.listOfOptions[index].options);
        }

        dropDown.AddOptions(m_DropOptions);
    }
}

[System.Serializable]
public class DropDownData
{
    public ListOfOption[] listOfOptions;
}

[System.Serializable]
public class ListOfOption
{
    public string options;
    public string code;
}