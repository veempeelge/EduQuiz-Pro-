using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]

public class Soal
{
    public string soal;
    public Jawaban[] jawaban;

    public void PrintSoal()
    {
        Debug.Log($"Soal: {soal}");
    }
}

[System.Serializable]
public class Jawaban
{
    public string jawaban;
    public bool benar;
}

[System.Serializable]
public class SoalSoal
{
    public Soal[] soalsoal;
}