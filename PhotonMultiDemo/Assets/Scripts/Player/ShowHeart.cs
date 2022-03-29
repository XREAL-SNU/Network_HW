using UnityEngine;
using Photon.Pun;

public class ShowHeart : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private GameObject heart;
    bool show = false;
    // Start is called before the first frame update
    void awake()
    {
        if(heart==null)
        {
            Debug.LogError("Heart가 없음");
        }
        else
        {
            heart.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // trigger Beams active state
        if(this.photonView.IsMine)
        {
            ProcessInputs ();
        }
        //과제2: 개인한테만 하트가 뜨게 해두고 이 것 동기화하게 해보기
        //먼저 ProcessInputs의 조건을 this.photonView.IsMine으로 변경해야 함.

        if (heart != null && show != heart.activeInHierarchy)
        {
            heart.SetActive(show);
        }
    }

    void ProcessInputs()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            if(!show)
            {
                show=true;
            }
        }
        if(Input.GetKeyUp(KeyCode.H))
        {
            if(show)
            {
                show=false;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(show);
        }
        else
        {
            this.show = (bool)stream.ReceiveNext();
        }
    }

    //두번째로는 IPunObservable을 상속받고 OnPhotonSerializeView에서 show를 전송해야 한다.
    //세 번째로는 인스펙터에서 ObservedComponent에 ShowHeart를 추가해야 한다.
}
