using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 raydir = new Vector3(transform.forward.x, 0, transform.forward.z).normalized * 2f;
        Debug.DrawRay(transform.position, raydir);
        if (Physics.Raycast(transform.position, raydir, out hit))
        {
            if (Input.GetKeyDown(KeyCode.E) && hit.collider.CompareTag("door"))
            {
                Animator doorAnimator = hit.collider.transform.parent.gameObject.GetComponent<Animator>();
                doorAnimator.SetBool("doorOpen", !doorAnimator.GetBool("doorOpen"));
            }
        }   
    }


}
