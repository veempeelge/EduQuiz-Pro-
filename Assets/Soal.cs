using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Soal
{
    public string soal;
    public Jawaban[] jawaban;
}

public class Jawaban
{
    public string jawaban;
    public bool benar;
}