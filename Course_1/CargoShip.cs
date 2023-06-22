using System;

namespace Course_1
{
    public class CargoShip : BaseShip
    {
        #region Properties
        public int CarryingCapacity { get => carryingCapacity; 
            set
            {
                if (value < 0) { throw new ArgumentException("Wrong Ship Data"); }
                carryingCapacity = value;
            }
        }
        #endregion

        private int carryingCapacity;

        public CargoShip(string name = "Unknown", string registrationPort = "Unsigned",
                         int enginePower = 0, int draught = 0,
                         int carryingCapacity = 0) 
                         : base(name, registrationPort, enginePower, draught)
        {
            if (carryingCapacity < 0)
            {
                throw new ArgumentException("Wrong Cargo Ship Data");
            }

            this.carryingCapacity = carryingCapacity;
        }

        public override bool TryModify(string param, string newValue)
        {
            bool result = true;

            if (base.TryModify(param, newValue))
            {
                return result;
            }

            switch (param)
            {
                case "CarryingCapacity":
                    carryingCapacity = int.Parse(newValue);
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        public override string Info()
        {
            return base.Info() + $" Carring capacity: {carryingCapacity}";
        }
    }
}
