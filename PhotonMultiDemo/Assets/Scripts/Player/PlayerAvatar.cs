using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour, IPunInstantiateMagicCallback
{
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        // store this gameobject as this player's charater in Player.TagObject
        info.Sender.TagObject = this.gameObject;
        
        Debug.Log($"Player {info.Sender.NickName}'s Avatar is instantiated/t={info.SentServerTime}");
        InitializeMaterials();
    }

    void InitializeMaterials()
    {
        var suitRenderer = transform.Find("Space_Suit/Tpose_/Man_Suit/Body").GetComponent<Renderer>();
        // make a copy of the material
        suitRenderer.materials[0] = new Material(suitRenderer.materials[0]);
    }
}
