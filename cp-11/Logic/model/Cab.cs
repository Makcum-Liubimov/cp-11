using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cp_11.Logic.model
{
    public abstract class Cab
    {
        abstract public int CostIncrease();
    }

    enum CabType
    {
        sleeping = 0,
        coupe = 1,
        firstClass = 2,
        secondClass = 3,
        platskart = 4,
        sleepingW = 5,
        coupeW = 6,
        firstClassW = 7,
        secondClassW = 8,
        platskartW = 9
    };

    public class Sleeping : Cab
    {
        int Number { get; set; }
        bool forWomen { get; set; }
        CabType type = CabType.sleeping;
        public override int CostIncrease()
        {
            return 500;
        }
        public Sleeping(int num, bool isW)
        {
            Number = num;
            forWomen = isW;
        }
        public Sleeping()
        {
            Number = -1;

        }

    }

    public class Coupe : Cab
    {
        int Number { get; set; }
        bool forWomen { get; set; }
        CabType type = CabType.coupe;
        public override int CostIncrease()
        {
            return 350;
        }
        public Coupe(int num, bool isW)
        {
            Number = num;
            forWomen = isW;
        }
        public Coupe()
        {
            Number = -1;

        }
    }

    public class FirstClass : Cab
    {
        int Number { get; set; }
        bool forWomen { get; set; }
        CabType type = CabType.firstClass;
        public override int CostIncrease()
        {
            return 750;
        }
        public FirstClass(int num, bool isW)
        {
            Number = num;
            forWomen = isW;
        }
        public FirstClass()
        {
            Number = -1;

        }
    }

    public class SecondClass : Cab
    {
        int Number { get; set; }
        bool forWomen { get; set; }
        CabType type = CabType.secondClass;
        public override int CostIncrease()
        {
            return 375;
        }
        public SecondClass(int num, bool isW)
        {
            Number = num;
            forWomen = isW;
        }
        public SecondClass()
        {
            Number = -1;

        }
    }

    public class Platskart : Cab
    {
        int Number { get; set; }
        bool forWomen { get; set; }
        CabType type = CabType.platskart;
        public override int CostIncrease()
        {
            return 100;
        }
        public Platskart(int num, bool isW)
        {
            Number = num;
            forWomen = isW;
        }
        public Platskart()
        {
            Number = -1;

        }
    }

}
