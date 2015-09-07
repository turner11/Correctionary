using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CommonObjects;


    public static class Extensions
    {
        /// <summary>
        /// Adds the range to the instance. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="collectionObj">The collection obj.</param>
        /// <returns></returns>
        public static ICollection<T> AddRange<T>(this ICollection<T> list,
                                      IEnumerable<T> collectionObj)
        {
            foreach (T item in collectionObj)
            {
                list.Add(item);
            }
            return list;
        }


    /// <summary>
    /// Coverts an Enums to string by it's description. falls back to ToString
    /// </summary>
    /// <param name="enumVal">The enum val.</param>
    /// <returns></returns>
    public static string ToStringByDescription(this Enum enumVal)
    {
        EnumToStringUsingDescription inter = new EnumToStringUsingDescription();
        string str = inter.EnumToString(enumVal);
        return str;
    }

    /// <summary>
    /// Coverts an Enums to string by it's description. falls back to ToString.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="flagSeperator">The string to separate flag values with.</param>
    /// <returns></returns>
    public static string ToStringByDescription(this Enum enumVal, string flagSeperator)
    {
        EnumToStringUsingDescription inter = new EnumToStringUsingDescription();
        string str = inter.EnumToString(enumVal, flagSeperator);
        return str;
    }

   





}

