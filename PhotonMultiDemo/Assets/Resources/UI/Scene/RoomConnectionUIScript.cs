using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomConnectionUIScript : UIScene
{

    bool syncScenes = true;

    void Start()
    {
        Init();
    }

    enum Images
    {
        RoomJoinBtn
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        GetUIComponent<Image>((int)Images.RoomJoinBtn).gameObject.BindEvent(OnClick_JoinRandomRoom);
        Debug.Log("UI init");
    }

    public const string GAME_LEVEL_KEY = "Level";
    public void OnClick_JoinRandomRoom(PointerEventData data)
    {
        Debug.Log("OnClick_JoinRandomRoom");
        PhotonNetwork.JoinRandomOrCreateRoom();
        UIManager.UI.CloseSceneUI();
    }
}
