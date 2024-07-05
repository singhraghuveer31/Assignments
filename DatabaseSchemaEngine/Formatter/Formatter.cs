namespace DatabaseSchemaEngine.Formatter
{
	public static class Formatter
	{
		private static Dictionary<string, List<IFormatRule>> _formatters = new Dictionary<string, List<IFormatRule>>();

		public static void RegisterFormatterFor(string typeName, IFormatRule formatRule)
		{
			if (_formatters.ContainsKey(typeName))
			{
				_formatters[typeName].Add(formatRule);
			}
			else
			{
				_formatters.Add(typeName, new List<IFormatRule> { formatRule });
			}
		}

		public static List<IFormatRule> GetFormatterFor(string typeName)
		{
			return _formatters[typeName];
		}

		public static void Format<T>(this T entity) where T : IFormattable
		{
			List<IFormatRule> formatRules = GetFormatterFor(entity.GetType().Name);
			foreach (var rule in formatRules)
			{
				entity.Format(rule);
			}
		}
	}
}
