using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataImporter.Core.Tests
{
    public sealed class AssertEx
    {
        public static void NoExceptionThrown<T>(Action a) where T : Exception
        {
            try
            {
                a();
            }
            catch (T)
            {
                Assert.Fail("Expected no {0} to be thrown", typeof(T).Name);
            }
        }
    }
}
