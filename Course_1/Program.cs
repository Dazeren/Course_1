using System;
using static System.Console;

namespace Course_1
{
    class Program
    {
        #region Exeptions
        public static ArgumentException LogError() => new ArgumentException("Wrong Prompt");
        public static ArgumentException LogArrayWrongSize() => new ArgumentException("Wrong Array Size");
        public static ArgumentException LogShipNotFound() => new ArgumentException("Ship Not Found");

        #endregion

        public static void AddRandomShip(PortSystem portSystem, int count, string shipType)
        {
            Random rng = new Random();
            for (int i = 0; i < count; i++)
            {
                portSystem.AddShip(RandomPortElements.GenerateShip(shipType));
            }
        }

        public static void AssignRandomCrewToShip(BaseShip ship, int count)
        {
            for (int i = 0; i < count; i++)
            {
                ship.AddCrewMember(RandomPortElements.GenerateRandomCrewMember());
            }
        }

        public static void StartProgramCycle(PortSystem portSystem)
        {
            string userInputCommand = ReadLine();

            string[] commandParams = userInputCommand.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                switch (commandParams[0])
                {
                    case "Help":
                        #region Help
                        WriteLine("Help - show all available comands\n" +
                                  "AddRandomShip {Ship Type} {Ship Count} - adding random ships of selected type\n" +
                                  "AddShip {Ship Type} {Ship Parametrs} - manually add ship, parametrs vary of Ship Type\n" +
                                  "ModifyShip {Ship Name} {New Value} - modify ship parametr by ship name and type\n" +
                                  "RemoveShip {Ship Name} - removes ship by name\n" +
                                  "TryAddRawboats {Ship Name} - increases rawboats amount in passanger ship\n" +
                                  "Info {Type} - shows info about: \"AllShips\", \"Crew {Ship Name}\", \"Ship {Ship Name}\"\n" +
                                  "Available Ship Types: Base, Passenger, Cargo\n");
                        break;
                        #endregion
                    case "AddRandomShip":
                        #region AddRandomShip {ShipType} {ShipCount}
                        if (!commandParams.IsArrayMinSize(3)) throw LogArrayWrongSize();
                        if (!int.TryParse(commandParams[2], out int randomShipsCount)) throw LogError();

                        string randomShipType = commandParams[1];

                        AddRandomShip(portSystem, randomShipsCount, randomShipType);
                        WriteLine($"{randomShipsCount} random ships added to port\n");
                        break;
                        #endregion
                    case "AddShip":
                        #region AddShip {ShipType} {ShipName} {NewValues}...
                        if (!commandParams.IsArrayMinSize(6)) throw LogArrayWrongSize();
                        if (string.IsNullOrEmpty(commandParams[1])) throw LogError();

                        if (!(int.TryParse(commandParams[4], out int enginePower) &&
                            int.TryParse(commandParams[5], out int draught))) throw LogError();

                        string name = commandParams[2];
                        string registrationPort = commandParams[3];

                        switch (commandParams[1])
                        {
                            case "Base":
                                BaseShip baseShip = new BaseShip(name, registrationPort, enginePower, draught);
                                portSystem.AddShip(baseShip);
                                break;

                            case "Passenger":
                                if (!commandParams.IsArrayMinSize(9)) throw LogArrayWrongSize();
                                if (!(int.TryParse(commandParams[6], out int passengerAmount) &&
                                    int.TryParse(commandParams[7], out int rawboatAmount) &&
                                    int.TryParse(commandParams[8], out int rawboatCapacity))) throw LogError();

                                PassengerShip passengerShip = new PassengerShip(name, registrationPort, enginePower, draught,
                                                                                passengerAmount,rawboatAmount,rawboatCapacity);
                                portSystem.AddShip(passengerShip);
                                break;

                            case "Cargo":
                                if (!commandParams.IsArrayMinSize(7)) throw LogArrayWrongSize();
                                if (!int.TryParse(commandParams[6], out int carryingCapacity)) throw LogError();

                                CargoShip cargoShip = new CargoShip(name, registrationPort, enginePower, draught,carryingCapacity);

                                portSystem.AddShip(cargoShip);
                                break;
                            default:
                                throw LogError();
                        }

                        WriteLine("Ship successfully added\n");
                        break;
                        #endregion
                    case "ModifyShip":
                        #region ModifyShip {ShipName} {ParamName} {NewValue}
                        if (!commandParams.IsArrayMinSize(4)) throw LogArrayWrongSize();
                        if (string.IsNullOrEmpty(commandParams[1]) ||
                            string.IsNullOrEmpty(commandParams[2]))  throw LogError();

                        BaseShip baseShipModify = portSystem.FindShipByName(commandParams[1]);

                        if (!baseShipModify.TryModify(commandParams[2], commandParams[3])) throw LogError();

                        WriteLine("Ship successfully modified\n");
                        break;
                    #endregion
                    case "RemoveShip":
                        #region RemoveShip {ShipName}
                        if (string.IsNullOrEmpty(commandParams[1])) throw LogError();

                        BaseShip shipToRemove = portSystem.FindShipByName(commandParams[1]);
                        if (shipToRemove == null) throw LogShipNotFound();

                        portSystem.RemoveShip(shipToRemove);

                        WriteLine($"Ship {commandParams[1]} Removed");
                        break;
                        #endregion
                    case "Info":
                        #region Info {InfoType}
                        if (string.IsNullOrEmpty(commandParams[1])) throw LogError();
                        switch (commandParams[1])
                        {
                            case "AllShips":
                                WriteLine($"{portSystem.GetAllShipsInfo()}\n");
                                break;

                            case "Crew":
                                if (string.IsNullOrEmpty(commandParams[2])) throw LogError();
                                BaseShip shipCrew =  portSystem.FindShipByName(commandParams[2]);

                                if (shipCrew == null) throw LogShipNotFound();

                                WriteLine($"{shipCrew.CrewInfo()}\n");
                                break;

                            case "Ship":
                                if (string.IsNullOrEmpty(commandParams[2])) throw LogError();
                                BaseShip shipInfo = portSystem.FindShipByName(commandParams[2]);

                                if (shipInfo == null) throw LogShipNotFound();

                                WriteLine($"{shipInfo.Info()}\n");
                                break;
                        }
                        break;
                        #endregion
                    case "TryAddRawboats":
                        #region TryAddRawboats {ShipName}
                        if (string.IsNullOrEmpty(commandParams[1])) throw LogError();
                        PassengerShip passengerShipForRawboats = portSystem.FindShipByName(commandParams[1]) as PassengerShip;
                        if (passengerShipForRawboats == null) throw LogShipNotFound();

                        string tryIncreaseRawboatResult = passengerShipForRawboats.TryIncreaseRawboatAmount() ? "Increase successful" : "Increase not needed";
                        WriteLine($"{tryIncreaseRawboatResult}\n");
                        break;
                        #endregion
                    case "Exit":
                        return;
                    default:
                        throw LogError();
                }
            }
            catch (Exception ex)
            {
                WriteLine("An unexpected error occurred: " + ex.Message);
            }

            StartProgramCycle(portSystem);
        }

        static void Main(string[] args)
        {
            PortSystem portSystem = new PortSystem();
            WriteLine("Welcome to Port System Simulator \nType \"Help\" for available comand list\n");
            StartProgramCycle(portSystem);
        }
    }
}
