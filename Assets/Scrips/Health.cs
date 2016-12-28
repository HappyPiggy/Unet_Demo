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

   public void GetDamage(int damage) {
       if (!isServer) return;  //只在服务器端检测血量


        currentHealth -= damage;

        if (currentHealth <= 0) {

            currentHealth = 0;
            print("Dead");
            RpcReSpawn();
        }

       
    
    }


   void OnHealthChange(int health) {

       hpSlider.value = health / (float)maxHealth;
   }

    [ClientRpc]
   void RpcReSpawn() {
        //和player的都localPlayer处理后同步给其他客户端
        //其他逻辑在服务器端处理后同步到客户端
       if (!isLocalPlayer) return;
       transform.position = Vector3.zero;
   }

}
