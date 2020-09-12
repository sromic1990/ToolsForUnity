using UnityEngine;

namespace Sourav.IdleGameEngine.IdleCurrency
{
    public class SampleScript : MonoBehaviour
    {
        public IdleCurrency.IdleCurrency value1;
        public IdleCurrency.IdleCurrency value2;

        void Start()
        {

            //IdleCurrency a = new IdleCurrency(8, 22);
            //IdleCurrency b = new IdleCurrency(5, 20);
            //IdleCurrency c = b * a;
            //Debug.Log(c.Value + "   " + c.Exp);

            //Debug.Log(c.ToShortString());

        }


        public void Add() {
            IdleCurrency.IdleCurrency newC = value1 + value2;
            Debug.Log("Result --> " + newC.ToShortString()+ "    SaveAbleString --> " + newC.GetStringForSave());
        }

        public void Subtract()
        {
            IdleCurrency.IdleCurrency newC = value1 - value2;
            Debug.Log("Result --> " + newC.ToShortString() + "    SaveAbleString --> " + newC.GetStringForSave());

        }

        public void Multiply()
        {
            IdleCurrency.IdleCurrency newC = value1 * value2;
            Debug.Log("Result --> " + newC.ToShortString()+ "    SaveAbleString --> " + newC.GetStringForSave());
        }
        public void Divide()
        {
            IdleCurrency.IdleCurrency newC = value1 / value2;
            Debug.Log("Result --> " + newC.ToShortString()+ "    SaveAbleString --> " + newC.GetStringForSave());
        }


    }
}
