namespace DatabaseSchemaEngine.Helper
{
	public class Map<T1, T2> where T1 : notnull where T2 : notnull
	{
		private readonly Dictionary<T1, T2> forward = new();
		private readonly Dictionary<T2, T1> reverse = new();

		public Map()
		{
			this.Forward = new Indexer<T1, T2>(forward);
			this.Reverse = new Indexer<T2, T1>(reverse);
		}

		public class Indexer<T3, T4> where T3 : notnull
		{
			private Dictionary<T3, T4> dictionary;
			public Indexer(Dictionary<T3, T4> dictionary)
			{
				this.dictionary = dictionary;
			}
			public T4 this[T3 index]
			{
				get { return dictionary[index]; }
				set { dictionary[index] = value; }
			}
			public bool ContainsKey(T3 key) 
			{
				return dictionary.ContainsKey(key);
			}
		}

		public void Add(T1 t1, T2 t2)
		{
			forward.Add(t1, t2);
			reverse.Add(t2, t1);
		}

		public Indexer<T1, T2> Forward { get; private set; }
		public Indexer<T2, T1> Reverse { get; private set; }
	}
}
