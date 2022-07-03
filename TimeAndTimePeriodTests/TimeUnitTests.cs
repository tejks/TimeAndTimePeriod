using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TimeAndTimePeriod;

namespace TimeAndTimePeriodTests
{
    [TestClass]
    public class TimeUnitTests
    {
        #region Constructor tests ================================

        [TestMethod, TestCategory("Constructors")]
        [DataRow(10, 10, 10)]
        [DataRow(23, 59, 59)]
        [DataRow(0, 0, 0)]
        [DataRow(0, 59, 59)]
        [DataRow(23, 59, 59)]
        [DataRow(6, 0, 0)]
        [DataRow(14, 0, 45)]
        [DataRow(23, 45, 1)]
        public void Constructor_3params_Default(int x, int y, int z)
        {
            var time = new Time(x, y, z);

            Assert.AreEqual(time.Hours, (byte)x);
            Assert.AreEqual(time.Minutes, (byte)y);
            Assert.AreEqual(time.Seconds, (byte)z);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(10, 10)]
        [DataRow(23, 59)]
        [DataRow(0, 0)]
        [DataRow(0, 59)]
        [DataRow(23, 59)]
        [DataRow(6, 0)]
        [DataRow(14, 0)]
        [DataRow(23, 12)]
        public void Constructor_2params_Default(int x, int y)
        {
            var time = new Time(x, y);

            Assert.AreEqual(time.Hours, (byte)x);
            Assert.AreEqual(time.Minutes, (byte)y);
            Assert.AreEqual(time.Seconds, (byte)0);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(10)]
        [DataRow(23)]
        [DataRow(0)]
        [DataRow(6)]
        [DataRow(14)]
        public void Constructor_1param_Default(int x)
        {
            var time = new Time(x);

            Assert.AreEqual(time.Hours, (byte)x);
            Assert.AreEqual(time.Minutes, (byte)0);
            Assert.AreEqual(time.Seconds, (byte)0);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("10:10:10", 10, 10, 10)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("0:0:0", 0, 0, 0)]
        [DataRow("0:59:59", 0, 59, 59)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("6:0:0", 6, 0, 0)]
        [DataRow("14:0:45", 14, 0, 45)]
        [DataRow("23:45:1", 23, 45, 1)]
        public void Constructor_1param_String(string str, int result_x, int result_y, int result_z)
        {
            var time = new Time(str);

            Assert.AreEqual(time.Hours, (byte)result_x);
            Assert.AreEqual(time.Minutes, (byte)result_y);
            Assert.AreEqual(time.Seconds, (byte)result_z);
        }


