
namespace Assets.Scripts.lib.Damage
{
    public class Resistance
    {
        public DamageType DamageType { get; set; }
        public ResistanceType ResistanceType { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
    }

    public enum ResistanceType
    {
        Shield,
        Armor,
        ForceField
    }
}
