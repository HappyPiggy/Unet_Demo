using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public const int maxHealth = 100;


    //当服务器端改变后出发函数，自动分发到客户端
    [SyncVar(hook="OnHealthChange")]
    public int currentHealth = maxHealth;

    public Slider hpSlider;
    public bool destroyOnDeath=false;

    private NetworkStartPosition[] startPoints;



    void Start() {

        if(isLocalPlayer)
             startPoints = FindObjectsOfType<NetworkStartPosition>();
    
    }

   public void GetDamage(int damage) {
       if (!isServer) return;  //只在服务器端检测血量


        currentHealth -= damage;

        if (currentHealth <= 0) {

            currentHealth = 0;
            if (destroyOnDeath) {
                Destroy(gameObject);
                return;
            }
            currentHealth = maxHealth;
            print("Dead");
            RpcReSpawn();
        }

       
    
    }


   void OnHealthChange(int health) {

       hpSlider.value = health / (float)maxHealth;
   }

    [ClientRpc]
   void RpcReSpawn() {
        //和player有关的都localPlayer处理后同步给其他客户端
        //其他逻辑在服务器端处理后同步到客户端
       if (!isLocalPlayer) return;

       Vector3 startPos = Vector3.zero;
       if (startPoints != null && startPoints.Length > 0) { 
        startPos=startPoints[Random.Range(0,startPoints.Length)].transform.position;
       }
       transform.position = startPos;

      

   }

}
