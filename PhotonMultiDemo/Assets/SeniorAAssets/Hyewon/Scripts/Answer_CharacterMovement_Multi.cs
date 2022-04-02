using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer_CharacterMovement_Multi : CharacterControllerThirdPerson
{
    PhotonView _view;
    protected override void Start()
    {
        _view = GetComponent<PhotonView>();
        if (_view.IsMine) base.Start();
    }

    protected override void Update()
    {
        if(_view.IsMine) base.Update();
    }
}