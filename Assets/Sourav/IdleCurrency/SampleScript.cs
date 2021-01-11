using UnityEngine;

namespace Sourav.Idle
{
    public class SampleScript : MonoBehaviour
    {
        public IdleCurrency value1;
        public IdleCurrency value2;

        void Start()
        {

            //IdleCurrency a = new IdleCurrency(8, 22);
            //IdleCurrency b = new IdleCurrency(5, 20);
            //IdleCurrency c = b * a;
            //Debug.Log(c.Value + "   " + c.Exp);

            //Debug.Log(c.ToShortString());

        }


        public void Add() {
            IdleCurrency newC = value1 + value2;
            Debug.Log("Result --> " + newC.ToShortString()+ "    SaveAbleString --> " + newC.GetStringForSave());
        }

        public void Subtract()
        {
            IdleCurrency newC = value1 - value2;
            Debug.Log("Result --> " + newC.ToShortString() + "    SaveAbleString --> " + newC.GetStringForSave());

        }

        public void Multiply()
        {
            IdleCurrency newC = value1 * value2;
            Debug.Log("Result --> " + newC.ToShortString()+ "    SaveAbleString --> " + newC.GetStringForSave());
        }
        public void Divide()
        {
            IdleCurrency newC = value1 / value2;
            Debug.Log("Result --> " + newC.ToShortString()+ "    SaveAbleString --> " + newC.GetStringForSave());
        }


    }
}
