using System;
using DynamicList.DynamicListClass;
using System.Reflection;
using System.Linq;

namespace DynamicList
{
    public class Class1 
    { }

    [ExportClassAttribute.ExportClass]
    public class Class2 { }

    [ExportClassAttribute.ExportClass]
    class Class3 { }

    class Program
    {
        static void Main(string[] args)
        {
            DynamicListClass.DynamicList<int> dynamicList = new DynamicList<int>();
            for (int i = 0; i < 7; i++)
            {
                dynamicList.Add(i);
            }
            ShowList(dynamicList);
            dynamicList.Remove(3);
            ShowList(dynamicList);
            dynamicList.RemoveAt(3);
            ShowList(dynamicList);
            dynamicList.RemoveAt(4);
            ShowList(dynamicList);
            Console.WriteLine(dynamicList[3]);
            dynamicList.Clear();
            ShowList(dynamicList);
            GetAssemblyExportClass();
        }

        static void ShowList(DynamicList<int> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        static void GetAssemblyExportClass()
        {
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            foreach (var type in assembly.GetTypes().Where(t => t.IsPublic && t.IsDefined(typeof(ExportClassAttribute.ExportClassAttribute))))
            {
                Console.WriteLine(type.FullName);
            }
        }

    }
}
