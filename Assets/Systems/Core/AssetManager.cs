using UnityEngine;

public class AssetManager : MonoBehaviour
{

    public static AssetManager instance;
    public GameObject fireball;
    public FlyingProjectileScriptable fireballStats;

    private void Start()
    {
        if(!instance)
            instance = this;
    }
}
