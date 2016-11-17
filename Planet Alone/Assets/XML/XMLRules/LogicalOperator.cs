//
// LogicalOperator.cs.cs
//
// This file was generated by XMLSPY 2004 Enterprise Edition.
//
// YOU SHOULD NOT MODIFY THIS FILE, BECAUSE IT WILL BE
// OVERWRITTEN WHEN YOU RE-RUN CODE GENERATION.
//
// Refer to the XMLSPY Documentation for further details.
// http://www.altova.com/xmlspy
//


using System;
using System.Collections;
using System.Xml;
using Altova.Types;

namespace XMLRules
{
	public class LogicalOperator : Altova.Node
	{
		#region Forward constructors
		public LogicalOperator() : base() { SetCollectionParents(); }
		public LogicalOperator(XmlDocument doc) : base(doc) { SetCollectionParents(); }
		public LogicalOperator(XmlNode node) : base(node) { SetCollectionParents(); }
		public LogicalOperator(Altova.Node node) : base(node) { SetCollectionParents(); }
		#endregion // Forward constructors

		public override void AdjustPrefix()
		{
			int nCount;

			nCount = DomChildCount(NodeType.Element, "", "And");
			for (int i = 0; i < nCount; i++)
			{
				XmlNode DOMNode = GetDomChildAt(NodeType.Element, "", "And", i);
				InternalAdjustPrefix(DOMNode, true);
			}

			nCount = DomChildCount(NodeType.Element, "", "Or");
			for (int i = 0; i < nCount; i++)
			{
				XmlNode DOMNode = GetDomChildAt(NodeType.Element, "", "Or", i);
				InternalAdjustPrefix(DOMNode, true);
			}
		}


		#region And accessor methods
		public int GetAndMinCount()
		{
			return 1;
		}

		public int AndMinCount
		{
			get
			{
				return 1;
			}
		}

		public int GetAndMaxCount()
		{
			return 1;
		}

		public int AndMaxCount
		{
			get
			{
				return 1;
			}
		}

		public int GetAndCount()
		{
			return DomChildCount(NodeType.Element, "", "And");
		}

		public int AndCount
		{
			get
			{
				return DomChildCount(NodeType.Element, "", "And");
			}
		}

		public bool HasAnd()
		{
			return HasDomChild(NodeType.Element, "", "And");
		}

		public SchemaString GetAndAt(int index)
		{
			return new SchemaString(GetDomNodeValue(GetDomChildAt(NodeType.Element, "", "And", index)));
		}

		public SchemaString GetAnd()
		{
			return GetAndAt(0);
		}

		public SchemaString And
		{
			get
			{
				return GetAndAt(0);
			}
		}

		public void RemoveAndAt(int index)
		{
			RemoveDomChildAt(NodeType.Element, "", "And", index);
		}

		public void RemoveAnd()
		{
			while (HasAnd())
				RemoveAndAt(0);
		}

		public void AddAnd(SchemaString newValue)
		{
			AppendDomChild(NodeType.Element, "", "And", newValue.ToString());
		}

		public void InsertAndAt(SchemaString newValue, int index)
		{
			InsertDomChildAt(NodeType.Element, "", "And", index, newValue.ToString());
		}

		public void ReplaceAndAt(SchemaString newValue, int index)
		{
			ReplaceDomChildAt(NodeType.Element, "", "And", index, newValue.ToString());
		}
		#endregion // And accessor methods

		#region And collection
        public AndCollection	MyAnds = new AndCollection( );

        public class AndCollection: IEnumerable
        {
            LogicalOperator parent;
            public LogicalOperator Parent
			{
				set
				{
					parent = value;
				}
			}
			public AndEnumerator GetEnumerator() 
			{
				return new AndEnumerator(parent);
			}
		
			IEnumerator IEnumerable.GetEnumerator() 
			{
				return GetEnumerator();
			}
        }

        public class AndEnumerator: IEnumerator 
        {
			int nIndex;
			LogicalOperator parent;
			public AndEnumerator(LogicalOperator par) 
			{
				parent = par;
				nIndex = -1;
			}
			public void Reset() 
			{
				nIndex = -1;
			}
			public bool MoveNext() 
			{
				nIndex++;
				return(nIndex < parent.AndCount );
			}
			public SchemaString  Current 
			{
				get 
				{
					return(parent.GetAndAt(nIndex));
				}
			}
			object IEnumerator.Current 
			{
				get 
				{
					return(Current);
				}
			}
    	}
	
        #endregion // And collection

		#region Or accessor methods
		public int GetOrMinCount()
		{
			return 1;
		}

		public int OrMinCount
		{
			get
			{
				return 1;
			}
		}

		public int GetOrMaxCount()
		{
			return 1;
		}

		public int OrMaxCount
		{
			get
			{
				return 1;
			}
		}

		public int GetOrCount()
		{
			return DomChildCount(NodeType.Element, "", "Or");
		}

		public int OrCount
		{
			get
			{
				return DomChildCount(NodeType.Element, "", "Or");
			}
		}

		public bool HasOr()
		{
			return HasDomChild(NodeType.Element, "", "Or");
		}

		public SchemaString GetOrAt(int index)
		{
			return new SchemaString(GetDomNodeValue(GetDomChildAt(NodeType.Element, "", "Or", index)));
		}

		public SchemaString GetOr()
		{
			return GetOrAt(0);
		}

		public SchemaString Or
		{
			get
			{
				return GetOrAt(0);
			}
		}

		public void RemoveOrAt(int index)
		{
			RemoveDomChildAt(NodeType.Element, "", "Or", index);
		}

		public void RemoveOr()
		{
			while (HasOr())
				RemoveOrAt(0);
		}

		public void AddOr(SchemaString newValue)
		{
			AppendDomChild(NodeType.Element, "", "Or", newValue.ToString());
		}

		public void InsertOrAt(SchemaString newValue, int index)
		{
			InsertDomChildAt(NodeType.Element, "", "Or", index, newValue.ToString());
		}

		public void ReplaceOrAt(SchemaString newValue, int index)
		{
			ReplaceDomChildAt(NodeType.Element, "", "Or", index, newValue.ToString());
		}
		#endregion // Or accessor methods

		#region Or collection
        public OrCollection	MyOrs = new OrCollection( );

        public class OrCollection: IEnumerable
        {
            LogicalOperator parent;
            public LogicalOperator Parent
			{
				set
				{
					parent = value;
				}
			}
			public OrEnumerator GetEnumerator() 
			{
				return new OrEnumerator(parent);
			}
		
			IEnumerator IEnumerable.GetEnumerator() 
			{
				return GetEnumerator();
			}
        }

        public class OrEnumerator: IEnumerator 
        {
			int nIndex;
			LogicalOperator parent;
			public OrEnumerator(LogicalOperator par) 
			{
				parent = par;
				nIndex = -1;
			}
			public void Reset() 
			{
				nIndex = -1;
			}
			public bool MoveNext() 
			{
				nIndex++;
				return(nIndex < parent.OrCount );
			}
			public SchemaString  Current 
			{
				get 
				{
					return(parent.GetOrAt(nIndex));
				}
			}
			object IEnumerator.Current 
			{
				get 
				{
					return(Current);
				}
			}
    	}
	
        #endregion // Or collection

        private void SetCollectionParents()
        {
            MyAnds.Parent = this; 
            MyOrs.Parent = this; 
	}
}
}
