namespace Server.DataBaseCore.SignatureFolderData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SignatureData
    {
        public static IEnumerable<TDataType> Where<TDataType>(this IEnumerable<TDataType> source, Func<TDataType, bool> predicate)
        {
            return Enumerable.Where(source, predicate);
        }

        public static IEnumerable<DataTypeObject> Select<TDataType, DataTypeObject>(this IEnumerable<TDataType> source, Func<TDataType, DataTypeObject> predicate)
        {
            return Enumerable.Select(source, predicate);
        }
    }
}
