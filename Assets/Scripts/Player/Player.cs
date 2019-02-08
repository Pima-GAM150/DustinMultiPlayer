using Photon.Pun;

public class Player : MonoBehaviourPun, IPunInstantiateMagicCallback
{
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        NetworkedObjects.Instance.AddPlayer(this.photonView);
    }
}
