using UnityEngine;

public interface IGunController
{
    void Fire();
    void FireMultiple(int shots);
    void FireInDirection(Vector3 direction);
    bool Reload(int ammo);
}