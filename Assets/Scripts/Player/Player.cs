using Photon.Pun;
using TMPro;

public class Player : MonoBehaviourPun, IPunObservable, IPunInstantiateMagicCallback, IDamageable
{
	public int Health = 100;

	public int Score;

	public TextMeshProUGUI ScoreText;

	public static Player Instance;

	private void Start()
	{
		Score = 0;
	}

	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		if (photonView.IsMine)
		{
			Instance = this;
		}

		NetworkedObjects.Instance.AddPlayer(this.photonView);
	}

	private void OnDestroy()
	{
		NetworkedObjects.Instance.RemovePlayer(this.photonView);
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
		Score++;
	}

	private void OnGUI()
	{
		ScoreText.text = "Players Killed : " + Score;
	}
}
