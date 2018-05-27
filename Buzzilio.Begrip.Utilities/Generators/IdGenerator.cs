using shortid;

namespace Buzzilio.Begrip.Utilities.Generators
{
    public class IdGenerator
    {
        public static string GetShortId()
        {
            return ShortId.Generate(true, false);
        }
    }
}
