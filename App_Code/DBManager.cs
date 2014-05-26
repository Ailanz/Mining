using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Hosting;

/// <summary>
/// Summary description for DBManager
/// </summary>
public class DBManager
{
    private String dbLocation = "";
    private OleDbConnection con = null;
    public static String defaultDbLocation = "";

    static DBManager()
    {
        DBManager.defaultDbLocation = HostingEnvironment.MapPath(System.IO.Path.Combine("~/App_Data/", Constants.DB_NAME));
    }

    public DBManager(string dbLocation)
	{
        this.dbLocation = dbLocation;
	}

    public void Connect()
    {
        if (this.con == null)
        {
            string connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + this.dbLocation;
            this.con = new OleDbConnection(connection);
            con.Open();
        }
    }

    public void Close()
    {
        this.con.Close();
    }

    public List<String> GetTableNames()
    {
        // We only want user tables, not system tables
        //http://msdn.microsoft.com/en-us/library/ms254934%28v=vs.80%29.aspx explanation on the restrictions
        string[] restrictions = new string[4];
        restrictions[3] = "Table";

        // Get list of user tables
        DataTable userTables = this.con.GetSchema("Tables", restrictions);        

        List<string> tableNames = new List<string>();
        for (int i = 0; i < userTables.Rows.Count; i++)
        {
            tableNames.Add(userTables.Rows[i][2].ToString());
        }
        return tableNames;
    }

    public OleDbDataReader Execute(string sqlQuery)
    {
        OleDbCommand cmd = new OleDbCommand();
        cmd.CommandText = sqlQuery;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        return cmd.ExecuteReader();
    }

    public int Insert(string sqlQuery)
    {
        OleDbCommand cmd = new OleDbCommand();
        cmd.CommandText = sqlQuery;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        return cmd.ExecuteNonQuery();
    }

    public List<String> GetColumnNames(String tableName)
    {
        var results = new List<String>();
        string query = "select top 1 * from [" + tableName + "]";
        using (var cmd = new OleDbCommand(query, this.con))
        using (var reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly))
        {
            var table = reader.GetSchemaTable();
            var nameCol = table.Columns["ColumnName"];
            foreach (DataRow row in table.Rows)
            {
                results.Add(row[nameCol].ToString());
            }
        }
        return results;        
    }


    public List<Object> GetColumnValues(string table, string column)
    {
        return GetColumnValues(table, column, true, null);
    }

    public List<Object> GetColumnValues(string table, string column, bool distinct, Operand op)
    {
        String query = distinct ? "select distinct " + column + " from [" + table + "]" : "select " + column + " from [" + table + "]";
        query += GetWhereClause(op);
        OleDbDataReader reader = this.Execute(query);
        List<Object> result = new List<Object>();
        while (reader.Read())
        {
            result.Add(reader.GetValue(0).ToString());
        }
        reader.Close();
        return result;
    }

    public List<Object> GetDistinctValues(string table, string column, Operand op)
    {
        String query = "select distinct " + column + " from [" + table + "]";
        var equalsRules = op.GetEqualRules();

        query += GetWhereClause(op);

        OleDbDataReader reader = Execute(query);

        List<Object> result = new List<Object>();
        while (reader.Read())
        {
            result.Add(reader.GetValue(0).ToString());
        }
        reader.Close();
        return result;
    }

    private String GetWhereClause(Operand op)
    {
        if (op == null) { return ""; };

        var equalsRules = op.GetEqualRules();

        String query = "";
        if (equalsRules.Count != 0)
        {
            query = " where ";
            foreach (var kv in equalsRules)
            {
                long numberTest;
                DateTime dateTest;
                if (Int64.TryParse(kv.Value.ToString(), out numberTest))
                {
                    query += "[" + kv.Key + "]=" + kv.Value + " AND ";
                }
                else if(DateTime.TryParse(kv.Value.ToString(), out dateTest))
                {
                    query += "["+kv.Key + "]=#" + kv.Value + "# AND ";
                }
                else
                {
                    query += "[" + kv.Key + "]='" + kv.Value + "' AND ";
                }
            }
            //Get rid of the last AND
            query = query.Substring(0, query.Length - 4);
        }
        return query; 
    }
}