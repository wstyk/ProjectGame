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
    bool FlameOff;

    void Start()
    {
        FireBallDistance = 3f;
        FlameThrowerDistance = 10f;
        FlameCD = 3;
        FlameOff = true;
    }

	void Update () {
        CmdCooldowns();
        if (FlameOff) CmdFlameCD();

		if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdFireSpell("fireBall");
        }
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            CmdFireSpell("flameCreate");
        }
        if(Input.GetKey(KeyCode.Mouse2))
        {
            CmdFireSpell("flameHold");
        }
        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            CmdFireSpell("flameDestroy");
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
        if (spell == "fireBall" && FireBallCD <=0)
        {
            FireBallCD = 1f;
            Instantiate(fireBallPrefab, gameObject.transform.position + gameObject.transform.forward * FireBallDistance + new Vector3(0, -2, 0), gameObject.transform.rotation);
        }

        //FlameThrow
        if (spell == "flameCreate" && FlameCD > 0)
        {
            Flame = Instantiate(flamePrefab, gameObject.transform.position + gameObject.transform.forward * FlameThrowerDistance + new Vector3(0, -3, 0), gameObject.transform.rotation * Quaternion.Euler(90, 0, 0), gameObject.transform);
        }
        if(spell == "flameHold" && FlameCD > 0 && Flame != null)
        {
            FlameCD -= Time.deltaTime;
            FlameOff = false;
        }
        if(spell == "flameDestroy" && Flame != null || FlameCD <= 0)
        {
            Destroy(Flame);
            StartCoroutine("FlameWait");
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
