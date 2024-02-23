using UnityEngine;
using System.Threading.Tasks;

public class Sniper : Gun
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        leftBullets = maxBullets;
        leftCharger = maxCharger;
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetKey(KeyCode.Mouse1))
            UnityEngine.Camera.main.fieldOfView = 10f;
        else
        UnityEngine.Camera.main.fieldOfView = 60f;
    }
}
