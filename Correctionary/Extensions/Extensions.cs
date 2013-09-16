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
        /// Gets the descrition attribute of the enum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetDescrition(this Enum value)
        {
            EnumToStringUsingDescription interpeter = new EnumToStringUsingDescription();
            return interpeter.GetEnumDescrition(value);
        }

       
    }

