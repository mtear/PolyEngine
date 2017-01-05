namespace PolyEngine
{
	public class Object
	{


		#region Public Variables

		public string name = "";
		//Hideflags TODO

		#endregion Public Variables


		#region Private Variables

		private static int instanceID = 0;
		private int m_instanceID = 0;

		#endregion Private Variables


		#region Constructors

		public Object()
		{
			m_instanceID = instanceID++;
		}

		#endregion Constructors


		#region Public Functions

		public int GetInstanceID()
		{
			return m_instanceID;
		}

		public override string ToString()
		{
			return name;
		}

		#endregion Public Functions


		#region Operators

		public static explicit operator bool(Object x)
		{
			return x != null;
		}

		#endregion Operators


	}
}

