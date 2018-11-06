using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AbpDemo.Application.Test
{
    public class TestService : ITestService
    {
        List<int> demoList = new List<int>();
        public List<int> GetTestMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                demoList.Add(i);
            }
            return demoList;
            //Console.WriteLine("Hello Java");
        }


        public string GetAll()
        {
            return ("Success");
        }


        public string GetById(int id)
        {
            return ("Success" + id);
        }
    }
}
