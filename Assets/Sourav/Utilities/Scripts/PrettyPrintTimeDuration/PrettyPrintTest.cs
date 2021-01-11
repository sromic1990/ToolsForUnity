using Sirenix.OdinInspector;
using Sourav.Engine.Core.GameElementRelated;

namespace Sourav.Utilities.Scripts.PrettyPrintTimeDuration
{
    public class PrettyPrintTest : GameElement
    {
        [Button]
        public void PrettyPrint(int seconds, TypeOfPrint type)
        {
            PrettyPrintFromSeconds.GetPrettyString(seconds, type);
        }
    }
}
