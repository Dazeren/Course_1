using System;
using System.Collections.Generic;
using System.Text;

namespace Course_1
{
    public class BaseShip
    {
        #region Properties
        public string Name { get => name; set => name = value; }
        public string RegistrationPort { get => registrationPort; set => registrationPort = value; }
        public int EnginePower { get => enginePower; 
            set 
            {
                if(value < 0) { throw new ArgumentException("Wrong Ship Data"); }
                enginePower = value; 
            }
        }
        public int Draught { get => draught; 
            set
            {
                if (value < 0) { throw new ArgumentException("Wrong Ship Data"); }
                draught = value;
            }
        }
        public List<CrewMember> Crew { get => crew; private set => crew = value; }
        #endregion

        private string name;
        private string registrationPort;
        private int enginePower;
        private int draught;
        private List<CrewMember> crew = new List<CrewMember>();

        public BaseShip(string name = "Unknown", string registrationPort = "Unsigned",
                        int enginePower = 0, int draught = 0)
        {
            if (enginePower < 0 || draught < 0)
            {
                throw new ArgumentException("Wrong Ship Data");
            }

            this.name = name;
            this.registrationPort = registrationPort;
            this.enginePower = enginePower;
            this.draught = draught;
        }

        public void AddCrewMember(CrewMember human)
        {
            crew.Add(human);
            human.AssignShip(name);
        }

        public void AssignCrew(List<CrewMember> crew)
        {
            this.crew = crew;
            foreach (CrewMember crewMember in crew)
            {
                crewMember.AssignShip(name);
            }
        }

        public void RemoveCrewMember(CrewMember human)
        {
            crew.Remove(human);
        }

        public virtual bool TryModify(string param, string newValue)
        {
            bool result = true;
            switch (param)
            {
                case "Name":
                    name = newValue;
                    break;
                case "RegistrationPort":
                    registrationPort = newValue;
                    break;
                case "EnginePower":
                    enginePower = int.Parse(newValue);
                    break;
                case "Draught":
                    draught = int.Parse(newValue);
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        public string CrewInfo()
        {
            StringBuilder result = new StringBuilder();

            foreach(CrewMember crewMember in crew)
            {
                result.Append($"{crewMember.Info()}\n");
            }

            return result.ToString();
        }

        public virtual string Info()
        {
            return $"Name: {name}, Registration Port: {registrationPort}, EnginePower: {enginePower}, Draught: {draught}";
        }
    }
}