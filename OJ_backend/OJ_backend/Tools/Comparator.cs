using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace OJ_backend.Tools
{

    public class Comparator
    {
        [DllImport("../x64/Debug/COMPARATOR.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool Compare([MarshalAs(UnmanagedType.LPStr)]string filePath1, [MarshalAs(UnmanagedType.LPStr)]string filePath2);



        //test
        [DllImport("../x64/Debug/COMPARATOR.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool TestString([MarshalAs(UnmanagedType.LPStr)]string s);

        [DllImport("../x64/Debug/COMPARATOR.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static int TestInt(int s);
    }
}
