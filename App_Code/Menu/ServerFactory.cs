using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

/// <summary>
/// Summary description for ServerFactory
/// </summary>
public class ServerFactory
{
    public List<Server> serverList = new List<Server>();
    private DataTable dataTable = null;
	public ServerFactory()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void Add(Server server)
    {
        this.serverList.Add(server);
    }

    public void CreateServer(string firstname, string lastname, int age, char sex)
    {
        MenuDbManager db = new MenuDbManager(DBManager.defaultDbLocation);
        db.Connect();
        db.InsertServer(firstname, lastname, age, sex);
        db.Close();
        serverList.Add(new Server(firstname, lastname, age, sex));
    }

    public Server GetServer(int id)
    {
        var server = serverList.First(i => i.id == id);
        return server;
    }

    public void CreateServer(Server server)
    {
        CreateServer(server.firstname, server.lastname, server.age, server.sex);
    }

    public DataTable GetServerDataTable()
    {
        if(dataTable == null)
        {
            throw new Exception("Server DataTable not initialized");
        }
        return dataTable;
    }

    public void LoadServers()
    {
        MenuDbManager db = new MenuDbManager(DBManager.defaultDbLocation);
        db.Connect();
        dataTable = db.GetTable(MenuDbManager.Table.SERVER);
        // Load all Servers into Session Variable
        foreach(DataRow r in dataTable.Rows)
        {
            Server server = new Server();
            server.id = Convert.ToInt16(r["ID"]);
            server.firstname = r["FIRST_NAME"].ToString();
            server.lastname = r["LAST_NAME"].ToString();
            server.age = Convert.ToInt16(r["AGE"]);
            server.sex = Convert.ToChar(r["SEX"]);
            serverList.Add(server);
        }

        db.Close();
    }
}