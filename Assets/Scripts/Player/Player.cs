using Photon.Pun;

public class Player : MonoBehaviourPun, IPunObservable, IPunInstantiateMagicCallback, IDamageable
{
    public float Health = 100;

    public static Player Instance;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        NetworkedObjects.Instance.AddPlayer(this.photonView);

        if(photonView.IsMine)
        {
            Instance = this;
        }
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
