using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DecisionTreeFactory
/// </summary>
public class DecisionTreeFactory
{
    DBManager db = null;
    public List<String> filteredSelectedColumns { get; set; }
    public String targetColumn { get; set; }
    public String tableName { get; set; }

	public DecisionTreeFactory(DBManager db, List<String> selectedColumns, String targetColumn, String tableName)
	{
        this.db = db;
        this.db.Connect();
        this.filteredSelectedColumns = selectedColumns.Where(li => !li.Equals(targetColumn)).ToList();
        this.targetColumn = targetColumn;
        this.tableName = tableName;
	}

    public DecisionTree CreateTree()
    {
        Queue<String> columnQueue = this.GetColumnQueue();
        return this.CreateTree(columnQueue, columnQueue.Peek(), new Operand());
    }

    private DecisionTree CreateTree(Queue<String> prioritizedColumns, String currItem, Operand operation)
    {
        //nodeValue = ColumnName
        String nodeValue;
        String debugOperation = " | " + operation.ToString();

        nodeValue = currItem.Equals(prioritizedColumns.Peek()) ? prioritizedColumns.Dequeue() : currItem;
 
        //DecisionTree node = new DecisionTree(nodeValue + debugOperation);

        if(prioritizedColumns.Count!=0)
        {
            DecisionTree node = new DecisionTree(nodeValue);
            List<Object> possibleValues = db.GetDistinctValues(this.tableName, nodeValue, operation);
            String nextValue = prioritizedColumns.Peek();
            possibleValues.ForEach(delegate(Object obj){
                Queue<String> queueClone = new Queue<String>();
                prioritizedColumns.ToList<String>().ForEach(i => queueClone.Enqueue(i));
                node.AddEdge(obj, CreateTree(queueClone, nextValue, Operand.Clone(operation).Add(Operand.Comparison.EQUALS, nodeValue, obj)));
            });
            return node;            
        }
        else //Leaf Node so we add one more node for its value
        {
            DecisionTreeLeaf node = new DecisionTreeLeaf(nodeValue);
            List<Object> possibleValues = db.GetColumnValues(this.tableName, nodeValue, false, operation);
            node.AddLeafValue(possibleValues);            
            node.GetValuesCount().ToList().ForEach(delegate(KeyValuePair<string, int> kv)
            {
                DecisionTreeLeaf leaf = new DecisionTreeLeaf(kv.Key + " : " + kv.Value + debugOperation);
                node.AddEdge(kv.Key, leaf);
            });             
            return node;
        }
    }

    public Queue<String> GetColumnQueue()
    {
        List<String> allColumns = new List<String>();
        Queue<String> queue = new Queue<String>();
        filteredSelectedColumns.ForEach(l => queue.Enqueue(l));
        queue.Enqueue(targetColumn);

        return queue; 
    }






}