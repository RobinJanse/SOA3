namespace Domain.SCM
{
	public class Code
	{
		private readonly string code;

		public Code(string code)
		{
			this.code = code;
		}

		public string GetCode()
		{
			return code;
		}
	}
}