using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        // PhotonNetwork.IsMessageQueueRunning = true;

        Vector3 pos = new Vector3(Random.Range(-150.0f, 150.0f), 5.0f, Random.Range(-150.0f, 150.0f));

        // 통신이 가능한 탱크 생성(이름, 생성위치, 생성방향?, 그룹아이디)
        PhotonNetwork.Instantiate("Tank", pos, Quaternion.identity, 0);
    }
}
