using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Utility;

public class TankCtrl : MonoBehaviour
{
    private Transform tr;
    public float speed = 10.0f;
    private PhotonView pv;

    void Start()
    {
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();

        // 내 탱크만. 불필요한 연산을 줄이기 위해서
        if (pv.IsMine)
        {
            // tr.Find : 탱크의 자식 중에서 찾음
            Camera.main.GetComponent<SmoothFollow>().target = tr.Find("CamPivot").transform;
            // 리지드바디 중심 변경. 원활한 경사로 이동을 위해서
            GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -5.0f, 0);
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

    }

    void Update()
    {
        // 내 포톤인지 체크를 하고 내 것만 조작
        if (pv.IsMine)
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            tr.Translate(Vector3.forward * Time.deltaTime * speed * v);
            tr.Rotate(Vector3.up * Time.deltaTime * 100.0f * h);
        }
    }
}
