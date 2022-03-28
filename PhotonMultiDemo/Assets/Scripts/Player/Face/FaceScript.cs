using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;

public class FaceScript : MonoBehaviour, IPunInstantiateMagicCallback
{
    public MaterialPropertyBlock AvatarFace;
    public float ShowFaceTime;
    public List<Texture> FaceTextures;
    public byte DefaultFaceIndex;

    PhotonView view;
    int _currentTextureIndex;
    bool _crRunning;
    // coroutine for displaying face
    IEnumerator _coroutine;



    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        // local copy of material
        var faceRenderer = transform.Find("Space_Suit/Tpose_/Man_Suit/Face").GetComponent<SkinnedMeshRenderer>();
        faceRenderer.materials[0] = new Material(faceRenderer.materials[0]);

        AvatarFace = new MaterialPropertyBlock();
        view = PhotonView.Get(this);
        Debug.Log($"FaceScript/ {PhotonView.Get(this).Owner.NickName}'s material copied");
    }

    private void Start()
    {
        // load the textures
        if(FaceTextures.Count < 1)
        {
            Object[] tex = Resources.LoadAll("Avatar Face/emojiTextures", typeof(Texture));
            for (int i = 0; i < tex.Length; ++i)
            {
                FaceTextures.Add((Texture)tex[i]);
            }

        }

        // set to default face at beginning
        if (PhotonView.Get(this).IsMine && DefaultFaceIndex < FaceTextures.Count - 1)
        {
            SetFace(DefaultFaceIndex);
        }

    }

    private void Update()
    {
        if (!view.IsMine) return;
        if (Input.GetKeyDown(KeyCode.Alpha1)) view.RPC("ShowFace", RpcTarget.All, (byte)0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) view.RPC("ShowFace", RpcTarget.All, (byte)1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) view.RPC("ShowFace", RpcTarget.All, (byte)2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) view.RPC("ShowFace", RpcTarget.All, (byte)3);
    }

    [PunRPC]
    void ShowFace(byte index)
    {
        if (_crRunning)
        {
            StopCoroutine(_coroutine);
            _crRunning = false;
        }
        _coroutine = ShowFaceCoroutine(index);
        StartCoroutine(_coroutine);
    }

    IEnumerator ShowFaceCoroutine(byte index)
    {
        _crRunning = true;

        SetFace(index);

        yield return new WaitForSeconds(ShowFaceTime);

        SetFace(DefaultFaceIndex);

        _crRunning = false;
    }


    public void SetFace(byte index)
    {
        AvatarFace.SetTexture("_MainTex", FaceTextures[index]);

        var faceRenderer = transform.Find("Space_Suit/Tpose_/Man_Suit/Face").GetComponent<SkinnedMeshRenderer>();
        faceRenderer.SetPropertyBlock(AvatarFace);

        _currentTextureIndex = index;
    }

}