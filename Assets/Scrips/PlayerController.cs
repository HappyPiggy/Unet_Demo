using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    public float spinVelocity;
    public float moveVelocity;


    void Update()
    {

        if (!isLocalPlayer)
            return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.Rotate(spinVelocity * Time.deltaTime * Vector3.up * h);
        transform.Translate(moveVelocity * Time.deltaTime * Vector3.forward * v);
    }
}
