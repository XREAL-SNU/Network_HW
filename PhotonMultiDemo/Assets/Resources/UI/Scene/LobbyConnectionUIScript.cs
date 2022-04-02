using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyConnectionUIScript : UIScene
{

    bool syncScenes = true;

    void Start()
    {
        Init();
    }

    enum Images
    {
        LobbyJoinBtn
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        GetUIComponent<Image>((int)Images.LobbyJoinBtn).gameObject.BindEvent(OnClick_JoinLobby);
        Debug.Log("UI init");
    }

    public const string GAME_LEVEL_KEY = "Level";
    public void OnClick_JoinLobby(PointerEventData data)
    {
        Debug.Log("OnClick_JoinLobby");
        PhotonNetwork.JoinLobby();
        UIManager.UI.CloseSceneUI();
    }
}
