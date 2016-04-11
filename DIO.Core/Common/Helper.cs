﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DIO.Core.Common
{
    public static class Helper
    {

        
        public static string GetParameters(MethodBase method, string[] parameters)
        {
            StringBuilder parameter = new StringBuilder();
            ParameterInfo[] errorParameter = method.GetParameters();
            for (int i = 0; i < errorParameter.Count(); i++)
            {
                parameter.AppendFormat("{0}={1};", errorParameter[i].Name, parameters[i].ToString());
            }
            string studentData = parameter.ToString();
            return studentData;
        }
        
        /// <summary>
        /// Returns a set of items that are unique by the specified key
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        /// <summary>
        /// Checks wether the Date is within the range of dates specified in the parameter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startRange"></param>
        /// <param name="endRange"></param>
        /// <returns>true if the value is within startRange and endRange</returns>
        public static bool WithinDateRange(this DateTime value, DateTime startRange, DateTime endRange)
        {
            return (startRange <= value && value <= endRange);
        }
        /// <summary>
        /// Finds each value within the source that satisfies the condition specified by the predicate
        /// </summary>
        /// <typeparam name="TSource">The data type of the source</typeparam>
        /// <typeparam name="TValue">The data type of the values</typeparam>
        /// <param name="source">IEnumerable where the values will be searched</param>
        /// <param name="values">IEnumerable of values to be searched</param>
        /// <param name="predicate">specifies the condition for the comparison</param>
        /// <returns>Returns IEnumerable of items from source where predicate returns true</returns>
        public static IEnumerable<TSource> FindEach<TSource, TValue>(this IEnumerable<TSource> source, IEnumerable<TValue> values, Func<TSource, TValue, bool> predicate)
        {
            foreach (TValue value in values)
            {
                foreach (TSource item in source)
                {
                    if (predicate.Invoke(item, value))
                    {
                        yield return item;
                    }
                }
            }
        }
        /// <summary>
        /// Performs specified action on iteration of each item
        /// </summary>
        /// <typeparam name="TSource">Type of the Enumerable item</typeparam>
        /// <param name="items">Enumerable items where the action is to be performed</param>
        /// <param name="itemAction">A Function that will be invoked for every iteration and takes the individual item as its parameter</param>
        public static void Each<TSource>(this IEnumerable<TSource> items, Action<TSource> itemAction)
        {
            foreach (TSource item in items)
            {
                itemAction(item);
            }
        }
        /// <summary>
        /// Gets the Field name specified by FieldNameAttribute, if the attribute does not exist returns PropertyInfo.Name as default
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static string GetFieldNameOrDefault(this PropertyInfo propertyInfo)
        {
            FieldNameAttribute fieldNameAttribute = (FieldNameAttribute)propertyInfo.GetCustomAttributes(typeof(FieldNameAttribute), false).FirstOrDefault();
            string fieldName = propertyInfo.Name;
            if (fieldNameAttribute != null) fieldName = fieldNameAttribute.fieldName ?? fieldName;
            return fieldName;
        }
        /// <summary>
        /// Checks whether the property has IgnorePropertyAttribute, and return the ignoreProperty value of the attribute. Return false if the attribute is not set
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool IgnoreField(this PropertyInfo propertyInfo)
        {
            IgnorePropertyAttribute ignorePropertyAttribute = (IgnorePropertyAttribute)propertyInfo.GetCustomAttributes(typeof(IgnorePropertyAttribute), false).FirstOrDefault();
            return ignorePropertyAttribute == null ? false : (ignorePropertyAttribute.ignoreAccess == IgnoreAccess.ReadWrite ? ignorePropertyAttribute.ignoreProperty : false);
        }
        public static bool IgnoreOnRead(this PropertyInfo propertyInfo)
        {
            IgnorePropertyAttribute ignorePropertyAttribute = (IgnorePropertyAttribute)propertyInfo.GetCustomAttributes(typeof(IgnorePropertyAttribute), false).FirstOrDefault();
            return ignorePropertyAttribute == null ? false : (ignorePropertyAttribute.ignoreAccess == IgnoreAccess.ReadOnly ? ignorePropertyAttribute.ignoreProperty : false);
        }
        public static bool IgnoreOnWrite(this PropertyInfo propertyInfo)
        {
            IgnorePropertyAttribute ignorePropertyAttribute = (IgnorePropertyAttribute)propertyInfo.GetCustomAttributes(typeof(IgnorePropertyAttribute), false).FirstOrDefault();
            return ignorePropertyAttribute == null ? false : (ignorePropertyAttribute.ignoreAccess == IgnoreAccess.WriteOnly ? ignorePropertyAttribute.ignoreProperty : false);
        }
        public static string NullIfEmpty(this string value)
        {
            return value == string.Empty ? null : value;
        }
        public static string ToCsvString<TSource>(this IEnumerable<TSource> items)
        {
            StringBuilder stringBuilder = new StringBuilder();
            PropertyInfo[] props = typeof(TSource).GetProperties();
            foreach (TSource item in items)
            {
                foreach (PropertyInfo prop in props)
                {
                    stringBuilder.AppendFormat("{0}, ", prop.GetValue(item, null));
                }
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
        //based from http://stackoverflow.com/questions/13074202/passing-strongly-typed-property-name-as-argument
        public static IEnumerable<TSource> FilterBy<TSource, TProperty, TValue>(this IEnumerable<TSource> source, Expression<Func<TSource, TProperty>> property, TValue value)
        {
            string propName = getMemberInfo(property).Name;
            return source.Where(src => src.GetType().GetProperty(propName).GetValue(src, null).Equals(value));
        }
        private static MemberInfo getMemberInfo<TObject, TProperty>(Expression<Func<TObject, TProperty>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null)
            {
                return member.Member;
            }
            throw new ArgumentException("Member does not exist.");
        }
    }
    public enum IgnoreAccess
    {
        ReadWrite = 0,
        ReadOnly = 1,
        WriteOnly = -1
    }
    /// <summary>
    /// a property or field attribute that specifies if the field is to be ignored when retreiving data. best use for computed field
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnorePropertyAttribute : Attribute
    {
        public bool ignoreProperty { get; set; }
        public IgnoreAccess ignoreAccess { get; set; }
        public IgnorePropertyAttribute()
        {
            this.ignoreProperty = true;
            this.ignoreAccess = IgnoreAccess.ReadWrite;
        }
        public IgnorePropertyAttribute(bool ignoreProperty)
        {
            this.ignoreProperty = ignoreProperty;
            this.ignoreAccess = IgnoreAccess.ReadWrite;
        }
        public IgnorePropertyAttribute(bool ignoreProperty, IgnoreAccess ignoreAccess)
        {
            this.ignoreProperty = ignoreProperty;
            this.ignoreAccess = ignoreAccess;
        }
    }
    /// <summary>
    /// a property or field attribute that specifies the internal name of the field that the target is associated
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class FieldNameAttribute : Attribute
    {
        public string fieldName { get; set; }
        public Guid fieldID { get; set; }
        public int index { get; set; }
        public FieldNameAttribute() { }
        public FieldNameAttribute(string fieldName)
        {
            this.fieldName = fieldName;
        }
        public FieldNameAttribute(Guid fieldID)
        {
            this.fieldID = fieldID;
        }
        public FieldNameAttribute(int index)
        {
            this.index = index;
        }
    }
    /// <summary>
    /// a class attribute that specifies the name of the SharePoint List that the target is associated
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class SPListNameAttribute : Attribute
    {
        public string listName { get; set; }
        public bool useClassName { get; set; }
        public SPListNameAttribute() { }
        public SPListNameAttribute(string listName)
        {
            this.listName = listName;
        }
        public SPListNameAttribute(bool useClassName)
        {
            this.useClassName = useClassName;
        }
    }
    /// <summary>
    /// a class attribute that specifies the name of the Database table that the target is associated
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class SQLTableNameAttribute : Attribute
    {
        public string tableName { get; set; }
        public bool useClassName { get; set; }
        public SQLTableNameAttribute() { }
        public SQLTableNameAttribute(string tableName)
        {
            this.tableName = tableName;
        }
        public SQLTableNameAttribute(bool useClassName)
        {
            this.useClassName = useClassName;
        }
    }

    //credits to Matthew Yarlett
    //https://social.msdn.microsoft.com/Forums/office/en-US/92c1a750-0624-4887-b0f0-1c61234ab6b3/saving-file-to-another-server-using-c?forum=sharepointdevelopmentprevious#11691439-640e-49a7-a185-3a87328910d0
    public class Impersonator : IDisposable
    {
        public Impersonator(string userName, string domainName, string password)
        {
            ImpersonateValidUser(userName, domainName, password);
        }

        public void Dispose()
        {
            UndoImpersonation();
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int LogonUser(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(IntPtr handle);

        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        private WindowsImpersonationContext _impersonationContext = null;

        private void ImpersonateValidUser(string userName, string domain, string password)
        {
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            try
            {
                if (!RevertToSelf())
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                if (LogonUser(userName, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref token) == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                if (DuplicateToken(token, 2, ref tokenDuplicate) == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                var tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                _impersonationContext = tempWindowsIdentity.Impersonate();
            }
            finally
            {
                if (token != IntPtr.Zero)
                {
                    CloseHandle(token);
                }
                if (tokenDuplicate != IntPtr.Zero)
                {
                    CloseHandle(tokenDuplicate);
                }
            }
        }

        private void UndoImpersonation()
        {
            if (_impersonationContext != null)
            {
                _impersonationContext.Undo();
            }
        }
    }
}
