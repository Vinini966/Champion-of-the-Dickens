using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEne : MonoBehaviour
{
    public enum Stance { IDLE, GAURD, CHARGE}
    [SerializeField]
    public Stance stance = Stance.IDLE;
    public enum facing { RIGHT, LEFT };
    [SerializeField]
    public facing dir = facing.RIGHT;

    [Range(0f, 5.0f)]
    public float SpeedWagon;//best girl
    [Range(0f, 5.0f)]
    public float maxAcell;

    [Range(0.0f, 1f)]
    public float rayLength;
    private float curveMod = 0.01f;

    private Vector2 velocity;

    [Range(0f, 5.0f)]
    public float Sight;

    private Transform trans;
    public Transform rayTrans;
    private CapsuleCollider2D eneCapCol;
    private Animator anim;

    public GameObject start;
    public GameObject endt;
    public GameObject player;

    public GameObject heartPrefab;
    public List<GameObject> hearts;
    public GameObject heathLayout;

    private bool wall;
    public bool dying = false;
    private bool dirChange = false;
    private bool reachedEnd = false;
    public int look;
    private Timer lookTmr;
    private Timer atkTimer;
    private Vector3 setScale;

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        eneCapCol = GetComponent<CapsuleCollider2D>();
        trans = GetComponent<Transform>();
        if (endt == null)
            endt = gameObject;
        if (start == null)
            start = gameObject;
        trans.position = start.transform.position;
        player = GameObject.Find("Player");
        look = (int)(Random.Range(0f, 10f));
        lookTmr = new Timer(Random.Range(3f, 10f));
        lookTmr.startTimer();
        atkTimer = new Timer(5f);
        atkTimer.zeroTimer();
        setScale = trans.localScale;
        for(int i = 0; i < health; i++)
        {
            hearts.Add(Instantiate(heartPrefab, heathLayout.transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
        var sign = 0f;
        var plSign = player.transform.position.x - trans.position.x;
        if (reachedEnd)
            sign = start.transform.position.x - trans.position.x;
        else
            sign = endt.transform.position.x - trans.position.x;
        if (Mathf.Sign(sign) < 0)
        {
            trans.localScale = new Vector3(setScale.x, setScale.y, 1);
            if (dir == facing.LEFT)
                dirChange = true;
            else
                dirChange = false;
            dir = facing.RIGHT;
        }
        else if (Mathf.Sign(sign) > 0)
        {
            trans.localScale = new Vector3(-setScale.x, setScale.y, 1);
            if (dir == facing.RIGHT)
                dirChange = true;
            else
                dirChange = false;
            dir = facing.LEFT;
        }

        switch (stance)
        {
            case Stance.IDLE://face player
                lookTmr.timerUpdate();
                if (lookTmr.checkTimer())
                {
                    lookTmr.resetTimer();
                    lookTmr.startTimer();
                    look = (int)(Random.Range(0f, 10f));
                }
                if (look < 6)
                {
                    trans.localScale = new Vector3(1, 1, 1);
                    dir = facing.RIGHT;
                }
                else
                {
                    trans.localScale = new Vector3(-1, 1, 1);
                    dir = facing.LEFT;
                }

                break;

            case Stance.GAURD://walk between 2 points
                
                if (Mathf.Abs(sign) > 0.05f && !dying)
                {
                    velocity.x = Mathf.MoveTowards(velocity.x, SpeedWagon * Mathf.Sign(sign), maxAcell * Time.deltaTime);
                    if (dirChange)
                        velocity.x /= 2.5f;
                    anim.SetBool("Walking", true);
                }
                else
                {
                    anim.SetBool("Walking", false);
                    reachedEnd = !reachedEnd;
                    if (dirChange)
                        velocity.x /= 2.5f;
                }
                    
                break;

            case Stance.CHARGE:
                RaycastHit2D loS = Physics2D.Raycast(rayTrans.position, player.transform.position - trans.position);
                Debug.DrawRay(rayTrans.position, player.transform.position - trans.position, Color.magenta);
                if (Mathf.Sign(plSign) < 0)
                {
                    trans.localScale = new Vector3(1, 1, 1);
                    if (dir == facing.RIGHT)
                        dirChange = true;
                    else
                        dirChange = false;
                    dir = facing.LEFT;
                }
                else if (Mathf.Sign(plSign) > 0)
                {
                    trans.localScale = new Vector3(-1, 1, 1);
                    if (dir == facing.LEFT)
                        dirChange = true;
                    else
                        dirChange = false;
                    dir = facing.RIGHT;
                }
                if (loS.collider != null)
                {
                    //print(loS.collider.ToString()+ " : " + loS.distance.ToString());
                    if(loS.distance < Sight)
                    {
                        if(loS.collider.tag == "Player")
                        {
                            if (Mathf.Abs(plSign) > 0.2f && !dying)
                            {
                                velocity.x = Mathf.MoveTowards(velocity.x, SpeedWagon * Mathf.Sign(plSign), maxAcell * Time.deltaTime);
                                if (dirChange)
                                    velocity.x /= 2.5f;
                                anim.SetBool("Walking", true);
                            }
                            else
                            {
                                anim.SetBool("Walking", false);
                                reachedEnd = !reachedEnd;
                                if (dirChange)
                                    velocity.x /= 2.5f;
                            }
                        }
                    }
                }
                break;
        }



        if (dying)
        {
            velocity.x = 0;
            trans.localScale = new Vector3(-1, 1, 1);
            dir = facing.LEFT;
        }
        

        velocity.y += Physics2D.gravity.y * Time.deltaTime;


        //ground detection
        LayerMask lM = 1 << 8;
        RaycastHit2D hit2D = Physics2D.Raycast(rayTrans.position, -Vector2.up, rayLength, lM);
        RaycastHit2D hit2DL = Physics2D.Raycast(rayTrans.position, RotatePoint(-Vector2.up, 1, 35f), rayLength - curveMod, lM);
        RaycastHit2D hit2DR = Physics2D.Raycast(rayTrans.position, RotatePoint(-Vector2.up, 1, -35f), rayLength - curveMod, lM);

        Debug.DrawRay(rayTrans.position, RotatePoint(-Vector2.up, 1, 35f) * (rayLength - curveMod), Color.green);
        Debug.DrawRay(rayTrans.position, RotatePoint(-Vector2.up, 1, -35f) * (rayLength - curveMod), Color.blue);
        Debug.DrawRay(rayTrans.position, -Vector2.up * rayLength, Color.red);

        //Debug.Log((Vector3)(-Vector2.up * rayLength) + rayTrans.position);
        if (hit2D.collider != null)
        {
            //landing Anim
            if (hit2D.collider.tag == "Ground")
            {
               //Debug.Log("Triggered feet");
                velocity.y = 0;
            }
        }
        else if (hit2DL.collider != null)
        {
            //landing Anim
            if (hit2DL.collider.tag == "Ground")
            {
                //Debug.Log("Triggered slope");
                velocity.y -= Physics2D.gravity.y * Time.deltaTime;
            }
        }
        else if (hit2DR.collider != null)
        {
            //landing Anim
            if (hit2DR.collider.tag == "Ground")
            {
                //Debug.Log("Triggered slope");
                velocity.y -= Physics2D.gravity.y * Time.deltaTime;
            }
        }

        if (wall)
            velocity.x = 0;

        trans.Translate(velocity * Time.deltaTime);

        wall = false;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position + (Vector3)eneCapCol.offset, eneCapCol.size, 0);
        foreach (Collider2D hit in hits)
        {
            if (hit == eneCapCol || hit.tag == "Attack")
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(eneCapCol);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                
                if (Vector2.Angle(colliderDistance.normal, Vector2.right) == 180 && velocity.x > 0)
                {
                    //Debug.Log("Triggered");
                    wall = true;
                }
                if (Vector2.Angle(colliderDistance.normal, Vector2.left) == 180 && velocity.x < 0)
                {
                    //Debug.Log("Triggered");
                    wall = true;
                }
            }
        }
        atkTimer.timerUpdate();
    }

    public Vector2 RotatePoint(Vector2 inputPoint, float radius, float degrees)
    {
        float rads = degrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(rads + Mathf.Acos(inputPoint.x / radius));
        //Debug.Log("X: " + Mathf.Acos(inputPoint.x / radius));
        float y = Mathf.Sin(rads + Mathf.Asin(inputPoint.y / radius));
        //Debug.Log("Y: " + y);
        Vector2 output = new Vector2(x, y);
        return output;
    }

    public void death()
    {
        Destroy(gameObject);
    }

    public void dealDamage()
    {
        player.GetComponent<Player_Controler>().TakeDamage(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision);
        if(collision.tag == "Player" && !dying && atkTimer.checkTimer())
        {
            atkTimer.startTimer();
            anim.SetTrigger("Atack");
            velocity.x = 0;
            anim.SetBool("Walking", false);
            
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !dying)
        {
            velocity.x = 0;
            anim.SetBool("Walking", false);
            if (atkTimer.checkTimer())
            {
                anim.SetTrigger("Atack");
                atkTimer.resetTimer();
                atkTimer.startTimer();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !dying)
        {
            atkTimer.resetTimer();
            atkTimer.zeroTimer();
        }
    }

    public void TakeDamage(int amt)
    {
        if(health - amt <= 0)
        {
            health = 0;
        }
        else
            health -= amt;

        while(hearts.Count > health)
        {
            Destroy(hearts[0]);
            hearts.RemoveAt(0);
        }
    }
}
