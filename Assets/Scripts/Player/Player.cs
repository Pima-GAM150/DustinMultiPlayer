using Photon.Pun;

public class Player : MonoBehaviourPun, IPunObservable, IPunInstantiateMagicCallback, IDamageable
{
    public float Health = 100;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        NetworkedObjects.Instance.AddPlayer(this.photonView);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        stream.Serialize(ref Health);
    }

    public void TakeDamage()
    {
        Health -= 10;
    }
}
