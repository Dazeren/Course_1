using System;

namespace Course_1
{
    public class CrewMember
    {
        #region Properties
        public string FullName { get => fullName; private set => fullName = value; }
        public int Age { get => age; private set => age = value; }
        public double Height { get => height; private set => height = value; }
        public string ShipName { get => shipName; private set => shipName = value; }
        #endregion

        private string fullName;
        private int age;
        private double height;
        private string shipName;


        public CrewMember(string fullName = "Unknown", int age = 0, double height = 0)
        {
            if (height < 0 || age < 0)
            {
                throw new ArgumentException("Wrong Human Data");
            }

            this.fullName = fullName;
            this.age = age;
            this.height = height;
        }


        public void AssignShip(string shipName)
        {
            this.shipName = shipName;
        }

        public string Info()
        {
            return $"Full name: {fullName}; Age: {age}; height: {height} cm; Ship name: {shipName}";
        }
    }
}