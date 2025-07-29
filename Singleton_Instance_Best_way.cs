using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace async
{
    public class Singleton_Instance_Best_way
    {

        public enum Class1Type{
            LocalDataBase,
            RemoteDataBase
        }
        private static Dictionary<Class1Type, Singleton_Instance_Best_way?> class1Instances = new Dictionary<Class1Type, Singleton_Instance_Best_way?>();
        //lazy initaliztion / multi-type - singleton instance pattern
        public static Singleton_Instance_Best_way GetSignelInstance(Class1Type type)
        {
            if (!class1Instances.ContainsKey(type))
            {
                class1Instances[type] = new Singleton_Instance_Best_way();
            }
            return class1Instances[type]!;
        }

    }
}
