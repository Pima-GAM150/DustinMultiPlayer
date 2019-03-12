using Photon.Pun;
using TMPro;

public class Player : MonoBehaviourPun, IPunObservable, IPunInstantiateMagicCallback, IDamageable
{
	public int Health = 100;

	public int Score = 0;

	public TextMeshProUGUI ScoreText;

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
		stream.Serialize(ref Score);
	}

	public void TakeDamage()
	{
		Health -= 10;
	}

	public void OnPlayerKilled()
	{
		print("player killed");
		Score++;
	}

	private void OnGUI()
	{
		ScoreText.text = "Players Killed : " + Score;
	}
}
