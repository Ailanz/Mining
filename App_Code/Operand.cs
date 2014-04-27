using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// Used for conditions in SQL
public class Operand
{
    public enum Comparison { EQUALS, LESS_THAN, LESS_EQUALS_THAN, GREATER_THAN, GREATER_EQUALS_THAN }
    Dictionary<String, Object> equalsList = new Dictionary<String, Object>(); //<ColumnName, Value>
    List<String> groupByList = new List<String>();
    bool distinct = false;
    bool failFlag = false;
	public Operand()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool IsEmpty()
    {
        return equalsList.Count == 0;
    }

    public bool Evaluate(String key, Object value)
    {
        if (failFlag) return false;
        // 1: Check Equals. If it does not contain restriction or if restriction matches, true.
        if(!equalsList.ContainsKey(key) || value.Equals(equalsList[key]))
        {
            return true;
        }
        //TODO: Check other operations
        return false;
    }

    public Operand Add(Comparison compare, String key, Object value)
    {
        if (compare.Equals(Comparison.EQUALS))
        {
            equalsList[key] = value;
        }
        return this;
    }

    public Operand AddGroupBy(String key)
    {
        groupByList.Add(key);
        return this;
    }

    public Operand SetDistinct(bool distinct)
    {
        this.distinct = distinct;
        return this;
    }

    public Dictionary<String, Object> GetEqualRules()
    {
        return equalsList;
    }

    public static Operand Clone(Operand op)
    {
        Operand cloned = new Operand();
        foreach(var kv in op.GetEqualRules())
        {
            cloned.Add(Comparison.EQUALS, kv.Key, kv.Value);
        }
        foreach(String g in op.groupByList)
        {
            cloned.groupByList.Add(g);
        }
        return cloned;
    }

    public String ToString()
    {
        String result = null;
        foreach (var kv in this.GetEqualRules())
        {
            result += "<" + kv.Key+ ", " + kv.Value +"> ";
        }
        if(groupByList.Count()!= 0)
        {
            result += "<GROUP_BY: " + String.Join(", ", groupByList);
        }
        return result;
    }

    public Operand Fail()
    {
        this.failFlag = true;
        return this;
    }
}