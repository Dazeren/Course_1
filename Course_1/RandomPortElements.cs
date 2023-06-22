using System;

namespace Course_1
{
    class RandomPortElements
    {
        public static string GeneratePersonName()
        {
            string fullName = "";

            string[] surnames = { "Gorbunov", "Melnik", "Shevchenko", "Filatov", "Grun", "Smirnov", "Novikov", "Petrov" };
            string[] names = { "Ivan", "Oleg", "Vlad", "Kostya", "Taras", "Vitalya", "Fedya", "Peter" };

            fullName = $"{surnames.RandomElement()} {names.RandomElement()}";

            return fullName;
        }

        public static string GenerateShipName()
        {
            string[] names = { "Nadezhda", "Bismark", "Waterbender", "Nautilus", "Titanik", "IceCrusher", "Iceberg", "SSFriendship", "Albatros",
                               "OwlGlory", "LastTrip", "Pathfinder", "Arstozka", "Ortstan", "Fictory", "MaXim", "Submarine", "Waterfall",
                               "Mivva", "Rudsberg", "Kadilom", "Berta", "Derge", "Edge", "NewHorizon"};

            return names.RandomElement();
        }

        public static string GenerateShipPort()
        {
            string[] names = { "Odessa", "Shanghai ", "Singapoure", "Rotterdam", "Manila" };

            return names.RandomElement();
        }

        public static CrewMember GenerateRandomCrewMember()
        {
            Random rng = new Random();
            int age = rng.Next(18, 60);
            double height = rng.Next(100, 210);
            return new CrewMember(GeneratePersonName(), age, height);
        }

        public static BaseShip GenerateShip(string shipType)
        {
            Random rng = new Random();
            string name = RandomPortElements.GenerateShipName();
            string registrationPort = RandomPortElements.GenerateShipPort();
            int enginePower = rng.Next(100, 800);
            int draught = rng.Next(10, 100);

            switch (shipType)
            {
                case "Base":
                    return new BaseShip(name, registrationPort, enginePower, draught);
                case "Passenger":
                    int passangerAmount = rng.Next(10, 100);
                    int rawboatAmount = rng.Next(1, 10);
                    int rawboatCapacity = rng.Next(2, 6);
                    return new PassengerShip(name, registrationPort, enginePower, draught,
                                                                    passangerAmount, rawboatAmount, rawboatCapacity);
                case "Cargo":
                    int carryingCapacity = rng.Next(1000, 9000);
                    return new CargoShip(name, registrationPort, enginePower, draught, carryingCapacity);
                default:
                    throw new ArgumentException("Ship not found!");
            }
        }
    }
}
