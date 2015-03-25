using System.Collections.Generic;
using Assets.Scripts.lib.Damage;

namespace Assets.Scripts.lib
{
    public interface IDestructibleObject
    {
        void Damage(DamageType type, int quantity);
    }

    abstract class DestructibleObject : IDestructibleObject
    {
        public int Life { get; set; }
        public IDictionary<ResistanceType, Resistance> Resistances { get; set; }

        protected virtual double GetDamageReduction(DamageType type)
        {
            var dr = 0;
            foreach (var resistance in Resistances)
            {
                if (resistance.Value.DamageType == type)
                    dr += resistance.Value.Quantity;
            }

            return 3*(.25 - ((75/(dr + 150))^2));
        }

        public void Damage(DamageType type, int quantity)
        {
            Life = Life - (int)(quantity*GetDamageReduction(type));
        }
    }


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
