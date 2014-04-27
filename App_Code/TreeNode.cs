using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TreeNode
/// </summary>
public class TreeNode
{
    public String value {get; set;}
	
    public TreeNode()
    {
        value = String.Empty;
    }
    
    public TreeNode(String value)
	{
        this.value = value;
	}

}