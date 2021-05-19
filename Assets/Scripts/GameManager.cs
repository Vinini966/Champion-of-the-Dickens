using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public enum melee{BASIC, REACH, BROAD};
    public enum thrown{BASIC, LOB, QUICK};

    [Header("Attack")]
    public GameObject player;
    public GameObject atk;

    [Header("Melee")]
    [SerializeField]
    public melee sword = melee.BASIC;

    [Header("Ranged")]
    [SerializeField]
    public thrown arrow = thrown.BASIC;
    public GameObject[] thrownObjs;

    [Header("Timers")]
    public Timer meleeTmr;
    public Timer meleeActTmr;
    public Timer rangeTmr;

    
    private float swordCool;
    private string atkType;
    private float trowCool;
    private bool paused = false;

    [Header("Misc")]
    public Animator anim;
    //public GameObject currentRange;

    [Header("Health UI")]
    public Sprite[] heartsBase;
    public Image[] hearts;

    [Header("Weapon UI")]
    public Sprite[] meleeWeapon;
    public Image WeaponImg;
    public Sprite[] thrownWeapons;
    public Image ThrownImg;


    //Atacks
    public void WeaponTypeSword(melee m)
    {
        sword = m;
        switch (m)
        {
            case melee.BASIC:
                atkType = "Sword";
                atk.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0f);
                atk.GetComponent<BoxCollider2D>().size = new Vector2(0.15f, 0.15f);
                WeaponImg.sprite = meleeWeapon[0];
                swordCool = 0.5f;
                break;
            case melee.REACH:
                atkType = "Spear";
                atk.GetComponent<BoxCollider2D>().offset = new Vector2(0.1f, 0f);
                atk.GetComponent<BoxCollider2D>().size = new Vector2(0.3f, 0.1f);
                WeaponImg.sprite = meleeWeapon[1];
                swordCool = 0.25f;
                break;
            case melee.BROAD:
                atkType = "Axe";
                atk.GetComponent<BoxCollider2D>().offset = new Vector2(-0.025f, 0.09f);
                atk.GetComponent<BoxCollider2D>().size = new Vector2(0.2f, 0.4f);
                WeaponImg.sprite = meleeWeapon[2];
                swordCool = 0.75f;
                break;
        }

    }

    public void SwingSword()
    {
        if (meleeTmr.checkTimer() && rangeTmr.checkTimer())//checks if both weapons are not on cool down
        {
            player.GetComponent<Player_Controler>().sound.PlayOneShot(player.GetComponent<Player_Controler>().Atack);
            print("Attack");
            anim.SetTrigger(atkType);
            meleeTmr.setTimer(swordCool);
            //WeaponTypeSword(sword);
            atk.transform.localPosition = new Vector3(0.257f, -0.097f);
            atk.GetComponent<BoxCollider2D>().enabled = true;
            meleeTmr.startTimer();
            meleeActTmr.startTimer();
        }
        

    }


    //Ranged Attack
    public void WeaponTypeRanged(thrown ar)
    {
        
        arrow = ar;
        switch (ar)
        {
            case thrown.BASIC:
                trowCool = 0.75f;
                ThrownImg.sprite = thrownWeapons[0];
                break;
            case thrown.LOB:
                trowCool = 0.5f;
                ThrownImg.sprite = thrownWeapons[1];
                break;
            case thrown.QUICK:
                trowCool = 0.25f;
                ThrownImg.sprite = thrownWeapons[2];
                break;
            
        }
    }

    public void ThrowWeapon()
    {
        print("Throw");
        if (meleeTmr.checkTimer() && rangeTmr.checkTimer())//checks if both weapons are not on cool down
        {
            atk.transform.localPosition = new Vector3(0.292f, 0.049f);
            GameObject tmp;
            tmp = Instantiate(thrownObjs[(int)arrow], atk.transform.position, atk.transform.rotation);
            tmp.GetComponent<Projectile>().player = player.GetComponent<Player_Controler>();
            rangeTmr.setTimer(trowCool);
            rangeTmr.startTimer();
        }
    }



    //Health
    public void updateHealth(int hp)
    {
        for(int ndx = 0; ndx < 3; ndx++)//iterate 3 times? Iterate over all Hearts.
        {
            if((ndx*2)+2 > hp)//not full
            {
                if(((ndx*2)+2) - hp == 1)
                {
                    hearts[ndx].sprite = heartsBase[1];
                }
                else if ((ndx * 2) - hp < 1)
                {
                    hearts[ndx].sprite = heartsBase[2];
                }
                //else
                //{
                //    hearts[ndx].sprite = heartsBase[0];
                //}
            }
        }


    }

    private void Start()
    {
        anim = player.GetComponent<Animator>();
        WeaponTypeSword(sword);
        meleeTmr = new Timer(swordCool);
        meleeActTmr = new Timer(0.1f);
        ThrownImg.sprite = thrownWeapons[0];
        WeaponTypeRanged(arrow);
        rangeTmr = new Timer(trowCool);
        meleeTmr.zeroTimer();
        meleeActTmr.zeroTimer();
        rangeTmr.zeroTimer();
    }

    private void Update()
    {
        meleeTmr.timerUpdate();
        meleeActTmr.timerUpdate();
        if (meleeActTmr.checkTimer())
        {
            atk.GetComponent<BoxCollider2D>().enabled = false;
        }
        rangeTmr.timerUpdate();
        if (Input.GetButtonDown("Cancel"))
        {
            if (!paused)
                OnGamePause();
            else
                OnGameResume();
            
        }
            
            
    }

    private void OnGamePause()
    {
        paused = !paused;
        print("Pausing");
        meleeTmr.pauseTimer();
        meleeActTmr.pauseTimer();
        rangeTmr.pauseTimer();
        Object[] ob = FindObjectsOfType<Animator>();
        foreach( Animator am in ob)
        {
            am.enabled = false;
        }
        ob = null;
        ob = FindObjectsOfType<BasicEne>();
        foreach(BasicEne bE in ob)
        {
            bE.enabled = false;
        }
        ob = null;
        ob = FindObjectsOfType<LightFx>();
        foreach (LightFx lF in ob)
        {
            lF.enabled = false;
        }
        ob = null;
        ob = FindObjectsOfType<Projectile>();
        foreach (Projectile pR in ob)
        {
            pR.enabled = false;
        }
        ob = null;
        ob = FindObjectsOfType<LobProjectile>();
        foreach (LobProjectile lP in ob)
        {
            lP.enabled = false;
        }
        player.GetComponent<Player_Controler>().enabled = false;
        StartCoroutine(loadPause());

    }

    public void OnGameResume()
    {
        paused = !paused;
        print("Resuming");
        Object[] ob = FindObjectsOfType<Animator>();
        foreach (Animator am in ob)
        {
            am.enabled = true;
        }
        ob = null;
        ob = FindObjectsOfType<BasicEne>();
        foreach (BasicEne bE in ob)
        {
            bE.enabled = true;
        }
        ob = null;
        ob = FindObjectsOfType<LightFx>();
        foreach (LightFx lF in ob)
        {
            lF.enabled = true;
        }
        ob = null;
        ob = FindObjectsOfType<Projectile>();
        foreach (Projectile pR in ob)
        {
            pR.enabled = true;
        }
        ob = null;
        ob = FindObjectsOfType<LobProjectile>();
        foreach (LobProjectile lP in ob)
        {
            lP.enabled = true;
        }
        player.GetComponent<Player_Controler>().enabled = true;
        meleeTmr.resumeTimer();
        meleeActTmr.resumeTimer();
        rangeTmr.resumeTimer();
        SceneManager.UnloadSceneAsync("Pause");
    }

    IEnumerator loadPause() //Starts the Game
    {
        Debug.Log("Start Game");
        AsyncOperation aOA = SceneManager.LoadSceneAsync("Pause", LoadSceneMode.Additive);
        while (!aOA.isDone)
        {
            print("Loading");
            yield return null;
        }
        Transform cam = GameObject.Find("Player").transform;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Pause"));
        GameObject[] d = SceneManager.GetSceneByName("Pause").GetRootGameObjects();
        d[0].transform.position = cam.position;
        d[0].transform.position += new Vector3(0, 0, -10);
    }
}
