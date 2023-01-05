using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "";
            Console.WriteLine("nhap ten:");
            name = Console.ReadLine();
            List<string> nameArr = new List<string>();
            int startIndex = 0;
            for (int i = 1; i < name.Length; i++)
            {
                if (Char.IsUpper(name[i]))
                {
                    nameArr.Add(name.Substring(startIndex, i - startIndex));
                    startIndex = i;
                }
            }
            nameArr.Add(name.Substring(startIndex, name.Length - startIndex));
            string fullName = "";
            foreach (string str in nameArr)
            {
                fullName += str;
            }
            Console.WriteLine(fullName);
            Console.ReadKey();
        }
    }
}
