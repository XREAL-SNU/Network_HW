using Cinemachine;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    // singleton, dont destroy on load
    public static PlayerManager Instance = null;

    private void Awake()
    {
        // singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }




    public override void OnJoinedRoom()
    {
        // instantiate camera, locally
        var prefab = (GameObject)Resources.Load("PhotonPrefab/PlayerFollowCamera");
        var cam = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        cam.name = "PlayerFollowCamera";

        // instantiate player and link
        var player = PhotonNetwork.Instantiate("PhotonPrefab/CharacterPrefab", Vector3.zero, Quaternion.identity);
        if (cam != null && player != null) cam.GetComponent<CinemachineVirtualCamera>().Follow = player.transform.Find("FollowTarget");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("PlayerManager/CreatedRoom");
    }

}
