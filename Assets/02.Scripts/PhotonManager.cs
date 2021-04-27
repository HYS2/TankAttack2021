using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // 같은 버전을 가진 사람들끼리 네트워크 연결되도록
    private readonly string gameVersion = "v1.0";
    private string userId = "Hayomi";

    // 로비에서 받는 텍스트
    public TMP_InputField userIdText;
    public TMP_InputField roomNameText;

    void Awake()
    {
        // 게임 버전 지정
        PhotonNetwork.GameVersion = gameVersion;
        // 유저명 지정
        PhotonNetwork.NickName = userId;
        
        // 서버 접속
        PhotonNetwork.ConnectUsingSettings();

        // 방장. loadlevel을 쓰려면
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        userId = PlayerPrefs.GetString("USER_ID", $"USER_{Random.Range(0,100):00}");
        userIdText.text = userId;
        PhotonNetwork.NickName = userId;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Server!");

        // 랜덤한 룸 생성
        // PhotonNetwork.JoinRandomRoom();

        // 로비 접속
        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Join Lobby!");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"code = {returnCode}, msg = {message}");

        // 룸 속성을 설정
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 30;

        // 룸 생성
        PhotonNetwork.CreateRoom("My Room", ro);
    }

    // 룸 생성 완료 콜백함수
    public override void OnCreatedRoom()
    {
        Debug.Log("방 생성 완료");
    }

    // 룸에 입장했을 때 호출되는 콜백함수
    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 완료");
        Debug.Log(PhotonNetwork.CurrentRoom.Name);

        // 통신이 가능한 탱크 생성(이름, 생성위치, 생성방향?, 그룹아이디)
        // PhotonNetwork.Instantiate("Tank", new Vector3(0, 5.0f, 0), Quaternion.identity, 0);

        // 방장이 넘어가면 다같이 넘어가는? 카트라이더 같은 느낌
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("BattleField");
        }
    }

    public void OnLoginClick()
    {
        if (string.IsNullOrEmpty(userIdText.text))
        {
            userId = $"USER_{Random.Range(0,100):00}";
            userIdText.text = userId;
        }

        PlayerPrefs.SetString("USER_ID", userIdText.text);
        PhotonNetwork.NickName = userIdText.text;
        PhotonNetwork.JoinRandomRoom();
    }
}
