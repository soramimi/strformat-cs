using Soramimi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter {
	class Example {
		static int passed = 0;
		static int failed = 0;
		static int total = 0;
		static void update(String expected, String actual)
		{
			if (expected == actual) {
				passed++;
			} else {
				strformat.New("#%5d\n").d(total).PrintOut();
				strformat.New("[fail] expected '%s'\n").s(expected).PrintOut();
				strformat.New(" , but returned '%s'\n").s(actual).PrintOut();
				failed++;
			}
			total++;
		}
		static void assertEquals(String expected, String actual)
		{
				update(expected, actual);
		}
		static void assertEquals(int expected, int actual)
		{
			update(expected.ToString(), actual.ToString());
		}
		static void assertEquals(uint expected, uint actual)
		{
			update(expected.ToString(), actual.ToString());
		}
		static void assertEquals(long expected, long actual)
		{
			update(expected.ToString(), actual.ToString());
		}
		static void assertEquals(ulong expected, ulong actual)
		{
			update(expected.ToString(), actual.ToString());
		}
		static void Test()
		{
			// 0
			assertEquals(0, strformat.strtol("0", 0));
			assertEquals(0, strformat.strtol(" +0", 0));
			assertEquals(0, strformat.strtol("  -0", 0));
			assertEquals(123, strformat.strtol("\t123", 0));
			assertEquals(123, strformat.strtol("\r+123", 0));
			assertEquals(-123, strformat.strtol("\n-123", 0));
			assertEquals(2147483647, strformat.strtol(" \t2147483647", 0));
			assertEquals(2147483647, strformat.strtol("\t +2147483647", 0));
			assertEquals(-2147483648, strformat.strtol(" \r\n-2147483648", 0));
			assertEquals(123456, strformat.strtol("\r\n123456Z", 0));
			// 10
			assertEquals(0, strformat.strtol("0", 10));
			assertEquals(0, strformat.strtol("+0", 10));
			assertEquals(0, strformat.strtol("-0", 10));
			assertEquals(123, strformat.strtol("123", 10));
			assertEquals(123, strformat.strtol("+123", 10));
			assertEquals(-123, strformat.strtol("-123", 10));
			assertEquals(2147483647, strformat.strtol("2147483647", 10));
			assertEquals(2147483647, strformat.strtol("+2147483647", 10));
			assertEquals(-2147483648, strformat.strtol("-2147483648", 10));
			assertEquals(123456, strformat.strtol("123456Z", 10));
			// 20
			assertEquals(0, strformat.strtol("0", 0));
			assertEquals(0, strformat.strtol("+0", 0));
			assertEquals(0, strformat.strtol("-0", 0));
			assertEquals(509, strformat.strtol("0775", 0));
			assertEquals(509, strformat.strtol("+0775", 0));
			assertEquals(-509, strformat.strtol("-0775", 0));
			assertEquals(2147483647, strformat.strtol("017777777777", 0));
			assertEquals(-2147483647, strformat.strtol("-017777777777", 0));
			assertEquals(42798, strformat.strtol("0123456z", 0));
			assertEquals(-42798, strformat.strtol("-0123456z", 0));
			// 30
			assertEquals(0, strformat.strtol("0", 8));
			assertEquals(0, strformat.strtol("+0", 8));
			assertEquals(0, strformat.strtol("-0", 8));
			assertEquals(509, strformat.strtol("775", 8));
			assertEquals(509, strformat.strtol("+775", 8));
			assertEquals(-509, strformat.strtol("-775", 8));
			assertEquals(2147483647, strformat.strtol("17777777777", 8));
			assertEquals(-2147483647, strformat.strtol("-17777777777", 8));
			assertEquals(42798, strformat.strtol("123456z", 8));
			assertEquals(-42798, strformat.strtol("-123456z", 8));
			// 40
			assertEquals(0, strformat.strtol("0x0", 0));
			assertEquals(0, strformat.strtol("+0x0", 0));
			assertEquals(0, strformat.strtol("-0x0", 0));
			assertEquals(80, strformat.strtol("0x50", 0));
			assertEquals(80, strformat.strtol("+0x50", 0));
			assertEquals(-80, strformat.strtol("-0x50", 0));
			assertEquals(2147483647, strformat.strtol("0x7fffffff", 0));
			assertEquals(2147483647, strformat.strtol("+0x7fffffff", 0));
			assertEquals(-2147483647, strformat.strtol("-0x7fffffff", 0));
			assertEquals(1193046, strformat.strtol("0x123456Z", 0));
			// 50
			assertEquals(0, strformat.strtol("0", 16));
			assertEquals(0, strformat.strtol("+0", 16));
			assertEquals(0, strformat.strtol("-0", 16));
			assertEquals(80, strformat.strtol("50", 16));
			assertEquals(80, strformat.strtol("+50", 16));
			assertEquals(-80, strformat.strtol("-50", 16));
			assertEquals(2147483647, strformat.strtol("7fffffff", 16));
			assertEquals(2147483647, strformat.strtol("+7fffffff", 16));
			assertEquals(-2147483647, strformat.strtol("-7fffffff", 16));
			assertEquals(1193046, strformat.strtol("123456Z", 16));
			// 60
			assertEquals(0, strformat.strtoll("0", 0));
			assertEquals(0, strformat.strtoll("+0", 0));
			assertEquals(0, strformat.strtoll("-0", 0));
			assertEquals(123, strformat.strtoll("123", 0));
			assertEquals(123, strformat.strtoll("+123", 0));
			assertEquals(-123, strformat.strtoll("-123", 0));
			assertEquals(9223372036854775807, strformat.strtoll("9223372036854775807", 0));
			assertEquals(9223372036854775807, strformat.strtoll("+9223372036854775807", 0));
			assertEquals(-9223372036854775808, strformat.strtoll("-9223372036854775808", 0));
			assertEquals(123456, strformat.strtoll("123456Z", 0));
			// 70
			assertEquals(0, strformat.strtoll("0", 10));
			assertEquals(0, strformat.strtoll("+0", 10));
			assertEquals(0, strformat.strtoll("-0", 10));
			assertEquals(123, strformat.strtoll("123", 10));
			assertEquals(123, strformat.strtoll("+123", 10));
			assertEquals(-123, strformat.strtoll("-123", 10));
			assertEquals(9223372036854775807, strformat.strtoll("9223372036854775807", 10));
			assertEquals(9223372036854775807, strformat.strtoll("+9223372036854775807", 10));
			assertEquals(-9223372036854775808, strformat.strtoll("-9223372036854775808", 10));
			assertEquals(123456, strformat.strtoll("123456Z", 10));
			// 80
			assertEquals(0, strformat.strtoll("0", 0));
			assertEquals(0, strformat.strtoll("+0", 0));
			assertEquals(0, strformat.strtoll("-0", 0));
			assertEquals(509, strformat.strtoll("0775", 0));
			assertEquals(509, strformat.strtoll("+0775", 0));
			assertEquals(-509, strformat.strtoll("-0775", 0));
			assertEquals(9223372036854775807, strformat.strtoll("0777777777777777777777", 0));
			assertEquals(-9223372036854775807, strformat.strtoll("-0777777777777777777777", 0));
			assertEquals(42798, strformat.strtoll("0123456z", 0));
			assertEquals(-42798, strformat.strtoll("-0123456z", 0));
			// 90
			assertEquals(0, strformat.strtoll("0", 8));
			assertEquals(0, strformat.strtoll("+0", 8));
			assertEquals(0, strformat.strtoll("-0", 8));
			assertEquals(509, strformat.strtoll("775", 8));
			assertEquals(509, strformat.strtoll("+775", 8));
			assertEquals(-509, strformat.strtoll("-775", 8));
			assertEquals(9223372036854775807, strformat.strtoll("777777777777777777777", 8));
			assertEquals(-9223372036854775807, strformat.strtoll("-777777777777777777777", 8));
			assertEquals(42798, strformat.strtoll("123456z", 8));
			assertEquals(-42798, strformat.strtoll("-123456z", 8));
			// 100
			assertEquals(0, strformat.strtoll("0x0", 0));
			assertEquals(0, strformat.strtoll("+0x0", 0));
			assertEquals(0, strformat.strtoll("-0x0", 0));
			assertEquals(80, strformat.strtoll("0x50", 0));
			assertEquals(80, strformat.strtoll("+0x50", 0));
			assertEquals(-80, strformat.strtoll("-0x50", 0));
			assertEquals(9223372036854775807, strformat.strtoll("0x7fffffffffffffff", 0));
			assertEquals(9223372036854775807, strformat.strtoll("+0x7fffffffffffffff", 0));
			assertEquals(-9223372036854775807, strformat.strtoll("-0x7fffffffffffffff", 0));
			assertEquals(1193046, strformat.strtoll("0x123456Z", 0));
			// 110
			assertEquals(0, strformat.strtoll("0", 16));
			assertEquals(0, strformat.strtoll("+0", 16));
			assertEquals(0, strformat.strtoll("-0", 16));
			assertEquals(80, strformat.strtoll("50", 16));
			assertEquals(80, strformat.strtoll("+50", 16));
			assertEquals(-80, strformat.strtoll("-50", 16));
			assertEquals(9223372036854775807, strformat.strtoll("7fffffffffffffff", 16));
			assertEquals(9223372036854775807, strformat.strtoll("+7fffffffffffffff", 16));
			assertEquals(-9223372036854775807, strformat.strtoll("-7fffffffffffffff", 16));
			assertEquals(1193046, strformat.strtoll("123456Z", 16));
			// 120
			assertEquals(0U, strformat.strtoul("0", 0));
			assertEquals(0U, strformat.strtoul("+0", 0));
			assertEquals(0U, strformat.strtoul("-0", 0));
			assertEquals(123U, strformat.strtoul("123", 0));
			assertEquals(123U, strformat.strtoul("+123", 0));
			assertEquals(4294967173U, strformat.strtoul("-123", 0));
			assertEquals(4294967295U, strformat.strtoul("4294967295", 0));
			assertEquals(4294967295U, strformat.strtoul("+4294967295", 0));
			assertEquals(2147483648U, strformat.strtoul("-2147483648", 0));
			assertEquals(123456U, strformat.strtoul("123456Z", 0));
			// 130
			assertEquals(0U, strformat.strtoul("0", 10));
			assertEquals(0U, strformat.strtoul("+0", 10));
			assertEquals(0U, strformat.strtoul("-0", 10));
			assertEquals(123U, strformat.strtoul("123", 10));
			assertEquals(123U, strformat.strtoul("+123", 10));
			assertEquals(4294967173U, strformat.strtoul("-123", 10));
			assertEquals(4294967295U, strformat.strtoul("4294967295", 10));
			assertEquals(4294967295U, strformat.strtoul("+4294967295", 10));
			assertEquals(2147483648U, strformat.strtoul("-2147483648", 10));
			assertEquals(123456U, strformat.strtoul("123456Z", 10));
			// 140
			assertEquals(0U, strformat.strtoul("0", 0));
			assertEquals(0U, strformat.strtoul("+0", 0));
			assertEquals(0U, strformat.strtoul("-0", 0));
			assertEquals(509U, strformat.strtoul("0775", 0));
			assertEquals(509U, strformat.strtoul("+0775", 0));
			assertEquals(4294966787U, strformat.strtoul("-0775", 0));
			assertEquals(4294967295U, strformat.strtoul("037777777777", 0));
			assertEquals(2147483649U, strformat.strtoul("-017777777777", 0));
			assertEquals(42798U, strformat.strtoul("0123456z", 0));
			assertEquals(4294924498U, strformat.strtoul("-0123456z", 0));
			// 150
			assertEquals(0U, strformat.strtoul("0", 8));
			assertEquals(0U, strformat.strtoul("+0", 8));
			assertEquals(0U, strformat.strtoul("-0", 8));
			assertEquals(509U, strformat.strtoul("775", 8));
			assertEquals(509U, strformat.strtoul("+775", 8));
			assertEquals(4294966787U, strformat.strtoul("-775", 8));
			assertEquals(4294967295U, strformat.strtoul("37777777777", 8));
			assertEquals(2147483649U, strformat.strtoul("-17777777777", 8));
			assertEquals(42798U, strformat.strtoul("123456z", 8));
			assertEquals(4294924498U, strformat.strtoul("-123456z", 8));
			// 160
			assertEquals(0U, strformat.strtoul("0x0", 0));
			assertEquals(0U, strformat.strtoul("+0x0", 0));
			assertEquals(0U, strformat.strtoul("-0x0", 0));
			assertEquals(80U, strformat.strtoul("0x50", 0));
			assertEquals(80U, strformat.strtoul("+0x50", 0));
			assertEquals(4294967216U, strformat.strtoul("-0x50", 0));
			assertEquals(2147483647U, strformat.strtoul("0x7fffffff", 0));
			assertEquals(2147483647U, strformat.strtoul("+0x7fffffff", 0));
			assertEquals(2147483649, strformat.strtoul("-0x7fffffff", 0));
			assertEquals(1193046U, strformat.strtoul("0x123456Z", 0));
			// 170
			assertEquals(0U, strformat.strtoul("0", 16));
			assertEquals(0U, strformat.strtoul("+0", 16));
			assertEquals(0U, strformat.strtoul("-0", 16));
			assertEquals(80U, strformat.strtoul("50", 16));
			assertEquals(80U, strformat.strtoul("+50", 16));
			assertEquals(4294967216U, strformat.strtoul("-50", 16));
			assertEquals(4294967295U, strformat.strtoul("ffffffff", 16));
			assertEquals(4294967295U, strformat.strtoul("+ffffffff", 16));
			assertEquals(2147483649U, strformat.strtoul("-7fffffff", 16));
			assertEquals(1193046U, strformat.strtoul("123456Z", 16));
			// 180
			assertEquals(0UL, strformat.strtoull("0", 0));
			assertEquals(0UL, strformat.strtoull("+0", 0));
			assertEquals(0UL, strformat.strtoull("-0", 0));
			assertEquals(123UL, strformat.strtoull("123", 0));
			assertEquals(123UL, strformat.strtoull("+123", 0));
			assertEquals(123UL, strformat.strtoull("-123", 0));
			assertEquals(9223372036854775807UL, strformat.strtoull("9223372036854775807", 0));
			assertEquals(9223372036854775807UL, strformat.strtoull("+9223372036854775807", 0));
			assertEquals(9223372036854775808UL, strformat.strtoull("-9223372036854775808", 0));
			assertEquals(123456, strformat.strtoull("123456Z", 0));
			// 190
			assertEquals(0UL, strformat.strtoull("0", 10));
			assertEquals(0UL, strformat.strtoull("+0", 10));
			assertEquals(0UL, strformat.strtoull("-0", 10));
			assertEquals(123UL, strformat.strtoull("123", 10));
			assertEquals(123UL, strformat.strtoull("+123", 10));
			assertEquals(123UL, strformat.strtoull("-123", 10));
			assertEquals(18446744073709551615UL, strformat.strtoull("18446744073709551615", 10));
			assertEquals(18446744073709551615UL, strformat.strtoull("+18446744073709551615", 10));
			assertEquals(9223372036854775808UL, strformat.strtoull("-9223372036854775808", 10));
			assertEquals(123456, strformat.strtoull("123456Z", 10));
			// 200
			assertEquals(0UL, strformat.strtoull("0", 0));
			assertEquals(0UL, strformat.strtoull("+0", 0));
			assertEquals(0UL, strformat.strtoull("-0", 0));
			assertEquals(509UL, strformat.strtoull("0775", 0));
			assertEquals(509UL, strformat.strtoull("+0775", 0));
			assertEquals(509UL, strformat.strtoull("-0775", 0));
			assertEquals(18446744073709551615UL, strformat.strtoull("01777777777777777777777", 0));
			assertEquals(9223372036854775807UL, strformat.strtoull("-0777777777777777777777", 0));
			assertEquals(42798UL, strformat.strtoull("0123456z", 0));
			assertEquals(42798UL, strformat.strtoull("-0123456z", 0));
			// 210
			assertEquals(0UL, strformat.strtoull("0", 8));
			assertEquals(0UL, strformat.strtoull("+0", 8));
			assertEquals(0UL, strformat.strtoull("-0", 8));
			assertEquals(509UL, strformat.strtoull("775", 8));
			assertEquals(509UL, strformat.strtoull("+775", 8));
			assertEquals(509UL, strformat.strtoull("-775", 8));
			assertEquals(18446744073709551615UL, strformat.strtoull("1777777777777777777777", 8));
			assertEquals(9223372036854775807UL, strformat.strtoull("-777777777777777777777", 8));
			assertEquals(42798UL, strformat.strtoull("123456z", 8));
			assertEquals(42798UL, strformat.strtoull("-123456z", 8));
			// 220
			assertEquals(0UL, strformat.strtoull("0x0", 0));
			assertEquals(0UL, strformat.strtoull("+0x0", 0));
			assertEquals(0UL, strformat.strtoull("-0x0", 0));
			assertEquals(80UL, strformat.strtoull("0x50", 0));
			assertEquals(80UL, strformat.strtoull("+0x50", 0));
			assertEquals(80UL, strformat.strtoull("-0x50", 0));
			assertEquals(18446744073709551615UL, strformat.strtoull("0xffffffffffffffff", 0));
			assertEquals(18446744073709551615UL, strformat.strtoull("+0xffffffffffffffff", 0));
			assertEquals(9223372036854775807UL, strformat.strtoull("-0x7fffffffffffffff", 0));
			assertEquals(1193046UL, strformat.strtoull("0x123456Z", 0));
			// 230
			assertEquals(0UL, strformat.strtoull("0", 16));
			assertEquals(0UL, strformat.strtoull("+0", 16));
			assertEquals(0UL, strformat.strtoull("-0", 16));
			assertEquals(80UL, strformat.strtoull("50", 16));
			assertEquals(80UL, strformat.strtoull("+50", 16));
			assertEquals(80UL, strformat.strtoull("-50", 16));
			assertEquals(18446744073709551615UL, strformat.strtoull("ffffffffffffffff", 16));
			assertEquals(18446744073709551615UL, strformat.strtoull("+ffffffffffffffff", 16));
			assertEquals(9223372036854775807UL, strformat.strtoull("-7fffffffffffffff", 16));
			assertEquals(1193046UL, strformat.strtoull("123456Z", 16));
		}
		static void PrintResult()
		{
			strformat.New("\n total: %d\npassed: %d\nfailed: %d\n").d(total).d(passed).d(failed).PrintErr();
		}
		static void Main(string[] args)
		{
			Test();
			PrintResult();
//			bool f = int.TryParse("123abc", System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out n);
			//strformat.New("(%d)").s("-0x50").PrintOut();
		}
	}
}
