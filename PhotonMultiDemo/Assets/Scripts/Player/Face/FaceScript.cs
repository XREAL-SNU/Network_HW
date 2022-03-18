using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;

public class FaceScript : MonoBehaviour
{
    public Material AvatarFace;
    public float ShowFaceTime;
    public List<Texture> FaceTextures;
    public byte DefaultFaceIndex;


    int _currentTextureIndex;
    bool _crRunning;
    // coroutine for displaying face
    IEnumerator _coroutine;


    private void Start()
    {
        if (DefaultFaceIndex < FaceTextures.Count - 1)
        {
            SetFace(DefaultFaceIndex);
        }
        else
        {
            throw new System.IndexOutOfRangeException();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) PhotonView.Get(this).RPC("ShowFace", RpcTarget.All, 0);
        if (Input.GetKey(KeyCode.Alpha2)) PhotonView.Get(this).RPC("ShowFace", RpcTarget.All, 1);
        if (Input.GetKey(KeyCode.Alpha3)) PhotonView.Get(this).RPC("ShowFace", RpcTarget.All, 2);
        if (Input.GetKey(KeyCode.Alpha4)) PhotonView.Get(this).RPC("ShowFace", RpcTarget.All, 3);
    }

    [PunRPC]
    void ShowFace(byte index)
    {
        if (_crRunning)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = ShowFaceCoroutine(index);
        StartCoroutine(_coroutine);
    }

    IEnumerator ShowFaceCoroutine(byte index)
    {
        _crRunning = true;

        SetFace(index);

        yield return new WaitForSeconds(ShowFaceTime);

        SetFace(0);

        _crRunning = false;
    }


    public void SetFace(byte index)
    {
        AvatarFace.SetTexture("_MainTex", FaceTextures[index]);
        _currentTextureIndex = index;
    }
}