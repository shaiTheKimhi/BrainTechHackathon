using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

public class ScriptDon : MonoBehaviour {
    const string serverPath = "";
    delegate int func(Variable[] vars);
    Dictionary<string, func> functions = new Dictionary<string, func>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static string HttpGet(string url)
    {
        string content = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }
    public void Run()
    {
        string raw = HttpGet(serverPath);//server path;
        List<List<string>> script = parseScript(raw);
        RunScript(script);
    }
    public List<List<string>> parseScript(string raw)
    {
        //TODO: complete code here
        return new List<List<string>>();
    }
    public void RunScript(List<List<string>> l)
    {
        foreach(List<string> temp in l)
        {
            Variable[] v = Variable.Parse(temp[1]);
            functions[temp[0]](v);
        }
    }
    
}
public class Variable
{
    public string Type { get; private set; }
    public int num { get; private set; }
    public string str { get;private set; }
    public bool b { get;private set; }
    public Variable(object arg)
    {
        try
        {
            num = int.Parse(arg.ToString());
            Type = "int";
        }
        catch { }
        if(arg.GetType().Equals(typeof(string)))
        {
            str = arg.ToString();
            Type = "string";
        }
    }
    public static Variable[] Parse(string str)
    {
        string[] arr = str.Split(",".ToCharArray());
        Variable[] v = new Variable[arr.Length];
        for(int i=0;i<arr.Length;i++)
        {
            v[i] = new Variable(arr[i]);
        }
        return v;
    }
}