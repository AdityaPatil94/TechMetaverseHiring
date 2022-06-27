using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReadyPlayerMe;
public class AvatarGetter : MonoBehaviour
{
	public static AvatarGetter Instance;
	public string Url;
	public GameObject Player;
	public GameObject Cube;
	public Avatar PlayerAvatar;
	public InputField AvatarLink;
	public string playerName;

	public AvatarLoader avatarLoader;
 
	private void Awake()
    {
        if(Instance == null)
        {
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
        {
			Destroy(gameObject);
        }
    }

    public void SetAvatarLink()
	{
		string playerName = AvatarLink.text;

		if (!playerName.Equals(""))
		{
			Url = playerName;
			//LoadAvatar();
		}
	}

	public void LoadAvatar()
	{
		//Debug.LogError("Load Avatar called");
		avatarLoader = new AvatarLoader();
		avatarLoader.OnCompleted += AvatarLoadingCompleted;
		avatarLoader.OnFailed += AvatarLoadingFailed;
		avatarLoader.OnProgressChanged += AvatarLoadingProgressChanged;
		avatarLoader.LoadAvatar(Url);
	}

	private void AvatarLoadingCompleted(object sender, CompletionEventArgs args)
	{

		Debug.LogError($"{args.Avatar.name} is imported!");
		Player = args.Avatar.gameObject;
		Player.name = "player" + Random.Range(0, 9);
		playerName = Player.name;
		Player.SetActive(false);
		Player.transform.SetParent(this.gameObject.transform);
		
	}

	private void AvatarLoadingFailed(object sender, FailureEventArgs args)
	{
		//Debug.LogError($"Failed with {args.Type}: {args.Message}");
	}
	private void AvatarLoadingProgressChanged(object sender, ProgressChangeEventArgs args)
	{
		Debug.LogError($"Progress: {args.Progress * 100}%");
	}

}
