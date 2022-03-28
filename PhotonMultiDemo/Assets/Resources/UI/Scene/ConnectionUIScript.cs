using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ConnectionUIScript : UIScene
{

    const string SCENE_NAME_KEY = "SceneName";
    public string DefaultSceneName = "SampleScene";

    void Start()
    {
        Init();
    }

    enum Images
    {
        JoinBtn
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        GetUIComponent<Image>((int)Images.JoinBtn).gameObject.BindEvent(OnClick_JoinRandom);
        Debug.Log("UI init");
    }

    public const string GAME_LEVEL_KEY = "Level";
    public void OnClick_JoinRandom(PointerEventData data)
    {
        Debug.Log("OnClick_JoinRandom");

        LoadFirstRoom();
        UIManager.UI.CloseSceneUI();
    }

    public void LoadFirstRoom()
    {
        // must bein lobby to execute
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.CustomRoomPropertiesForLobby = new string[] { SCENE_NAME_KEY };
        roomOptions.CustomRoomProperties = GetFilter(SCENE_NAME_KEY, DefaultSceneName);
        Debug.Log($"JoinRoomTeleport/ do we have correct room options? ({roomOptions.CustomRoomProperties.ToString()})");

        PlayerManager.Instance.SceneNameToLoad = DefaultSceneName;

        PhotonNetwork.JoinRandomOrCreateRoom(
            expectedCustomRoomProperties: GetFilter(SCENE_NAME_KEY, DefaultSceneName),
            roomOptions: roomOptions
        );
    }

    public Hashtable GetFilter(string key, object value)
    {
        Hashtable ht = new Hashtable();
        ht.Add(key, value);
        return ht;
    }
}
