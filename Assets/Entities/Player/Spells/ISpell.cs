
using UnityEngine;

public interface ISpell
{
    public void Cast(Transform spawnLocation, LayerMask targetMask);
    public bool IsReady();
    public void SetMaxCooldown(float newCooldown);
}
