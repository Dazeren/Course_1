using System;

namespace Course_1
{
    public class PassengerShip : BaseShip
    {
        #region Properties
        public int PassengerAmount { get => passengerAmount; 
            set
            {
                if (value < 0) { throw new ArgumentException("Wrong Ship Data"); }
                passengerAmount = value;
            }
        }
        public int RawboatAmount { get => rawboatAmount; 
            set
            {
                if (value < 0) { throw new ArgumentException("Wrong Ship Data"); }
                rawboatAmount = value;
            }
        }
        public int RawboatCapacity { get => rawboatCapacity; private set => rawboatCapacity = value; }
        #endregion

        private int passengerAmount;
        private int rawboatAmount;
        private int rawboatCapacity;

        public PassengerShip(string name = "Unknown", string registrationPort = "Unsigned",
                             int enginePower = 0, int draught = 0,
                             int passangerAmount = 0, int rawboatAmount = 0, int rawboatCapacity = 0) 
                             : base(name, registrationPort, enginePower, draught)
        {
            if (passangerAmount < 0 || rawboatCapacity < 0 || rawboatAmount < 0)
            {
                throw new ArgumentException("Wrong Passanger Ship Data");
            }

            this.passengerAmount = passangerAmount;
            this.rawboatAmount = rawboatAmount;
            this.rawboatCapacity = rawboatCapacity;
        }


        public bool HasEnoughtRawboats() => Crew.Count + passengerAmount <= rawboatAmount * rawboatCapacity;

        public bool TryIncreaseRawboatAmount()
        {
            if (HasEnoughtRawboats()) return false;

            int humansCount = Crew.Count + passengerAmount;
            int rawboatSeats = rawboatAmount * rawboatCapacity;
            int neededSeats = humansCount - rawboatSeats;
            float neededRawboards = neededSeats / rawboatCapacity;
            rawboatAmount += (int)Math.Ceiling(neededRawboards);
            return true;
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
                case "PassengerAmount":
                    passengerAmount = int.Parse(newValue);
                    break;
                case "RawboatAmount":
                    rawboatAmount = int.Parse(newValue);
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        public override string Info()
        {
            return base.Info() + $" Passenger amount: {passengerAmount}, Rawboat amount: {rawboatAmount}, Rawboat capacity: {rawboatCapacity}";
        }
    }
}
