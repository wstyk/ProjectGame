using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerHP : NetworkBehaviour {
    [HideInInspector]
    public GameObject hpbar;
    [HideInInspector]
    public Text UIText;
    [SyncVar]
    float HP;
    [HideInInspector]
    public float maxHP;
    [SerializeField]
    PlayerNet playerNet;

    void Start()
    {
        HP = maxHP;
    }

    //Funkcja zadająca obrażenia
    [ServerCallback]
    public void TakeDamage(float dmg)
    {
        HP -= dmg;
        RpcRemoteChange(HP);
        TargetLocalChange(connectionToClient, HP);
        if (HP <= 0) Dead();
    }
    
    //Resetowanie HP do max
    [ServerCallback]
    public void ResetHP()
    {
        HP = maxHP;
        TakeDamage(0);
    }
    
    //Zmiana paska nad głową
    [ClientRpc]
    public void RpcRemoteChange(float UpdatedHP)
    {
        hpbar.transform.localScale = new Vector3(UpdatedHP/maxHP, 1f, 1f);
    }
    
    //Zmiana lokalnego canvasu
    [TargetRpc]
    void TargetLocalChange(NetworkConnection player, float UpdatedHP)
    {
        UIText.text = UpdatedHP + "/" + maxHP;
    }
    
    //Funckja wywołująca funkcję śmierci z skryptu PlayerNet
    [ServerCallback]
    void Dead()
    {
        playerNet.Die();
    }
}
