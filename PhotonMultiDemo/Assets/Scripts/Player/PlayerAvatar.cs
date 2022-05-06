using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAvatar : MonoBehaviour, IPunInstantiateMagicCallback
{

    public void Start()
    {
        
        var animatorView = GetComponent<PhotonAnimatorView>();
        animatorView.SetParameterSynchronized("Speed", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Discrete);
        animatorView.SetParameterSynchronized("Jump", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);
        animatorView.SetParameterSynchronized("Grounded", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);
        animatorView.SetParameterSynchronized("FreeFall", PhotonAnimatorView.ParameterType.Bool, PhotonAnimatorView.SynchronizeType.Discrete);
        animatorView.SetParameterSynchronized("MotionSpeed", PhotonAnimatorView.ParameterType.Float, PhotonAnimatorView.SynchronizeType.Discrete);
    }
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        // store this gameobject as this player's charater in Player.TagObject
        info.Sender.TagObject = this.gameObject;

    }

}
