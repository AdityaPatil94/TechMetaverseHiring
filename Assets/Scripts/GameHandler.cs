
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using ReadyPlayerMe;
using StarterAssets;

public class GameHandler : MonoBehaviourPunCallbacks
{

	#region Public Fields

	static public GameHandler Instance;
	//public AvatarHandler avatarHandler;
	public GameObject FeedBackCanvas;
	#endregion

	#region Private Fields

	private GameObject instance;
	private GameObject WhiteBoard;
	[Tooltip("The prefab to use for representing the player")]
	[SerializeField]
	private GameObject playerPrefab;

	[SerializeField]
	private GameObject Player;
	[SerializeField]
	private GameObject JoinButton;
	[SerializeField]
	private ThirdPersonController controller;
	[SerializeField]
	private Vector3 SpawnPosition;
	private bool isWhiteBoardActive;
	#endregion

	#region MonoBehaviour CallBacks

	/// <summary>
	/// MonoBehaviour method called on GameObject by Unity during initialization phase.
	/// </summary>
	void Start()
	{
		Instance = this;
		isWhiteBoardActive = true;
		if(JoinButton != null)
        {
			JoinButton.SetActive(false);
			if (PhotonNetwork.IsMasterClient)
			{
				JoinButton.SetActive(true);
			}
		}
		var avatarLoader = new AvatarLoader();
		avatarLoader.OnCompleted += AvatarLoadingCompleted;
		// in case we started this demo with the wrong scene being active, simply load the menu scene
		if (!PhotonNetwork.IsConnected)
		{
			//SceneManager.LoadScene("PunBasics-Launcher");
			return;
		}

		if (playerPrefab == null)
		{ // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.

			Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
		}
		else
		{


			if (PlayerManager.LocalPlayerInstance == null)
			{
				Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
				
				// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
				Vector3 Temp = SpawnPosition + new Vector3(Random.Range(-2,2), 0f, Random.Range(-8, 2));
				Player = PhotonNetwork.Instantiate(this.playerPrefab.name, Temp, Quaternion.identity, 0);
				WhiteBoard = PhotonNetwork.Instantiate("Whiteboard", Vector3.zero, Quaternion.identity, 0);
				WhiteBoard.SetActive(false);
			}
			else
			{

				Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
			}
		}
		
	}

	public void ToggleWhiteBoard()
	{
		isWhiteBoardActive = !isWhiteBoardActive;
		if (isWhiteBoardActive)
		{
			WhiteBoard.SetActive(false);
		}
		else
        {
			WhiteBoard.SetActive(true);
        }
		
	}
	public void AvatarLoadingCompleted(object sender, CompletionEventArgs args)
    {
		//Debug.Log();
	}
	/// <summary>
	/// MonoBehaviour method called on GameObject by Unity on every frame.
	/// </summary>
	void Update()
	{
		// "back" button of phone equals "Escape". quit app if that's pressed
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//QuitApplication();
			GetFeedbackFromUser();
		}
	}

	#endregion

	#region Photon Callbacks

	/// <summary>
	/// Called when a Photon Player got connected. We need to then load a bigger scene.
	/// </summary>
	/// <param name="other">Other.</param>
	public override void OnPlayerEnteredRoom(Player other)
	{
		Debug.Log("OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting

		if (PhotonNetwork.IsMasterClient)
		{
			Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

			LoadArena();
		}
	}

	/// <summary>
	/// Called when a Photon Player got disconnected. We need to load a smaller scene.
	/// </summary>
	/// <param name="other">Other.</param>
	public override void OnPlayerLeftRoom(Player other)
	{
		Debug.Log("OnPlayerLeftRoom() " + other.NickName); // seen when other disconnects

		if (PhotonNetwork.IsMasterClient)
		{
			Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

			LoadArena();
		}
	}

	/// <summary>
	/// Called when the local player left the room. We need to load the launcher scene.
	/// </summary>
	public override void OnLeftRoom()
	{
		SceneManager.LoadScene("PunBasics-Launcher");
	}

	#endregion

	#region Public Methods

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}

	public void StopPlayerInput()
    {
		//controller = Player.GetComponentInChildren<ThirdPersonController>();
		//controller.CanMove(false);
		Debug.Log("Typing...");
    }

	public void ChangeRoom()
	{
		SceneManager.LoadScene("InterviewRoom");
	}

	public void QuitApplication()
	{
		Application.Quit();

	}

	public void GetFeedbackFromUser()
	{
		FeedBackCanvas.SetActive(true);

	}

	#endregion

	#region Private Methods

	void LoadArena()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
		}

		Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);

		PhotonNetwork.LoadLevel("PunBasics-Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
	}

	#endregion

}
