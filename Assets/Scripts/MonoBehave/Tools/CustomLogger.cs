using UnityEngine;

public class CustomLogger {

    static bool log = true;
    static bool debugging = true;
    static bool tracing = false;
    private string caller;

    public CustomLogger(string caller) {
        this.caller = caller;
    }

    //[System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void info (object message) {
        if (log) Debug.Log ("<color=purple>INFO</color>" + " :: <color=blue>" + caller + "</color> :: " + message.ToString());
    }

    //[System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void debug (object message) {
        if (log & debugging) Debug.Log ("<color=green>DEBUG</color>" + " :: <color=blue>" + caller + "</color> :: " + message.ToString());
    }

    //[System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void trace (object message) {
        if (log & tracing) Debug.Log ("<color=orange>TRACE</color>" + " :: <color=blue>" + caller + "</color> :: " + message.ToString());
    }

    //[System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void error (object message) {
        if (log) Debug.LogError ("<color=red><b>ERROR</b></color>" + " :: <color=blue>" + caller + "</color> :: " + message.ToString());
    }

    //[System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void warn (object message) {
        if (log) Debug.LogWarning ("<color=#FFD700>WARNING</color>" + " :: <color=blue>" + caller + "</color> :: " + message.ToString());
    }
}
