using Soramimi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatter {
	class Test {
		static void Main(string[] args)
		{
			strformat.New("(%010.4f)").f(123.456789).PrintOut();
		}
	}
}
