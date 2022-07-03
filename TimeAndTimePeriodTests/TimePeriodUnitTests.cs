using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TimeAndTimePeriod;

namespace TimeAndTimePeriodTests
{
    [TestClass]
    public class TimePeriodUnitTests
    {
        #region Constructor tests ================================

        [TestMethod, TestCategory("Constructors")]
        [DataRow(1)]
        [DataRow(231252)]
        [DataRow(23323)]
        [DataRow(long.MaxValue)]
        public void Constructor_1param_Default(long x)
        {
            var timePeriod = new TimePeriod(x);

            Assert.AreEqual(timePeriod.Time, x);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(10, 10, 10)]
        [DataRow(23, 59, 59)]
        [DataRow(0, 3, 0)]
        [DataRow(0, 59, 59)]
        [DataRow(23, 59, 59)]
        [DataRow(6, 0, 0)]
        [DataRow(14, 0, 45)]
        [DataRow(23, 45, 1)]
        public void Constructor_3params_Default(int x, int y, int z)
        {
            var timePeriod = new TimePeriod(x, y, z);

            Assert.AreEqual(timePeriod.Time, x * 3600 + y * 60 + z);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(10, 10)]
        [DataRow(23, 59)]
        [DataRow(0, 1)]
        [DataRow(0, 59)]
        [DataRow(23, 59)]
        [DataRow(6, 0)]
        [DataRow(14, 0)]
        [DataRow(23, 12)]
        public void Constructor_2params_Default(int x, int y)
        {
            var timePeriod = new TimePeriod(x, y);

            Assert.AreEqual(timePeriod.Time, x * 3600 + y * 60 + 0);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(10)]
        [DataRow(23)]
        [DataRow(0)]
        [DataRow(6)]
        [DataRow(14)]
        public void Constructor_1param_Default(int x)
        {
            var timePeriod = new TimePeriod(x);

            Assert.AreEqual(timePeriod.Time, x * 3600 + 0 * 60 + 0);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("10:10:10", 10, 10, 10)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("0:1:0", 0, 1, 0)]
        [DataRow("0:59:59", 0, 59, 59)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("6:0:0", 6, 0, 0)]
        [DataRow("14:0:45", 14, 0, 45)]
        [DataRow("23:45:1", 23, 45, 1)]
        public void Constructor_1param_String(string str, int result_x, int result_y, int result_z)
        {
            var timePeriod = new TimePeriod(str);

            Assert.AreEqual(timePeriod.Time, result_x * 3600 + result_y * 60 + result_z);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(-10, 10, 10)]
        [DataRow(0, -59, 59)]
        [DataRow(-6, 0, 0)]
        [DataRow(-23, 45, 1)]
        [DataRow(-10, 10, 10)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_Default_ArgumentOutOfRangeException(int x, int y, int z)
        {
            _ = new TimePeriod(x, y, z);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(-10, 10)]
        [DataRow(0, -59)]
        [DataRow(-23, 12)]
        [DataRow(14, -12)]
        [DataRow(1004, -1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_Default_ArgumentOutOfRangeException(int x, int y)
        {
            _ = new TimePeriod(x, y);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("-1:0:0")]
        [DataRow("10:-10:10")]
        [DataRow("23:50:-59")]
        [DataRow("0:0:-1")]
        [DataRow("0:-59:60")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_String_ArgumentOutOfRangeException(string str)
        {
            _ = new TimePeriod(str);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("24|10|10")]
        [DataRow("23?60?59")]
        [DataRow("something")]
        [DataRow("0?:59:60")]
        [DataRow("10:==:10")]
        [DataRow("18:59")]
        [DataRow("0:0:1sa")]
        [DataRow("0:0")]
        [DataRow("0")]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_1param_String_ArgumentException(string str)
        {
            _ = new TimePeriod(str);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(23, 59, 59, 10, 10, 10, "13:59:59")]
        [DataRow(0, 59, 59, 0, 20, 15, "00:39:44")]
        [DataRow(23, 59, 59, 3, 59,59, "20:00:00")]
        [DataRow(6, 0, 0, 2, 0, 0, "04:00:00")]
        [DataRow(14, 0, 45, 0, 0, 0, "14:00:45")]
        [DataRow(23, 45, 1, 20, 45, 0, "23:00:01")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_Timer(int x1, int y1, int z1, int x2, int y2, int z2, string expect)
        {
            var time_x = new Time(x1, y1, z1);
            var time_y = new Time(x2, y2, z2);

            var result = new TimePeriod(time_x, time_y);
            var expected = new TimePeriod(expect);

            Assert.AreEqual(expected, result);
        }

        #endregion

        #region ToString tests ===================================

        [TestMethod, TestCategory("ToString")]
        [DataRow("10:10:10", 10, 10, 10)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("00:59:59", 0, 59, 59)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("06:00:00", 6, 0, 0)]
        [DataRow("14:00:45", 14, 0, 45)]
        [DataRow("23:45:01", 23, 45, 1)]
        public void ToString_Default(string result, int x, int y, int z)
        {
            var timePeriod = new TimePeriod(x, y, z);

            Assert.AreEqual(result, timePeriod.ToString());
        }

        [TestMethod, TestCategory("ToString")]
        [DataRow("10:10:10", 10, 10, 10)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("00:59:59", 0, 59, 59)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("06:00:00", 6, 0, 0)]
        [DataRow("14:00:45", 14, 0, 45)]
        [DataRow("23:45:01", 23, 45, 1)]
        public void ToString_New_Time(string result, int x, int y, int z)
        {
            var time = new TimePeriod(x, y, z);

            var newTime = new TimePeriod(time.ToString());

            Assert.AreEqual(newTime.Time, time.Time);
        }

        #endregion

        #region Equals ===========================================

        [TestMethod, TestCategory("Equals")]
        [DataRow(10, 10, 10)]
        [DataRow(23, 59, 59)]
        [DataRow(0, 59, 59)]
        [DataRow(23, 59, 59)]
        [DataRow(6, 0, 0)]
        [DataRow(14, 0, 45)]
        [DataRow(23, 45, 1)]
        public void Equals_Time(int x, int y, int z)
        {
            var time_x = new TimePeriod(x, y, z);
            var time_y = new TimePeriod(x, y, z);

            Assert.IsTrue(time_x.Equals(time_y));
        }

        [TestMethod, TestCategory("Equals")]
        [DataRow("10:10:10", 10, 10, 10)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("0:59:59", 0, 59, 59)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("6:0:0", 6, 0, 0)]
        [DataRow("14:0:45", 14, 0, 45)]
        [DataRow("23:45:1", 23, 45, 1)]
        public void Equals_Time_From_String_And_Int(string str, int x, int y, int z)
        {
            var time_x = new TimePeriod(x, y, z);
            var time_y = new TimePeriod(str);

            Assert.IsTrue(time_x.Equals(time_y));
        }

        [TestMethod, TestCategory("Equals")]
        [DataRow(10, 10, 11, 10, 10, 10)]
        [DataRow(22, 59, 59, 23, 59, 59)]
        public void NotEquals_Time(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var time_x = new TimePeriod(x1, y1, z1);
            var time_y = new TimePeriod(x2, y2, z2);

            Assert.IsFalse(time_x.Equals(time_y));
        }

        [TestMethod, TestCategory("Equals")]
        [DataRow("11:10:10", 10, 10, 10)]
        [DataRow("22:59:59", 23, 59, 59)]
        [DataRow("23:21:59", 23, 59, 59)]
        [DataRow("14:0:46", 14, 0, 45)]
        public void NotEquals_Time_From_String_And_Int(string str, int x, int y, int z)
        {
            var time_x = new TimePeriod(x, y, z);
            var time_y = new TimePeriod(str);

            Assert.IsFalse(time_x.Equals(time_y));
        }

        #endregion

        #region Operators overloading ============================

        [DataTestMethod, TestCategory("Operators overloading")]
        [DataRow(10, 10, 10)]
        [DataRow(23, 59, 59)]
        [DataRow(0, 59, 59)]
        [DataRow(23, 59, 59)]
        [DataRow(6, 0, 0)]
        [DataRow(14, 0, 45)]
        [DataRow(23, 45, 1)]
        public void Operator_Equals(int x, int y, int z)
        {
            var time_x = new TimePeriod(x, y, z);
            var time_y = new TimePeriod(x, y, z);

            Assert.IsTrue(time_x == time_y);
        }

        [DataTestMethod, TestCategory("Operators overloading")]
        [DataRow(10, 10, 11, 10, 10, 10)]
        [DataRow(22, 59, 59, 23, 59, 59)]
        public void Operator_NotEquals(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var time_x = new TimePeriod(x1, y1, z1);
            var time_y = new TimePeriod(x2, y2, z2);

            Assert.IsTrue(time_x != time_y);
        }

        [DataTestMethod, TestCategory("Operators overloading")]
        [DataRow(10, 10, 11, 10, 10, 10)]
        [DataRow(23, 59, 59, 22, 59, 59)]
        [DataRow(1, 0, 0, 0, 59, 59)]
        [DataRow(0, 0, 2, 0, 0, 1)]
        [DataRow(0, 1, 0, 0, 0, 59)]
        public void Operator_GreaterThan(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var time_x = new TimePeriod(x1, y1, z1);
            var time_y = new TimePeriod(x2, y2, z2);

            Assert.IsTrue(time_x > time_y);
        }

        [DataTestMethod, TestCategory("Operators overloading")]
        [DataRow(10, 10, 11, 10, 10, 10)]
        [DataRow(23, 59, 59, 22, 59, 59)]
        [DataRow(1, 0, 0, 0, 59, 59)]
        [DataRow(0, 0, 2, 0, 0, 1)]
        [DataRow(0, 1, 0, 0, 0, 59)]
        [DataRow(10, 10, 10, 10, 10, 10)]
        [DataRow(23, 59, 59, 23, 59, 59)]
        [DataRow(0, 59, 59, 0, 59, 59)]
        [DataRow(23, 59, 59, 23, 59, 59)]
        [DataRow(6, 0, 0, 6, 0, 0)]
        [DataRow(14, 0, 45, 14, 0, 45)]
        [DataRow(23, 45, 1, 23, 45, 1)]
        public void Operator_GreaterThan_Or_Equals(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var time_x = new TimePeriod(x1, y1, z1);
            var time_y = new TimePeriod(x2, y2, z2);

            Assert.IsTrue(time_x >= time_y);
        }

        [DataTestMethod, TestCategory("Operators overloading")]
        [DataRow(10, 10, 11, 10, 10, 10)]
        [DataRow(23, 59, 59, 22, 59, 59)]
        [DataRow(1, 0, 0, 0, 59, 59)]
        [DataRow(0, 0, 2, 0, 0, 1)]
        [DataRow(0, 1, 0, 0, 0, 59)]
        public void Operator_LessThan(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var time_y = new TimePeriod(x1, y1, z1);
            var time_x = new TimePeriod(x2, y2, z2);

            Assert.IsTrue(time_x < time_y);
        }

        [DataTestMethod, TestCategory("Operators overloading")]
        [DataRow(10, 10, 11, 10, 10, 10)]
        [DataRow(23, 59, 59, 22, 59, 59)]
        [DataRow(1, 0, 0, 0, 59, 59)]
        [DataRow(0, 0, 2, 0, 0, 1)]
        [DataRow(0, 1, 0, 0, 0, 59)]
        [DataRow(10, 10, 10, 10, 10, 10)]
        [DataRow(23, 59, 59, 23, 59, 59)]
        [DataRow(0, 59, 59, 0, 59, 59)]
        [DataRow(23, 59, 59, 23, 59, 59)]
        [DataRow(6, 0, 0, 6, 0, 0)]
        [DataRow(14, 0, 45, 14, 0, 45)]
        [DataRow(23, 45, 1, 23, 45, 1)]
        public void Operator_LessThan_Or_Equals(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var time_y = new TimePeriod(x1, y1, z1);
            var time_x = new TimePeriod(x2, y2, z2);

            Assert.IsTrue(time_x <= time_y);
        }

        #endregion

        #region Plus And Minus operatinons =========================

        [DataTestMethod, TestCategory("Plus And Minus operatinons")]
        [DataRow("10:10:10", "10:10:10", "20:20:20")]
        [DataRow("23:59:59", "00:00:01", "00:00:00")]
        [DataRow("00:00:00", "24:00:00", "00:00:00")]
        [DataRow("00:00:00", "10:03:22", "10:03:22")]
        [DataRow("23:52:28", "00:07:32", "00:00:00")]
        [DataRow("14:52:28", "20:00:00", "10:52:28")]
        [DataRow("14:52:28", "02:00:00", "16:52:28")]
        [DataRow("00:02:14", "00:14:51", "00:17:05")]
        [DataRow("00:00:08", "00:00:24", "00:00:32")]
        public void Time_Plus_Operator(string x, string y, string expect)
        {
            var time = new TimePeriod(x);
            var timePeriod = new TimePeriod(y);

            var result = time + timePeriod;

            var expected = new TimePeriod(expect);

            Assert.IsTrue(result == expected);
        }

        [DataTestMethod, TestCategory("Plus And Minus operatinons")]
        [DataRow("10:10:10", "10:10:10", "20:20:20")]
        [DataRow("23:59:59", "00:00:01", "00:00:00")]
        [DataRow("00:00:00", "24:00:00", "00:00:00")]
        [DataRow("00:00:00", "10:03:22", "10:03:22")]
        [DataRow("23:52:28", "00:07:32", "00:00:00")]
        [DataRow("14:52:28", "20:00:00", "10:52:28")]
        [DataRow("14:52:28", "02:00:00", "16:52:28")]
        [DataRow("00:02:14", "00:14:51", "00:17:05")]
        [DataRow("00:00:08", "00:00:24", "00:00:32")]
        public void Time_Plus_Method(string x, string y, string expect)
        {
            var time = new TimePeriod(x);
            var timePeriod = new TimePeriod(y);

            var result = time.Plus(timePeriod);

            var expected = new TimePeriod(expect);

            Assert.IsTrue(result == expected);
        }

        [DataTestMethod, TestCategory("Plus And Minus operatinons")]
        [DataRow("10:10:10", "10:10:10", "20:20:20")]
        [DataRow("23:59:59", "00:00:01", "00:00:00")]
        [DataRow("00:00:00", "24:00:00", "00:00:00")]
        [DataRow("00:00:00", "10:03:22", "10:03:22")]
        [DataRow("23:52:28", "00:07:32", "00:00:00")]
        [DataRow("14:52:28", "20:00:00", "10:52:28")]
        [DataRow("14:52:28", "02:00:00", "16:52:28")]
        [DataRow("00:02:14", "00:14:51", "00:17:05")]
        [DataRow("00:00:08", "00:00:24", "00:00:32")]
        public void Time_Plus_Static_Method(string x, string y, string expect)
        {
            var time = new TimePeriod(x);
            var timePeriod = new TimePeriod(y);

            var result = TimePeriod.Plus(time, timePeriod);

            var expected = new TimePeriod(expect);

            Assert.IsTrue(result == expected);
        }

        [DataTestMethod, TestCategory("Plus And Minus operatinons")]
        [DataRow("10:10:10", "10:10:10", "00:00:00")]
        [DataRow("23:59:59", "00:00:01", "23:59:58")]
        [DataRow("00:00:00", "24:00:00", "00:00:00")]
        [DataRow("00:00:00", "00:00:01", "23:59:59")]
        [DataRow("00:00:00", "10:03:22", "13:56:38")]
        public void Time_Minus_Operator(string x, string y, string expect)
        {
            var time = new TimePeriod(x);
            var timePeriod = new TimePeriod(y);

            var result = time - timePeriod;

            var expected = new TimePeriod(expect);

            Assert.IsTrue(result == expected);
        }

        [DataTestMethod, TestCategory("Plus And Minus operatinons")]
        [DataRow("10:10:10", "10:10:10", "00:00:00")]
        [DataRow("23:59:59", "00:00:01", "23:59:58")]
        [DataRow("00:00:00", "24:00:00", "00:00:00")]
        [DataRow("00:00:00", "00:00:01", "23:59:59")]
        [DataRow("00:00:00", "10:03:22", "13:56:38")]
        public void Time_Minus_Method(string x, string y, string expect)
        {
            var time = new TimePeriod(x);
            var timePeriod = new TimePeriod(y);

            var result = time.Minus(timePeriod);

            var expected = new TimePeriod(expect);

            Assert.IsTrue(result == expected);
        }

        [DataTestMethod, TestCategory("Plus And Minus operatinons")]
        [DataRow("10:10:10", "10:10:10", "00:00:00")]
        [DataRow("23:59:59", "00:00:01", "23:59:58")]
        [DataRow("00:00:00", "24:00:00", "00:00:00")]
        [DataRow("00:00:00", "00:00:01", "23:59:59")]
        [DataRow("00:00:00", "10:03:22", "13:56:38")]
        public void Time_Minus_Static_Method(string x, string y, string expect)
        {
            var time = new TimePeriod(x);
            var timePeriod = new TimePeriod(y);

            var result = TimePeriod.Minus(time, timePeriod);

            var expected = new TimePeriod(expect);

            Assert.IsTrue(result == expected);
        }

        #endregion
    }
}
