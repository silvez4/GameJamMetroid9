using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    private bool moving = true;

    public LayerMask mask;
    private LineRenderer line;

    private Rigidbody2D rb;
    private DistanceJoint2D joint;
    private GameObject player;

    private Vector3 direct;
    private float angle;
    void Start()
    {

        player = PlayerController.instance.gameObject;

        //TODO: change mouseposition to angle
        direct = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direct.z = 0;

        Vector3 dir = transform.position-direct;
        dir = transform.InverseTransformDirection(dir);
        float angle = (Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg)+180;
        transform.rotation= Quaternion.Euler(0,0,angle);
        Debug.Log(angle);

        

        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();

        joint = player.AddComponent<DistanceJoint2D>();
        joint.autoConfigureDistance = true;
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = Vector3.zero;
        joint.connectedBody = GetComponent<Rigidbody2D>();
        joint.enabled = false;

        line = GetComponent<LineRenderer>();
    }

    void Update(){
        if (Physics2D.OverlapCircle(transform.position, 0.01f, mask)){
            moving = false;
             joint.enabled = true;
            joint.autoConfigureDistance = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("Jump")){
            DestroyHook();
        }
    }

    void FixedUpdate()
    {
        var pts = new Vector3[2];
        pts[0] = transform.position;
        pts[1] = player.transform.position;

     line.SetPositions(pts);

            if (moving){
                if (Vector3.Distance(transform.position, player.transform.position) > 8)
                    DestroyHook();

                transform.Translate(0.6f,0,0);
                //transform.position = Vector3.MoveTowards(transform.position, direct, 0.8f);
            }else{
               joint.distance = Mathf.MoveTowards(joint.distance,1.2f, 0.25f);
            }
    }

    private void DestroyHook(){
        Destroy(joint);
        Destroy(gameObject);
    }
}
