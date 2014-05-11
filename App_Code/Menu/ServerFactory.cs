using System;
using System.Collections.Generic;
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
        String dbLocation = HostingEnvironment.MapPath(Path.Combine("~/App_Data/", Constants.DB_NAME));
        MenuDbManager db = new MenuDbManager(dbLocation);
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

    public void LoadServers()
    {
        String dbLocation = HostingEnvironment.MapPath(System.IO.Path.Combine("~/App_Data/", Constants.DB_NAME));
        MenuDbManager db = new MenuDbManager(dbLocation);
        db.Connect();

        System.Data.OleDb.OleDbDataReader dataReader = db.GetTable(MenuDbManager.Table.SERVER);
        // Load all Servers into Session Variable
        while (dataReader.Read())
        {
            Server server = new Server();
            server.id = Convert.ToInt16(dataReader["ID"]);
            server.firstname = dataReader["FIRST_NAME"].ToString();
            server.lastname = dataReader["LAST_NAME"].ToString();
            server.age = Convert.ToInt16(dataReader["AGE"]);
            server.sex = Convert.ToChar(dataReader["SEX"]);
            serverList.Add(server);
        }

        db.Close();
    }
}