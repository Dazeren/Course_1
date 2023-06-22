using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Course_1
{
    class PortSystem
    {
        public List<BaseShip> ShipList { get => shipList; private set => shipList = value; }
        
        private List<BaseShip> shipList = new List<BaseShip>();

        public PortSystem() { }

        public void AddShip(BaseShip baseShip) => shipList.Add(baseShip);
        
        public void RemoveShip(BaseShip baseShip) => shipList.Remove(baseShip);

        public BaseShip FindShipByName(string name) => shipList.Where(x => x.Name == name).FirstOrDefault();

        public string GetAllShipsInfo()
        {
            StringBuilder result = new StringBuilder();

            foreach (var ship in shipList)
            {
                result.Append($"{ship.Info()}\n");
            }

            return result.ToString();
        }

    }
}
