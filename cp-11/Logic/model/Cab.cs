using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cp_11.Logic.model
{
    public abstract class Cab
    {
        public int Number { get; set; }
        public bool forWomen { get; set; }
        abstract public int CostIncrease();
        abstract public string CabName { get;  }
        
    }
   
   public   enum CabType
    {
        sleeping = 0,
        coupe = 1,
        firstClass = 2,
        secondClass = 3,
        platskart = 4,
        sleepingForWomen = 5,
        coupeForWomen = 6,
        firstClassForWomen = 7,
        secondClassForWomen = 8,
        platskartForWomen = 9
    };

    public class Sleeping : Cab
    {
        
        CabType type;
        public override string  CabName
        {
            get => Number.ToString() + " " + type.ToString();
        }
        public override int CostIncrease()
        {
            return 500;
        }
       
        public Sleeping(int num, bool isW)
        {
            Number = num;
            forWomen = isW;
            if (isW) type = CabType.sleepingForWomen;
            else type = CabType.sleeping;

        }
        public Sleeping()
        {
            Number = -1;

        }

    }

    public class Coupe : Cab
    {
        
        CabType type = CabType.coupe;
        public override int CostIncrease()
        {
            return 350;
        }
        public override string CabName
        {
            get => Number.ToString() + " " + type.ToString();
        }
        public Coupe(int num, bool isW)
        {
            Number = num;
            forWomen = isW;
            if (isW) type = CabType.coupeForWomen;
            else type = CabType.coupe;
        }
        public Coupe()
        {
            Number = -1;

        }
    }

    public class FirstClass : Cab
    {
     
        CabType type = CabType.firstClass;
        public override int CostIncrease()
        {
            return 750;
        }
        public override string CabName
        {
            get => Number.ToString() + " " + type.ToString();
        }
        public FirstClass(int num, bool isW)
        {
            Number = num;
            forWomen = isW;
            if (isW) type = CabType.firstClassForWomen;
            else type = CabType.firstClass;
        }
        public FirstClass()
        {
            Number = -1;

        }
    }

    public class SecondClass : Cab
    {
       
        CabType type = CabType.secondClass;
        public override int CostIncrease()
        {
            return 375;
        }
        public override string CabName
        {
            get => Number.ToString() + " " + type.ToString();
        }
        public SecondClass(int num, bool isW)
        {
            Number = num;
            forWomen = isW;
            if (isW) type = CabType.secondClassForWomen;
            else type = CabType.secondClass;
        }
        public SecondClass()
        {
            Number = -1;

        }
    }

    public class Platskart : Cab
    {
       
        CabType type = CabType.platskart;
        public override int CostIncrease()
        {
            return 100;
        }
        public override string CabName
        {
            get => Number.ToString() + " " + type.ToString();
        }
        public Platskart(int num, bool isW)
        {
            Number = num;
            forWomen = isW;
            if (isW) type = CabType.platskartForWomen;
            else type = CabType.platskart;
        }
        public Platskart()
        {
            Number = -1;

        }
    }

}
