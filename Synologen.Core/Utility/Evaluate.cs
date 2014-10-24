namespace Spinit.Wpc.Synologen.Core.Utility
{
    public static class Evaluate
    {
         public static bool AreNullOrEmpty(params object[] items)
         {
            foreach (var item in items)
            {
                if (item == null)
                {
                    continue;
                }

                if (item.GetType().FullName == "System.String")
                {
                    var stringValue = (string)item;
                    if (string.IsNullOrEmpty(stringValue))
                    {
                        continue;
                    }
                }

                return false;
            }

            return true;
        }
    }
}