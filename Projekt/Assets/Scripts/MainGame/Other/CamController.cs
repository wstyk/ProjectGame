using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{

    public Transform player;
    Vector3 camPos;
    Transform camTrans;
    public Camera cam;
    float distance;
    public float currentX;
    public float speedX, zoomSpeed, YMax, YMin;
    void Start()
    {
        distance =  70f;
        speedX = 3f;
        camTrans = transform;
    }

    void Update()
    {
        CameraRot();
    }

    //Jako że kamera ma się poruszać za graczem, chcemy żeby zmiana pozycji kamery odbywała się po ewentualnej zmianie pozycji gracza, dlatego używam LateUpdate
    void LateUpdate()
    {
        CameraPos();
    }


    //Skrypt do pozycji kamery, ustawia pozycję kamery na wybranej pozycji a następnie, 
    //jako że ciągle obraca w odpowiedni sposób kamerę, po prostu ją "cofa" do tyłu, 
    //aby kontrolować pozycję kamery należy zmienić wartośći w Vector3 przy camPos
    void CameraPos()
    {
        camPos = player.transform.position + new Vector3(0, 70, 0);
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(0, currentX, 0);
        camTrans.position = camPos + rotation * direction;
        camTrans.LookAt(player.transform.position + new Vector3(0, 20, 0));
    }
    //Skrypt do obracania kamery
    void CameraRot()
    {
        currentX += Input.GetAxis("Mouse X") * speedX;
    }

}