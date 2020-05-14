using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    private bool inUse;
    private bool moving;

    public LayerMask mask;
    private LineRenderer line;

    private Rigidbody2D rb;
    private DistanceJoint2D joint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
        joint.connectedBody = GameObject.Find("Player@GameObject").GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
        joint.enabled = false;

    }

    void Update(){
        if (Physics2D.OverlapCircle(transform.position, 0.01f, mask)){
            moving = false;
             joint.enabled = true;
            joint.autoConfigureDistance = false;
        }



        if (Input.GetKeyDown(KeyCode.E) && !inUse){
        //rb.AddForce(new Vector2(3000,3000)); 
        moving = true;
        inUse = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        var points = new Vector3[2];
        points[0] = transform.position;
        points[1] = PlayerController.instance.transform.position;
        line.SetPositions(points);
        if (inUse){
            if (moving){
                if (Vector3.Distance(transform.position, joint.connectedBody.transform.position) > 8)
                    Destroy(gameObject);
                transform.Translate(0.1f,0.1f,0);
            }else{
               joint.distance = Mathf.MoveTowards(joint.distance,1.2f, 0.2f);
                //joint.connectedAnchor = new Vector2(Mathf.MoveTowards(joint.connectedAnchor.x,0, 0.09f), Mathf.Lerp(joint.connectedAnchor.y, 1.2f, 0.05f));
            }

        }
        else{
           transform.position = PlayerController.instance.transform.position;
        }
    }


}
