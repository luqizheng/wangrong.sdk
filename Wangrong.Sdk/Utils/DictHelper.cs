﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ornament.PaymentGateway.Helper
{
    public static class DictHelper
    {
        public static void AddDict<T>(this T t, IDictionary<string, string> dict, Expression<Func<T, string>> key)
        {
            string keyName = null;
            var value = t.Get(key, out keyName);
            dict.Add(keyName, value ?? "");
        }

        public static void AddDict<T>(this T t, IDictionary<string, string> dict, Expression<Func<T, DateTime>> key,
            string format = "yyyyMMdd")
        {
            string keyName = null;
            var value = t.Get(key, out keyName);
            dict.Add(keyName, value.ToString(format));
        }

        public static void AddDict<T>(this T t, IDictionary<string, string> dict, Expression<Func<T, DateTime?>> key,
            string format = "yyyyMMdd")
        {
            string keyName = null;
            var value = t.Get(key, out keyName);
            dict.Add(keyName, value?.ToString(format) ?? "");
        }

        public static void AddDict<T>(this T t, IDictionary<string, string> dict, Expression<Func<T, int>> key)
        {
            string keyName = null;
            var value = t.Get(key, out keyName);
            dict.Add(keyName, value.ToString());
        }

        public static void AddDict<T>(this T t, IDictionary<string, string> dict, Expression<Func<T, int?>> key)
        {
            string keyName = null;
            var value = t.Get(key, out keyName);
            dict.Add(keyName, value?.ToString() ?? "");
        }

        public static void AddDict<T>(this T t, IDictionary<string, string> dict, Expression<Func<T, bool>> key)
        {
            string keyName = null;
            var value = t.Get(key, out keyName);
            dict.Add(keyName, value ? "1" : "0");
        }

        private static bool Get<T>(this T t, Expression<Func<T, bool>> key, out string keyName)
        {
            keyName = t.GetKeyName(key);
            return key.Compile().Invoke(t);
        }

        private static string Get<T>(this T t, Expression<Func<T, string>> key, out string keyName)
        {
            keyName = t.GetKeyName(key);
            return key.Compile().Invoke(t);
        }

        private static int Get<T>(this T t, Expression<Func<T, int>> key, out string keyName)
        {
            keyName = t.GetKeyName(key);
            return key.Compile().Invoke(t);
        }

        private static int? Get<T>(this T t, Expression<Func<T, int?>> key, out string keyName)
        {
            keyName = t.GetKeyName(key);
            return key.Compile().Invoke(t);
        }

        private static DateTime Get<T>(this T t, Expression<Func<T, DateTime>> key, out string keyName)
        {
            keyName = t.GetKeyName(key);
            return key.Compile().Invoke(t);
        }

        private static DateTime? Get<T>(this T t, Expression<Func<T, DateTime?>> key, out string keyName)
        {
            keyName = t.GetKeyName(key);
            return key.Compile().Invoke(t);
        }


        public static string GetKeyName<T, TProperty>(this T t, Expression<Func<T, TProperty>> key)
        {
            var keyName = "";
            if (key.Body is MemberExpression memberExpres)
                keyName = memberExpres.Member.Name;
            else if (key.Body is UnaryExpression memberExpress)
                keyName = ((MemberExpression) memberExpress.Operand).Member.Name;

            return Format(keyName);
        }

        public static string GetValueFrom<T>(this T t, IDictionary<string, string> dict,
            Expression<Func<T, string>> key)
        {
            var keyName = GetKeyName(t, key);

            if (dict.ContainsKey(keyName))
                return dict[keyName];
            return null;
        }

        public static DateTime GetValueFrom<T>(this T t, IDictionary<string, string> dict,
            Expression<Func<T, DateTime>> key, string format = "yyyyMMdd")
        {
            var keyName = GetKeyName(t, key);


            if (dict.ContainsKey(keyName))
                return DateTime.ParseExact(dict[keyName], format, null);
            return DateTime.MinValue;
        }


        public static int GetValueFrom<T>(this T t, IDictionary<string, string> dict,
            Expression<Func<T, int>> key)
        {
            var keyName = GetKeyName(t, key);


            if (dict.ContainsKey(keyName))
                return Convert.ToInt32(dict[keyName]);
            return 0;
        }

        public static bool GetValueFrom<T>(this T t, IDictionary<string, string> dict,
            Expression<Func<T, bool>> key)
        {
            var keyName = GetKeyName(t, key);


            if (dict.ContainsKey(keyName))
                return dict[keyName] == "1";
            return false;
        }

        private static string Format(string input)
        {
            var ary = input.ToCharArray();
            ary[0] = char.ToLower(ary[0]);
            return new string(ary);
        }
    }
}