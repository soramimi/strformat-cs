using System;

namespace soramimi {
    class Example {
    	static void Main(string[] args)
		{
			strformat x = new strformat("The quick brown %s jumps the laze %s\n");
			x.s("mouse").s("cat").put();

			string s = ((strformat)"The %s library, Copyright (C) %d %s\n")["strformat"][2022]["S.Fuchita"];
			Console.Write(s);
		}
    }
}