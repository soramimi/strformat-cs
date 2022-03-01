using System;
using System.Collections.Generic;
using System.Text;

/**
* strformat : printf compatible string formatter
* Copyright (C) S.Fuchita (@soramimi_jp)
*/

namespace soramimi {
	class strformat {
		string text_;
		class Part {
			public string s;
			public Part(string str)
			{
				s = str;
			}
		}
		List<Part> parts_;
		public strformat(string f)
		{
			text_ = f;
			reset();
		}
		int head_;
		int next_;
		int lflag_;
		int width_;
		int precision_;
		bool force_sign_;
		public void reset()
		{
			parts_ = new List<Part>();
			head_ = 0;
			next_ = 0;
			lflag_ = 0;
			width_ = 0;
			precision_ = 0;
			force_sign_ = false;
		}
		void flush()
		{
			string s = text_.Substring(head_, next_ - head_);
			parts_.Add(new Part(s));
			head_ = next_;
		}
		char nextchar(int offset = 0)
		{
			return (next_ + offset < text_.Length) ? text_[next_ + offset] : '\0';
		}
		bool advance(bool complete)
		{
			bool r = false;
			while (next_ < text_.Length) {
				if (nextchar() == '%') {
					if (nextchar(1) == '%') {
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
		int getnum(int alternate_value)
		{
			int value = -1;
			if (nextchar() == '*') {
				next_++;
			} else {
				while (true) {
					char c = nextchar();
					if (!Char.IsDigit(c))
						break;
					if (value < 0) {
						value = 0;
					} else {
						value *= 10;
					}
					value += (int)(c - '0');
					next_++;
				}
			}
			if (value < 0) {
				value = alternate_value;
			}
			return value;
		}
		class NumberParser {
			int radix_ = 10;
			bool sign_ = false;
			ulong value_u64_ = 0;
			double value_dbl_ = 0;
			double value_dbl_den_ = 0;
			public NumberParser(string s)
			{
				int i = 0;
				while (i < s.Length && Char.IsWhiteSpace(s[i])) {
					i++;
				}
				if (i < s.Length) {
					if (s[i] == '+') {
						i++;
					} else if (s[i] == '-') {
						sign_ = true;
						i++;
					}
					int k = i;
					if (i < s.Length && s[i] == '0') {
						if (i + 1 < s.Length && s[i] == 'x') {
							i += 2;
							radix_ = 16;
						} else {
							int j = i + 1;
							while (j < s.Length) {
								char c = s[j];
								if (c < '0' || c > '7') {
									break;
								}
								radix_ = 8;
								j++;
							}
						}
					}
					i = k;
					while (i < s.Length) {
						int n = -1;
						char c = s[i];
						if (Char.IsDigit(c)) {
							n = (int)(c - '0');
						} else {
							c = Char.ToUpper(c);
							if (c >= 'A' && c <= 'F') {
								n = (int)(c - 'A');
							}
						}
						if (n < 0 || n >= radix_) {
							break;
						}
						i++;
						value_u64_ = value_u64_ * (ulong)radix_ + (ulong)n;
						value_dbl_ = value_dbl_ * 10 + n;
					}
					if (i < s.Length) {
						char c = s[i];
						if (radix_ == 10 && (c == '.' || c == 'e' || c == 'E')) {
							value_dbl_den_ = 1;
							i++;
							if (c == '.') {
								while (i < s.Length) {
									c = s[i];
									if (Char.IsDigit(c)) {
										i++;
										int n = (int)(c - '0');
										value_dbl_ = value_dbl_ * 10 + n;
										value_dbl_den_ *= 10;
									} else {
										break;
									}
								}
							}
							if (c == 'e' || c == 'E') {
								i++;
								bool sign = false;
								if (i < s.Length) {
									c = s[i];
									if (c == '+') {
										i++;
									} else if (c == '-') {
										sign = true;
										i++;
									}
									int e = 0;
									while (i < s.Length) {
										c = s[i];
										if (!Char.IsDigit(c)) {
											break;
										}
										e = e * 10 + (int)(c - '0');
										i++;
									}
									if (e != 0) {
										if (sign) {
											value_dbl_den_ *= Math.Pow(10, e);
										} else {
											value_dbl_ *= Math.Pow(10, e);
										}
									}
								}
							}
							value_u64_ = (ulong)(value_dbl_ / value_dbl_den_);
						} else {
							while (i < s.Length) {
								c = s[i];
								if (!Char.IsDigit(c)) {
									break;
								}
								i++;
								int n = (int)(c - '0');
								value_u64_ = value_u64_ * (ulong)radix_ + (ulong)n;
							}
							value_dbl_ = value_u64_;
						}
					}
				}
			}
			public bool isDouble()
			{
				return value_dbl_den_ != 0;
			}
			public int value_i32()
			{
				return (int)value_u64();
			}
			public uint value_u32()
			{
				return (uint)value_u64();
			}
			public long value_i64()
			{
				long v = (long)value_u64_;
				if (sign_) {
					v = -v;
				}
				return v;
			}
			public ulong value_u64()
			{
				ulong v = value_u64_;
				if (sign_) {
					v = (ulong)-(long)v;
				}
				return v;
			}
			public double value_dbl()
			{
				double v = (value_dbl_den_ != 0) ? (value_dbl_ / value_dbl_den_) : value_dbl_;
				if (sign_) {
					v = -v;
				}
				return v;
			}
		}
		int num_i32(string s)
		{
			NumberParser p = new NumberParser(s);
			return (int)p.value_u64();
		}
		long num_i64(string s)
		{
			NumberParser p = new NumberParser(s);
			return (long)p.value_u64();
		}
		uint num_u32(string s)
		{
			NumberParser p = new NumberParser(s);
			return (uint)p.value_u64();
		}
		ulong num_u64(string s)
		{
			NumberParser p = new NumberParser(s);
			return (ulong)p.value_u64();
		}
		double num_dbl(string s)
		{
			NumberParser p = new NumberParser(s);
			return p.value_dbl();
		}
		static string format_dbl(double val, int precision, bool trim_unnecessary_zeros, bool force_sign)
		{
			if (Double.IsNaN(val)) {
				return "#NAN";
			}
			if (Double.IsInfinity(val)) {
				return "#INF";
			}

			bool sign = val < 0;
			if (sign) {
				val = -val;
			}

			if (precision < 0) {
				precision = 0;
			}

			int pt = (val == 0) ? 0 : (int)Math.Floor(Math.Log10(val));
			pt++;
			val *= Math.Pow(10, -pt);
			int length = precision;
			if (pt < 0) {
				if (precision > 0 && precision <= -pt) {
					pt = -precision;
					length = 0;
				} else {
					length += pt;
				}
			}
			int significant = Math.Max(length, 17);
			double adjust = Math.Pow(10, -significant) * 5;

			char[] buffer = new char[length + 4 + (pt < 0 ? -pt : 0)];
			int ptr = 3;
			int end = 3;
			int dot = -1;

			if (pt < 0) {
				int n = -pt;
				if (n > precision) {
					n = precision;
				}
				if (n > 0) {
					buffer[end++] = '0';
				}
				if (length > n) {
					length -= n;
				}
			}

			for (int i = 0; i < length; i++) {
				if (i == pt) {
					dot = end;
					buffer[end++] = '.';
				}
				if (i < significant) {
					val *= 10;
					double v = Math.Floor(val);
					val -= v;
					val += adjust;
					adjust = 0;
					buffer[end++] = (char)((int)v + (int)'0');
				} else {
					buffer[end++] = '0';
				}
			}

			if (ptr == end) {
				buffer[end++] = '0';
			} else {
				if (buffer[ptr] == '.') {
					buffer[--ptr] = '0';
				}
			}

			if (sign) {
				buffer[--ptr] = '-';
			} else if (force_sign) {
				buffer[--ptr] = '+';
			}

			if (trim_unnecessary_zeros && dot >= 0) {
				while (dot < end) {
					char c = buffer[end - 1];
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

			return new string(buffer, ptr, end - ptr);
		}
		static string format_i32(int value, bool force_sign)
		{
			int n = 30;
			char[] buffer = new char[n];
			int end = n - 1;
			int ptr = end;

			if (value == 0) {
				buffer[--ptr] = '0';
			} else {
				if (value == 1 << 31) {
					buffer[--ptr] = '8';
					value /= 10;
				}
				bool sign = value < 0;
				if (sign) {
					value = -value;
				}

				while (value != 0) {
					char c = (char)(value % 10 + (int)'0');
					value /= 10;
					buffer[--ptr] = c;
				}
				if (sign) {
					buffer[--ptr] = '-';
				} else if (force_sign) {
					buffer[--ptr] = '+';
				}
			}

			return new string(buffer, ptr, end - ptr);
		}
		static string format_u32(uint value, bool force_sign)
		{
			int n = 30;
			char[] buffer = new char[n];
			int end = n - 1;
			int ptr = end;

			if (value == 0) {
				buffer[--ptr] = '0';
			} else {
				while (value != 0) {
					char c = (char)(value % 10 + (int)'0');
					value /= 10;
					buffer[--ptr] = c;
				}
			}

			return new string(buffer, ptr, end - ptr);
		}
		static string format_i64(long value, bool force_sign)
		{
			int n = 30;
			char[] buffer = new char[n];
			int end = n - 1;
			int ptr = end;

			if (value == 0) {
				buffer[--ptr] = '0';
			} else {
				if (value == 1 << 63) {
					buffer[--ptr] = '8';
					value /= 10;
				}
				bool sign = value < 0;
				if (sign) {
					value = -value;
				}

				while (value != 0) {
					char c = (char)(value % 10 + (int)'0');
					value /= 10;
					buffer[--ptr] = c;
				}
				if (sign) {
					buffer[--ptr] = '-';
				} else if (force_sign) {
					buffer[--ptr] = '+';
				}
			}

			return new string(buffer, ptr, end - ptr);
		}
		static string format_u64(ulong value, bool force_sign)
		{
			int n = 30;
			char[] buffer = new char[n];
			int end = n - 1;
			int ptr = end;

			if (value == 0) {
				buffer[--ptr] = '0';
			} else {
				while (value != 0) {
					char c = (char)(value % 10 + (int)'0');
					value /= 10;
					buffer[--ptr] = c;
				}
			}

			return new string(buffer, ptr, end - ptr);
		}
		string format_d(int value)
		{
			return format_i32(value, force_sign_);
		}
		string format_ld(long value)
		{
			return format_i64(value, force_sign_);
		}
		string format_u(uint value)
		{
			return format_u32(value, force_sign_);
		}
		string format_lu(ulong value)
		{
			return format_u64(value, force_sign_);
		}
		string format_f(double value, bool trim_unnecessary_zeros)
		{
			int pr = precision_ < 0 ? 6 : precision_;
			return format_dbl(value, pr, trim_unnecessary_zeros, force_sign_);
		}
		void format(Func<char, string> fn, int width, int precision)
		{
			if (advance(false)) {
				if (nextchar() == '%') {
					next_++;
				}
			}

			force_sign_ = false;
			lflag_ = 0;

			width_ = getnum(width_);

			if (nextchar() == '.') {
				next_++;
			}

			precision_ = getnum(precision);

			while (nextchar() == 'l') {
				lflag_++;
				next_++;
			}
			char c = nextchar();
			if (Char.IsLetter(c)) {
				next_++;
			}
			if (fn != null) {
				parts_.Add(new Part(fn(c)));
			}
			head_ = next_;
		}
		public strformat a<T>(T t, int width = -1, int precision = -1)
		{
			format(new Func<char, string>((char hint) => {
				if (typeof(T) == typeof(string)) {
					string s = t.ToString();
					switch (hint) {
					case 'd':
						return (lflag_ == 0) ? format_d(num_i32(s)) : format_ld(num_i64(s));
					case 'u':
						return (lflag_ == 0) ? format_u(num_u32(s)) : format_lu(num_u64(s));
					case 'f':
						return format_f(num_dbl(s), false);
					case 'c':
						return ((char)num_i32(s)).ToString();
					}
					return s;
				} else if (typeof(T) == typeof(int)) {
					int v = (int)(object)t;
					switch (hint) {
					case 'd':
						return (lflag_ == 0) ? format_d((int)v) : format_ld((long)v);
					case 'u':
						return (lflag_ == 0) ? format_u((uint)v) : format_lu((ulong)v);
					case 'f':
						return format_f(v, false);
					case 'c':
						return ((char)v).ToString();
					}
				} else if (typeof(T) == typeof(uint)) {
					uint v = (uint)(object)t;
					switch (hint) {
					case 'd':
						return (lflag_ == 0) ? format_d((int)v) : format_ld((long)v);
					case 'u':
						return (lflag_ == 0) ? format_u((uint)v) : format_lu((ulong)v);
					case 'f':
						return format_f(v, false);
					case 'c':
						return ((char)v).ToString();
					}
				} else if (typeof(T) == typeof(long)) {
					long v = (long)(object)t;
					switch (hint) {
					case 'd':
						return (lflag_ == 0) ? format_d((int)v) : format_ld((long)v);
					case 'u':
						return (lflag_ == 0) ? format_u((uint)v) : format_lu((ulong)v);
					case 'f':
						return format_f(v, false);
					case 'c':
						return ((char)v).ToString();
					}
				} else if (typeof(T) == typeof(ulong)) {
					ulong v = (ulong)(object)t;
					switch (hint) {
					case 'd':
						return (lflag_ == 0) ? format_d((int)v) : format_ld((long)v);
					case 'u':
						return (lflag_ == 0) ? format_u((uint)v) : format_lu((ulong)v);
					case 'f':
						return format_f(v, false);
					case 'c':
						return ((char)v).ToString();
					}
				} else if (typeof(T) == typeof(double)) {
					double v = (double)(object)t;
					if (Double.IsNaN(v)) {
						return "#NAN";
					}
					if (Double.IsInfinity(v)) {
						return "#INF";
					}
					switch (hint) {
					case 'd':
						return (lflag_ == 0) ? format_d((int)v) : format_ld((long)v);
					case 'u':
						return (lflag_ == 0) ? format_u((uint)v) : format_lu((ulong)v);
					case 'f':
						return format_f(v, false);
					case 's':
						return format_f(v, true);
					case 'c':
						return ((char)v).ToString();
					}
				} else if (typeof(T) == typeof(char)) {
					char v = (char)(object)t;
					switch (hint) {
					case 'd':
						return (lflag_ == 0) ? format_d((int)v) : format_ld((long)v);
					case 'u':
						return (lflag_ == 0) ? format_u((uint)v) : format_lu((ulong)v);
					case 'f':
						return format_f(v, false);
					case 'c':
						return ((char)v).ToString();
					}
				}
				return t.ToString();
			}), width, precision);
			return this;
		}
		public strformat s(string s, int width = -1, int precision = -1)
		{
			format(new Func<char, string>((char hint) => { return s; }), width, precision);
			return this;
		}
		public strformat c(char v, int width = -1, int precision = -1)
		{
			return a(v, width, precision);
		}
		public strformat f(double v, int width = -1, int precision = -1)
		{
			return a(v, width, precision);
		}
		public strformat d(int v, int width = -1, int precision = -1)
		{
			if (lflag_ > 0) {
				return ld(v);
			} else {
				format(new Func<char, string>((char hint) => { return Convert.ToString(v); }), width, precision);
				return this;
			}
		}
		public strformat ld(long v, int width = -1, int precision = -1)
		{
			return a(v, width, precision);
		}
		public strformat u(uint v, int width = -1, int precision = -1)
		{
			return a(v, width, precision);
		}
		public strformat lu(ulong v, int width = -1, int precision = -1)
		{
			return a(v, width, precision);
		}

		public string str()
		{
			StringBuilder sb = new StringBuilder();
			advance(true);
			for (int i = 0; i < parts_.Count; i++) {
				Part p = parts_[i];
				sb.Append(p.s);
			}
			return sb.ToString();
		}
		public void put()
		{
			Console.Write(str());
		}
		public void err()
		{
			Console.Error.Write(str());
		}
		public override string ToString()
		{
			return str();
		}

		public static implicit operator strformat(string a)
		{
			return new strformat(a);
		}
		public static implicit operator string(strformat a)
		{
			return a.str();
		}

		public strformat this[int v] => a(v);
		public strformat this[uint v] => a(v);
		public strformat this[long v] => a(v);
		public strformat this[ulong v] => a(v);
		public strformat this[double v] => a(v);
		public strformat this[char v] => a(v);
		public strformat this[object v] => a(v.ToString());
	}
}

