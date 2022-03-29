using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class SimpleStickyNote : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stickyNotePrefab;
    public InputField noteContent;
    private GameObject _player;
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame



    public void CreateStickyNote()
    {
        _player = GameObject.FindWithTag("Player");
        Vector3 mainCamRotation = Camera.main.transform.rotation.eulerAngles;
        Vector3 forwardRotation = new Vector3(0, mainCamRotation.y, 0);
        //if(StickyNoteNetworkManager.Instance.networked)
        Text noteText = stickyNotePrefab.transform.Find("ContentPanel").transform.Find("Text").gameObject.GetComponent<Text>();
        noteText.text = noteContent.text;
            PhotonNetwork.Instantiate(stickyNotePrefab.name, _player.transform.position, Quaternion.Euler(forwardRotation), 0);
        //else
        //    Instantiate(stickyNotePrefab, Vector3.zero, Quaternion.identity);
        noteContent.text = " ";
        this.gameObject.SetActive(false);
    }

}
