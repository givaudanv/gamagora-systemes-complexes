using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus
{
    public Color color;
    public float spreadRate;
    public float infectionPower;
    public float infectionSpeed;
    public int nbSpore;
    public float sporeLifetime;
    public float sporeSpeed;

    public Virus(Color col, float srate, float ipwr, float ispeed, int nbv, float sporetime, float sporesp)
    {
        color = col;
        spreadRate = srate;
        infectionPower = ipwr;
        infectionSpeed = ispeed;
        nbSpore = nbv;
        sporeLifetime = sporetime;
        sporeSpeed = sporesp;
    }
}
