using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    override public void Start()
    {
        base.Start();
        maxBullets = 8;
        maxCharger = 72;
        leftBullets = maxBullets;
        leftCharger = maxCharger;
        dmg = 1;
        range = 20f;
    }
}
