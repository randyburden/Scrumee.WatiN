using System.Threading;
using WatiN.Core;

namespace Scrumee.Tests.WatiN.Helpers
{
    /// <summary>
    /// Ensures that we are reusing the same IE instance
    /// for all tests
    /// </summary>
    public class IEStaticInstanceHelper
    {
        private IE _ie;
        private int _ieThread;
        private string _ieHwnd;

        public IE IE
        {
            get
            {
                var currentThreadId = GetCurrentThreadId();
                if ( currentThreadId != _ieThread )
                {
                    _ie = Browser.AttachTo<IE>( Find.By( "hwnd", _ieHwnd ) );
                    _ieThread = currentThreadId;
                }
                return _ie;
            }
            set
            {
                _ie = value;
                _ieHwnd = _ie.hWnd.ToString();
                _ieThread = GetCurrentThreadId();
            }
        }

        private static int GetCurrentThreadId()
        {
            return Thread.CurrentThread.GetHashCode();
        }
    }
}
