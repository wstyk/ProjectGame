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
    [SerializeField]
    string Off1, Off2, Off3, Deff1, ChosenOff, ChosenDeff;
    bool FlameOff;

    void Start()
    {

        FireBallDistance = 3f;
        FlameThrowerDistance = 10f;
        FlameCD = 3;
        FlameOff = true;
        ChosenOff = PlayerPrefs.GetString("ChosenOffElement");
        ChosenDeff = PlayerPrefs.GetString("ChosenDeffElement");
        if(ChosenOff == "Fire")
        {
            Off1 = PlayerPrefs.GetString("Off1");
            Off2 = PlayerPrefs.GetString("Off2");
            Off3 = PlayerPrefs.GetString("Off3");
        }
        if(ChosenDeff == "Fire")
        {
            Deff1 = PlayerPrefs.GetString("Deff1");
        }
    }

	void Update () {
        CmdCooldowns();
        if (FlameOff) CmdFlameCD();
        if(ChosenOff == "Fire")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) CmdSpell(Off1 + "Down");
            else if (Input.GetKeyUp(KeyCode.Mouse0)) CmdSpell(Off1 + "Up");
            else if (Input.GetKey(KeyCode.Mouse0)) CmdSpell(Off1 + "Hold");

            if (Input.GetKeyDown(KeyCode.Mouse2)) CmdSpell(Off2 + "Down");
            else if (Input.GetKeyUp(KeyCode.Mouse2)) CmdSpell(Off2 + "Up");
            else if (Input.GetKey(KeyCode.Mouse2)) CmdSpell(Off2 + "Hold");

            if (Input.GetKeyDown(KeyCode.Mouse3)) CmdSpell(Off3 + "Down");
            else if (Input.GetKeyUp(KeyCode.Mouse3)) CmdSpell(Off3 + "Up");
            else if (Input.GetKey(KeyCode.Mouse3)) CmdSpell(Off3 + "Hold");
        }
        if(ChosenDeff == "Fire")
        {
            if (Input.GetKeyDown(KeyCode.Mouse1)) CmdSpell(Deff1 + "Down");
            else if (Input.GetKeyUp(KeyCode.Mouse1)) CmdSpell(Deff1 + "Up");
            else if (Input.GetKey(KeyCode.Mouse1)) CmdSpell(Deff1 + "Hold");
        }
    }

    //Komunikacja klient-serwer, klient nie może bezpośrednio wykonać czegoś
    //na serwerze, musi zmusić serwer do zrobienia tego.
    //Każda funkcja musi mieć przedrostek Cmd.

    [Command]
    void CmdSpell(string spell)
    {
        RpcSpell(spell);
    }


    //Komendy wykonywane przez serwer na serwerze tak aby były widoczne dla każdego klienta
    //Każda funkcja musi mieć przedrostek Rpc
    //Wszystko co związane z spellami musi być wykonywane w tych funkcjach

    //Spelle
    [ClientRpc]
    void RpcSpell(string spell)
    {
        //Kula ognia
        if (spell == "FireBallDown" && FireBallCD <=0)
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
