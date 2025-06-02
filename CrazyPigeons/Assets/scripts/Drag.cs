using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Collider2D drag;
    public LayerMask layer;
    [SerializeField] 
    private bool clicked;
    Touch touch;

    public LineRenderer lineFront;
    public LineRenderer lineBack;

    private Ray leftCatapuiltRay;
    private CircleCollider2D passaroCol;
    private UnityEngine.Vector2 catapulTotBird;
    private UnityEngine.Vector3 pointL;
    private SpringJoint2D spring;
    private UnityEngine.Vector2 prevVel;
    private Rigidbody2D passaroRB;

    public GameObject bomb;

    //Limite

    private Transform catapult;
    private Ray rayToMT;

    //Rastro

    private TrailRenderer rastro;

    public Rigidbody2D CatapultRB;
    public bool estouPronto = false;

   [SerializeField]
    public AudioSource audioPassaro;
    public GameObject audioMortePassaro;

    private Transform pontoMorte;

    void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        lineFront = (LineRenderer)GameObject.FindWithTag("LF").GetComponent<LineRenderer>();
        lineBack = (LineRenderer)GameObject.FindWithTag("LB").GetComponent<LineRenderer>();
        CatapultRB = GameObject.FindWithTag("LB").GetComponent<Rigidbody2D>();
        spring.connectedBody = CatapultRB;
        drag = GetComponent<Collider2D>();
        leftCatapuiltRay = new Ray(lineFront.transform.position, UnityEngine.Vector3.zero);
        passaroCol = GetComponent<CircleCollider2D>();

        passaroRB = GetComponent<Rigidbody2D>();

        catapult = spring.connectedBody.transform;
        rayToMT = new Ray(catapult.position, UnityEngine.Vector3.zero);

        rastro = GetComponentInChildren<TrailRenderer>();

        audioPassaro = GetComponent<AudioSource>();

        pontoMorte = GameObject.Find("MORRE").GetComponent<Transform>();
    }

    void Start()
    {
        SetupLine();
    }

    void Update()
    {
        LineUpdate();
        SpringEffect();
        prevVel = passaroRB.velocity;

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            UnityEngine.Vector2 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(wp, UnityEngine.Vector2.zero, Mathf.Infinity, layer);

            if (hit.collider != null)
            {
                clicked = true;
            }

            if (clicked)
            {
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    UnityEngine.Vector3 tPos = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(touch.position.x, touch.position.y, 10));

                    catapulTotBird = tPos - catapult.position;
                    if (catapulTotBird.magnitude > 4)
                    {
                        rayToMT.direction = catapulTotBird;
                        tPos = rayToMT.GetPoint(4);
                    }

                    transform.position = tPos;
                }
            }

            if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && clicked)
            {
                passaroRB.isKinematic = false;
                clicked = false;
                MataPassaro();
            }
        }
#endif

#if UNITY_EDITOR
        if (clicked)
        {
            Dragging();
        }

        if (!clicked && !passaroRB.isKinematic)
        {
            MataPassaro();
        }
#endif


    if (passaroRB.isKinematic == false)
    {
        UnityEngine.Vector3 posCam = Camera.main.transform.position;
        posCam.x = transform.position.x;
        posCam.x = Mathf.Clamp (posCam.x, GAMEMANAGER.instance.objE.position.x, GAMEMANAGER.instance.objD.position.x);
        Camera.main.transform.position = posCam;
    }

    }


    void SetupLine()
    {
        lineFront.SetPosition(0, lineFront.transform.position);
        lineBack.SetPosition(0, lineBack.transform.position);
    }

    void LineUpdate()
    {
        if (transform.name == GAMEMANAGER.instance.nomePassaro)
        {
            catapulTotBird = transform.position - lineFront.transform.position;
            leftCatapuiltRay.direction = catapulTotBird;
            pointL = leftCatapuiltRay.GetPoint(catapulTotBird.magnitude + passaroCol.radius);
            lineFront.SetPosition(1, pointL);
            lineBack.SetPosition(1, pointL);
        }
    }

    void SpringEffect()
    {
        if (spring != null && GAMEMANAGER.instance.passarosEmCena > 0)
        {
            if (!passaroRB.isKinematic)
            {
                if (prevVel.sqrMagnitude > passaroRB.velocity.sqrMagnitude)
                {
                    lineFront.enabled = false;
                    lineBack.enabled = false;
                    Destroy(spring);
                    passaroRB.velocity = prevVel;
                }
            }
            else if (passaroRB.isKinematic && transform.position == GAMEMANAGER.instance.pos.position)
            {
                lineFront.enabled = true;
                lineBack.enabled = true;
            }
        }
    }

    void MataPassaro()
    {
        if (passaroRB.velocity.magnitude == 0 && passaroRB.IsSleeping() || transform.position.x > pontoMorte.position.x)
        {
            StartCoroutine(TempoMorte());
        }
    }

    IEnumerator TempoMorte()
    {
        yield return new WaitForSeconds(1);
        Instantiate(bomb, new UnityEngine.Vector2(transform.position.x, transform.position.y), UnityEngine.Quaternion.identity);
        Instantiate(audioMortePassaro, new UnityEngine.Vector2(transform.position.x, transform.position.y), UnityEngine.Quaternion.identity);
        Destroy(gameObject);
        GAMEMANAGER.instance.passarosNum -= 1;
        GAMEMANAGER.instance.passarosEmCena = 0;
        estouPronto = false;
        GAMEMANAGER.instance.passaroLancado = false;
    }

    void Dragging()
    {
        if (passaroRB.isKinematic)
        {
            UnityEngine.Vector3 mouseWP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWP.z = 0f;

            catapulTotBird = mouseWP - catapult.position;

            if (catapulTotBird.magnitude > 4.2F)
            {
                rayToMT.direction = catapulTotBird;
                mouseWP = rayToMT.GetPoint(4.2F);
            }

            transform.position = mouseWP;
        }
    }

    void OnMouseDown()
    {
        if (GAMEMANAGER.instance.pausado == false)
        {
            if (transform.position == GAMEMANAGER.instance.pos.position)
            {
                clicked = true;
                rastro.enabled = false;
                estouPronto = true;
            }
        }
    }

    void OnMouseUp()
    {
        if (estouPronto)
        {
            passaroRB.isKinematic = false;
            clicked = false;
            rastro.enabled = true;
            GAMEMANAGER.instance.passaroLancado = true;
            audioPassaro.Play ();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       if (col.gameObject.CompareTag("moedasTag"))
       {
        GAMEMANAGER.instance.moedasGame += 50;

        UIMANAGER.instance.moedasTxt.text = GAMEMANAGER.instance.moedasGame.ToString ();
        Destroy (col.gameObject);

       } 
    }



}
