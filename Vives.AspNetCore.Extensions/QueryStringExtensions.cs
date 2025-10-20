using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Quic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualBasic;

namespace Vives.AspNetCore.Extensions
{
    public static class QueryStringExtensions
    {
        public static string AddQueryString(this string queryString, object? queryObject)
        {
            if(queryObject == null)
                return queryString;

            var parameters = queryObject.ToTupleEnumerable();

            foreach(var parameter in parameters)
            {
                queryString = QueryHelpers.AddQueryString(queryString, parameter.Item1, parameter.Item2);
            }

            return queryString;
        }

        public static string AddDictionaryQueryString(this string queryString, IDictionary? queryObject, string propertyName)
        {
            if (queryObject == null)
                return queryString;

            var parameters = queryObject.ToTupleEnumerable(propertyName);

            foreach(var parameter in parameters)
            {
                queryString = QueryHelpers.AddQueryString(queryString, parameter.Item1, parameter.Item2);
            }

            return queryString;
        }



        public static IEnumerable<Tuple<string, string>> ToTupleEnumerable(this object queryObject, string? propertyName = null)
        {
            var objectType = queryObject.GetType();
            var properties = objectType
                .GetProperties()
                .Where(p => p.GetValue(queryObject, null) != null).ToList();

            foreach (var property in properties)
            {
                var newPropertyName = String.IsNullOrWhiteSpace(propertyName) ? propertyName : $"{propertyName}.{property.Name}";
                var newPropertyValue = property.GetValue(queryObject, null);

                if (newPropertyValue is null)
                    continue;

                var newPropertyValueType = newPropertyValue.GetType();

                //if its normal use value as text
                if (newPropertyValueType.IsCustomValueType())
                {
                    yield return new Tuple<string, string>(newPropertyName, newPropertyValue.ToFormattedString());
                }
                else if (newPropertyValueType.IsList())
                {
                    var list = (IEnumerable)newPropertyValue;
                    foreach (var item in list)
                    {
                        yield return new Tuple<string, string>(newPropertyName, item.ToFormattedString());
                    }
                }
                else if (newPropertyValueType.IsDictionary())
                {
                    var dictionary = (IDictionary)newPropertyValue;
                    var tupleEnumerable = dictionary.ToTupleEnumerable(newPropertyName);
                    foreach(var tuple in tupleEnumerable)
                    {
                        yield return tuple;
                    }
                }
                else //if something else, traverse all its properties
                {
                    var newPropertyTuples = newPropertyValue.ToTupleEnumerable(newPropertyName);
                    foreach(var newPropertyTuple in newPropertyTuples)
                    {
                        yield return newPropertyTuple;
                    }
                }

            }
        }

        public static IEnumerable<Tuple<string, string>> ToTupleEnumerable(this IDictionary dictionary, string propertyName)
        {
            foreach(var key in dictionary.Keys)
            {
                var dictionaryPropertyName = $"{propertyName}[{key}]";

                if (!dictionary.Contains(key))
                    continue;

                var dictionaryValue = dictionary[key];
                if (dictionaryValue is null)
                    continue;

                yield return new Tuple<string, string>(dictionaryPropertyName, dictionaryValue.ToFormattedString());
            }
        }




        public static bool IsCustomValueType(this Type type)
        {
            return type.IsPrimitive || type.IsValueType || (type == typeof(string));
        }

        public static string ToFormattedString(this object objectToFormat)
        {
            if (objectToFormat is DateTime dateTimeObject)
                return dateTimeObject.ToString("yyyy-MM-dd HH:mm:ss");

            var formattedString = objectToFormat.ToString();

            if (formattedString is null)
                return string.Empty;

            return formattedString;
        }

        public static bool IsList(this Type type)
        {
            return type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        public static bool IsDictionary(this Type type)
        {
            return type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
        }
    }
}
