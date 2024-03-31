using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задание_4_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OneD_Array<int> int_arr = new OneD_Array<int>();
            int_arr.AddValue(1488);
            int_arr.AddValue(69);

            int_arr.SortArray();

            int_arr.MaxVal();
            int_arr.MinVal();


            OneD_Array<bool> bool_arr = new OneD_Array<bool>();
            bool_arr.AddValue(true);
            bool_arr.AddValue(false);

            bool_arr.SortArray();

            bool_arr.MaxVal();
            bool_arr.MinVal();

        }
    }
}
