using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy
{

    public static new int KILLS = 0;

    public override void sendKill()
    {
        BigEnemy.KILLS++;
    }

    public override void Start()
    {
        base.Start();
        speed = 1;
    }


}
