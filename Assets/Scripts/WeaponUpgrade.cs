[System.Serializable]
public class WeaponUpgrade
{
    public WeaponType weaponType;
    public int level;

    public WeaponUpgrade(WeaponType type, int lvl)
    {
        weaponType = type;
        level = lvl;
    }
}
public enum WeaponType { MachineGun, BasicRocket, HomingRocket, SuperRocket }