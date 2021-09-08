using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton 
{
    private static Singleton instance;

    private int score = 0;
    private bool newRecord = false;

    private Singleton() { } //Constructor privado.

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public bool NewRecord
    {
        get { return newRecord; }
        set { newRecord = value; }
    }
}
