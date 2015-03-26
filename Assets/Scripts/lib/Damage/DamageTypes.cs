
namespace Assets.Scripts.lib.Damage
{
    public enum DamageType
    {
        //TODO: need to attach numbers that represent actual co-efficients
        //Exotic Types
        Fire = 0,
        Cold = 10,
        Acid = 20,
        Electric = 30,
        Proton = 40,
        Radiation = 50,
        Toxic = 60,

        //Collision
        Kinetic = 1,

        //weapon energies
        Photon,
        Disrupter,
        Plasma,
        Tetryon,
        Polaron,
        AntiProton

    }
}
