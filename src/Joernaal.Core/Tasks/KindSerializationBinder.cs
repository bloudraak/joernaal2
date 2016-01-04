using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace Joernaal.Tasks
{
    [AttributeUsage(AttributeTargets.Class)]
    sealed class KindAttribute : Attribute
    {
        public KindAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

    }

    public class KindSerializationBinder : DefaultSerializationBinder
    {
        private Dictionary<string, Type> _nameToType;
        private Dictionary<Type, string> _typeToName;

        public KindSerializationBinder()
        {
            var customDisplayNameTypes =
                this.GetType()
                    .Assembly
                    .GetTypes()
                    .Where(x => x
                        .GetCustomAttributes(false)
                        .Any(y => y is KindAttribute));

            _nameToType = customDisplayNameTypes.ToDictionary(
                t => t.GetCustomAttributes(false).OfType<KindAttribute>().First().Value,
                t => t);

            _typeToName = _nameToType.ToDictionary(
                t => t.Value,
                t => t.Key);

        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            if (false == _typeToName.ContainsKey(serializedType))
            {
                base.BindToName(serializedType, out assemblyName, out typeName);
                return;
            }

            var name = _typeToName[serializedType];

            assemblyName = null;
            typeName = name;
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            if (_nameToType.ContainsKey(typeName))
                return _nameToType[typeName];

            return base.BindToType(assemblyName, typeName);
        }
    }
}