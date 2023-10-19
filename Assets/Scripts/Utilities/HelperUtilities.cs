using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities {
    /// <summary>
    /// Checks to see if string is empty.
    /// </summary>
    public static bool ValidateCheckEmptyString(Object thisObject, string fieldName, string stringToCheck) {
        if (stringToCheck != "") return false;
        
        Debug.Log(fieldName + " is empty and must contain a value in object " + thisObject.name);
        return true;
    }

    public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerableObjectToCheck) {
        var error = false;
        var count = 0;

        foreach (var item in enumerableObjectToCheck) {
            if (item == null) {
                Debug.Log(fieldName + " has null values in object " + thisObject.name);
                error = true;
            } else {
                count++;
            }
        }

        if (count == 0) {
            Debug.Log(fieldName + " has no values in object " + thisObject.name);
            error = true;
        }

        return error;
    }
}
