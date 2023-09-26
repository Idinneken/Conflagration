using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;

namespace Extensions
{
    public static class SystemExtensions
    {
        public static bool HasMethod_<T>(this T thingToCheck, string methodName, BindingFlags bindingFlags) where T : Type
        {
            try
            {
                var type = thingToCheck.GetType();
                return type.GetMethod(methodName, bindingFlags) != null;
            }
            catch (AmbiguousMatchException)
            {
                // ambiguous means there is more than one result,
                // which means: a method with that name does exist
                return true;
            }
        }

        public static bool HasMethod_(this Type thingToCheck, string methodName, BindingFlags bindingFlags)
        {
            try
            {
                return thingToCheck.GetMethod(methodName, bindingFlags) != null;
            }
            catch (AmbiguousMatchException)
            {
                // ambiguous means there is more than one result,
                // which means: a method with that name does exist
                return true;
            }
        }

        public static bool HasMember_(this Type thingToCheck, string memberName, BindingFlags bindingFlags)
        {
            try
            {
                return thingToCheck.GetMember(memberName, bindingFlags) != null;
            }
            catch (AmbiguousMatchException)
            {
                // ambiguous means there is more than one result,
                // which means: a method with that name does exist
                return true;
            }
        }

        public static bool HasField_(this Type thingToCheck, string variableName, BindingFlags bindingFlags)
        {
            try
            {
                return thingToCheck.GetField(variableName, bindingFlags) != null;
            }
            catch (AmbiguousMatchException)
            {
                // ambiguous means there is more than one result,
                // which means: a method with that name does exist
                return true;
            }
        }

        public static List<Type> GetTypes_(this List<object> objects_)
        {
            List<Type> types = new();

            foreach (object ob in objects_)
            {
                types.Add(ob.GetType());
            }

            return types;
        }

        public static List<int> GetLongestSequence(this List<int> list_)
        {
            var groupNumber = 0;
            list_.Sort();

            if (list_.Count > 0)
            {
                return list_.Select((value, index) =>
                    new
                    {
                        Item = value,
                        Index = index
                    })
                    .GroupBy(x => x.Index == 0 || x.Item - list_[x.Index - 1] == 1
                        ? groupNumber :
                        ++groupNumber)
                    .OrderByDescending(x => x.Count())
                    .First()
                    .Select(x => x.Item).ToList();
            }

            return new List<int>() { 1 };

        }

        public static Dictionary<int, int> GetDuplicateQuantities(this List<int> list_)
        {
            return list_.GroupBy(value => value).Where(value => value.Count() > 0).ToDictionary(value => value.Key, duplicates => duplicates.Count());
        }

        public static void AddIfDoesntExist<T>(this IEnumerable<T> collection, T value)
        {
            if (!collection.Contains(value))
            {
                collection.Append(value);
                Debug.Log("thumbs up");
            }
        }

        public static bool ContainsList<T>(this List<T> checkedAgainst, List<T> checkedThrough)
        {
            foreach (T element in checkedThrough)
            {
                if (!checkedAgainst.Contains(element))
                {
                    return false;
                }
            }

            return true;
        }

    }

    public static class UnityExtensions
    {
        public static bool IsOnLayer_(this GameObject gameObject_, LayerMask layer_)
        {
            if ((layer_.value & (1 << gameObject_.layer)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ChangePos_(this CharacterController controller_, Vector3 position_)
        {
            controller_.enabled = false;
            controller_.transform.position = position_;
            controller_.enabled = true;
        }

        public static void ChangePos_(this MonoBehaviour monoBehaviour_, CharacterController controller_, Vector3 position_)
        {
            controller_.enabled = false;
            controller_.transform.position = position_;
            controller_.enabled = true;
        }

        public static Vector3 Absolute_(this Vector3 vector_)
        {
            return new Vector3(Mathf.Abs(vector_.x), Mathf.Abs(vector_.y), Mathf.Abs(vector_.z));
        }

        public static float Absolute_(this float float_)
        {
            return Mathf.Abs(float_);
        }

        public static float AngleBetweenPoints_(this Vector3 vertexPoint, Vector3 point1, Vector3 point2)
        {
            return Vector3.Angle(point1 - vertexPoint, point2 - vertexPoint);
        }

        public static IEnumerator WaitForSeconds(this MonoBehaviour _, float time_)
        {
            yield return new WaitForSeconds(time_);
        }

        public static void InstantiatePrefabsAndGetComponents<T>(this MonoBehaviour _, List<GameObject> prefabs_, List<GameObject> objects_, List<T> components_)
        {
            foreach (GameObject prefab in prefabs_)
            {
                GameObject gameObject = GameObject.Instantiate(prefab);
                objects_.Add(gameObject);
            }

            foreach (GameObject @object in objects_)
            {
                components_.Add(@object.GetComponent<T>());
            }

        }

        public static void InstantiatePrefabsAndGetComponents<T>(this MonoBehaviour _, List<GameObject> prefabs_, List<GameObject> objects_, List<T> components_, GameObject parent_)
        {
            foreach (GameObject prefab in prefabs_)
            {
                GameObject gameObject = GameObject.Instantiate(prefab, parent_.transform);
                objects_.Add(gameObject);
            }

            foreach (GameObject @object in objects_)
            {
                components_.Add(@object.GetComponent<T>());
            }

        }

        public static void InstantiatePrefabAndGetComponent<T>(this MonoBehaviour _, GameObject prefab_, List<GameObject> objectListBeingAddedTo_, List<T> componentListBeingAddedTo_)
        {
            GameObject gameObject = GameObject.Instantiate(prefab_);
            objectListBeingAddedTo_.Add(gameObject);
            componentListBeingAddedTo_.Add(gameObject.GetComponent<T>());
        }

        public static void InstantiatePrefabAndGetComponent<T>(this MonoBehaviour _, GameObject prefab_, List<GameObject> objectListBeingAddedTo_, List<T> componentListBeingAddedTo_, GameObject parent_)
        {
            GameObject gameObject = GameObject.Instantiate(prefab_, parent_.transform);
            objectListBeingAddedTo_.Add(gameObject);
            componentListBeingAddedTo_.Add(gameObject.GetComponent<T>());
        }

        public static void InstantiatePrefabAndGetComponent<T>(this MonoBehaviour mono_, GameObject prefab_, out GameObject object_, out T component_)
        {
            object_ = GameObject.Instantiate(prefab_);
            component_ = object_.GetComponent<T>();
        }

        public static void InstantiatePrefabAndGetComponent<T>(this MonoBehaviour _, GameObject prefab_, out GameObject object_, out T component_, GameObject parent_)
        {
            object_ = GameObject.Instantiate(prefab_, parent_.transform);
            component_ = object_.GetComponent<T>();
        }

        public static void GetComponentsFromList_<T>(this MonoBehaviour _, List<GameObject> objects_, out List<T> components_)
        {
            components_ = new List<T>();

            foreach (GameObject @object in objects_)
            {
                components_.Add(@object.GetComponent<T>());
            }

        }

        public static void SetActiveRecursively_(this GameObject gameObject_, bool state_)
        {
            gameObject_.SetActive(state_);

            foreach (Transform transfrom in gameObject_.GetComponentsInChildren<Transform>())
            {
                transfrom.gameObject.SetActive(state_);
            }
        }


        //https://stackoverflow.com/a/60189474
        public static bool IsNull<T>(this T myObject) where T : class
        {
            if (myObject is UnityEngine.Object obj)
            {
                if (!obj)
                {
                    return true;
                }
            }
            else
            {
                if (myObject == null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