        [TestMethod, TestCategory("Constructors")]
        [DataRow(-10, 10, 10)]
        [DataRow(23, -59, 59)]
        [DataRow(22, 0, -1)]
        [DataRow(0, -59, 59)]
        [DataRow(23, -59, -59)]
        [DataRow(-6, 0, 0)]
        [DataRow(-14, 0, -45)]
        [DataRow(-23, -45, -1)]
        [DataRow(-10, 10, 10)]
        [DataRow(24, 59, 59)]
        [DataRow(23, 60, 0)]
        [DataRow(23, 0, 60)]
        [DataRow(23, 100, 59)]
        [DataRow(24, 0, 0)]
        [DataRow(100, 100, 100)]
        [DataRow(0, 0, 60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_Default_ArgumentOutOfRangeException(int x, int y, int z)
        {
            _ = new Time(x, y, z);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(-10, 10)]
        [DataRow(23, -59)]
        [DataRow(24, 0)]
        [DataRow(0, -59)]
        [DataRow(24, -59)]
        [DataRow(100, 100)]
        [DataRow(14, 100)]
        [DataRow(-23, 12)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_Default_ArgumentOutOfRangeException(int x, int y)
        {
            _ = new Time(x, y);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(100)]
        [DataRow(24)]
        [DataRow(-6)]
        [DataRow(-14)]
        [DataRow(-24)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_Default_ArgumentOutOfRangeException(int x)
        {
            _ = new Time(x);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("24:10:10")]
        [DataRow("23:60:59")]
        [DataRow("-1:0:0")]
        [DataRow("0:59:60")]
        [DataRow("10:-10:10")]
        [DataRow("23:50:-59")]
        [DataRow("0:0:-1")]
        [DataRow("0:-59:60")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_String_ArgumentOutOfRangeException(string str)
        {
            _ = new Time(str);
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
        [DataRow("something:something")]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_1param_String_ArgumentException(string str)
        {
            _ = new Time(str);
        }

        #endregion

        #region ToString tests ===================================

        [TestMethod, TestCategory("ToString")]
        [DataRow("10:10:10", 10, 10, 10)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("00:00:00", 0, 0, 0)]
        [DataRow("00:59:59", 0, 59, 59)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("06:00:00", 6, 0, 0)]
        [DataRow("14:00:45", 14, 0, 45)]
        [DataRow("23:45:01", 23, 45, 1)]
        public void ToString_Default(string result, int x, int y, int z)
        {
            var time = new Time(x, y, z);

            Assert.AreEqual(result, time.ToString());
        }

        [TestMethod, TestCategory("ToString")]
        [DataRow("10:10:10", 10, 10, 10)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("00:00:00", 0, 0, 0)]
        [DataRow("00:59:59", 0, 59, 59)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("06:00:00", 6, 0, 0)]
        [DataRow("14:00:45", 14, 0, 45)]
        [DataRow("23:45:01", 23, 45, 1)]
        public void ToString_New_Time(string result, int x, int y, int z)
        {
            var time = new Time(x, y, z);

            var newTime = new Time(time.ToString());

            Assert.AreEqual(newTime.Hours, (byte)x);
            Assert.AreEqual(newTime.Minutes, (byte)y);
            Assert.AreEqual(newTime.Seconds, (byte)z);
        }

        #endregion

        #region Equals ===========================================

        [TestMethod, TestCategory("Equals")]
        [DataRow(10, 10, 10)]
        [DataRow(23, 59, 59)]
        [DataRow(0, 0, 0)]
        [DataRow(0, 59, 59)]
        [DataRow(23, 59, 59)]
        [DataRow(6, 0, 0)]
        [DataRow(14, 0, 45)]
        [DataRow(23, 45, 1)]
        public void Equals_Time(int x, int y, int z)
        {
            var time_x = new Time(x, y, z);
            var time_y = new Time(x, y, z);

            Assert.IsTrue(time_x.Equals(time_y));
        }

        [TestMethod, TestCategory("Equals")]
        [DataRow("10:10:10", 10, 10, 10)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("0:0:0", 0, 0, 0)]
        [DataRow("0:59:59", 0, 59, 59)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("6:0:0", 6, 0, 0)]
        [DataRow("14:0:45", 14, 0, 45)]
        [DataRow("23:45:1", 23, 45, 1)]
        public void Equals_Time_From_String_And_Int(string str, int x, int y, int z)
        {
            var time_x = new Time(x, y, z);
            var time_y = new Time(str);

            Assert.IsTrue(time_x.Equals(time_y));
        }

        [TestMethod, TestCategory("Equals")]
        [DataRow(10, 10, 11, 10, 10, 10)]
        [DataRow(22, 59, 59, 23, 59, 59)]
        [DataRow(1, 1, 1, 0, 0, 0)]
        [DataRow(0, 0, 0, 0, 59, 59)]
        public void NotEquals_Time(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var time_x = new Time(x1, y1, z1);
            var time_y = new Time(x2, y2, z2);

            Assert.IsFalse(time_x.Equals(time_y));
        }

        [TestMethod, TestCategory("Equals")]
        [DataRow("11:10:10", 10, 10, 10)]
        [DataRow("22:59:59", 23, 59, 59)]
        [DataRow("1:1:1", 0, 0, 0)]
        [DataRow("0:0:0", 0, 59, 59)]
        [DataRow("23:21:59", 23, 59, 59)]
        [DataRow("14:0:46", 14, 0, 45)]
        public void NotEquals_Time_From_String_And_Int(string str, int x, int y, int z)
        {
            var time_x = new Time(x, y, z);
            var time_y = new Time(str);

            Assert.IsFalse(time_x.Equals(time_y));
        }

        [TestMethod, TestCategory("Equals")]
        [DataRow(null, 10, 10, 10)]
        public void NotEquals_Null(object toEqual, int x, int y, int z)
        {
            var time_x = new Time(x, y, z);

            Assert.IsFalse(time_x.Equals(toEqual));
        }

        #endregion

        #region Operators overloading ============================

        [DataTestMethod, TestCategory("Operators overloading")]
        [DataRow(10, 10, 10)]
        [DataRow(23, 59, 59)]
        [DataRow(0, 0, 0)]
        [DataRow(0, 59, 59)]
        [DataRow(23, 59, 59)]
        [DataRow(6, 0, 0)]
        [DataRow(14, 0, 45)]
        [DataRow(23, 45, 1)]
        public void Operator_Equals(int x, int y, int z)
        {
            var time_x = new Time(x, y, z);
            var time_y = new Time(x, y, z);

            Assert.IsTrue(time_x == time_y);
        }

        [DataTestMethod, TestCategory("Operators overloading")]
        [DataRow(10, 10, 11, 10, 10, 10)]
        [DataRow(22, 59, 59, 23, 59, 59)]
        [DataRow(1, 1, 1, 0, 0, 0)]
        [DataRow(0, 0, 0, 0, 59, 59)]
        public void Operator_NotEquals(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var time_x = new Time(x1, y1, z1);
            var time_y = new Time(x2, y2, z2);

            Assert.IsTrue(time_x != time_y);
        }

        [DataTestMethod, TestCategory("Operators overloading")]
        [DataRow(10, 10, 11, 10, 10, 10)]
        [DataRow(23, 59, 59, 22, 59, 59)]
        [DataRow(1, 1, 1, 0, 0, 0)]
        [DataRow(1, 0, 0, 0, 59, 59)]
        [DataRow(0, 0, 2, 0, 0, 1)]
        [DataRow(0, 1, 0, 0, 0, 59)]
        public void Operator_GreaterThan(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var time_x = new Time(x1, y1, z1);
            var time_y = new Time(x2, y2, z2);

            Assert.IsTrue(time_x > time_y);
        }

        [DataTestMethod, TestCategory("Operators overloading")]
        [DataRow(10, 10, 11, 10, 10, 10)]
        [DataRow(23, 59, 59, 22, 59, 59)]
        [DataRow(1, 1, 1, 0, 0, 0)]
        [DataRow(1, 0, 0, 0, 59, 59)]
        [DataRow(0, 0, 2, 0, 0, 1)]
        [DataRow(0, 1, 0, 0, 0, 59)]
        [DataRow(10, 10, 10, 10, 10, 10)]
        [DataRow(23, 59, 59, 23, 59, 59)]
        [DataRow(0, 0, 0, 0, 0, 0)]
        [DataRow(0, 59, 59, 0, 59, 59)]
        [DataRow(23, 59, 59, 23, 59, 59)]
        [DataRow(6, 0, 0, 6, 0, 0)]
        [DataRow(14, 0, 45, 14, 0, 45)]
        [DataRow(23, 45, 1, 23, 45, 1)]
        public void Operator_GreaterThan_Or_Equals(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var time_x = new Time(x1, y1, z1);
            var time_y = new Time(x2, y2, z2);

            Assert.IsTrue(time_x >= time_y);
        }

        [DataTestMethod, TestCategory("Operators overloading")]
        [DataRow(10, 10, 11, 10, 10, 10)]
        [DataRow(23, 59, 59, 22, 59, 59)]
        [DataRow(1, 1, 1, 0, 0, 0)]
        [DataRow(1, 0, 0, 0, 59, 59)]
        [DataRow(0, 0, 2, 0, 0, 1)]
        [DataRow(0, 1, 0, 0, 0, 59)]
        public void Operator_LessThan(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var time_y = new Time(x1, y1, z1);
            var time_x = new Time(x2, y2, z2);

            Assert.IsTrue(time_x < time_y);
        }

        [DataTestMethod, TestCategory("Operators overloading")]
        [DataRow(10, 10, 11, 10, 10, 10)]
        [DataRow(23, 59, 59, 22, 59, 59)]
        [DataRow(1, 1, 1, 0, 0, 0)]
        [DataRow(1, 0, 0, 0, 59, 59)]
        [DataRow(0, 0, 2, 0, 0, 1)]
        [DataRow(0, 1, 0, 0, 0, 59)]
        [DataRow(10, 10, 10, 10, 10, 10)]
        [DataRow(23, 59, 59, 23, 59, 59)]
        [DataRow(0, 0, 0, 0, 0, 0)]
        [DataRow(0, 59, 59, 0, 59, 59)]
        [DataRow(23, 59, 59, 23, 59, 59)]
        [DataRow(6, 0, 0, 6, 0, 0)]
        [DataRow(14, 0, 45, 14, 0, 45)]
        [DataRow(23, 45, 1, 23, 45, 1)]
        public void Operator_LessThan_Or_Equals(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            var time_y = new Time(x1, y1, z1);
            var time_x = new Time(x2, y2, z2);

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
            var time = new Time(x);
            var timePeriod = new TimePeriod(y);

            var result = time + timePeriod;

            var expected = new Time(expect);

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
            var time = new Time(x);
            var timePeriod = new TimePeriod(y);

            var result = time.Plus(timePeriod);

            var expected = new Time(expect);

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
            var time = new Time(x);
            var timePeriod = new TimePeriod(y);

            var result = Time.Plus(time, timePeriod);

            var expected = new Time(expect);

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
            var time = new Time(x);
            var timePeriod = new TimePeriod(y);

            var result = time - timePeriod;

            var expected = new Time(expect);

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
            var time = new Time(x);
            var timePeriod = new TimePeriod(y);

            var result = time.Minus(timePeriod);

            var expected = new Time(expect);

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
            var time = new Time(x);
            var timePeriod = new TimePeriod(y);

            var result = Time.Minus(time, timePeriod);

            var expected = new Time(expect);

            Assert.IsTrue(result == expected);
        }

        #endregion
    }
}
