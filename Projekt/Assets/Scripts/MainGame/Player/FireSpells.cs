using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireSpells : NetworkBehaviour {
#if UNITY_EDITOR
    [String("Camera reference:")]
#endif
    [SerializeField]
    GameObject PlayerCam;
    float FireBallCD;
    float FlameCD;
    float FireBallDistance;
    float FlameThrowerDistance;
#if UNITY_EDITOR
    [String("Spells prefab reference:")]
#endif
    [SerializeField]
    GameObject fireBallPrefab;
    [SerializeField]
    GameObject flamePrefab;
    GameObject Flame;
    public string Off1, Off2, Off3, Deff1;
    bool FlameOff;

    void Start()
    {

        FireBallDistance = 3f;
        FlameThrowerDistance = 10f;
        FlameCD = 3;
        FlameOff = true;
        if(PlayerPrefs.GetString("ChosenOffElement") == "Fire")
        {
            Off1 = PlayerPrefs.GetString("Off1");
            Off2 = PlayerPrefs.GetString("Off2");
            Off3 = PlayerPrefs.GetString("Off3");
        }
        if(PlayerPrefs.GetString("ChosenDeffElement") == "Fire")
        {
            Deff1 = PlayerPrefs.GetString("Deff1");
        }
    }

	void Update () {
        CmdCooldowns();
        if (FlameOff) CmdFlameCD();
        if(PlayerPrefs.GetString("ChosenOffElement") == "Fire")
        {
            if (Input.GetKey(KeyCode.Mouse0)) CmdFireSpell(Off1);
            if (Input.GetKey(KeyCode.Mouse2)) CmdFireSpell(Off2);
            if (Input.GetKey(KeyCode.Space)) CmdFireSpell(Off3);
        }
        if(PlayerPrefs.GetString("ChosenDeffElement") == "Fire")
        {
            if (Input.GetKey(KeyCode.Mouse1)) CmdFireSpell(Deff1);
        }
        

    }

    //Komunikacja klient-serwer, klient nie może bezpośrednio wykonać czegoś
    //na serwerze, musi zmusić serwer do zrobienia tego.
    //Każda funkcja musi mieć przedrostek Cmd.

    [Command]
    void CmdFireSpell(string spell)
    {
        RpcFireSpell(spell);
    }


    //Komendy wykonywane przez serwer na serwerze tak aby były widoczne dla każdego klienta
    //Każda funkcja musi mieć przedrostek Rpc
    //Wszystko co związane z spellami musi być wykonywane w tych funkcjach

    //Spelle
    [ClientRpc]
    void RpcFireSpell(string spell)
    {
        //Kula ognia
        if (spell == "FireBall" && FireBallCD <=0)
        {
            FireBallCD = 1f;
            Instantiate(fireBallPrefab, gameObject.transform.position + gameObject.transform.forward * FireBallDistance + new Vector3(0, 10, 0), gameObject.transform.rotation);
        }

        //FlameThrow
        if(spell == "FlameThrower")
        {
            //Trzeba zaprogramować od nowa
        }
    }

    //Czas Odnownienia
    [Command]
    void CmdCooldowns()
    {
        RpcCooldowns();
    }
    [ClientRpc]
    void RpcCooldowns()
    {
        if (FireBallCD > 0) FireBallCD -= Time.deltaTime;
    }

    [Command]
    void CmdFlameCD()
    {
        RpcFlameCD();
    }
    [ClientRpc]
    void RpcFlameCD()
    {
        if (FlameCD < 3) FlameCD += Time.deltaTime;
    }
    IEnumerator FlameWait()
    {
        yield return new WaitForSeconds(1f);
        FlameOff = true;
    }
}
