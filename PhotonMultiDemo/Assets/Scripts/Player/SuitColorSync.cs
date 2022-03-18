using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SuitColorSync : MonoBehaviourPunCallbacks
{
    const string SUIT_COLOR_KEY = "SuitColor";
    public enum ColorEnum
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
        suitRenderer = transform.Find("Space_Suit/Tpose_/Man_Suit/Body").GetComponent<Renderer>();
    }

    public override void OnPlayerPropertiesUpdate(Player player, Hashtable updatedProps)
    {
        block.SetColor("_Color", GetColor((ColorEnum)updatedProps[SUIT_COLOR_KEY]));
        suitRenderer.SetPropertyBlock(block);
    }

    public void SetColorProperty(ColorEnum col)
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { SUIT_COLOR_KEY, col } });
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R)) SetColorProperty(ColorEnum.Red);
        if (Input.GetKey(KeyCode.G)) SetColorProperty(ColorEnum.Green);
        if (Input.GetKey(KeyCode.B)) SetColorProperty(ColorEnum.Blue);
    }

}
