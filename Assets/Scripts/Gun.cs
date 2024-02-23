using System.Collections;
using UnityEngine;

/*
    This class was/is a try i gave to inheritance, as I have programmed with java but didn't know how csharp inheritance was, but is very useful for reusing code and maintenance
*/


public class Gun : MonoBehaviour
{
    public int dmg;
    public float range = 20f;

    [SerializeField]
    public GameObject view;

    public AudioSource[] aud;

    [SerializeField]
    private float coolDown = 0.5f;

    private bool onCoolDown = false;



    [SerializeField]
    private UI_Manager ui;
    public int leftBullets;
    public int leftCharger;

    public int maxBullets;
    public int maxCharger;

    public virtual void Start()
    {
        aud = gameObject.GetComponents<AudioSource>();
        

    }
    // Update is called once per frame
    public virtual void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();
        if(Input.GetKeyDown(KeyCode.R))
            reload();
    }
    //Weapon shooting works with raycast
    public void Shoot()
    {
        if(!onCoolDown && leftBullets>0)
        {
            StartCoroutine(coolDownCorroutine());
            RaycastHit hit;
            aud[0].Play();
            leftBullets--;
            ui.setAmmo(leftBullets,leftCharger);
            if(Physics.Raycast(view.transform.position, view.transform.forward, out hit, range))
            {
                if(hit.transform.tag == "Enemy")
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    enemy.getShot();
                }
            }
        }
    }
    public virtual void reload()
    {
       int neededBullets;
        if(leftCharger>0 && leftBullets<maxBullets){
            aud[1].Play();
            if(leftCharger<maxBullets){
                leftBullets += leftCharger;
                leftCharger -= leftCharger;
            }
            else if(leftBullets>=0)
            {
                neededBullets = maxBullets - leftBullets;
                leftBullets += neededBullets;
                leftCharger -= neededBullets;
            }
            else
            {
                leftBullets = leftCharger;
                leftCharger -= leftBullets;
            }
            ui.setAmmo(leftBullets,leftCharger);
    }
}

    IEnumerator coolDownCorroutine()
    {
        onCoolDown = true;
        yield return new WaitForSeconds(coolDown);
        onCoolDown = false;
    }


}
