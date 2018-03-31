using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Soramimi {

	class strformat {

		private const String digits_upper = "0123456789ABCDEF";
		private const String digits_lower = "0123456789abcdef";

		public static int strtol(char[] str, int radix)
		{
			int value = 0;
			int pos = 0;
			bool sign = false;
			while (pos < str.Length) {
				char c = str[pos];
				if (!Char.IsWhiteSpace(c)) {
					break;
				}
				pos++;
			}
			if (pos < str.Length) {
				char c = str[pos];
				if (c == '+') {
					pos++;
				} else if (c == '-') {
					pos++;
					sign = true;
				}
			}
			if (pos < str.Length) {
				char c = str[pos];
				if (c == '0') {
					pos++;
					if (pos < str.Length) {
						char d = str[pos];
						if (d == 'x' || d == 'X') {
							pos++;
							if (radix == 0) {
								radix = 16;
							}
						} else {
							if (radix == 0) {
								radix = 8;
							}
						}
					}
				}
			}
			if (radix != 8 && radix != 16) {
				radix = 10;
			}
			while (pos < str.Length) {
				int n = -1;
				char c = Char.ToUpper(str[pos]);
				pos++;
				if (Char.IsDigit(c)) {
					n = c - '0';
				} else if (Char.IsLetter(c)) {
					n = (c - 'A') + 10;
				}
				if (n < 0 || n >= radix) {
					break;
				}
				if (sign) {
					if (radix == 8 && value < (-2147483648 / 8)) {
						break;
					} else if (radix == 10 && value < (-2147483648 / 10)) {
						break;
					} else if (radix == 16 && value < (-2147483648 / 16)) {
						break;
					}
					if (value < -2147483648 + n) {
						break;
					}
					value = value * radix - n;
				} else {
					if (radix == 8 && value > (2147483647 / 8)) {
						break;
					} else if (radix == 10 && value > (2147483647 / 10)) {
						break;
					} else if (radix == 16 && value > (2147483647 / 16)) {
						break;
					}
					if (value > 2147483647 - n) {
						break;
					}
					value = value * radix + n;
				}
			}
			return value;
		}

		public static long strtoll(char[] str, int radix)
		{
			long value = 0;
			int pos = 0;
			bool sign = false;
			while (pos < str.Length) {
				char c = str[pos];
				if (!Char.IsWhiteSpace(c)) {
					break;
				}
				pos++;
			}
			if (pos < str.Length) {
				char c = str[pos];
				if (c == '+') {
					pos++;
				} else if (c == '-') {
					pos++;
					sign = true;
				}
			}
			if (pos < str.Length) {
				char c = str[pos];
				if (c == '0') {
					pos++;
					if (pos < str.Length) {
						char d = str[pos];
						if (d == 'x' || d == 'X') {
							pos++;
							if (radix == 0) {
								radix = 16;
							}
						} else {
							if (radix == 0) {
								radix = 8;
							}
						}
					}
				}
			}
			if (radix != 8 && radix != 16) {
				radix = 10;
			}
			while (pos < str.Length) {
				int n = -1;
				char c = Char.ToUpper(str[pos]);
				pos++;
				if (Char.IsDigit(c)) {
					n = c - '0';
				} else if (Char.IsLetter(c)) {
					n = (c - 'A') + 10;
				}
				if (n < 0 || n >= radix) {
					break;
				}
				if (sign) {
					if (radix == 8 && value < (-9223372036854775808 / 8)) {
						break;
					} else if (radix == 10 && value < (-9223372036854775808 / 10)) {
						break;
					} else if (radix == 16 && value < (-9223372036854775808 / 16)) {
						break;
					}
					if (value < -9223372036854775808 + n) {
						break;
					}
					value = value * radix - n;
				} else {
					if (radix == 8 && value > (9223372036854775807 / 8)) {
						break;
					} else if (radix == 10 && value > (9223372036854775807 / 10)) {
						break;
					} else if (radix == 16 && value > (9223372036854775807 / 16)) {
						break;
					}
					if (value > 9223372036854775807 - n) {
						break;
					}
					value = value * radix + n;
				}
			}
			return value;
		}

		public static uint strtoul(char[] str, int radix)
		{
			uint value = 0;
			int pos = 0;
			bool sign = false;
			while (pos < str.Length) {
				char c = str[pos];
				if (!Char.IsWhiteSpace(c)) {
					break;
				}
				pos++;
			}
			if (pos < str.Length) {
				char c = str[pos];
				if (c == '+') {
					pos++;
				} else if (c == '-') {
					pos++;
					sign = true;
				}
			}
			if (pos < str.Length) {
				char c = str[pos];
				if (c == '0') {
					pos++;
					if (pos < str.Length) {
						char d = str[pos];
						if (d == 'x' || d == 'X') {
							pos++;
							if (radix == 0) {
								radix = 16;
							}
						} else {
							if (radix == 0) {
								radix = 8;
							}
						}
					}
				}
			}
			if (radix != 8 && radix != 16) {
				radix = 10;
			}
			while (pos < str.Length) {
				int n = -1;
				char c = Char.ToUpper(str[pos]);
				pos++;
				if (Char.IsDigit(c)) {
					n = c - '0';
				} else if (Char.IsLetter(c)) {
					n = (c - 'A') + 10;
				}
				if (n < 0 || n >= radix) {
					break;
				}
				if (radix == 8 && value > (4294967295 / 8)) {
					break;
				} else if (radix == 10 && value > (4294967295 / 10)) {
					break;
				} else if (radix == 16 && value > (4294967295 / 16)) {
					break;
				}
				if (value > 4294967295 - n) {
					break;
				}
				uint v = (value * (uint)radix) + (uint)n;
				if (sign && v > 2147483648) {
					break;
				}
				value = v;
			}
			return sign ? (uint)-value : value;
		}

		public static ulong strtoull(char[] str, int radix)
		{
			ulong value = 0;
			int pos = 0;
			bool sign = false;
			while (pos < str.Length) {
				char c = str[pos];
				if (!Char.IsWhiteSpace(c)) {
					break;
				}
				pos++;
			}
			if (pos < str.Length) {
				char c = str[pos];
				if (c == '+') {
					pos++;
				} else if (c == '-') {
					pos++;
					sign = true;
				}
			}
			if (pos < str.Length) {
				char c = str[pos];
				if (c == '0') {
					pos++;
					if (pos < str.Length) {
						char d = str[pos];
						if (d == 'x' || d == 'X') {
							pos++;
							if (radix == 0) {
								radix = 16;
							}
						} else {
							if (radix == 0) {
								radix = 8;
							}
						}
					}
				}
			}
			if (radix != 8 && radix != 16) {
				radix = 10;
			}
			while (pos < str.Length) {
				int n = -1;
				char c = Char.ToUpper(str[pos]);
				pos++;
				if (Char.IsDigit(c)) {
					n = c - '0';
				} else if (Char.IsLetter(c)) {
					n = (c - 'A') + 10;
				}
				if (n < 0 || n >= radix) {
					break;
				}
				if (radix == 8 && value > (18446744073709551615 / 8)) {
					break;
				} else if (radix == 10 && value > (18446744073709551615 / 10)) {
					break;
				} else if (radix == 16 && value > (18446744073709551615 / 16)) {
					break;
				}
				if (value > 18446744073709551615 - (ulong)n) {
					break;
				}
				ulong v = (value * (ulong)radix) + (ulong)n;
				if (sign && v > 9223372036854775808) {
					break;
				}
				value = v;
			}
			return value;
		}

		public static int strtol(String str, int radix)
		{
			return strtol(str.ToCharArray(), radix);
		}

		public static long strtoll(String str, int radix)
		{
			return strtoll(str.ToCharArray(), radix);
		}

		public static uint strtoul(String str, int radix)
		{
			return strtoul(str.ToCharArray(), radix);
		}

		public static ulong strtoull(String str, int radix)
		{
			return strtoull(str.ToCharArray(), radix);
		}

		class Part {
			public char[] data;
		}

		List<Part> list_;

		String format_;
		int head_;
		int next_;
		bool upper_;
		bool zero_padding_;
		bool align_left_;
		bool force_sign_;
		int width_;
		int precision_;
		int lflag_;

		public strformat(String format)
		{
			format_ = format;
			head_ = 0;
			next_ = 0;
			list_ = new List<Part>();
		}

		public static strformat New(String format)
		{
			return new strformat(format);
		}

		private Part alloc_part(int head, int next)
		{
			Part p = new Part();
			p.data = format_.Substring(head, next - head).ToCharArray();
			return p;
		}

		private static Part alloc_part(char[] arr, int pos, int len)
		{
			Part p = new Part();
			p.data = new char[len];
			Array.Copy(arr, pos, p.data, 0, len);
			return p;
		}

		private static Part alloc_part(String str)
		{
			Part p = new Part();
			p.data = str.ToArray();
			return p;
		}

		private static void add_part(List<Part> list, Part part)
		{
			if (part != null) {
				list.Add(part);
			}
		}

		private static void add_chars(List<Part> list, char c, int n)
		{
			Part p = new Part();
			p.data = new char[n];
			for (int i = 0; i < n; i++) {
				p.data[i] = c;
			}
			add_part(list, p);
		}

		private static Part format_double(double val, int precision, bool trim_unnecessary_zeros, bool force_sign)
		{
			if (Double.IsNaN(val)) return alloc_part("#NAN");
			if (Double.IsInfinity(val)) return alloc_part("#INF");

			bool sign = val < 0;
			if (sign) val = -val;

			if (precision < 0) precision = 0;

			int pt = (val == 0) ? 0 : (int)Math.Floor(Math.Log10(val));
			pt++;
			val *= Math.Pow(10.0, -pt);
			int length = precision;
			if (pt < 0) {
				if (precision > 0 && precision <= -pt) {
					pt = -precision;
					length = 0;
				}
			} else {
				length += pt;
			}
			int significant = Math.Min(length, 17);
			double adjust = Math.Pow(10.0, -significant) * 5;


			char[] tmp = new char[length + 4 + (pt < 0 ? -pt : 0)];
			int ptr = 3;
			int end = ptr;
			int dot = -1;

			if (pt < 0) {
				int n = -pt;
				if (n > precision) {
					n = precision;
				}
				if (n > 0) {
					tmp[end++] = '.';
					for (int i = 0; i < n; i++) {
						tmp[end++] = '0';
					}
					if (length > n) {
						length -= n;
					}
				}
			}

			for (int i = 0; i < length; i++) {
				if (i == pt) {
					dot = end;
					tmp[end++] = '.';
				}
				if (i < significant) {
					val *= 10;
					double v = Math.Floor(val);
					val -= v;
					val += adjust;
					adjust = 0;
					tmp[end++] = (char)(v + '0');
				} else {
					tmp[end++] = '0';
				}
			}

			if (ptr == end) {
				tmp[end++] = '0';
			} else {
				if (tmp[ptr] == '.') {
					tmp[--ptr] = '0';
				}
			}

			if (sign) {
				tmp[--ptr] = '-';
			} else if (force_sign) {
				tmp[--ptr] = '+';
			}

			if (trim_unnecessary_zeros && dot >= 0) {
				while (dot < end) {
					char c = tmp[end - 1];
					if (c == '.') {
						end--;
						break;
					}
					if (c != '0') {
						break;
					}
					end--;
				}
			}

			return alloc_part(tmp, ptr, end - ptr);
		}

		private static Part format_int32(int val, bool force_sign)
		{
			int n = 30;
			char[] tmp = new char[n];
			int end = n - 1;
			int ptr = end;
			tmp[end] = (char)0;

			if (val == 0) {
				tmp[--ptr] = '0';
			} else {
				if (val == (int)1 << 31) {
					tmp[--ptr] = '8';
					val /= 10;
				}
				bool sign = (val < 0);
				if (sign) val = -val;

				while (val != 0) {
					int c = val % 10 + '0';
					val /= 10;
					tmp[--ptr] = (char)c;
				}
				if (sign) {
					tmp[--ptr] = '-';
				} else if (force_sign) {
					tmp[--ptr] = '+';
				}
			}

			return alloc_part(tmp, ptr, end - ptr);
		}

		private static Part format_uint32(uint val)
		{
			int n = 30;
			char[] tmp = new char[n];
			int end = n - 1;
			int ptr = end;
			tmp[end] = (char)0;

			if (val == 0) {
				tmp[--ptr] = '0';
			} else {
				while (val != 0) {
					int c = (int)(val % 10) + '0';
					val /= 10;
					tmp[--ptr] = (char)c;
				}
			}

			return alloc_part(tmp, ptr, end - ptr);
		}

		private static Part format_int64(long val, bool force_sign)
		{
			int n = 30;
			char[] tmp = new char[n];
			int end = n - 1;
			int ptr = end;
			tmp[end] = (char)0;

			if (val == 0) {
				tmp[--ptr] = '0';
			} else {
				if (val == (long)1 << 63) {
					tmp[--ptr] = '8';
					val /= 10;
				}
				bool sign = (val < 0);
				if (sign) val = -val;

				while (val != 0) {
					int c = (int)(val % 10) + '0';
					val /= 10;
					tmp[--ptr] = (char)c;
				}
				if (sign) {
					tmp[--ptr] = '-';
				} else if (force_sign) {
					tmp[--ptr] = '+';
				}
			}

			return alloc_part(tmp, ptr, end - ptr);
		}

		private static Part format_uint64(ulong val)
		{
			int n = 30;
			char[] tmp = new char[n];
			int end = n - 1;
			int ptr = end;
			tmp[end] = (char)0;

			if (val == 0) {
				tmp[--ptr] = '0';
			} else {
				while (val != 0) {
					int c = (int)(val % 10) + '0';
					val /= 10;
					tmp[--ptr] = (char)c;
				}
			}

			return alloc_part(tmp, ptr, end - ptr);
		}

		private static Part format_oct32(uint val, bool upper)
		{
			int n = 30;
			char[] tmp = new char[n];
			int end = n - 1;
			int ptr = end;
			tmp[end] = (char)0;

			String digits = upper ? digits_upper : digits_lower;

			if (val == 0) {
				tmp[--ptr] = '0';
			} else {
				while (val != 0) {
					char c = digits[(int)(val & 7)];
					val >>= 3;
					tmp[--ptr] = c;
				}
			}

			return alloc_part(tmp, ptr, end - ptr);
		}

		private static Part format_oct64(ulong val, bool upper)
		{
			int n = 30;
			char[] tmp = new char[n];
			int end = n - 1;
			int ptr = end;
			tmp[end] = (char)0;

			String digits = upper ? digits_upper : digits_lower;

			if (val == 0) {
				tmp[--ptr] = '0';
			} else {
				while (val != 0) {
					char c = digits[(int)(val & 7)];
					val >>= 3;
					tmp[--ptr] = c;
				}
			}

			return alloc_part(tmp, ptr, end - ptr);
		}

		private static Part format_hex32(uint val, bool upper)
		{
			int n = 30;
			char[] tmp = new char[n];
			int end = n - 1;
			int ptr = end;
			tmp[end] = (char)0;

			String digits = upper ? digits_upper : digits_lower;

			if (val == 0) {
				tmp[--ptr] = '0';
			} else {
				while (val != 0) {
					char c = digits[(int)(val & 15)];
					val >>= 4;
					tmp[--ptr] = c;
				}
			}

			return alloc_part(tmp, ptr, end - ptr);
		}

		private static Part format_hex64(ulong val, bool upper)
		{
			int n = 30;
			char[] tmp = new char[n];
			int end = n - 1;
			int ptr = end;
			tmp[end] = (char)0;

			String digits = upper ? digits_upper : digits_lower;

			if (val == 0) {
				tmp[--ptr] = '0';
			} else {
				while (val != 0) {
					char c = digits[(int)(val & 15)];
					val >>= 4;
					tmp[--ptr] = c;
				}
			}

			return alloc_part(tmp, ptr, end - ptr);
		}

		private void flush()
		{
			if (head_ < next_) {
				Part p = alloc_part(head_, next_);
				list_.Add(p);
				head_ = next_;
			}
		}

		private char charAt(int i)
		{
			if (i < format_.Length) {
				return format_[i];
			}
			return '\0';
		}

		private bool advance(bool complete)
		{
			bool r = false;
			while (charAt(next_) != 0) {
				if (charAt(next_) == '%') {
					if (charAt(next_ + 1) == '%') {
						next_++;
						flush();
						next_++;
						head_ = next_;
					} else if (complete) {
						next_++;
					} else {
						r = true;
						break;
					}
				} else {
					next_++;
				}
			}
			flush();
			return r;
		}

		private int get_number(int alternate_value)
		{
			int value = -1;
			if (charAt(next_) == '*') {
				next_++;
			} else {
				while (true) {
					char c = charAt(next_);
					if (!Char.IsDigit(c)) break;
					if (value < 0) {
						value = 0;
					} else {
						value *= 10;
					}
					value += c - '0';
					next_++;
				}
			}
			if (value < 0) {
				value = alternate_value;
			}
			return value;
		}

		private void format(Func<int, Part> callback, int width, int precision)
		{
			if (advance(false)) {
				if (charAt(next_) == '%') {
					next_++;
				}

				upper_ = false;
				zero_padding_ = false;
				align_left_ = false;
				force_sign_ = false;
				width_ = -1;
				precision_ = -1;
				lflag_ = 0;

				while (true) {
					char c = charAt(next_);
					if (c == '0') {
						zero_padding_ = true;
					} else if (c == '+') {
						force_sign_ = true;
					} else if (c == '-') {
						align_left_ = true;
					} else {
						break;
					}
					next_++;
				}


				width_ = get_number(width);

				if (charAt(next_) == '.') {
					next_++;
				}

				precision_ = get_number(precision);

				while (charAt(next_) == 'l') {
					lflag_++;
					next_++;
				}

				Part p = null;

				{
					char c = charAt(next_);
					if (Char.IsUpper(c)) {
						upper_ = true;
						c = Char.ToLower(c);
					}
					if (Char.IsLetter(c)) {
						p = callback((int)c);
						next_++;
					}
					if (p != null) {
						int padlen = width_ - p.data.Length;
						if (padlen > 0 && !align_left_) {
							if (zero_padding_) {
								char d = p.data[0];
								add_chars(list_, '0', padlen);
								if (d == '+' || d == '-') {
									list_[list_.Count].data[0] = d;
									p.data[0] = '0';
								}
							} else {
								add_chars(list_, ' ', padlen);
							}
						}

						add_part(list_, p);

						if (padlen > 0 && align_left_) {
							add_chars(list_, ' ', padlen);
						}
					}
				}

				head_ = next_;
			}
		}

		private Part format_c(char v)
		{
			Part p = new Part();
			p.data = new char[1];
			p.data[0] = v;
			return p;
		}

		private Part format_f(double value, bool trim_unnecessary_zeros)
		{
			int pr = precision_;
			if (pr < 0) pr = 6;
			return format_double(value, pr, false, force_sign_);
		}

		private Part format(double value, int hint)
		{
			if (hint != 0) {
				switch (hint) {
				case 'c': return format_c((char)value);
				case 'd': return format((long)value, 0);
				case 'u': return format((ulong)value, 0);
				case 'o': return format_o64((ulong)value, 0);
				case 'x': return format_x64((ulong)value, 0);
				case 's': return format_f(value, true);
				}
			}
			return format_f(value, false);
		}

		private Part format(int value, int hint)
		{
			if (hint != 0) {
				switch (hint) {
				case 'c': return format_c((char)value);
				case 'u': return format((uint)value, 0);
				case 'o': return format_o32((uint)value, 0);
				case 'x': return format_x32((uint)value, 0);
				case 'f': return format((double)value, 0);
				}
			}
			return format_int32(value, force_sign_);
		}

		private Part format(uint value, int hint)
		{
			if (hint != 0) {
				switch (hint) {
				case 'c': return format_c((char)value);
				case 'd': return format((int)value, 0);
				case 'o': return format_o32(value, 0);
				case 'x': return format_x32(value, 0);
				case 'f': return format((double)value, 0);
				}
			}
			return format_uint32(value);
		}

		private Part format(long value, int hint)
		{
			if (hint != 0) {
				switch (hint) {
				case 'c': return format_c((char)value);
				case 'u': return format((ulong)value, 0);
				case 'o': return format_o64((ulong)value, 0);
				case 'x': return format_x64((ulong)value, 0);
				case 'f': return format((double)value, 0);
				}
			}
			return format_int64(value, force_sign_);
		}

		private Part format(ulong value, int hint)
		{
			if (hint != 0) {
				switch (hint) {
				case 'c': return format_c((char)value);
				case 'd': return format((long)value, 0);
				case 'o': return format_oct64(value, false);
				case 'x': return format_hex64(value, false);
				case 'f': return format((double)value, 0);
				}
			}
			return format_uint64(value);
		}

		private Part format(String value, int hint)
		{
			if (hint != 0) {
				switch (hint) {
				case 'c': {
						int v = strtol(value, 10);
						return format(v, 0);
					}
				case 'd':
					if (lflag_ == 0) {
						int v = strtol(value, 0);
						return format(v, 0);
					} else {
						long v = strtoll(value, 0);
						return format(v, 0);
					}
				case 'u':
					if (lflag_ == 0) {
						uint v = strtoul(value, 0);
						return format(v, 0);
					} else {
						ulong v = strtoull(value, 0);
						return format(v, 0);
					}
				case 'o':
					if (lflag_ == 0) {
						uint v = strtoul(value, 0);
						return format_oct32(v, upper_);
					} else {
						ulong v = strtoull(value, 0);
						return format_oct64(v, upper_);
					}
				case 'x':
					if (lflag_ == 0) {
						uint v = strtoul(value, 0);
						return format_hex32(v, upper_);
					} else {
						ulong v = strtoull(value, 0);
						return format_hex64(v, upper_);
					}
				case 'f': {
						double v = Double.Parse(value);
						return format(v, 0);
					}
				}
			}
			return alloc_part(value);
		}

		private Part format_o32(uint value, int hint)
		{
			if (hint != 0) {
				switch (hint) {
				case 'c': return format_c((char)value);
				case 'd': return format(value, 0);
				case 'u': return format(value, 0);
				case 'x': return format_x32(value, 0);
				case 'f': return format((double)value, 0);
				}
			}
			return format_oct32(value, upper_);
		}

		private Part format_o64(ulong value, int hint)
		{
			if (hint != 0) {
				switch (hint) {
				case 'c': return format_c((char)value);
				case 'd': return format((long)value, 0);
				case 'u': return format(value, 0);
				case 'x': return format_x64(value, 0);
				case 'f': return format((double)value, 0);
				}
			}
			return format_oct64(value, upper_);
		}

		private Part format_x32(uint value, int hint)
		{
			if (hint != 0) {
				switch (hint) {
				case 'c': return format_c((char)value);
				case 'd': return format(value, 0);
				case 'u': return format(value, 0);
				case 'o': return format_o32(value, 0);
				case 'f': return format((double)value, 0);
				}
			}
			return format_hex32(value, upper_);
		}

		private Part format_x64(ulong value, int hint)
		{
			if (hint != 0) {
				switch (hint) {
				case 'c': return format_c((char)value);
				case 'd': return format((long)value, 0);
				case 'u': return format(value, 0);
				case 'o': return format_o64(value, 0);
				case 'f': return format((double)value, 0);
				}
			}
			return format_hex64(value, upper_);
		}

		public strformat a(Object v)
		{
			format((c) => {
				return alloc_part(v.ToString());
			}, -1, -1);
			return this;
		}

		public strformat f(double v)
		{
			format((hint) => {
				return format(v, hint);
			}, -1, -1);
			return this;
		}

		public strformat c(char v)
		{
			format((hint) => {
				return format(v, hint);
			}, -1, -1);
			return this;
		}

		public strformat d(int v)
		{
			format((hint) => {
				return format(v, hint);
			}, -1, -1);
			return this;
		}

		public strformat u(uint v)
		{
			format((hint) => {
				return format(v, hint);
			}, -1, -1);
			return this;
		}

		public strformat ld(long v)
		{
			format((hint) => {
				return format(v, hint);
			}, -1, -1);
			return this;
		}

		public strformat lu(ulong v)
		{
			format((hint) => {
				return format(v, hint);
			}, -1, -1);
			return this;
		}

		public strformat x(uint v)
		{
			format((hint) => {
				return format(v, hint);
			}, -1, -1);
			return this;
		}

		public strformat lx(ulong v)
		{
			format((hint) => {
				return format(v, hint);
			}, -1, -1);
			return this;
		}

		public strformat o(uint v)
		{
			format((hint) => {
				return format(v, hint);
			}, -1, -1);
			return this;
		}

		public strformat lo(ulong v)
		{
			format((hint) => {
				return format(v, hint);
			}, -1, -1);
			return this;
		}

		public strformat s(String v)
		{
			format((hint) => {
				return format(v, hint);
			}, -1, -1);
			return this;
		}

		public override String ToString()
		{
			advance(true);
			StringBuilder sb = new StringBuilder();
			foreach (Part p in list_) {
				sb.Append(p.data);
			}
			return sb.ToString();
		}

		public void PrintOut()
		{
			String s = ToString();
			Console.Write(s);
		}

		public void PrintErr()
		{
			String s = ToString();
			Console.Error.Write(s);
		}

	}
}
