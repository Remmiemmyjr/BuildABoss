using UnityEngine;

public class SceneEntryPoint : MonoBehaviour
{
    public RoomDoorTypes entryType;
    [SerializeField] private AudioClip sceneMusic; // TODO: this is temp and sucks ass

    private void Start()
    {
        AudioManager.Instance.StopSong();
        AudioManager.Instance.PlaySong(sceneMusic);
    }
}
