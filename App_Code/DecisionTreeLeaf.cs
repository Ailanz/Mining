using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Leaf of the decision tree 

public class DecisionTreeLeaf : DecisionTree
{
    List<Object> leafValueMap = new List<Object>();
	public DecisionTreeLeaf()
	{

	}

    public DecisionTreeLeaf(String value) : base(value)
    {

    }

    public void AddLeafValue(Object value)
    {
        this.leafValueMap.Add(value);
    }

    public void AddLeafValue(List<Object> values)
    {
        if (values != null)
        {
            this.leafValueMap.AddRange(values);
        }
    }

    public override Dictionary<String, int> GetValuesCount(Operand op)
    {
        var valueMap = new Dictionary<string, int>();
        foreach(Object obj in leafValueMap)
        {
            if (op.Evaluate(this.node.value, obj))
            {
                int count = 0;
                bool assigned = valueMap.TryGetValue(obj.ToString(), out count);
                if (assigned)
                {
                    valueMap[obj.ToString()] = count + 1;
                }
                else
                {
                    valueMap[obj.ToString()] = 1;
                }
            }
        }
        return valueMap;
    }
}