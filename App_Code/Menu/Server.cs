using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

/// <summary>
/// Summary description for Server
/// </summary>
public class Server
{
    public int id { get; set; }
    public string firstname { get; set; }
    public string lastname {get; set;}
    public int age { get; set; }
    public char sex { get; set; }
	public Server()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Server(string firstname, string lastname, int age, char sex)
    {
        this.firstname = firstname;
        this.lastname = lastname;
        this.age = age;
        this.sex = sex;
    }

}