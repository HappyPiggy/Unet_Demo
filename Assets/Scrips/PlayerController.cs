using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    public float spinVelocity;
    public float moveVelocity;

    public Transform BulletSpawn;
    public GameObject Bullet;


    void Update()
    {

        if (!isLocalPlayer)
            return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.Rotate(spinVelocity * Time.deltaTime * Vector3.up * h);
        transform.Translate(moveVelocity * Time.deltaTime * Vector3.forward * v);

        if (Input.GetKeyDown(KeyCode.Space)) {
            CmdFire();   
        }
    }


    public override void OnStartLocalPlayer()
    {
      //  base.OnStartLocalPlayer();
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    [Command] //在client调用 在server端运行
    public void CmdFire()
    {
        GameObject go = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation) as GameObject;
        go.GetComponent<Rigidbody>().velocity = BulletSpawn.forward * 15;

        Destroy(go, 1);

        NetworkServer.Spawn(go);
    }
}
