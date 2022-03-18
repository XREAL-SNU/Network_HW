using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SuitColorSync : MonoBehaviourPunCallbacks
{
    const string SUIT_COLOR_KEY = "SuitColor";
    enum ColorEnum
    {
        Red, Green, Blue
    }

    Color GetColor(ColorEnum col)
    {
        switch (col)
        {
            case ColorEnum.Red:
                return Color.red;
            case ColorEnum.Green:
                return Color.green;
            case ColorEnum.Blue:
                return Color.blue;
        }
        return Color.black;
    }

    MaterialPropertyBlock block;
    Renderer suitRenderer;
    void Start()
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { SUIT_COLOR_KEY, ColorEnum.Blue } });
        block = new MaterialPropertyBlock();
        block.SetColor("_Color", Color.black);
        suitRenderer = transform.Find("Space_Suit/Man_Suit/Body").GetComponent<Renderer>();

    }

    public override void OnPlayerPropertiesUpdate(Player player, Hashtable updatedProps)
    {
        Debug.Log($"player {player.NickName}'s color updated to {updatedProps[SUIT_COLOR_KEY]}");
        Debug.Log("parsed enum : " + (ColorEnum)updatedProps[SUIT_COLOR_KEY]);
        block.SetColor("_Color", GetColor((ColorEnum)updatedProps[SUIT_COLOR_KEY]));
        suitRenderer.SetPropertyBlock(block);

    }
}
