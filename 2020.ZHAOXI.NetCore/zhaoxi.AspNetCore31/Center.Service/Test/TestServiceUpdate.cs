﻿using Center.Interface;
using System;

namespace Center.Service
{
    public class TestServiceUpdate : ITestServiceA
    {
        public TestServiceUpdate()
        {
            Console.WriteLine($"{this.GetType().Name} --V2被构造。。。");
        }

        public void Show()
        {
            Console.WriteLine("A123456  V2");
        }
    }
}
