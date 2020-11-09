using System;
using DynamicList.DynamicListClass;

namespace DynamicList
{
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
        }

        static void ShowList(DynamicList<int> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
    }
}
