using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseNetCoreClassProject1.Interfaces;

namespace BaseNetCoreClassProject1.Classes
{
    public class Worker
    {

        public static void CompareValue<T>(List<T> sender) where T : class, IBase
        {
            foreach (var item in sender)
            {
                Debug.WriteLine(item.Id);
            }
        }

    }

}
