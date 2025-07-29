using mainNameSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace async
{
    public static class Exts
    {
        public static bool IsNotNullOrEmpty(this string? text) {
            if (text != null && text.Length > 0)
            {
                return true;
            }
            return false;
        }

        public static void IfNOtNullOrEmpty(this string? text, Action<string> onSuccess, Action onError) {
            if (text != null && text.Length > 0)
            {
                onSuccess(text);
            }
            else {
                onError();
            }
        }
    }
}
