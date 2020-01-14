using System.Collections;
using System.Collections.Generic;
using IronPython.Hosting;
using UnityEngine;

public class PythonExample : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        var engine = Python.CreateEngine();

        ICollection<string> searchPaths = engine.GetSearchPaths();

        //Path to the folder of greeter.py
        searchPaths.Add(Application.dataPath);
        //Path to the Python standard library
        searchPaths.Add(Application.dataPath + @"\Plugins\Lib\");
        engine.SetSearchPaths(searchPaths);

        dynamic py = engine.ExecuteFile(Application.dataPath + @"\Scripts\greeter.py");
        dynamic greeter = py.Greeter("Mika");
        Debug.Log(greeter.greet());
        Debug.Log(greeter.random_number(1,5));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
