using Soramimi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter {
	class Test {
		static void Main(string[] args)
		{
//			bool f = int.TryParse("123abc", System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out n);
			strformat.New("(%d)").s("-0x50").PrintOut();
		}
	}
}
