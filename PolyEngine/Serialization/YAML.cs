using System;
using System.Collections.Generic;
using System.IO;

namespace PolyEngine.Serialization
{
	public class YAML
	{
		public static void Parse(string scenename)
		{
			List<string> lines = new List<string>(File.ReadAllLines("Content/" + scenename + ".unity"));
			int headers = 0;
			bool inobject = false;
			String classname = "";

			for (int i = 0; i < lines.Count; i++)
			{
				string line = lines[i].Trim();
				if (line == ""){}
				else if (line == "%YAML 1.1"){headers++;}
				else if (line == "%TAG !u! tag:unity3d.com,2011:"){headers++;}
				else {
					if (headers != 2) return;

					//Out of header zone
					//Being state machine
					if (!inobject)
					{
						if (!line.StartsWith("--- !u!")) return;
						inobject = true;
						string meat = line.Substring(7);
						string[] chunks = meat.Split(' ');
						string mclassname = classnames[int.Parse(chunks[0])];
						classname = (mclassname.Contains(".")) ? mclassname.Substring(mclassname.LastIndexOf('.'))
						                                                   : mclassname;
						Type type = Type.GetType(mclassname);
						Object myObject = (Object)Activator.CreateInstance(type);
					}
					else { //In Object

					}

				}
			}

		}

		private class YObj
		{
			List<string> lines = new List<string>();
			List<YObj> objects = new List<YObj>();
		}





		public static string[] classnames = new string[2]{
			"",
			"PolyEngine.GameObject"
		};
	}
}

