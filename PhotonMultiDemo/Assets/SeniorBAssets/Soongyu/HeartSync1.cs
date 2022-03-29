using UnityEngine;
using Photon.Pun;

public class HeartSync1 : MonoBehaviourPun, IPunObservable
{
    //OnPhotonSerializeView를 활용해 동기화를 진행해보세요!
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
