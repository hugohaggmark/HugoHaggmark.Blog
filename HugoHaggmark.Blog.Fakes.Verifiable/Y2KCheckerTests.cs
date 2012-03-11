using System;
using System.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HugoHaggmark.Blog.Fakes.Verifiable
{
    public static class Y2KChecker
    {
        public static void Check()
        {
            if (DateTime.Now == new DateTime(2000, 1, 1))
                throw new ApplicationException("y2kbug!");
        }
    }

    [TestClass]
    public class Y2KCheckerTests_Step1
    {
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Check_WhenCalledWithTheDate_2000_1_1_ThenApplicationExceptionIsThrowned()
        {
            using(ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => { return new DateTime(2000, 1, 1); };

                Y2KChecker.Check();
            }
        }
    }

    [TestClass]
    public class Y2KCheckerTests_Step2
    {
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Check_WhenCalledWithY2KShimDateTime_ThenApplicationExceptionIsThrowned()
        {
            using (ShimsContext.Create())
            {
                Y2KShimDateTime shim = new Y2KShimDateTime();

                Y2KChecker.Check();
            }
        }
    }

    public class Y2KShimDateTime
    {
        public Y2KShimDateTime()
        {
            ShimDateTime.NowGet = () => { return new DateTime(2000, 1, 1); };
        }
    }
}
