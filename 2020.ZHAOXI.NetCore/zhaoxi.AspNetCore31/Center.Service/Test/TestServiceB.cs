using Center.Interface;
using System;

namespace Center.Service
{
    public class TestServiceB : ITestServiceB
    {
        public TestServiceB(ITestServiceA iTestServiceA)
        {
            Console.WriteLine($"{this.GetType().Name}被构造。。。");
        }


        public void Show()
        {
            Console.WriteLine($"This is TestServiceB B123456");
        }
    }
}
