using UnityEngine;
public class SoundManager : MonoBehaviour {
    public static SoundManager instance = null;
    [SerializeField] [Range(0, 1)] float soundFxVolume;
    [SerializeField] [Range(0, 1)] float musicVolume;
    public float SoundFxVolume
    {
        get { return soundFxVolume; }
        set { soundFxVolume = value; }
    }
    public float MusicVolume => musicVolume;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
